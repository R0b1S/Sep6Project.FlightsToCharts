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
   public class PlanesHub : Hub
   {
      private readonly SepDbContext _context;

      public PlanesHub(SepDbContext context)
      {
         _context = context;
      }

      public async Task GetAllPlanes()
      {
         try
         {
            var planes = await _context.Planes.ToArrayAsync();   //_containerData.GetAllBlobs();
            //await DbUtils.GetTableDataCount(_context, blobsMetadata);
            await Clients.All.SendAsync("SendAllPlanes", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(planes), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.All.SendAsync("SendAllPlanes", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
   }
}