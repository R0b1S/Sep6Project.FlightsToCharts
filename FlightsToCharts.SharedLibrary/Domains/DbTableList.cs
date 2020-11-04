using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsToCharts.SharedLibrary.Domains
{
   public class DbTableList
   {
      
      public static readonly List<string> Tables = new List<string> {
         "Airplanes",
         "Airlines",
         "Flights",
         "Planes",
         "Weather",
      };
   }
}
