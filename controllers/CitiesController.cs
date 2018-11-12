//using hello_world_web.model;
using hello_world_web.models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace hello_world_web.controllers
{
    [Route("api/cities")]
    public class CitiesController: Controller
    {
        [HttpGet()]
        public IActionResult GetCities() 
        {
            return Ok(CitiesDataStore.Current.Cities);
        }

        [HttpGet("{id}")]
        public IActionResult GetCity(int id) 
        {
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(c => c.Id == id);

            if(cityToReturn == null){
                return NotFound();
            }

            return Ok(cityToReturn);
        }

    }
}