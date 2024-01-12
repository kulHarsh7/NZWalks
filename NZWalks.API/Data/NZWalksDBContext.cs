using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.DomainModels;

namespace NZWalks.API.Data
{
    public class NZWalksDBContext:DbContext
    {
        public NZWalksDBContext(DbContextOptions dbContextOptions):base(dbContextOptions)
        {
                
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }
    }
}
