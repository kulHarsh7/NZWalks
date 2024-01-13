using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.DomainModels;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories.Implementation
{
    public class DifficultyRepository : IDifficultyRepository
    {
        private readonly NZWalksDBContext dbContext;

        public DifficultyRepository(NZWalksDBContext dBContext)
        {
            this.dbContext = dBContext;
        }
        public async Task<Difficulty> CreateDifficultyAsync(Difficulty difficulty)
        {
            await dbContext.Difficulties.AddAsync(difficulty);
            await dbContext.SaveChangesAsync();
            return difficulty;
        }

        public async Task<Difficulty?> DeleteDifficultyAsync(Guid Id)
        {
            var difficultyData = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == Id);
            if (difficultyData == null)
            {
                return null;
            }
            dbContext.Difficulties.Remove(difficultyData);
            await dbContext.SaveChangesAsync();
            return difficultyData;
        }

        public async Task<List<Difficulty>> GetAllDifficultyAsync()
        {
            return await dbContext.Difficulties.ToListAsync();
        }

        public async Task<Difficulty?> GetDifficultyAsync(Guid Id)
        {
            return await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == Id);

        }

        public async Task<Difficulty?> UpdateDifficultyAsync(Guid Id, Difficulty difficulty)
        {
            var diffcultyData = await dbContext.Difficulties.FirstOrDefaultAsync(x => x.Id == Id);
            if (diffcultyData == null)
            {
                return null;
            }
            diffcultyData.Name = difficulty.Name;
            await dbContext.SaveChangesAsync();
            return diffcultyData;
        }
    }
}
