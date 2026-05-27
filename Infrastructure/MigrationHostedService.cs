using LoonyBin.DAL;
using Microsoft.EntityFrameworkCore;

namespace LoonyBin.Infrastructure
{
    public class MigrationHostedService(IServiceProvider provider,
        ILogger<MigrationHostedService> logger) : IHostedService
    {
        public async Task StartAsync(CancellationToken ct)
        {
            using var scope = provider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<PatientDbContext>();

            logger.LogInformation("Applying migrations...");
            await dbContext.Database.MigrateAsync(ct);

            logger.LogInformation("Seeding initial data...");
            await DbSeeder.SeedAsync(dbContext, ct);

            logger.LogInformation("Database ready.");
        }

        public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
    }
}
