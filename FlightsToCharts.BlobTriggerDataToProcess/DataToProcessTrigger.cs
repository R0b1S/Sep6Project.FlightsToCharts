using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FlightsToCharts.Data;
using FlightsToCharts.SharedLibrary.Helpers;
using FlightsToCharts.SharedLibrary.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SharedLibrary.Models;
using System.Linq;
using FlightsToCharts.SharedLibrary.Utils;

namespace FlightsToCharts.BlobTriggerDataToProcess
{
   public static class DataToProcessTrigger
   {
      [FunctionName("DataToProcessTrigger")]
      public static async Task Run([BlobTrigger("data-to-process/{name}", Connection = "AzureWebJobsStorage")] Stream myBlob, string name, ILogger log)
      {
         try
         {
            var connString = System.Environment.GetEnvironmentVariable("Sep6DbConnString");
            using (var db = new SepDbContext(connString))
            {
               var csv = new CsvConverter();
               var tableName = "";
               IEnumerable<object> data;

               switch (name)
               {
                  case "airports.csv":
                     tableName = "Airports";
                     data = csv.GetAirportsData(myBlob);
                     break;
                  case "airlines.csv":
                     tableName = "Airlines";
                     data = csv.GetAirlinesData(myBlob);
                     break;
                  case "flights.csv":
                     tableName = "Flights";
                     data = csv.GetFlightsData(myBlob);
                     break;
                  case "planes.csv":
                     tableName = "Planes";
                     data = csv.GetPlanesData(myBlob);
                     break;
                  case "weather.csv":
                     tableName = "Weather";
                     data = csv.GetWeatherData(myBlob);
                     break;
                  default:
                     // if there is no name like we want exit task and log why
                     log.LogWarning($"File with name {name} is not supported");
                     return; 
               }

               // load metadata
               var metadata = await db.TablesMetadata.FindAsync(tableName);
               try
               {
                  // if metadata does not exist create one
                  if (metadata == null)
                  {
                     var meta = new TableMetadata
                     {
                        Table = tableName,
                        UploadDate = DateTime.Now,
                        EditDate = DateTime.Now,
                        DataCount = data.Count(),
                        DataMaxDate = MetadataUtils.GetMaxDate(data, tableName),
                        DataMinDate = MetadataUtils.GetMinDate(data, tableName),
                        DataProcessed = false
                     };

                     await db.TablesMetadata.AddAsync(meta);
                     await db.SaveChangesAsync();

                     // read new metadata
                     metadata = await db.TablesMetadata.FindAsync(tableName);
                  }
                  else
                  {
                     metadata.EditDate = DateTime.Now;
                     metadata.DataCount = data.Count();
                     metadata.DataMaxDate = MetadataUtils.GetMaxDate(data, tableName);
                     metadata.DataMinDate = MetadataUtils.GetMinDate(data, tableName);
                     metadata.DataProcessed = false;
                     await db.SaveChangesAsync();
                  }

                  // I`d like to change it to just adding distinct data
                  // but it would probably take waaay to much time to process that
                  // delete table before adding data
                  await db.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE [{tableName}]");

                  // I tried to do it by generic but it is either way to hard or I am just stupid
                  // Anyway it took way too much time and time is precious
                  if (tableName == "Airports")
                     await db.Airports.AddRangeAsync(data.Cast<Airport>());
                  else if (tableName == "Airlines")
                     await db.Airlines.AddRangeAsync(data.Cast<Airline>());
                  else if (tableName == "Planes")
                     await db.Planes.AddRangeAsync(data.Cast<Plane>());
                  else if (tableName == "Flights")
                     await db.Flights.AddRangeAsync(data.Cast<Flight>());
                  else if (tableName == "Weather")
                     await db.Weather.AddRangeAsync(data.Cast<Weather>());

                  // data are processed
                  metadata.DataProcessed = true;
                  // save all changes
                  await db.SaveChangesAsync();
               }
               catch (Exception e)
               {
                  if (metadata != null)
                  {
                     // set data processed to true if something wrong happen
                     metadata.DataProcessed = true;
                     await db.SaveChangesAsync();
                  }
                  log.LogError($"Error while saving [{tableName}] data to database ->" + e.Message);
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


