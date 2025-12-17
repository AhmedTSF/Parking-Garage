using Domain.Entities;
using Domain.Enums;
using Domain.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
{
    public void Configure(EntityTypeBuilder<Payment> builder)
    {
        builder.ToTable("Payments");

        // Primary key
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
               .UseIdentityColumn(1, 1);

        // Amount
        builder.Property(p => p.Amount)
               .IsRequired()
               .HasColumnType("decimal(18,2)");

        // Timestamp
        builder.Property(p => p.PaidAt)
               .IsRequired();

        // PaymentMethod as string
        builder.Property(p => p.PaymentMethod)
               .IsRequired()
               .HasConversion(
                        v => v.ToString().ToSpaced(),
                        v => Enum.Parse<PaymentMethod>(v.Replace(" ", "")))
               .HasMaxLength(25)
               ;

        // Relationship with Session
        builder.HasOne(p => p.Session)
               .WithMany(s => s.Payments)
               .HasForeignKey(p => p.SessionId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Cascade); 

        // Optional: Index on SessionId for faster queries
        builder.HasIndex(p => p.SessionId);
    }
}
