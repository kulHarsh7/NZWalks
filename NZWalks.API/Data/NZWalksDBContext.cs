using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.DomainModels;

namespace NZWalks.API.Data
{
    public class NZWalksDBContext:DbContext
    {
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> dbContextOptions):base(dbContextOptions)
        {
                
        }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
        public DbSet<Difficulty> Difficulties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var Difficultydata = new List<Difficulty>
            {
                new Difficulty()
                {
                  Id=Guid.Parse("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                  Name="Easy"
                },
                new Difficulty()
                {
                  Id=Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                  Name="Medium"
                },
                new Difficulty()
                {
                  Id=Guid.Parse("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
                  Name="Hard"
                }
            };
            modelBuilder.Entity<Difficulty>().HasData(Difficultydata);

            var walkData = new List<Walk>
            {
                new Walk()
                {
                    Id=Guid.Parse("327aa9f7-26f7-4ddb-8047-97464374bb63"),
                    Name="Mount Victoria Loop",
                    Description="This scenic walk takes you around the top of Mount Victoria, offering stunning views of Wellington and its harbor.",
                    lengthInKM=3.5,
                    WalkImageUrl="https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    DifficultyId=Guid.Parse("54466f17-02af-48e7-8ed3-5a4a8bfacf6f"),
                    RegionId=Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6")
                },
                new Walk()
                {
                    Id=Guid.Parse("1cc5f2bc-ff4b-47c0-a475-1add56c6497b"),
                    Name="Makara Beach Walkway",
                    Description="This walk takes you along the wild and rugged coastline of Makara Beach, with breathtaking views of the Tasman Sea.",
                    lengthInKM=8.2,
                    WalkImageUrl="https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    DifficultyId=Guid.Parse("ea294873-7a8c-4c0f-bfa7-a2eb492cbf8c"),
                    RegionId=Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6")
                },
                new Walk()
                {
                    Id=Guid.Parse("09601132-f92d-457c-b47e-da90e117b33c"),
                    Name="Botanic Garden Walk",
                    Description="Explore the beautiful Botanic Garden of Wellington on this leisurely walk, with a wide variety of plants and flowers to admire.",
                    lengthInKM=7.5,
                    WalkImageUrl="https://images.pexels.com/photos/4350631/pexels-photo-4350631.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1",
                    DifficultyId=Guid.Parse("f808ddcd-b5e5-4d80-b732-1ca523e48434"),
                    RegionId=Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153")
                }
            };
            modelBuilder.Entity<Walk>().HasData(walkData);

            var regionData = new List<Region>()
            {
                new Region()
                {
                    Id=Guid.Parse("906cb139-415a-4bbb-a174-1a1faf9fb1f6"),
                    Code="NSN",
                    Name="Nelson",
                    RegionImageUrl="https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id=Guid.Parse("f7248fc3-2585-4efb-8d1d-1c555f4087f6"),
                    Code="AKL",
                    Name="Auckland",
                    RegionImageUrl="https://images.pexels.com/photos/5169056/pexels-photo-5169056.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                },
                new Region()
                {
                    Id=Guid.Parse("14ceba71-4b51-4777-9b17-46602cf66153"),
                    Code="BOP",
                    Name="Bay Of Plenty",
                    RegionImageUrl="https://images.pexels.com/photos/13918194/pexels-photo-13918194.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1"
                }
            };
            modelBuilder.Entity<Region>().HasData(regionData);
        }
    }
}
