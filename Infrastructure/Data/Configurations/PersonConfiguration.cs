
using Domain.Entities;
using Domain.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("People");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .UseIdentityColumn(1, 1);

        builder.Property(p => p.NationalId)
            .IsRequired()
            .HasMaxLength(20);

        builder.Property(p => p.FirstName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.LastName)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(p => p.PhoneNumber)
            .IsRequired()
            .HasMaxLength(50);

        // Discriminator for TPH
        builder.HasDiscriminator<string>("PersonType")
            .HasValue<Customer>("Customer")
            .HasValue<User>("User");


    }
}
