using System.Threading.Tasks;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.Hosting
{
    internal static class DatabaseExtensions
    {
        internal static async Task EnsureDbUpToDate(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<GroupMangmentDbContext>();

                await context.Database.MigrateAsync();
            }
        }
    }
}
