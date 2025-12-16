using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CarConfiguration : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.ToTable("Cars");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn(1, 1);

            builder.Property(c => c.PlateNumber)
                .IsRequired()
                .HasMaxLength(15);

            builder.HasIndex(c => c.PlateNumber)
                .IsUnique();


            builder.HasOne(car => car.Customer)
                .WithMany(customer => customer.Cars)
                .HasForeignKey(car => car.CustomerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
