using System.Security.Cryptography.X509Certificates;
using Ambev.DeveloperEvaluation.Application;
using Ambev.DeveloperEvaluation.Common.HealthChecks;
using Ambev.DeveloperEvaluation.Common.Logging;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.IoC;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.WebApi.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace Ambev.DeveloperEvaluation.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Starting web application");

            WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
            builder.AddDefaultLogging();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();

            builder.AddBasicHealthChecks();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<DefaultContext>(options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("POSTGRES_CONNECTION_STRING") ??
                                       builder.Configuration.GetConnectionString("DefaultConnection");

                options.UseNpgsql(
                    connectionString,
                    b => b.MigrationsAssembly("Ambev.DeveloperEvaluation.ORM")
                );
            });

            builder.Services.AddJwtAuthentication(builder.Configuration);

            builder.RegisterDependencies();

            builder.Services.AddAutoMapper(typeof(Program).Assembly, typeof(ApplicationLayer).Assembly);

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(
                    typeof(ApplicationLayer).Assembly,
                    typeof(Program).Assembly
                );
            });
            
            // Configures Kestrel to use a custom HTTPS certificate located at
            // "/https/dev-certificate.pfx" with the password "senhaSegura123".
            // This setup is useful in environments where the default developer certificate
            // is not available, such as inside Docker containers.
            const string certificatePath = "/https/dev-certificate.pfx";
            const string certificatePassword = "senhaSegura123";

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                if (File.Exists(certificatePath))
                {
                    serverOptions.ConfigureHttpsDefaults(httpsOptions =>
                    {
                        httpsOptions.ServerCertificate = new X509Certificate2(certificatePath, certificatePassword);
                    });
                }
            });

            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            var app = builder.Build();
            app.UseMiddleware<ValidationExceptionMiddleware>();
            
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseBasicHealthChecks();

            app.MapControllers();
            
            Console.WriteLine("Initializing migrations...");
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DefaultContext>();
            dbContext.ApplyMigrations();

            app.Run();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Application terminated unexpectedly: {ex.Message}");
        }
        finally
        {
            Console.WriteLine("Application terminated unexpectedly");
            Log.CloseAndFlush();
        }
    }
}
