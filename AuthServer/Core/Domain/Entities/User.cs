using Microsoft.AspNetCore.Identity;

namespace AuthServer.Core.Domain.Entities;

public class User : IdentityUser
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string MiddleName { get; set; }
    public DateTime? DeactivationDate { get; set; }
    public DateTime? LastLoginDate { get; set; }
}