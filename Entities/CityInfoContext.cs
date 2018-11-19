using Microsoft.EntityFrameworkCore;

namespace hello_world_web.Entities
{
    public class CityInfoContext: DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options): base(options)
        {    
            //Database.EnsureCreated();
            Database.Migrate();
        }

        public DbSet<City> City { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }


    }
}