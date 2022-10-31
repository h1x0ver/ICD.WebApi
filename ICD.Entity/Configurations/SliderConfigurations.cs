using ICD.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ICD.Entity.Configurations;

public class SliderConfigurations : IEntityTypeConfiguration<Slider>
{
    public void Configure(EntityTypeBuilder<Slider> builder)
    {
        builder.Property(s => s.CreatedDate).HasDefaultValueSql("GETDATE()");
        builder.Property(s => s.isDeleted).HasDefaultValue(false);
    }
}