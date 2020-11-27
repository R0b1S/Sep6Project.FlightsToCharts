using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SharedLibrary.Models
{
   public class Airport
   {
      [Key]
      [Name("faa")]
      [StringLength(10)]
      public string FAA { get; set; }

      [Name("name")]
      [StringLength(200)]
      public string Name { get; set; }

      [Name("lon")]
      [NullValuesAttribute("NA")]
      public double? Lon { get; set; }

      [Name("lat")]
      [NullValuesAttribute("NA")]
      public double? Lat { get; set; }

      [Name("alt")]
      [NullValuesAttribute("NA")]
      public double? Alt { get; set; }

      [Name("tz")]
      [NullValuesAttribute("NA")]
      public double? Tz { get; set; }

      [Name("dst")]
      [StringLength(5)]
      public string Dst { get; set; }

      [Name("tzone")]
      [StringLength(200)]
      public string Tzone { get; set; }
   }
}
