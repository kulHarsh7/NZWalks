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

        public async Task<List<Walk>> GetAllWalkAsync(string? filterOn=null, string? filterValue = null, string? orderBy = null, 
            bool isAscending = true, int pageNumber = 1, int pageSize = 5)
        {
            var walkData = dbContext.Walks.AsQueryable();
            if(!string.IsNullOrEmpty(filterOn)&&!string.IsNullOrEmpty(filterValue))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walkData = walkData.Where(x => x.Name.Contains(filterValue));
                }
                else if(filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    walkData = walkData.Where(x => x.Description.Contains(filterValue));
                }
            }
             if(!string.IsNullOrEmpty(orderBy))
            {
                if (orderBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    walkData = isAscending ? walkData.OrderBy(x => x.Name):walkData.OrderByDescending(x=>x.Name);
                }
            }
          
            var skipResult = (pageNumber - 1) * pageSize;

            return await walkData.Skip(skipResult).Take(pageSize).ToListAsync();
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
