using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace API_P.Models
{
    public class NoteBook
    {
        public int NoteBookId { get; set; }
        [Required, StringLength(50)]
        public string NoteBookModel { get; set; }
        [Required, Column(TypeName = "date")]
        public DateTime ManufactureDate { get; set; } = DateTime.Today;
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        public bool Instock { get; set; }
        [Required, StringLength(30)]
        public string Picture { get; set; }
        public ICollection<Configuration> Configurations { get; set; } = new List<Configuration>();
    }
    public class Configuration
    {
        public int ConfigurationId { get; set; }
        [Required, StringLength(30)]
        public string ConfigurationDetails { get; set; }
        [Required, StringLength(50)]
        public string BrandCode { get; set; }
        [Required, ForeignKey("NoteBook")]
        public int NoteBookId { get; set; }
        public NoteBook NoteBook { get; set; }
    }
    public class NoteBookDbContext : DbContext
    {
        public NoteBookDbContext()
        {
            Database.SetInitializer(new DbInitiatializer());
        }
        public DbSet<NoteBook> NoteBooks { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
    }
    public class DbInitiatializer : DropCreateDatabaseIfModelChanges<NoteBookDbContext>
    {
        protected override void Seed(NoteBookDbContext context)
        {
            NoteBook d = new NoteBook { NoteBookModel = "HP 1500ps", ManufactureDate = new DateTime(2923, 2, 1), Instock = true, Picture = "1.jpg", Price = 120000 };
            d.Configurations.Add(new Configuration { ConfigurationDetails = "RAM 12GB", BrandCode = "76" });
            context.NoteBooks.Add(d);
            context.SaveChanges();
        }
    }
}