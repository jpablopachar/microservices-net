using PlatformService.Models;

namespace PlatformService.Data.Platforms
{
    public interface IPlatformRepository
    {
        Task<bool> SaveChanges();

        Task<IEnumerable<Platform>> GetAllPlatforms();

        Task<Platform> GetPlatformById(int id);

        Task CreatePlatform(Platform platform);
    }
}