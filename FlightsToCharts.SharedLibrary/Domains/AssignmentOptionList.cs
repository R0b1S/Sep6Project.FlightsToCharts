using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsToCharts.SharedLibrary.Domains
{
   public static class AssignmentOptionList
   {
      public class Option
      {
         public int ID { get; set; }
         public string Name { get; set; }
      }

      public static List<Option> Options = new List<Option> 
      {
         new Option { ID = 1, Name = "Total number of flights per month" },
         new Option { ID = 2, Name = "Total number of flights per month from the three origins in one plot (Frequency)" },
         new Option { ID = 3, Name = "Total number of flights per month from the three origins in one plot (Frequency Stacked)" },
         new Option { ID = 4, Name = "Total number of flights per month from the three origins in one plot (Stacked Percentage)" },
         new Option { ID = 5, Name = "The temperature (in Celsius) at JFK" }
      };

      public static Option[] GetOptions()
      {
         return Options.ToArray();
      }
      
   }
}
