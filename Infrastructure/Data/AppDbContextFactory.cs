using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Infrastructure.Data;

public class AppDbContextFactory //: IDesignTimeDbContextFactory<AppDbContext>
{
    //public AppDbContext CreateDbContext(string[] args)
    //{
    //    var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
    //    optionsBuilder.UseSqlServer("Server=.;Database=ParkingGarage;Integrated Security=SSPI;TrustServerCertificate=True;"); // Hardcoded or from env

    //    return new AppDbContext(optionsBuilder.Options);
    //}
}
