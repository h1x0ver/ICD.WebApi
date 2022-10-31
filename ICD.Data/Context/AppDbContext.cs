using ICD.Entity.Configurations;
using ICD.Entity.Entities;
using ICD.Entity.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ICD.Data.Context;

public class AppDbContext : IdentityDbContext<AppUser>
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Image> Images { get; set; }
    public DbSet<Slider> Sliders { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new SliderConfigurations());
        base.OnModelCreating(builder);
    }
}
