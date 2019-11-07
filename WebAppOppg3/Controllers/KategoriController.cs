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

        // Henter spørsmål basert på kategori
        public List<Spm> hentSpmTilKategori(String kategori)
        {
            var db = new DB2(_context);
            List<Spm> alleSpm = db.hentAlleSpm();
            List<Spm> spmTilKategori = new List<Spm>();
            foreach (Spm spm in alleSpm)
            {
                if (spm.Kategori == kategori)
                {
                    spmTilKategori.Add(spm);
                }
            }
            return spmTilKategori;
        }


    }
}