using Djay.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Djay
{
    public class DbMigration: IHostedService
    {
        private readonly IServiceProvider serviceProvider;

        public DbMigration(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(IServiceProvider));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var serviceScope = this.serviceProvider.CreateScope();
            var applicationDbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await applicationDbContext.Database.MigrateAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}