using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models
{
    public class Plane
    {
        
        [Name("tailnum")]
        [Key]
        [StringLength(20)]
        public string Tailnum { get; set; }

        [Name("year")]
        [NullValuesAttribute("NA")]
        public int? Year { get; set; }

        [Name("type")]
        [StringLength(100)]
        public string Type { get; set; }

        [Name("manufacturer")]
        [StringLength(100)]
        public string Manufacturer { get; set; }

        [Name("model")]
        [StringLength(100)]
        public string Model { get; set; }

        [Name("engines")]
        [NullValuesAttribute("NA")]
        public int? Engines { get; set; }

        [Name("seats")]
        [NullValuesAttribute("NA")]
        public int? Seats { get; set; }

        [Name("speed")]
        [NullValuesAttribute("NA")]
        public int? Speed { get; set; }

        [Name("engine")]
        [StringLength(100)]
        public string Engine { get; set; }
    }
}
