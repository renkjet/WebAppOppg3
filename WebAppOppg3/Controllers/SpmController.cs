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
            var db = new DB(_context);
            List<SpmDomene> alleSpm = db.hentAlleSpm();
            return Json(alleSpm);
        }

        // GET api/Spm/5
      

    }
}