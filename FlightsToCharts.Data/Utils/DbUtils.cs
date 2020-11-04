using Azure.Storage.Blobs.Models;
using FlightsToCharts.SharedLibrary.Domains;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightsToCharts.Data.Utils
{
   public static class DbUtils
   {
      public static async Task GetTableDataCount(SepDbContext context, List<BlobMetadataExtended> blobsMetadata)
      {
         foreach (var blob in blobsMetadata)
         {
            switch (blob.Name)
            {
               case "airlines.csv":
                  blob.Count = await context.Airlines.CountAsync();
                  break;
               case "airports.csv":
                  blob.Count = await context.Airports.CountAsync();
                  break;
               case "flights.csv":
                  blob.Count = await context.Flights.CountAsync();
                  break;
               case "planes.csv":
                  blob.Count = await context.Planes.CountAsync();
                  break;
               case "weather.csv":
                  blob.Count = await context.Weather.CountAsync();
                  break;
               default:
                  break;
            }
         }
      }
   }
}
