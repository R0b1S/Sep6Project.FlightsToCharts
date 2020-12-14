using System;
using System.Collections.Generic;
using System.Text;

namespace FlightsToCharts.SharedLibrary.Models
{
   public class WeatherView
   {

      public class NumberOfWeatherObservation
      {
         public int WeatherObservations { get; set; }
         public string Origin { get; set; }
      }
      public class MeanWeatherByOrigin
      {
         public string Origin { get; set; }
         public decimal Dewp { get; set; }
         public decimal Temperature { get; set; }
      }
      public class WeatherValues
      {
         public DateTime? TimeHour { get; set; }
         public string Origin { get; set; }
         public decimal TempC { get; set; }
         public decimal Dewp { get; set; }
      }
      public class DailyMeanWeatherValues
      {
         public int? Year { get; set; }
         public int? Month { get; set; }
         public int? Day { get; set; }
         public string Origin { get; set; }
         public decimal Temperature { get; set; }
         public decimal Dewp { get; set; }
      }
      public class DailyMeanWeatherValuesToChart
      {
         public DateTime Day { get; set; }
         public decimal EWR { get; set; }
         public decimal JFK { get; set; }
         public decimal LGA { get; set; }
      }

   }
}
