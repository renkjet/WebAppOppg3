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
    public class KategoriController : Controller
    {
        private readonly SpmContext _context;

        public KategoriController(SpmContext context)
        {
            _context = context;
        }

        // GET api/Kategori
        [HttpGet]
        public JsonResult Get()
        {
            var db = new DBs233765(_context);
            List<String> kategorier = new List<String>();
            List<Spm> alleSpm = db.hentAlleSpm();
            foreach(Spm s in alleSpm)
            {
                if (!kategorier.Contains(s.Kategori)){
                    kategorier.Add(s.Kategori);
                }
            }
            return Json(kategorier);
        }
    }
}