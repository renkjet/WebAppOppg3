using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAppOppg3.Models;

namespace WebAppOppg3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpmController : Controller
    {
        private readonly SpmContext _context;

        public SpmController(SpmContext context)
        {
            _context = context;
        }

        // GET api/Spm
        [HttpGet]
        public JsonResult Get()
        {
            var db = new DBs233765(_context);
            List<Spm> alleSpm = db.hentAlleSpm();
            return Json(alleSpm);
        }

        // GET api/Spm/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var db = new DBs233765(_context);
            Spm spm = db.hentEtSpm(id);
            return Json(spm);
        }

        // POST api/Spm
        [HttpPost]
        public JsonResult Post([FromBody] InnsendtSpmDomene innSpm)
        {
            if (ModelState.IsValid)
            {
                var db = new DBs233765(_context);
                bool OK = db.lagreInnsendtSpm(innSpm);
                if (OK)
                {
                    return Json("OK");
                }
            }
            return Json("Kunne ikke sette inn kunden i DB");
        }

        // PUT api/Spm/1
        [HttpPut("{id}")]
        public JsonResult Put (int id, [FromBody] Spm innSpm)
        {
            if (ModelState.IsValid)
            {
                var db = new DBs233765(_context);
                bool OK = db.endreSpm(id, innSpm);
                if (OK)
                {
                    return Json("Spørsmålet er endret");
                }
            }
            return Json("Kunne ikke endre spørsmålet i DB");
        }
    }
}