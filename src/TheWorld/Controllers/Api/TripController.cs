using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    public class TripController :Controller
    {
        private IWorldRepository _repository;
        public TripController (IWorldRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("api/trips")]
        public JsonResult Get()
        {
            var results = _repository.GetAllTripsWithStops();
            return Json(results);
            //return Json(new { name= "Seifu"} );
        }
    }
}