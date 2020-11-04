using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SharedLibrary.Models
{
    public class Flight
    {
        [Ignore]
        [Key]
        public Int32 Id { get; set; }

        [Name("year")]
        [NullValuesAttribute("NA")]
        public int? Year { get; set; }

        [Name("month")]
        [NullValuesAttribute("NA")]
        public int? Month { get; set; }

        [Name("day")]
        [NullValuesAttribute("NA")]
        public int? Day { get; set; }

        [Name("dep_time")]
        [NullValuesAttribute("NA")]
        public int? DepTime { get; set; }

        [Name("arr_time")]
        [NullValuesAttribute("NA")]
        public int? ArrTime { get; set; }

        [Name("dep_delay")]
        [NullValuesAttribute("NA")]
        public int? DepDelay { get; set; }

        [Name("arr_delay")]
        [NullValuesAttribute("NA")]
        public int? ArrDelay { get; set; }

        [Name("hour")]
        [NullValuesAttribute("NA")]
        public int? Hour { get; set; }

        [Name("minute")]
        [NullValuesAttribute("NA")]
        public int? Minute { get; set; }

        [Name("carrier")]
        [StringLength(200)]
        [ForeignKey("Airline")]
        public string Carrier { get; set; }

        [Name("tailnum")]
        [StringLength(20)]
        [ForeignKey("Plane")]
        public string Tailnum { get; set; }

        [Name("flight")]
        [NullValuesAttribute("NA")]
        [StringLength(20)]
        public string FlightNo { get; set; }

        [Name("origin")]
        [StringLength(5)]
        public string Origin { get; set; }

        [Name("dest")]
        [StringLength(5)]
        public string Dest { get; set; }

        [Name("air_time")]
        [NullValuesAttribute("NA")]
        public int? AirTime { get; set; }

        [Name("distance")]
        [NullValuesAttribute("NA")]
        public int? Distance { get; set; }
    }
}
