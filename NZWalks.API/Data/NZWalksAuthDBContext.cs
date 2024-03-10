using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDBContext : IdentityDbContext
    {
        public NZWalksAuthDBContext(DbContextOptions<NZWalksAuthDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerId = "8add3282-a052-4c5a-9342-6c6d974da376";
            var writerId = "45298d55-d636-41a2-92bc-f042148eea83";
            var roles = new List<IdentityRole>()
            {
              new IdentityRole()
              {
                  Id=readerId,
                  Name="Reader",
                  NormalizedName="Reader".ToUpper(),
                  ConcurrencyStamp=readerId
              },
                new IdentityRole()
              {
                  Id=writerId,
                  Name="Writer",
                  NormalizedName="Writer".ToUpper(),
                  ConcurrencyStamp=writerId
              }
            };

            builder.Entity<IdentityRole>().HasData(roles);
        }
       
    }
}
