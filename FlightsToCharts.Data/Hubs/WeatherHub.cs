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
            var weather = await _context.Weather.Select(x => new {
               x.Origin,
               x.TimeHour,
               TempCels = Math.Round(((x.Temp ?? 0) - 32) / 1.8, 2), // convert to celsius from farenheit
               DewCels = Math.Round(((x.Temp ?? 0) - 32) / 1.8, 2), // convert to celsius from farenheit
               x.Humid, // relative
               x.WindDir, // degrees
               WindGust = Math.Round(x.WindGust ?? 0, 4), // mph
               WindSpeed = Math.Round(x.WindSpeed ?? 0,4), // mph
               x.Precip, // inches
               x.Pressure, // millibars
               x.Visib, // miles
            }).ToArrayAsync();   //_containerData.GetAllBlobs();
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