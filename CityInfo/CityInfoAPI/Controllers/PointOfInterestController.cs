using AutoMapper;
using CityInfoAPI.Models;
using CityInfoAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI.Controllers
{
    [Route("api")]
    public class PointOfInterestController : Controller
    {
        private ICityInfoRepository _cityInfoRepository;
        private ILogger<PointOfInterestController> _logger;
        //Inyectar el servicio y usarlo en el delete
        //private LocalMailService _mailService;

        //Sobre la interfaz una vez creado las interfaces 
       // private IMailService _mailService;
        public PointOfInterestController(/*ILogger<PointOfInterestController> logger, IMailService mailService,*/ICityInfoRepository cityInfoRepository)
        {
            //_logger = logger;
            //_mailService = mailService;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("cities/{id}/pointofinterest")]
        public IActionResult GetPointOfInterest(int id)
        {
                if (!_cityInfoRepository.CityExist(id))
                {
                    _logger.LogInformation($"la ciudad con el id {id} no existe");
                    return NotFound();
                }

                var pointOfInterestForCity = _cityInfoRepository.GetPointsOfInterestsForCity(id);
                var pointOfInterestForCityResults = Mapper.Map<IEnumerable<PointOfInterestDto>>(pointOfInterestForCity);
                return Ok(pointOfInterestForCityResults);
            
            //catch (Exception ex)
            //{
            //    _logger.LogCritical($"Ciudad con el ID: {id} no se encontro mientras buscabamos POI", ex);
            //    return StatusCode(500, "Un problema ocurrio con el servidor");
            //}
        }

        //Metodos desde El Repository

        //Crear Punto de Interes
        [HttpPost("cities/{cityid}/pointofinterest")]
        public IActionResult CreatePointOfInterst([FromBody] PointOfInteresForCreation pointOfInteres, int cityid)
        {
            if (pointOfInteres == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(!_cityInfoRepository.CityExist(cityid))
            {
                return NotFound();
            }

            var finalPointOfInterst = Mapper.Map<Entities.PointOfInterest>(pointOfInteres);

            _cityInfoRepository.AddPointOfInterestForCity(cityid, finalPointOfInterst);

            if (!_cityInfoRepository.Save())
            {
                return StatusCode(500, "Hola k ace, tiene un error o k");
            }

            var CreatedPointOfInterestToReturn = Mapper.Map<Models.PointOfInterestDto>(finalPointOfInterst);
            return CreatedAtRoute("GetPointOfInterest", new { cityid = cityid, id = CreatedPointOfInterestToReturn.ID }, CreatedPointOfInterestToReturn);
        }

        //Obtener Punto de Interes por ID
        [HttpGet("{cityid}/pointofinterest/{id}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterestById(int cityid, int id)
        {
            if (!_cityInfoRepository.CityExist(cityid))
            {
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetPointOfInterestsForCity(cityid, id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            var pointOfInterestResult = Mapper.Map<PointOfInterestDto>(pointOfInterest);
            return Ok(pointOfInterestResult);
        }
        

        /* Metodos Desde el CiudadDataStore
        [HttpGet("point/{cityid}/pointofinterest/{id}", Name = "Creado")]
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

            if (ModelState.IsValid)
            {
                city.PointOfInterest.Add(finalPointOfInterest);
            }
            else
            {
                return BadRequest(ModelState);
            }

            return new CreatedAtRouteResult("Creado", new { id = city.Id }, finalPointOfInterest);
        }

        [HttpPut("{cityid}/pointofinterest/{id}")]
        public IActionResult Put([FromBody] PointOfInteresForCreationUpdate pointOfInteres, int cityid, int id)
        {
            if (pointOfInteres == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CiudadDataStore.Current.Ciudades.FirstOrDefault(c => c.Id == cityid);

            if (city == null)
            {
                return NotFound();
            }

            var idPointOfInterest = city.PointOfInterest.FirstOrDefault(p => p.ID == id);

            if (idPointOfInterest == null)
            {
                return NotFound();
            }

            idPointOfInterest.Name = pointOfInteres.Name;
            idPointOfInterest.Description = pointOfInteres.Description;

            return NoContent();
        }

        [HttpPatch("{cityid}/pointofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityid, int id, [FromBody] JsonPatchDocument<PointOfInteresForCreationUpdate> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var city = CiudadDataStore.Current.Ciudades.FirstOrDefault(c => c.Id == cityid);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(p => p.ID == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInteresForCreationUpdate
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };
            patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError("Description", "La descripcion no puede ser igual al nombre");
            }

            TryValidateModel(pointOfInterestToPatch);//Activar validaciones de las notaciones

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();

            /*
             [
	            {
		            "op": "replace",
		            "path": "/Name",
		            "value": "update"
	            },
	            {
		            "op":"replace",
		            "path":"/Description",
		            "value": "update"
	            }

            204 se usa para put y patch
            ]


            -Status Code:
                POST: 201 CREATED
                PUT: Actualizacion completa 
                PATCH: Parcial  (op, path, value) 204 o 200 
                DELETE: 204                
             * 
        }

        [HttpDelete("{cityid}/pointofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityid, int id)
        {
            var city = CiudadDataStore.Current.Ciudades.FirstOrDefault(c => c.Id == cityid);

            if (city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointOfInterest.FirstOrDefault(p => p.ID == id);

            if (pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointOfInterest.Remove(pointOfInterestFromStore);
            //Servicio
            //_mailService.Send($"Recurso Eliminado {pointOfInterestFromStore.Name}", $"El recurso con el id : {pointOfInterestFromStore.ID} ha sido eliminado");
            return NoContent();
        }*/
    }
}
