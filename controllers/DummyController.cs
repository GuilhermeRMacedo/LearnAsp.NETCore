using hello_world_web.Entities;
using Microsoft.AspNetCore.Mvc;

namespace hello_world_web.controllers
{
    public class DummyController: Controller
    {
        private CityInfoContext _ctx;

        public DummyController(CityInfoContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult TesteDb() 
        {
            return Ok();
        }


    }
}