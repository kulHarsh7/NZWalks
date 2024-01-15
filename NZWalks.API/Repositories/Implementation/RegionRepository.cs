using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.DomainModels;
using NZWalks.API.Repositories.Interfaces;

namespace NZWalks.API.Repositories.Implementation
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDBContext dbContext;

        public RegionRepository(NZWalksDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Region> CreateRegionAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;

        }
        public async Task<Region?> DeleteRegionAsync(Guid Id)
        {
            var regionData= await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id==Id);
            if(regionData==null)
            {
                return null;
            }
             dbContext.Regions.Remove(regionData);
             await dbContext.SaveChangesAsync();
             return regionData;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetRegionAsync(Guid Id)
        {
           return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<Region?> UpdateRegionAsync(Guid Id, Region region)
        {
            var regionData = await dbContext.Regions.FirstOrDefaultAsync(x=>x.Id== Id);
            if(regionData==null)
            {
                return null;
            }
            regionData.Name = region.Name;
            regionData.Code = region.Code;
            regionData.RegionImageUrl = region.RegionImageUrl;
            await dbContext.SaveChangesAsync();
            return regionData;
        }

    }
}
