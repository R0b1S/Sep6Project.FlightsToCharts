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
         new Option { ID = 5, Name = "The Top-10 destinations" },
         new Option { ID = 6, Name = "The Top-10 Origin-Destination number of flights" },
         new Option { ID = 7, Name = "The mean airtime of each of the origin" },
         new Option { ID = 8, Name = "How many weather observation there are for the origins" },
         new Option { ID = 9, Name = "For each of the three origins, all temperature attributes (in Celsius)" },
         new Option { ID = 10, Name = "The temperature (in Celsius) at JFK" },
         new Option { ID = 11, Name = "The daily mean temperature (in Celsius) at JFK" },
         new Option { ID = 12, Name = "The daily mean temperature (in Celsius) at each origin (same graph)" },
         new Option { ID = 13, Name = "Mean departure and arrival delay for each origin" },
         new Option { ID = 14, Name = "The manufacturers that have more than 200 planes" },
         new Option { ID = 15, Name = "The number of flights each manufacturer with more than 200 planes are responsible for" },
         new Option { ID = 16, Name = "The number of planes of each Airbus Model" }
      };

      public static Option[] GetOptions()
      {
         return Options.ToArray();
      }
      
   }
}
