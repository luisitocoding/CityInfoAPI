using CityInfoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI.Controllers
{
    [Produces("application/xml")]
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet()]
        public IActionResult Get()
        {
            return Ok(CiudadDataStore.Current.Ciudades);
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
