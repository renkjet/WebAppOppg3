using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppOppg3.Models;



namespace WebAppOppg3
{
    public class DB
    {
        private readonly SpmContext _context;
        public DB(SpmContext context)
        {
            _context = context;
        }

        // Henter alle spørsmål
        public List<SpmDomene> hentAlleSpm()
        {
            // merk, har oppdatert med Include for å laste uten lazy loading
            List<SpmDomene> alleSpm = _context.Spm.Select(s => new SpmDomene()
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


        public SpmDomene hentEtSpm(int id)
        {
            Spm dbSpm = _context.Spm.FirstOrDefault(s => s.Id == id);

            var etSpm = new SpmDomene()
            {
                Id = dbSpm.Id,
                Sporsmal = dbSpm.Sporsmal,
                Svar = dbSpm.Svar,
                Kategori = dbSpm.Kategori,
                TommelOpp = dbSpm.TommelOpp,
                TommelNed = dbSpm.TommelNed
            };
            return etSpm;
        }

        // Henter spørsmål basert på kategori
        public List<SpmDomene> hentSpmTilKategori(Kategori kategori)
        {
            List<SpmDomene> alleSpm = hentAlleSpm();
            List<SpmDomene> spmTilKategori = new List<SpmDomene>();
            foreach(SpmDomene spm in alleSpm)
            {
                if(spm.Kategori == kategori)
                {
                    spmTilKategori.Add(spm);
                }
            }
            return spmTilKategori;
        }

        // For å lagre øking i "upvotes" og "downvotes" til spørsmål
        public bool endreSpm(int Id, SpmDomene innSpm)
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

        // Lagrer innsendt spm til database
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

        // Øker "downvote" på spørsmål
        public bool OkTommelNed(int Id, Spm innSpm)
        {
            // Finner korrekt spm
            Spm funnetSpm = _context.Spm.FirstOrDefault(s => s.Id == Id);
            if (funnetSpm == null)
            {
                return false;
            }
            // øker spørsmålet sin tommelOpp
            funnetSpm.TommelNed = innSpm.TommelNed++;
            try
            {
                // lagre spm
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
