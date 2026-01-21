using Domain.Entities;
using Domain.Entities.Abstracts;
using Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
    public DbSet<Session> Sessions { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Payment> Payments { get; set; }
    public DbSet<Spot> ParkingSpots { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Setting> Settings { get; set; }


    // These two are optional, but better to adding them to simplicity 
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

    }


}