using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsToCharts.SharedLibrary.Domains
{
   public class BlobMetadata
   {
      public string Name { get; set; }

      public DateTime? UploadDate { get; set; }

      public DateTime? LastModified { get; set; }
   }

   public class BlobMetadataExtended
   {
      public string Name { get; set; }

      public DateTime? UploadDate { get; set; }

      public DateTime? LastModified { get; set; }

      public int Count { get; set; }
   }
}
