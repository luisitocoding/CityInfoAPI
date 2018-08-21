using AutoMapper;
using CityInfoAPI.Models;
using CityInfoAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI.Controllers
{
   
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        //Inyectar el repositorio 
        private ICityInfoRepository _cityInfoRepository; 
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        //Automaper se descarga un Nuget de ahi en Startup Metodo Configure se hace una sola vez
        //Esto es para que haga el mapeo 
        [HttpGet()]
        public IActionResult GetCities()
        {
            //return Ok(CiudadDataStore.Current.Ciudades);
            var citiesEntities = _cityInfoRepository.GetCities();
            var results = Mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(citiesEntities);

            return Ok(results);
        }

        [HttpGet("datos")]
        public IActionResult GetByName()
        {
            return new JsonResult(new List<object>
            {
                new {Nombre="Luis Angel"},
                new {Correo="luis@hotmail.com"}
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetCityById(int id, bool includePointOfInterest)
        {
            var city = _cityInfoRepository.GetCity(id, includePointOfInterest);
            
            if (city == null)
            {
                return NotFound();                           
            }

            if (includePointOfInterest)
            {
                var cityResult = Mapper.Map<CiudadDto>(city);
                return Ok(cityResult);
            }
            var cityWithoutPointOfInterest = Mapper.Map<CityWithoutPointOfInterestDto>(city);
            return Ok(cityWithoutPointOfInterest);
        }
        //Crear una api,que regrese los datos personales de los alumnos, ID,NOMBRE, EMAIL,TELEFONO, FECHANACIMIENTO: buscar por nombre, por id y por mes 
    }
}
