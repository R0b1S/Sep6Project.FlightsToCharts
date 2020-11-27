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
   public class WeatherHub : Hub
   {
      private readonly SepDbContext _context;

      public WeatherHub(SepDbContext context)
      {
         _context = context;
      }

      public async Task GetAllWeather()
      {
         try
         {
            var weather = await _context.Weather.ToArrayAsync();   //_containerData.GetAllBlobs();
            //await DbUtils.GetTableDataCount(_context, blobsMetadata);
            await Clients.All.SendAsync("SendAllWeather", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(weather), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.All.SendAsync("SendAllWeather", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
   }
}