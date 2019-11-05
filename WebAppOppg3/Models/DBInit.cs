using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppOppg3.Models
{
    public class DBInit
    {
        public static void Initialize(SpmContext context)
        {
            context.Database.EnsureCreated();

            if (context.Spm.Any())
            {
                return;
            }

            var spm = new Spm[]
            {
            new Spm
            {
                Sporsmal = "Hvor kan jeg  kjøpe billetter?",
                Svar = "Du kan kjøpe billett fra vy.no, appen, billettautomat etc.",
                Kategori = Kategori.Billetter,
                TommelOpp = 0,
                TommelNed = 0
            },

            new Spm
            {
                Sporsmal = "Hvem kan kjøpe periodebillett?",
                Svar = "Alle.",
                Kategori = Kategori.Billetter,
                TommelOpp = 0,
                TommelNed = 0
            },

            new Spm
            {
                Sporsmal = "Hvor mye baggasje kan jeg ta med?",
                Svar = "Så mye du vil.",
                Kategori = Kategori.Bagasje,
                TommelNed = 0,
                TommelOpp = 0
            },

            new Spm
            {
                Sporsmal = "Jeg har glemt noe ombord. Hvor kan jeg henvende meg for å få hjelp?",
                Svar = "Vi har hittegodtkontor på Oslo S. Ring oss eller send mail til hittegods.",
                Kategori = Kategori.Bagasje,
                TommelOpp = 0,
                TommelNed = 0
            }

       };

            foreach (Spm s in spm)
            {
                context.Spm.Add(s);

            }
            context.SaveChanges();
        }

       
    }
}
