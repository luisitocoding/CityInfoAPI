using CityInfoAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI
{
    public static class CityInfoContextExtension
    {
        public static void EnsureSeedDatForContext(this CityInfoDbContext context)
        {
            if (context.Cities.Any())
            {
                return;
            }
            var cities = new List<City>()
            {
                new City()
                {
                    Name="Iguala de la Independencia",
                    Description="Cuna de la Bandera Nacional",
                    PointOfInterest= new List<PointOfInterest>
                    {
                        new PointOfInterest()
                        {
                            Name="Asta Bandera Monumental",
                            Description="La mas alta de LatinoAmerica"
                        },
                        new PointOfInterest()
                        {
                            Name="Laguna de Tuxpan",
                            Description="Lugar de Recreacion Familiar"
                        }
                    }
                },
                new City()
                {
                    Name="Acapulco de Juarez",
                    Description="la Bahia mas hermosa del Mundo",
                    PointOfInterest= new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name="La Quebrada",
                            Description="Lugar de emociones externas"
                        }
                    }
                }
            };
            context.Cities.AddRange(cities);
            context.SaveChanges();
        }
    }
}