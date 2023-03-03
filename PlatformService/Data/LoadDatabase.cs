using PlatformService.Models;

namespace PlatformService.Data
{
    public class LoadDatabase
    {
        public static async Task InsertData(AppDbContext context)
        {
            if (!context.Platforms!.Any())
            {
                context.Platforms!.AddRange(
                    new Platform() { Name = "Dot Net", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "SQL Server Express", Publisher = "Microsoft", Cost = "Free" },
                    new Platform() { Name = "Kubernetes", Publisher = "Cloud Native Computing Foundation", Cost = "Free" }
                );

                await context.SaveChangesAsync();
            }
        }
    }
}