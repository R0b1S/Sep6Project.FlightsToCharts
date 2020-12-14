using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsToCharts.SharedLibrary.Models
{
   public class PlaneView
   {
      public class PlanesPerManufacturer
      {
         public int NumberOfPlanes { get; set; }
         public string Manufacturer { get; set; }
      }
      public class FlightsPerManufacturer
      {
         public int NumberOfFlightsResponsible { get; set; }
         public string Manufacturer { get; set; }
      }
      public class PlanesPerAirbusModel
      {
         public int NumberOfPlanes { get; set; }
         public string Model { get; set; }
      }
   }
}
