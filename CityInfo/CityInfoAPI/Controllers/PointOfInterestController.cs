using CityInfoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI.Controllers
{
    [Route("api")]
    public class PointOfInterestController:Controller
    {
        [HttpGet("cities/{id}/pointofinterest")]
        public IActionResult GetPointOfInterest(int id)
        {
            var city = CiudadDataStore.Current.Ciudades.FirstOrDefault(c => c.Id == id);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.PointOfInterest);
        }

        [HttpGet("point/{cityid}/pointofinterest/{id}",Name ="Creado")]
        public IActionResult GetPointOfInterestById(int cityid, int id)
        {
            var city = CiudadDataStore.Current.Ciudades.FirstOrDefault(c => c.Id == cityid);

            if (city == null)
            {
                return NotFound();
            }
            // return Ok(city.PointOfInterest);
            var PointOfInterest = city.PointOfInterest.FirstOrDefault(poi => poi.ID == id);
            if (PointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(PointOfInterest);
        }

        [HttpPost("{cityid}/pointofinterest")]
        public IActionResult CreatePointOfInterest([FromBody] PointOfInteresForCreation pointOfInteres, int cityId)
        {
            if (pointOfInteres == null)
            {
                return BadRequest();
            }

            var city = CiudadDataStore.Current.Ciudades.FirstOrDefault(c => c.Id == cityId);

            if (city == null)
            {
                return NotFound();
            }

            var maxPointOfInterestId = CiudadDataStore.Current.Ciudades.SelectMany(c => c.PointOfInterest).Max(p => p.ID);

            var finalPointOfInterest = new PointOfInterestDto
            {
                ID = ++maxPointOfInterestId,
                Name = pointOfInteres.Name,
                Description = pointOfInteres.Description
            };

            city.PointOfInterest.Add(finalPointOfInterest);

            return new CreatedAtRouteResult("Creado", new { id = city.Id }, finalPointOfInterest);
        }

    }
}
