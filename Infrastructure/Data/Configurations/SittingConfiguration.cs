using Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class SittingConfiguration : IEntityTypeConfiguration<Sitting>
{ 
    public void Configure(EntityTypeBuilder<Sitting> builder)
    {
        builder.ToTable("Sittings");

        builder.HasKey(s => s.Key);

        builder.Property(s => s.Key)
            .IsRequired()
            .HasMaxLength(100)
            .ValueGeneratedNever();

        builder.Property(s => s.Value)
            .IsRequired()
            .HasMaxLength(500);
    }
}
