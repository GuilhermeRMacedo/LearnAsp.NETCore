//using hello_world_web.model;
using AutoMapper;
using hello_world_web.models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace hello_world_web.controllers
{
    [Route("api/cities")]
    public class CitiesController: Controller
    {
        private ICityInfoRepository _cityInfoRepository;
        public CitiesController(ICityInfoRepository cityInfoRepository)
        {
            _cityInfoRepository = cityInfoRepository;
        }

        [HttpGet()]
        public IActionResult GetCities() 
        {
            //return Ok(CitiesDataStore.Current.Cities);
            var Cities = _cityInfoRepository.GetCities();

            //AUTO MAPPING(automapper)
            //Map my entity City in a List of CityWithoutPointsOfInterestDto
            var results = Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(Cities);

            //MANUAL MAPPING
            //var results = new List<CityWithoutPointsOfInterestDto>();
            // foreach (var cityEntity in Cities)
            // {
            //     results.Add(new CityWithoutPointsOfInterestDto(){
            //         Id = cityEntity.Id,
            //         Name = cityEntity.Name,
            //         Description = cityEntity.Description
            //     });
            // }
            
            return Ok(results);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest=false) 
        {

            var city = _cityInfoRepository.GetCity(id, includePointsOfInterest);

            if(city == null)
            {
                return NotFound();
            }

            if(includePointsOfInterest)
            {
                //AUTO MAPPING(automapper)
                var cityResult = Mapper.Map<CityDto>(city);

                //Manual Mapping
                // var cityResult = new CityDto(){
                //     Id = city.Id,
                //     Name = city.Name,
                //     Description = city.Description
                // };

                // foreach (var poi in city.PointsOfInterest)
                // {
                //     cityResult.PointsOfInterest.Add(new PointsOfInterestDto(){
                //         Id = poi.Id,
                //         Name = poi.Name,
                //         Description = poi.Description
                //     });
                // }

                return Ok(cityResult);
            }

            //AUTO MAPPING(automapper)
            var cityWithoutPointsOfInterestDto = Mapper.Map<CityWithoutPointsOfInterestDto>(city);

            //Manual Mapping
            // var cityWithoutPointsOfInterestDto = new CityWithoutPointsOfInterestDto(){
            //     Id = city.Id,
            //     Name = city.Name,
            //     Description = city.Description
            // };

            return Ok(cityWithoutPointsOfInterestDto);

            // var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            // if(cityToReturn == null){
            //     return NotFound();
            // }

            // return Ok(cityToReturn);
        }

    }
}