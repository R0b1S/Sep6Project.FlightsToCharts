using System;
using System.IO;
using System.Threading.Tasks;
using FlightsToCharts.Data;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.Helpers;

namespace FlightsToCharts.BlobTriggerDataToProcess
{
   public static class DataToProcessTrigger
   {
      [FunctionName("DataToProcessTrigger")]
      public static async Task Run([BlobTrigger("data-to-process/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name, ILogger log)
      {
         var connString = System.Environment.GetEnvironmentVariable("Sep6DbConnString");

         try
         {
            var csv = new CsvConverter();

            using (var db = new SepDbContext(connString))
            {
               if (name == "airports.csv")
               {
                  await db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Airports]");

                  var data = csv.GetAirportsData(myBlob);

                  await db.Airports.AddRangeAsync(data);
                  await db.SaveChangesAsync();
               }
               else if (name == "airlines.csv")
               {
                  await db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Airlines]");

                  var data = csv.GetAirlinesData(myBlob);

                  await db.Airlines.AddRangeAsync(data);
                  await db.SaveChangesAsync();
               }
               else if (name == "flights.csv")
               {
                  await db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Flights]");

                  var data = csv.GetFlightsData(myBlob);

                  await db.Flights.AddRangeAsync(data);
                  await db.SaveChangesAsync();
               }
               else if (name == "weather.csv")
               {
                  await db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Weather]");

                  var data = csv.GetWeatherData(myBlob);

                  await db.Weather.AddRangeAsync(data);
                  await db.SaveChangesAsync();
               }
               else if (name == "planes.csv")
               {
                  await db.Database.ExecuteSqlRawAsync("TRUNCATE TABLE [Planes]");

                  var data = csv.GetPlanesData(myBlob);

                  await db.Planes.AddRangeAsync(data);
                  await db.SaveChangesAsync();
               }
            }
         }
         catch (Exception e)
         {
            log.LogError(e.Message);
         }
      }
   }
}


