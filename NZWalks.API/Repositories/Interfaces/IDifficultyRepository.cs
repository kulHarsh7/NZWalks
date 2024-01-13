using NZWalks.API.Models.DomainModels;

namespace NZWalks.API.Repositories.Interfaces
{
    public interface IDifficultyRepository
    {
        Task<List<Difficulty>> GetAllDifficultyAsync();
        Task<Difficulty?> GetDifficultyAsync(Guid Id);
        Task<Difficulty> CreateDifficultyAsync(Difficulty difficulty);
        Task<Difficulty?> UpdateDifficultyAsync(Guid Id, Difficulty difficulty);
        Task<Difficulty?> DeleteDifficultyAsync(Guid Id);
    }
}
