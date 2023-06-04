using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API_P.ViewModels
{
    public class ConfigurationInputModel
    {
        [Required, StringLength(30)]
        public string ConfigurationDetails { get; set; }
        [Required, StringLength(50)]
        public string BrandCode { get; set; }
    }
}