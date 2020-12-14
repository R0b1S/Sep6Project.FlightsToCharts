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
using static FlightsToCharts.SharedLibrary.Models.WeatherView;

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
            await Clients.Caller.SendAsync("SendDropDownOptions", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(FlightsToCharts.SharedLibrary.Domains.AssignmentOptionList.GetOptions()), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendDropDownOptions", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
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

            await Clients.Caller.SendAsync("SendNoFlightsPerMonth", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(flightPerMon), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendNoFlightsPerMonth", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
      public async Task GetNoFlightsPerMonthOrigin()
      {
         try
         {
            var rawData = await _context.FlightsPerMonthsOrigins.OrderBy(z => z.Month).ToArrayAsync();

            var flightPerMon = new List<SharedLibrary.Models.FlightsView.FlightsPerMonthOriginToChart>();

            for(int i = 1; i <= 12; i++)
            {
               var temp = rawData.Where(x => x.Month == i).ToArray();

               flightPerMon.Add(new SharedLibrary.Models.FlightsView.FlightsPerMonthOriginToChart
               {
                  MonthName = new DateTime(2020, i, 1).ToString("MMM", CultureInfo.InvariantCulture),
                  EWR = temp.Where(y => y.Origin == "EWR").Select(z => z.NumberOfFlights).FirstOrDefault(),
                  JFK = temp.Where(y => y.Origin == "JFK").Select(z => z.NumberOfFlights).FirstOrDefault(),
                  LGA = temp.Where(y => y.Origin == "LGA").Select(z => z.NumberOfFlights).FirstOrDefault()
               }) ;
            }
            await Clients.Caller.SendAsync("SendNoFlightsPerMonthOrigin", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(flightPerMon), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendNoFlightsPerMonthOrigin", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
      public async Task GetTopTenDest()
      {
         try
         {
            var topTen = await _context.TopDestinations.OrderByDescending(x => x.NumberOfFlights).Take(10).ToArrayAsync();

            await Clients.Caller.SendAsync("SendTopTenDest", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(topTen), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendTopTenDest", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
      public async Task GetTopTenDestOrigin()
      {
         try
         {
            var topTen = await _context.TopDestinations.OrderByDescending(x => x.NumberOfFlights).Take(10).ToArrayAsync();
            var topTenOrigin = await _context.TopDestinationOrigins.ToArrayAsync();

            var results = new List<SharedLibrary.Models.FlightsView.FlightsPerMonthOriginToChart>();

            foreach (var top in topTen)
            {
               results.Add(new SharedLibrary.Models.FlightsView.FlightsPerMonthOriginToChart
               {
                  MonthName = top.Destination, // we reuse this, w/e
                  EWR = topTenOrigin.Where(y => y.Origin == "EWR" && y.Destination == top.Destination).Select(z => z.NumberOfFlights).FirstOrDefault(),
                  JFK = topTenOrigin.Where(y => y.Origin == "JFK" && y.Destination == top.Destination).Select(z => z.NumberOfFlights).FirstOrDefault(),
                  LGA = topTenOrigin.Where(y => y.Origin == "LGA" && y.Destination == top.Destination).Select(z => z.NumberOfFlights).FirstOrDefault()
               });
            }

            await Clients.Caller.SendAsync("SendTopTenDestOrigin", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(results), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendTopTenDestOrigin", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
      public async Task GetMeanAirtime()
      {
         try
         {
            var airtime = await _context.MeanAirtimeOrigins.ToArrayAsync();

            await Clients.Caller.SendAsync("SendMeanAirtime", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(airtime), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendMeanAirtime", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
      public async Task GetNumberOfWeatherObservations()
      {
         try
         {
            var obs = await _context.NumberOfWeatherObservations.ToArrayAsync();

            await Clients.Caller.SendAsync("SendNumberOfWeatherObservations", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(obs), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendNumberOfWeatherObservations", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
      public async Task GetMeanWeatherByOrigin()
      {
         try
         {
            var meanWeather = await _context.MeanWeatherByOrigin.ToArrayAsync();

            await Clients.Caller.SendAsync("SendMeanWeatherByOrigin", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(meanWeather), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendMeanWeatherByOrigin", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
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

            await Clients.Caller.SendAsync("SendAllMeasuredTempAtJFK", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(jfkTemp), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendAllMeasuredTempAtJFK", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
      public async Task GetDailyMeanTempAtJFK()
      {
         try
         {
            var jfkTemp = await _context.DailyMeanWeatherValues.Where(x => x.Origin == "JFK").Select(y => new {
               TimeHour = new DateTime(y.Year ?? 2020, y.Month ?? 1, y.Day ?? 1),
               y.Temperature
            }).ToArrayAsync();

            await Clients.Caller.SendAsync("SendDailyMeanTempAtJFK", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(jfkTemp), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendDailyMeanTempAtJFK", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
      public async Task GetDailyMeanTempAtOrigins()
      {
         try
         {
            var rawData = await _context.DailyMeanWeatherValues.ToArrayAsync();

            var meanTemp = new List<DailyMeanWeatherValuesToChart>();

            foreach(var day in rawData.Select(y => new DateTime(y.Year ?? 2020, y.Month ?? 1, y.Day ?? 1)).Distinct())
            {
               meanTemp.Add(new DailyMeanWeatherValuesToChart
               {
                  Day = day,
                  EWR = rawData.Where(x => x.Origin == "EWR" && x.Day == day.Day && x.Month == day.Month && x.Year == day.Year).Select(z => z.Temperature).FirstOrDefault(),
                  JFK = rawData.Where(x => x.Origin == "JFK" && x.Day == day.Day && x.Month == day.Month && x.Year == day.Year).Select(z => z.Temperature).FirstOrDefault(),
                  LGA = rawData.Where(x => x.Origin == "LGA" && x.Day == day.Day && x.Month == day.Month && x.Year == day.Year).Select(z => z.Temperature).FirstOrDefault()
               }) ;
            }

            await Clients.Caller.SendAsync("SendDailyMeanTempAtOrigins", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(meanTemp), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendDailyMeanTempAtOrigins", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }

      //
      public async Task GetMeanArrDepDelay()
      {
         try
         {
            var data = await _context.MeanArrDepDelays.ToArrayAsync();

            await Clients.Caller.SendAsync("SendMeanArrDepDelay", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(data), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendMeanArrDepDelay", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
      //
      public async Task GetPlanesPerManuf()
      {
         try
         {
            var data = await _context.PlanesPerManufacturers.OrderByDescending(x => x.NumberOfPlanes).Where(y => y.NumberOfPlanes > 200).ToArrayAsync();

            await Clients.Caller.SendAsync("SendPlanesPerManuf", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(data), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendPlanesPerManuf", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
      //
      public async Task GetFlightsPerManuf()
      {
         try
         {
            var manuf = await _context.PlanesPerManufacturers.OrderByDescending(x => x.NumberOfPlanes).Where(y => y.NumberOfPlanes > 200).Select(z => z.Manufacturer).ToArrayAsync();

            var data = await _context.FlightsPerManufacturers.Where(x => manuf.Contains(x.Manufacturer)).ToArrayAsync();

            await Clients.Caller.SendAsync("SendFlightsPerManuf", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(data), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendFlightsPerManuf", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
      //
      public async Task GetAirbusModels()
      {
         try
         {
            var data = await _context.PlanesPerAirbusModels.ToArrayAsync();

            await Clients.Caller.SendAsync("SendAirbusModels", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Ok, Data = JsonSerializer.Serialize(data), Message = null }));
         }
         catch (Exception e)
         {
            await Clients.Caller.SendAsync("SendAirbusModels", JsonSerializer.Serialize(new ResponseMessage { StatusCode = ResponseMessage.StatusCodeEnum.Error, Data = null, Message = $"Error: {e.Message}" }));
         }
      }
   }
}
