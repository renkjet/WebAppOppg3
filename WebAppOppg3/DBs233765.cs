using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppOppg3.Models;



namespace WebAppOppg3
{
    public class DBs233765
    {
        private readonly SpmContext _context;
        public DBs233765(SpmContext context)
        {
            _context = context;
        }

        public List<Spm> hentAlleSpm()
        {
            List<Spm> alleSpm = _context.Spm.Select(s => new Spm()
            {
                Id = s.Id,
                Kategori = s.Kategori,
                Sporsmal = s.Sporsmal,
                Svar = s.Svar,
                TommelOpp = s.TommelOpp,
                TommelNed = s.TommelNed
            }).ToList();
            return alleSpm;
        }


        // For å lagre øking i "upvotes" og "downvotes" til spørsmål
        public bool endreSpm(int Id, Spm innSpm)
        {
            // Finner korrekt spm
            Spm funnetSpm = _context.Spm.FirstOrDefault(s => s.Id == Id);
            if (funnetSpm == null)
            {
                return false;
            }

            funnetSpm.TommelOpp = innSpm.TommelOpp;
            funnetSpm.TommelNed = innSpm.TommelNed;

            try
            {
                _context.SaveChanges();
            }
            catch(Exception feil)
            {
                return false;
            }
            return true;
        }

        public bool lagreInnsendtSpm(InnsendtSpmDomene innSpm)
        {
            var nyttSpm = new InnsendtSpm
            {
                Id = innSpm.Id,
                Navn = innSpm.Navn,
                Sporsmal = innSpm.spm,
                Epost = innSpm.Epost
            };

            try
            {
                // lagre spm
                _context.InnsendtSpms.Add(nyttSpm);
                _context.SaveChanges();
            }
            catch (Exception feil)
            {
                return false;
            }
            return true;
        }
    }
}
