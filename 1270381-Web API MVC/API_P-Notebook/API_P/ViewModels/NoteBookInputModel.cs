using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_P.ViewModels
{
    public class NoteBookInputModel
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
        public List<ConfigurationInputModel> Configurations { get; set; } = new List<ConfigurationInputModel>();
    }
}