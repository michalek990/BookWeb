﻿using System.Linq.Expressions;
using Application.Models.Interfaces;
using AutoMapper;
using Domain.Enums;
using Domain.Entities;

namespace Application.Models;

public sealed record UserDetailsDto
{
    public string? Username { get; init; }
    public string? Email { get; init; }
    public Role Role { get; init; }
    public AccountStatus Status { get; init; }
    public DateTime CreatedAt { get; init; }
}

public sealed class UserDetailsDtoProfile : Profile
{
    public UserDetailsDtoProfile()
    {
        CreateMap<User, UserDetailsDto>();
    }
}

public sealed class UserDetailColumnSelector : IColumnSelector<User>
{
    public Dictionary<string, Expression<Func<User, object>>> ColumnSelector { get; } = new()
    {
        { nameof(User.Id), r => r.Id },
        { nameof(User.Username), r => r.Username! },
    };
}