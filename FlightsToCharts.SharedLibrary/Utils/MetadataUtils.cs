using SharedLibrary.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlightsToCharts.SharedLibrary.Utils
{
   public static class MetadataUtils
   {
      public static DateTime? GetMinDate(IEnumerable data, string table)
      {
         int year, month, day;
         DateTime? min;

         switch (table)
         {
            case "Flights":
               var flights = data.Cast<Flight>();

               // TODO change ?? 0 
               year = flights.Select(x => x.Year).Min() ?? 0;
               month = flights.Where(y => y.Year == year).Select(x => x.Month).Min() ?? 0;
               day = flights.Where(y => y.Year == year && y.Month == month).Select(x => x.Day).Min() ?? 0;

               min = new DateTime(year, month, day);
               return min;

            case "Weather":
               var weather = data.Cast<Weather>();

               year = weather.Select(x => x.Year).Min() ?? 0;
               month = weather.Where(y => y.Year == year).Select(x => x.Month).Min() ?? 0;
               day = weather.Where(y => y.Year == year && y.Month == month).Select(x => x.Day).Min() ?? 0;

               min = new DateTime(year, month, day);
               return min;

            default:
               return null;
         }
      }

      public static DateTime? GetMaxDate(IEnumerable data, string table)
      {
         int year, month, day;
         DateTime? max;

         switch (table)
         {
            case "Flights":
               var flights = data.Cast<Flight>();

               year = flights.Select(x => x.Year).Max() ?? 0;
               month = flights.Where(y => y.Year == year).Select(x => x.Month).Max() ?? 0;
               day = flights.Where(y => y.Year == year && y.Month == month).Select(x => x.Day).Max() ?? 0;

               max = new DateTime(year, month, day);
               return max;

            case "Weather":
               var weather = data.Cast<Weather>();

               year = weather.Select(x => x.Year).Max() ?? 0;
               month = weather.Where(y => y.Year == year).Select(x => x.Month).Max() ?? 0;
               day = weather.Where(y => y.Year == year && y.Month == month).Select(x => x.Day).Max() ?? 0;

               max = new DateTime(year, month, day);
               return max;

            default:
               return null;
         }
      }
   }
}
