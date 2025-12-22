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

        // Seed data
        builder.HasData(
            new Spot { Id = 1, SpotNumber = "A-01", IsOccupied = true },
            new Spot { Id = 2, SpotNumber = "A-02", IsOccupied = false },
            new Spot { Id = 3, SpotNumber = "A-03", IsOccupied = false },
            new Spot { Id = 4, SpotNumber = "A-04", IsOccupied = false },
            new Spot { Id = 5, SpotNumber = "A-05", IsOccupied = false },
            new Spot { Id = 6, SpotNumber = "A-06", IsOccupied = false },
            new Spot { Id = 7, SpotNumber = "A-07", IsOccupied = false },
            new Spot { Id = 8, SpotNumber = "A-08", IsOccupied = false },
            new Spot { Id = 9, SpotNumber = "A-09", IsOccupied = false },
            new Spot { Id = 10, SpotNumber = "A-10", IsOccupied = false },
            new Spot { Id = 11, SpotNumber = "B-01", IsOccupied = false },
            new Spot { Id = 12, SpotNumber = "B-02", IsOccupied = false },
            new Spot { Id = 13, SpotNumber = "B-03", IsOccupied = false },
            new Spot { Id = 14, SpotNumber = "B-04", IsOccupied = false },
            new Spot { Id = 15, SpotNumber = "B-05", IsOccupied = false },
            new Spot { Id = 16, SpotNumber = "B-06", IsOccupied = false },
            new Spot { Id = 17, SpotNumber = "B-07", IsOccupied = false },
            new Spot { Id = 18, SpotNumber = "B-08", IsOccupied = false },
            new Spot { Id = 19, SpotNumber = "B-09", IsOccupied = false },
            new Spot { Id = 20, SpotNumber = "B-10", IsOccupied = false },
            new Spot { Id = 21, SpotNumber = "C-01", IsOccupied = false },
            new Spot { Id = 22, SpotNumber = "C-02", IsOccupied = false },
            new Spot { Id = 23, SpotNumber = "C-03", IsOccupied = false },
            new Spot { Id = 24, SpotNumber = "C-04", IsOccupied = false },
            new Spot { Id = 25, SpotNumber = "C-05", IsOccupied = false },
            new Spot { Id = 26, SpotNumber = "C-06", IsOccupied = false },
            new Spot { Id = 27, SpotNumber = "C-07", IsOccupied = false },
            new Spot { Id = 28, SpotNumber = "C-08", IsOccupied = false },
            new Spot { Id = 29, SpotNumber = "C-09", IsOccupied = false },
            new Spot { Id = 30, SpotNumber = "C-10", IsOccupied = false },
            new Spot { Id = 31, SpotNumber = "D-01", IsOccupied = false },
            new Spot { Id = 32, SpotNumber = "D-02", IsOccupied = false },
            new Spot { Id = 33, SpotNumber = "D-03", IsOccupied = false },
            new Spot { Id = 34, SpotNumber = "D-04", IsOccupied = false },
            new Spot { Id = 35, SpotNumber = "D-05", IsOccupied = false },
            new Spot { Id = 36, SpotNumber = "D-06", IsOccupied = false },
            new Spot { Id = 37, SpotNumber = "D-07", IsOccupied = false },
            new Spot { Id = 38, SpotNumber = "D-08", IsOccupied = false },
            new Spot { Id = 39, SpotNumber = "D-09", IsOccupied = false },
            new Spot { Id = 40, SpotNumber = "D-10", IsOccupied = false }
        );
    }
}
