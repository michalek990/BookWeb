using Application.Common.Authentication;
using Application.Common.Exceptions;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Interfaces.Repositories;
using MediatR;

namespace Application.Users.Commands.RegisterUser;

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, UserDetailsDto>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUserPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;
    
    public RegisterUserCommandHandler(
        IUnitOfWork unitOfWork, 
        IMapper mapper, 
        IUserPasswordHasher passwordHasher, 
        ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _userRepository = unitOfWork.Users;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }
    
    public async Task<UserDetailsDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.ExistByUsername(request.Username))
        {
            throw new DuplicateCredentialsException("Username is already taken");
        }

        if (await _userRepository.ExistByEmail(request.Email))
        {
           throw new DuplicateCredentialsException("Email is already taken");
        }

        var newUser = new User
        {
            Username = request.Username,
            Email = request.Email,
            AccountStatus = AccountStatus.Active,
            Role = Role.User
        };

        var hashedPassword = _passwordHasher.HashPassword(request.Password, newUser);
        newUser.Password = hashedPassword;
        
        _userRepository.Add(newUser);
        await _unitOfWork.SaveAsync();

        return _mapper.Map<UserDetailsDto>(newUser);
    }
}