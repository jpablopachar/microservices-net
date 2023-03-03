using PlatformService.Models;
using Microsoft.EntityFrameworkCore;

namespace PlatformService.Data.Platforms
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly AppDbContext _context;

        public PlatformRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreatePlatform(Platform platform)
        {
            if (platform is null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            await _context.Platforms!.AddAsync(platform);
        }

        public async Task<IEnumerable<Platform>> GetAllPlatforms()
        {
            return await _context.Platforms!.ToListAsync();
        }

        public async Task<Platform> GetPlatformById(int id)
        {
            return await _context.Platforms!.FirstOrDefaultAsync(platform => platform.Id == id)!;
        }

        public async Task<bool> SaveChanges()
        {
            return ((await _context.SaveChangesAsync()) >= 0);
        }
    }
}