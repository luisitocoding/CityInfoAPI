using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI.Entities
{
    public class CityInfoDbContext:DbContext
    {
        //Registrar el contexto de datos en mi aplicacion "Startup"
        public CityInfoDbContext(DbContextOptions<CityInfoDbContext> options) : base(options)
        {
            //Llamar EnsureCreated() para forzar a crear la BD y crear DummyController
            Database.Migrate();
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }
       
    }
}
