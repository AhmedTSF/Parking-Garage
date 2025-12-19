using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class SpotConfiguration : IEntityTypeConfiguration<Spot>
{
    public void Configure(EntityTypeBuilder<Spot> builder)
    {
        builder.ToTable("Spots");

        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .UseIdentityColumn(1, 1);

        builder.Property(s => s.SpotNumber)
               .IsRequired()
               .HasMaxLength(10);

        builder.HasIndex(s => s.SpotNumber).IsUnique();

        builder.Property(s => s.IsOccupied)
               .IsRequired();
    }
}
