using NZWalks.API.Models.DomainModels;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();
        Task<Region?> GetRegionAsync(Guid Id);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region?> UpdateRegionAsync(Guid Id, Region region);
        Task<Region?> DeleteRegionAsync(Guid Id);

    }
}
