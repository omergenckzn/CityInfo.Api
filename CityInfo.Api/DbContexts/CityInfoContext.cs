using CityInfo.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.Api.DbContexts
{
    public class CityInfoContext : DbContext
    {

        public DbSet<City> Cities { get; set; } = null!;
        public DbSet<PointOfInterest> PointOfInterests { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
            {
                
            }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("New York City")
                {
                    Id = 1,
                    Description = "The one with that big park"
                },
                new City("Istanbul")
                {
                    Id = 2,
                    Description = "The constantinopolis"
                },
                 new City("Paris")
                 {
                     Id = 3,
                     Description = "The one with big tower"
                 }
                );

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Central park")
                {
                    Id=1,
                    CityId = 1,
                    Description = "Parrrkk"
                },
                new PointOfInterest("Empire state building")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "Parrrkk"
                },
                new PointOfInterest("FSM")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "Koprü"
                },
                new PointOfInterest("Eiffel Tower")
                {
                    Id = 4,
                    CityId = 3,
                    Description = "Copper alloy"
                },
                new PointOfInterest("Loure Museum")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "Muzede bir gece"
                }


                );

            base.OnModelCreating(modelBuilder);
        }


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}

    }
}
