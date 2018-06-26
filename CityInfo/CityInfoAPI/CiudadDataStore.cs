using CityInfoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI
{
    public class CiudadDataStore
    {
        public static CiudadDataStore Current { get; } = new CiudadDataStore();
        public List<CiudadDto> Ciudades { get; set; }

        public CiudadDataStore()
        {
            Ciudades = new List<CiudadDto>()
            {
                new CiudadDto{
                    Id =1,
                    Nombre ="Iguala de la independecia",
                    Descripcion ="Cuna de la bandera",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto{ID=1,Name="Asta Bandera Monumental",Description="La más alta de latinoamerica"},
                        new PointOfInterestDto{ID=2,Name="Laguna de Tuxpan",Description="Convivencia"}
                    }
                },
                new CiudadDto{
                    Id =2,
                    Nombre ="Acapulco",
                    Descripcion ="Lugar de la Quebrada",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto{ID=1,Name="La Quebrada",Description="es un acantilado de 45 metros de altura"},
                        new PointOfInterestDto{ID=2,Name="La Roqueta",Description="es una isla mexicana"},
                        new PointOfInterestDto{ID=3,Name="Barra Vieja",Description="es una playa de la costa del pacífico mexicano"}
                    }
                },
                new CiudadDto{
                    Id =3,
                    Nombre ="Chilpancingo",
                    Descripcion ="Capital del estado",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto{ID=1,Name="Congreso",Description="Diputados Locales"}
                    }
                },
                new CiudadDto{
                    Id =4,
                    Nombre ="Taxco de Alarcon",
                    Descripcion ="Pueblo Magico",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto{ID=1,Name="El cristo",Description="Estructura religiosa"},
                        new PointOfInterestDto{ID=2,Name="El cerro del Huisteco",Description="Atraccion Natural"},
                        new PointOfInterestDto{ID=3,Name="Catedral de santa prisca",Description="Templo Religioso"}
                    }
                },
                new CiudadDto{
                    Id =5,
                    Nombre ="Itxtapa, Zihuatanejo",
                    Descripcion ="Playita Magica",
                    PointOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto{ID=1,Name="Playa de las gatas",Description="Playa Local"},
                        new PointOfInterestDto{ID=2,Name="Museo Arqueologico",Description="Historia sobre la region"}
                    }
                }
            };
        }
    }
}
