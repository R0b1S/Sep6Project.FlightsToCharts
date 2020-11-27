using FlightsToCharts.SharedLibrary.Domains;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightsToCharts.Data.Hubs
{
   public class FlightsHub : Hub
   {
      private readonly SepDbContext _context;

      public FlightsHub(SepDbContext context)
      {
         _context = context;
      }

      public async Task GetAllFlights()
      {
         try
         {
            var flights = await _context.Flights.Select(x => new {
               DateOfFlight = new DateTime(x.Year ?? 1, x.Month ?? 1, x.Day ?? 1),
               DepSchedDelay = $"{((x.DepTime ?? 0) / 100):00}:{((x.DepTime ?? 0) % 100):00} ({x.DepDelay ?? '-'})",
               ArrSchedDelay = $"{((x.ArrTime ?? 0) / 100):00}:{((x.ArrTime ?? 0) % 100):00} ({x.ArrDelay ?? '-'})",
               AirTime = $"{((x.AirTime ?? 0) / 100):00}:{((x.AirTime ?? 0) % 100):00}",
               x.Carrier,
               x.Tailnum,
               FlightNo = int.Parse(x.FlightNo),
               x.Origin,
               x.Dest,
               x.Distance
            }).ToArrayAsync();
            
            //_containerData.GetAllBlobs();
            //await DbUtils.GetTableDataCount(_context, blobsMetadata);
            await Clients.All.SendAsync("SendAllFlights", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(flights), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.All.SendAsync("SendAllFlights", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
   }
}