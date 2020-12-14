using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsToCharts.SharedLibrary.Models
{
   public class FlightsView
   {
      public class FlightsPerMonth
      {
         public int Month { get; set; }
         public int FlightsNo { get; set; }
      }
      public class FlightsPerMonthOrigin
      {
         public int Month { get; set; }
         public int NumberOfFlights { get; set; }
         public string Origin { get; set; }
      }
      public class FlightsPerMonthOriginToChart
      {
         public string MonthName { get; set; }

         public int EWR { get; set; }
         public int JFK { get; set; }
         public int LGA { get; set; }

      }

      public class MeanAirtimeOrigin
      {
         public int AverageAirTime { get; set; }
         public string Origin { get; set; }
      }

      public class TopDestination
      {
         public int NumberOfFlights { get; set; }
         public string Destination { get; set; }
      }

      public class TopDestinationOrigin
      {
         public int NumberOfFlights { get; set; }
         public string Origin { get; set; }
         public string Destination { get; set; }
      }
      public class MeanArrDepDelay
      {
         public int DepartureDelay { get; set; }
         public string Origin { get; set; }
         public int ArrivalDelay { get; set; }
      }


   }
}
