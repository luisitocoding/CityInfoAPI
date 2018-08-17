using CityInfoAPI.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfoAPI.Controllers
{
    public class DummyController : Controller
    {
        private CityInfoDbContext _ctx;

        public DummyController(CityInfoDbContext ctx)
        {
            _ctx = ctx;
        }

        [HttpGet]
        [Route("api/testdatabase")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
