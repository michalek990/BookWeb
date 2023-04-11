using Domain.Common;
using Domain.Enums;

namespace Domain.Entities;

public class User : PernamentEntity
{ 
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public AccountStatus AccountStatus { get; set; }
    
    public virtual List<Review> Reviews { get; set; }
}