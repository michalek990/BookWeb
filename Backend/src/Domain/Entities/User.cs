using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class User : PernamentEntity
{
    public string Firstname { get; set; }
    public string Surname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}