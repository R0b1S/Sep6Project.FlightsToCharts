using Azure.Storage.Blobs.Models;
using FlightsToCharts.Data.Utils;
using FlightsToCharts.SharedLibrary.Domains;
using FlightsToCharts.SharedLibrary.Utils;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FlightsToCharts.Data.Hubs
{
   public class AssignmentHub : Hub
   {
      private readonly SepDbContext _context;
      //private readonly ContainerData _containerData;

      public AssignmentHub(SepDbContext context, IConfiguration Configuration)
      {
         _context = context;
         //_containerData = new ContainerData(Configuration);
      }

      public async Task GetDropDownOptions()
      {
         try
         {
            await Clients.All.SendAsync("SendDropDownOptions", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(FlightsToCharts.SharedLibrary.Domains.AssignmentOptionList.GetOptions()), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.All.SendAsync("SendDropDownOptions", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }

      public async Task GetNoFlightsPerMonth()
      {
         try
         {
            var rawData = await _context.FlightsPerMonths.ToArrayAsync();

            var flightPerMon = rawData.Select(y => new
            {
               MonthNum = y.Month,
               MonthString = new DateTime(2020, y.Month, 1).ToString("MMM", CultureInfo.InvariantCulture),
               Count = y.FlightsNo
            })
               .OrderBy(z => z.MonthNum)
               .ToArray();

            await Clients.All.SendAsync("SendNoFlightsPerMonth", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(flightPerMon), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.All.SendAsync("SendNoFlightsPerMonth", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
      public async Task GetAllMeasuredTempAtJFK()
      {
         try
         {
            var jfkTemp = await _context.Weather.Where(x => x.Origin == "JFK").Select(y => new {
               y.TimeHour,
               TempCels = Math.Round(((y.Temp ?? 0) - 32) / 1.8, 2)
            }).ToArrayAsync();

            await Clients.All.SendAsync("SendAllMeasuredTempAtJFK", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(jfkTemp), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.All.SendAsync("SendAllMeasuredTempAtJFK", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
   }
}
