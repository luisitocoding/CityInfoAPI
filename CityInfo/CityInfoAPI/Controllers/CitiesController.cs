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
        ICityInfoRepository _cityInfoRepository; 
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet()]
        public IActionResult Get()
        {
            //return Ok(CiudadDataStore.Current.Ciudades);
            var citiesEntities = _cityInfoRepository.GetCities();
            var results = new List<CityWithoutPointOfInterestDto>();
            foreach (var cityEntity in citiesEntities)
            {
                results.Add(new CityWithoutPointOfInterestDto
                {
                    Id = cityEntity.Id,
                    Description = cityEntity.Description,
                    Name = cityEntity.Name
                });
            }
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
        public IActionResult GetById(int id)
        {
            var ciudad = CiudadDataStore.Current.Ciudades.FirstOrDefault(c => c.Id == id);
            if (ciudad == null)
            {
                return NotFound();                           
            }
            return Ok(ciudad);
        }
        //Crear una api,que regrese los datos personales de los alumnos, ID,NOMBRE, EMAIL,TELEFONO, FECHANACIMIENTO: buscar por nombre, por id y por mes 
    }
}
