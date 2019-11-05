using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppOppg3.Models
{
        public class SpmDomene
        {
            public int Id { get; set; }
            public Kategori Kategori { get; set; }
            public String Sporsmal { get; set; }
            public String Svar { get; set; }
            public int TommelOpp { get; set; }
            public int TommelNed { get; set; }

        }

    public class InnsendtSpmDomene
    {
        public int Id { get; set; }
        [Required]
        [RegularExpression("^[a-zæøåA-ZÆØÅ. \\-]{2,50}$")]
        public String Navn { get; set; }
        [Required]
        [RegularExpression("^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$")]
        public String Epost { get; set; }
        [Required]
        [RegularExpression("^[a-zøæåA-ZØÆÅ.0-9!?.,;.() \\-]{2,250}$")] // Hvor mange tegn????
        public String Sporsmal { get; set; }
            
        }
    }

