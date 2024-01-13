using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.DomainModels;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories.Implementation
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDBContext dbContext;

        public WalkRepository(NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<Walk> CreateWalkAsync(Walk walk)
        {

            await dbContext.Walks.AddAsync(walk);
            await dbContext.SaveChangesAsync();
            return walk;
        }

        public async Task<Walk?> DeleteWalkAsync(Guid Id)
        {
            var walkData = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (walkData == null)
            {
                return null;
            }
            dbContext.Walks.Remove(walkData);
            await dbContext.SaveChangesAsync();
            return walkData;
        }

        public async Task<List<Walk>> GetAllWalkAsync()
        {
            return await dbContext.Walks.ToListAsync();
        }

        public async Task<Walk?> GetWalkAsync(Guid Id)
        {
            return await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Walk?> UpdateWalkAsync(Guid Id, Walk walk)
        {
            var walkData = await dbContext.Walks.FirstOrDefaultAsync(x => x.Id == Id);
            if (walkData == null)
            {
                return null;
            }
            walkData.Name = walk.Name;
            walkData.Description = walk.Description;
            walkData.lengthInKM = walk.lengthInKM;
            walkData.WalkImageUrl = walk.WalkImageUrl;
            walkData.DifficultyId = walk.DifficultyId;
            walkData.RegionId = walk.RegionId;
          
            await dbContext.SaveChangesAsync();
            return walkData;
        }
    }
}
