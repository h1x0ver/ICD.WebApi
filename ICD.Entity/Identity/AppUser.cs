using ICD.Entity.Base;
using ICD.Entity.Entities;
using Microsoft.AspNetCore.Identity;

namespace ICD.Entity.Identity;

public class AppUser : IdentityUser, IEntity
{
    public string? Firstname { get; set; }
    public string? Lastname { get; set; }
    public int? ProfileImageId { get; set; }
    public Image? ProfileImage { get; set; }
}
