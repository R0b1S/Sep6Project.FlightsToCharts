using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using SharedLibrary.Models;
using System.Threading.Tasks;
using System.Linq;

namespace FlightsToCharts.SharedLibrary.Helpers
{
   public class CsvConverter
   {
      private static BlobClientHelper blobHelper = new BlobClientHelper();

      public async Task<List<Airport>> GetAirportsData()
      {
         List<Airport> data = null;
         var airports = await blobHelper.GetBlob("airports.csv");

         if (airports.GetRawResponse().Status != 200 || airports.Value.Content == null)
            return null;

         using (var reader = new StreamReader(airports.Value.Content))
         using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
         {
            try
            {
               data = csv.GetRecords<Airport>().ToList();
            }
            catch (Exception)
            {
               // in case of failure
               // data are corrupted - bad conversion or name
            }
         }
         return data;
      }
      public List<Airport> GetAirportsData(Stream blobStream)
      {
         List<Airport> data = null;
         if (blobStream == null)
            return null;

         using (var reader = new StreamReader(blobStream))
         using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
         {
            try
            {
               data = csv.GetRecords<Airport>().ToList();
            }
            catch (Exception)
            {
               // in case of failure
               // data are corrupted - bad conversion or name
            }
         }
         return data;
      }
      
      public async Task<List<Airline>> GetAirlinesData()
      {
         List<Airline> data = null;
         var airlines = await blobHelper.GetBlob("airlines.csv");

         if (airlines.GetRawResponse().Status != 200 || airlines.Value.Content == null)
            return null;

         using (var reader = new StreamReader(airlines.Value.Content))
         using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
         {
            try
            {
               data = csv.GetRecords<Airline>().ToList();
            }
            catch (Exception)
            {
               // in case of failure
               // data are corrupted - bad conversion or name
            }
         }
         return data;
      }
      public List<Airline> GetAirlinesData(Stream blobStream)
      {
         List<Airline> data = null;

         if (blobStream == null)
            return null;

         using (var reader = new StreamReader(blobStream))
         using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
         {
            try
            {
               data = csv.GetRecords<Airline>().ToList();
            }
            catch (Exception)
            {
               // in case of failure
               // data are corrupted - bad conversion or name
            }
         }
         return data;
      }

      public async Task<List<Flight>> GetFlightsData()
      {
         List<Flight> data = null;
         var flights = await blobHelper.GetBlob("flights.csv");

         if (flights.GetRawResponse().Status != 200 || flights.Value.Content == null)
            return null;

         using (var reader = new StreamReader(flights.Value.Content))
         using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
         {
            try
            {
               data = csv.GetRecords<Flight>().ToList();
            }
            catch (Exception)
            {
               // in case of failure
               // data are corrupted - bad conversion or name
            }
         }
         return data;
      }
      public List<Flight> GetFlightsData(Stream blobStream)
      {
         List<Flight> data = null;

         if (blobStream == null)
            return null;

         using (var reader = new StreamReader(blobStream))
         using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
         {
            try
            {
               data = csv.GetRecords<Flight>().ToList();
            }
            catch (Exception e)
            {
               // in case of failure
               // data are corrupted - bad conversion or name
            }
         }
         return data;
      }

      public async Task<List<Plane>> GetPlanesData()
      {
         List<Plane> data = null;
         var planes = await blobHelper.GetBlob("planes.csv");

         if (planes.GetRawResponse().Status != 200 || planes.Value.Content == null)
            return null;

         using (var reader = new StreamReader(planes.Value.Content))
         using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
         {
            try
            {
               data = csv.GetRecords<Plane>().ToList();
            }
            catch (Exception)
            {
               // in case of failure
               // data are corrupted - bad conversion or name
            }
         }
         return data;
      }
      public List<Plane> GetPlanesData(Stream blobStream)
      {
         List<Plane> data = null;

         if (blobStream == null)
            return null;

         using (var reader = new StreamReader(blobStream))
         using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
         {
            try
            {
               data = csv.GetRecords<Plane>().ToList();
            }
            catch (Exception)
            {
               // in case of failure
               // data are corrupted - bad conversion or name
            }
         }
         return data;
      }

      public async Task<List<Weather>> GetWeatherData()
      {
         List<Weather> data = null;
         var weather = await blobHelper.GetBlob("weather.csv");

         if (weather.GetRawResponse().Status != 200 || weather.Value.Content == null)
            return null;

         using (var reader = new StreamReader(weather.Value.Content))
         using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
         {
            try
            {
               data = csv.GetRecords<Weather>().ToList();
            }
            catch (Exception)
            {
               // in case of failure
               // data are corrupted - bad conversion or name
            }
         }
         return data;
      }
      public List<Weather> GetWeatherData(Stream blobStream)
      {
         List<Weather> data = null;

         if (blobStream == null)
            return null;

         using (var reader = new StreamReader(blobStream))
         using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
         {
            try
            {
               data = csv.GetRecords<Weather>().ToList();
            }
            catch (Exception)
            {
               // in case of failure
               // data are corrupted - bad conversion or name
            }
         }
         return data;
      }
   }


}
