using NZWalks.API.Models.DomainModels;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllWalkAsync();
        Task<Walk?> GetWalkAsync(Guid Id);
        Task<Walk> CreateWalkAsync(Walk walk);
        Task<Walk?> UpdateWalkAsync(Guid Id, Walk walk);
        Task<Walk?> DeleteWalkAsync(Guid Id);
    }
}
