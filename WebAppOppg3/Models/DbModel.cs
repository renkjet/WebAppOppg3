using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAppOppg3.Models
{
    public class SpmContext : DbContext
    {
        public SpmContext(DbContextOptions<SpmContext> options)
            : base(options)
        { 
        }

        public DbSet<Spm> Spm { get; set; }
        public DbSet<InnsendtSpm> InnsendtSpms { get; set; }
    }

    public enum Kategori
    {
        Billetter,
        Bagasje,
        Spesialbehov,
        Rutetider
    }

    public class Spm
    {
        [Key]
        public int Id { get; set; }
        public Kategori Kategori { get; set; }
        public String Sporsmal { get; set; }
        public String Svar { get; set; }
        public int TommelOpp { get; set; }
        public int TommelNed { get; set; }

    }

    public class InnsendtSpm
    {
        [Key]
        public int Id { get; set; }
        public String Navn { get; set; }
        public String Epost { get; set; }
        public String Sporsmal { get; set; }
        
    }
}

