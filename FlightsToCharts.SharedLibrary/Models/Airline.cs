using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedLibrary.Models
{
    public class Airline
    {
        [Name("carrier")]
        [Key]
        [StringLength(5)]
        [Required]
        public string Carrier { get; set; }

        [Name("name")]
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
    }
}
