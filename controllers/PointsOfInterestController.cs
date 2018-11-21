//using hello_world_web.model;
using hello_world_web.models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using hello_world_web.Services;
using AutoMapper;

namespace hello_world_web.controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController: Controller
    {

        private ILogger<PointsOfInterestController> _logger;

        private IMailService _mailService;

        private ICityInfoRepository _cityInfoRepository;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,
        IMailService mailService, ICityInfoRepository cityInfoRepository)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId) 
        {
            try
            {
               if(!_cityInfoRepository.CityExists(cityId))
                {
                    _logger.LogInformation($"City with id {cityId} wasn't found when acessing points of interest");
                    return NotFound();
                }

                var pointsOfInterestForCity = _cityInfoRepository.GetPointsOfIterestForCity(cityId);

                var pointsOfInterestForCityResult = new List<PointsOfInterestDto>();

                foreach (var poi in pointsOfInterestForCity)
                {
                    pointsOfInterestForCityResult.Add(new PointsOfInterestDto(){
                        Id = poi.Id,
                        Name = poi.Name,
                        Description = poi.Description
                    });
                }

                return Ok(pointsOfInterestForCityResult);
            }
            catch (System.Exception)
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when acessing points of interest");
                return StatusCode(500, "A problem happenedd while handling your request.");
            }
        }

        [HttpGet("{cityId}/pointofinterest/{id}", Name="GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            if(!_cityInfoRepository.CityExists(cityId))
            {
                _logger.LogInformation($"City with id {cityId} wasn't found when acessing points of interest");
                return NotFound();
            }

            var pointOfInterest = _cityInfoRepository.GetPointOfInterestForCity(cityId, id);

            if(pointOfInterest == null)
            {   
                return NotFound();
            }

            //AUTOMAP(automapper)
            var pointsOfInterestResult = Mapper.Map<PointsOfInterestDto>(pointOfInterest);
            
            //MANUAL MAPPIING
            // var pointsOfInterestResult = new PointsOfInterestDto(){
            //     Id = pointOfInterest.Id,
            //     Name = pointOfInterest.Name,
            //     Description = pointOfInterest.Description
            // };

            return Ok(pointsOfInterestResult);

            // var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            // if(city == null){
            //     return NotFound();
            // }

            // var pointOfInterest = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            // if(pointOfInterest == null){
            //     return NotFound();
            // }  

            // return Ok(pointOfInterest);
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult createPointOfInterest (int cityId, 
        [FromBody] PointOfInterestForCreationDto pointsOfInterest)
        {
            if(pointsOfInterest == null)
            {
                return BadRequest();
            }

            if(pointsOfInterest.Name == pointsOfInterest.Description)
            {
                ModelState.AddModelError("Description", "The provided description should be diferent from the name");
            }

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            // if(city == null)
            // {
            //     return NotFound();
            // }

            if(!_cityInfoRepository.CityExists(cityId))
            {
                return NotFound();
            }

            //AUTO MAPPING
            //Map a entity from a dto(json object income) 
            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointsOfInterest);

            //demo, we dont need it because id is auto generated
            // var maxPointOfIterestId = CitiesDataStore.Current.Cities.SelectMany(
            //     c => c.PointsOfInterest).Max(p => p.Id);

            //MANUAL MAP
            // var finalPointOfInterest = new PointsOfInterestDto()
            // {
            //     Id = ++maxPointOfIterestId,
            //     Name =  pointsOfInterest.Name,
            //     Description = pointsOfInterest.Description
            // };

            //city.PointsOfInterest.Add(finalPointOfInterest);
            _cityInfoRepository.AddPointOfIterestForCity(cityId, finalPointOfInterest);
            
            if(!_cityInfoRepository.Save())
            {
                return StatusCode(500, "A Problem happened while handling your request");
            }

            var createdPointOfInterest = Mapper.Map<models.PointsOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest", new {cityId = cityId, Id = finalPointOfInterest.Id}, createdPointOfInterest);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(int cityId, int id,
        [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            if(pointOfInterest == null){
                return BadRequest();
            }

            if(pointOfInterest.Name == pointOfInterest.Description){
                ModelState.AddModelError("Description", "The provided description should be diferent from the name");
            }

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);

            if(city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);

            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            //finally update
            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int id, 
        [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            if(patchDoc == null)
            {
                return BadRequest();
            }

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if(pointOfInterestToPatch.Name == pointOfInterestToPatch.Description){
                ModelState.AddModelError("Description", "The provided description should be diferent from the name");
            }

            //validate after update
            TryValidateModel(pointOfInterestToPatch);

            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest (int cityId, int id)
        {
            var city = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == cityId);
            if(city == null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(p => p.Id == id);
            if(pointOfInterestFromStore == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestFromStore);
        
            _mailService.Send("Point of interest deleted",
            $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestFromStore.Id} was deleted");

            return NoContent();
        }
    }
}