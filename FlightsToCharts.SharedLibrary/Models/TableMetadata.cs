using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FlightsToCharts.SharedLibrary.Models
{
   public class TableMetadata
   {
      [Key]
      [StringLength(50)]
      [Required]
      public string Table { get; set; }

      public DateTime? UploadDate { get; set; }

      public DateTime? EditDate { get; set; }

      public DateTime? DataMinDate { get; set; }

      public DateTime? DataMaxDate { get; set; }

      public double? DataCount { get; set; }

      public bool? DataProcessed { get; set; }
   }
}
