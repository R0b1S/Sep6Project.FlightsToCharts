using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models
{
    public class Weather
    {
        [Ignore]
        [Key]
        public Int32 Id { get; set; }

        [Name("origin")]
        [StringLength(10)]
        public string Origin { get; set; }

        [Name("year")]
        [NullValuesAttribute("NA")]
        public int? Year { get; set; }

        [Name("month")]
        [NullValuesAttribute("NA")]
        public int? Month { get; set; }

        [Name("day")]
        [NullValuesAttribute("NA")]
        public int? Day { get; set; }

        [Name("hour")]
        [NullValuesAttribute("NA")]
        public int? Hour { get; set; }

        [Name("temp")]
        [NullValuesAttribute("NA")]
        public double? Temp { get; set; }

        [Name("dewp")]
        [NullValuesAttribute("NA")]
        public double? Dewp { get; set; }

        [Name("humid")]
        [NullValuesAttribute("NA")]
        public double? Humid { get; set; }

        [Name("wind_dir")]
        [NullValuesAttribute("NA")]
        public int? WindDir { get; set; }

        [Name("wind_speed")]
        [NullValuesAttribute("NA")]
        public double? WindSpeed { get; set; }

        [Name("wind_gust")]
        [NullValuesAttribute("NA")]
        public double? WindGust { get; set; }

        [Name("precip")]
        [NullValuesAttribute("NA")]
        public double? Precip { get; set; }

        [Name("pressure")]
        [NullValuesAttribute("NA")]
        public double? Pressure { get; set; }

        [Name("visib")]
        [NullValuesAttribute("NA")]
        public double? Visib { get; set; }

        [Name("time_hour")]
        [NullValuesAttribute("NA")]
        public DateTime? TimeHour { get; set; }
    }
}
