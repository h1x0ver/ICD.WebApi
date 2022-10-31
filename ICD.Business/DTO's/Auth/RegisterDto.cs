using System.ComponentModel.DataAnnotations;

namespace ICD.Business.DTO_s.Auth;

public class RegisterDto
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    [Compare(nameof(Password))]
    public string? ConfirmPassword { get; set; }
}
