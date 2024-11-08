using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Auth.Models;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    public int Id { get; set; }
    [Required]
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
    public bool IsActive { get; set; }

    public User(string email, string passwordHash)
    {
        Email = email;
        PasswordHash = passwordHash;
    }
}