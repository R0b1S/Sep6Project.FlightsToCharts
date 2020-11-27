using FlightsToCharts.Data;
using FlightsToCharts.SharedLibrary.Domains;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightsToCharts.Data.Hubs
{
   public class AirlinesHub : Hub
   {
      private readonly SepDbContext _context;

      public AirlinesHub(SepDbContext context)
      {
         _context = context;
      }

      public async Task GetAllAirlines()
      {
         try
         {
            var airlines = await _context.Airlines.ToArrayAsync();   //_containerData.GetAllBlobs();
            //await DbUtils.GetTableDataCount(_context, blobsMetadata);
            await Clients.All.SendAsync("SendAllAirlines", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(airlines), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.All.SendAsync("SendAllAirlines", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
   }
}
