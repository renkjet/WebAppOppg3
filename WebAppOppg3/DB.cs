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

        // Øker "upvote" på spørsmål
        public bool OkTommelOpp(int Id, Spm innSpm)
        {
            // Finner korrekt spm
            Spm funnetSpm = _context.Spm.FirstOrDefault(s => s.Id == Id);
            if(funnetSpm == null)
            {
                return false;
            }
            // øker spørsmålet sin tommelOpp
            funnetSpm.TommelOpp = innSpm.TommelOpp++;
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

        // Lagrer innsendt spm til database
        public bool sendInnSpm(InnsendtSpm innSpm)
        {
            var nyttSpm = new InnsendtSpm
            {
                Id = innSpm.Id,
                Navn = innSpm.Navn,
                Sporsmal = innSpm.Sporsmal,
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
