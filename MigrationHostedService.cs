using LoonyBin.DAL;
using Microsoft.EntityFrameworkCore;

namespace LoonyBin
{
    public class MigrationHostedService(IServiceProvider provider) : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = provider.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<PatientDbContext>();
            await db.Database.MigrateAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
