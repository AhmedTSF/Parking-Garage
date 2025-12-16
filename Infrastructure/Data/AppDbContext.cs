using Domain.Entities;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Spot> ParkingSpots { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Sitting> Sittings { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    }


}

/*
 
        // Configure owned types
        modelBuilder.Entity<Session>()
            .OwnsOne(s => s.DateTimeSlot);
        modelBuilder.Entity<Session>()
            .OwnsOne(s => s.CostPerHour);
 
 */
