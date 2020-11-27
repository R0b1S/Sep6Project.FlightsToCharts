using FlightsToCharts.SharedLibrary.Domains;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightsToCharts.Data.Hubs
{
   public class AirportsHub : Hub
   {
      private readonly SepDbContext _context;

      public AirportsHub(SepDbContext context)
      {
         _context = context;
      }

      public async Task GetAllAirports()
      {
         try
         {
            var airports = await _context.Airports.ToArrayAsync();   //_containerData.GetAllBlobs();
            //await DbUtils.GetTableDataCount(_context, blobsMetadata);
            await Clients.All.SendAsync("SendAllAirports", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(airports), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.All.SendAsync("SendAllAirports", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
   }
}