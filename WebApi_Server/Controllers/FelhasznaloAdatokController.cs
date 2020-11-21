using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_Common.Models;
using WebApi_Server.Repositories;

namespace WebApi_Server.Controllers
{
    [Route("api/konyvtar/adatok")]
    [ApiController]
    public class FelhasznaloAdatokController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<FelhasznaloAdatok>> Get()
        {
            var fAdatok = FelhasznaloAdatokRepository.GetData();
            return Ok(fAdatok);
        }

        [HttpGet("{neptunKod}")]
        public ActionResult<FelhasznaloAdatok> Get(string neptunKod)
        {
            var fAdatok = FelhasznaloAdatokRepository.GetData();

            var fAdat = fAdatok.FirstOrDefault(x => x.neptunKod == neptunKod);
            if (fAdat != null)
            {
                return Ok(fAdat);
            }

            return NotFound();
        }

        [HttpPost]
        public ActionResult Post([FromBody] FelhasznaloAdatok fAdat)
        {
            var fAdatok = FelhasznaloAdatokRepository.GetData().ToList();

            fAdatok.Add(fAdat);

            FelhasznaloAdatokRepository.StoreData(fAdatok);
            return Ok();
        }
        
    }
}
