using Application.DependencyInjection;
using Application.Security.Jwt;
using Infrastructure.DependencyInjection;
using Infrastructure.Security;
using Presentation.Middlewares;
using Serilog;

namespace Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog((context, services, config) =>
            {
                config.ReadFrom.Configuration(context.Configuration)
                        .ReadFrom.Services(services)
                        .Enrich.FromLogContext()
                        .WriteTo.Console()
                        .WriteTo.File(
                                path: "Logs/log-.txt",
                                rollingInterval: RollingInterval.Day,
                                rollOnFileSizeLimit: false,   // prevents _001 creation
                                retainedFileCountLimit: 31,  // keep 31 days and delete after that period);
                                shared: true);
            });


            builder.Services.AddControllers();

            builder.Services.AddSwaggerGen();

            builder.Configuration.AddUserSecrets<Program>(); 

            builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("Jwt"));

            builder.Services.AddInfrastructureServices(builder.Configuration["ConnectionStrings:DefaultConnection"]!);
            builder.Services.AddApplicationServices();
            builder.Services.AddJwtAuthentication(builder.Configuration, "Jwt");


            var app = builder.Build();

            if(app.Environment.IsDevelopment())
            {
                app.UseSwagger(); 
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
