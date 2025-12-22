using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Infrastructure.Data.Configurations;

public class SessionConfiguration : IEntityTypeConfiguration<Session>
{
    public void Configure(EntityTypeBuilder<Session> builder)
    {
        builder.ToTable("Sessions");

        // Primary key
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
               .UseIdentityColumn(1, 1);

        // Cost per hour
        builder.Property(s => s.CostPerHour)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        // Value Object mapping: DateTimeSlot
        builder.OwnsOne(s => s.DateTimeSlot, slot =>
        {
            slot.Property(d => d.EntryTimestamp)
                .IsRequired()
                .HasColumnName("EntryTimestamp");

            slot.Property(d => d.ExitTimestamp)
                .HasColumnName("ExitTimestamp");
        });

        // Relationships
        builder.HasOne(s => s.Car)
               .WithMany(c => c.Sessions)
               .HasForeignKey(s => s.CarId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(s => s.Spot)
                .WithMany(c => c.Sessions)
                .HasForeignKey(s => s.SpotId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict); 

        builder.HasOne(s => s.CreatedUser)
                .WithMany(u => u.Sessions)
                .HasForeignKey(s => s.CreatedUserId)
                .OnDelete(DeleteBehavior.Restrict);

        // Optional: indexes for performance
        builder.HasIndex(s => s.CarId);
        builder.HasIndex(s => s.SpotId);
    }
}
