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
                Sporsmal = "Hvordan kjøper jeg billett?",
                Svar = "På vy.no får du kjøpt de fleste billetter ved å bruke reiseplanleggeren. " +
                "Reiser du på Østlandet og i Hordaland, kan du på visse strekninger kjøpe 7 og 30 " +
                "dagers elektronisk periodebillett på vy.no. ",
                Kategori = "Billetter",
                TommelOpp = 0,
                TommelNed = 0
            },

            new Spm
            {
                Sporsmal = "Hvordan kjøper jeg periodebillett?",
                Svar = "Det enkleste for deg er å kjøpe periodebilletten i appen. " +
                "Vi tilbyr også periodebillett for Ruter, Kolumbus og Skyss. Enkelte varianter " +
                "selger vi kun i appen.",
                Kategori = "Billetter",
                TommelOpp = 0,
                TommelNed = 0
            },

            new Spm
            {
                Sporsmal = "Hvor mye baggasje kan jeg ta med?",
                Svar = "Du kan ta med deg inntil 30 kilo fordelt på maksimum 3 kolli. " +
                "Har du mer enn dette og skal reise mellom Oslo og Voss/Bergen eller Trondheim, " +
                "kan du benytte bagasjetransport. ",
                Kategori = "Bagasje",
                TommelNed = 0,
                TommelOpp = 0
            },

            new Spm
            {
                Sporsmal = "Jeg har glemt noe ombord. Hvor kan jeg henvende meg for å få hjelp?",
                Svar = "Hittegodskontoret på Olso s oppbevarer gjenglemt bagasje for lokaltog Østlandet, " +
                "Vestfoldbanen, Østfoldbanen og regiontog som stopper i Oslo. Oslokontoret håndterer " +
                "Rørosbanen for tog som har endestasjon Røros og Hamar. Du finner hittegodskontoret ved " +
                "oppbevaringsboksene på Oslo S.",
                Kategori = "Bagasje",
                TommelOpp = 0,
                TommelNed = 0
            },

            new Spm
            {
                Sporsmal = "Hvordan bestiller jeg billett hvis jeg har spesialbehov for reisenn?",
                Svar = "Har du spesielle behov for togreisen, er det viktig at du bestiller billetten gjennom " +
                "kundeservice eller på en betjent stasjon. Tjenester som rullestolplass og gratis plass for " +
                "førerhund/servicehund kan i dag ikke bestilles på nettet, men funksjonshemmede får likevel " +
                "tilbud om de ulike rabattene.",
                Kategori = "Spesialbehov",
                TommelOpp = 0,
                TommelNed = 0
            },

            new Spm
            {
                Sporsmal = "Kan jeg få assistanse på stasjonen?",
                Svar = "Har du nedsatt funksjonsevne og trenger " +
                "assistanse på stasjonen for å komme til eller fra toget, " +
                "kan du få hjelp. Assistanse bestilles hos Bane NOR. For å være garantert å " +
                "få tjenesten til riktig tidspunkt må du bestille den minst 24 timer på forhånd. " +
                "Tjenesten er gratis. ",
                Kategori = "Spesialbehov",
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
