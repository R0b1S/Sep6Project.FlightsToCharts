using FlightsToCharts.SharedLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SharedLibrary.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FlightsToCharts.Data
{
   public class SepDbContext : DbContext
   {
      private string _connString;

      public SepDbContext(DbContextOptions<SepDbContext> options) : base(options) { }
      public SepDbContext(String connString) : base()
      {
         _connString = connString;
      }

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
         // FLIGHTS
         modelBuilder.HasSequence<int>("FlightSeq").StartsAt(1).IncrementsBy(1); ;

         modelBuilder.Entity<Flight>()
             .Property(o => o.Id)
             .HasDefaultValueSql("NEXT VALUE FOR FlightSeq");

         // WEATHER
         modelBuilder.HasSequence<int>("WeatherSeq").StartsAt(1).IncrementsBy(1); ;

         modelBuilder.Entity<Weather>()
             .Property(o => o.Id)
             .HasDefaultValueSql("NEXT VALUE FOR WeatherSeq");

         // TABLE METADATA
         modelBuilder.Entity<TableMetadata>()
            .Property(o => o.DataProcessed)
            .HasDefaultValue(true);

         // VIEWS
         modelBuilder.Entity<FlightsView.FlightsPerMonth>()
            .HasNoKey()
            .ToView("FlightsPerMonth_View");

         modelBuilder.Entity<FlightsView.FlightsPerMonthOrigin>()
            .HasNoKey()
            .ToView("FlightsPerMonthFromOrigin_View");

         modelBuilder.Entity<FlightsView.MeanAirtimeOrigin>()
            .HasNoKey()
            .ToView("MeanAirTimePerOrigin_View");

         modelBuilder.Entity<FlightsView.TopDestination>()
            .HasNoKey()
            .ToView("FlightsToDestination_View");

         modelBuilder.Entity<FlightsView.TopDestinationOrigin>()
            .HasNoKey()
            .ToView("FlightsToDestinationPerOrigin_View");

         modelBuilder.Entity<WeatherView.NumberOfWeatherObservation>()
            .HasNoKey()
            .ToView("NumberOfWeatherObservationPerOrigin_View");

         modelBuilder.Entity<WeatherView.MeanWeatherByOrigin>()
            .HasNoKey()
            .ToView("MeanTempByOrigins_View");

         modelBuilder.Entity<WeatherView.DailyMeanWeatherValues>()
            .HasNoKey()
            .ToView("DailyMeanTempForOrigins_View");

         modelBuilder.Entity<PlaneView.FlightsPerManufacturer>()
            .HasNoKey()
            .ToView("ManufacturerFlightsNumber_View");

         modelBuilder.Entity<PlaneView.PlanesPerManufacturer>()
            .HasNoKey()
            .ToView("ManufacturerPlanesNumber_View");

         modelBuilder.Entity<PlaneView.PlanesPerAirbusModel>()
            .HasNoKey()
            .ToView("PlanesNumberPerAirbusModel_View");

         modelBuilder.Entity<FlightsView.MeanArrDepDelay>()
            .HasNoKey()
            .ToView("MeanDepAndArrDelayPerOrigin_View");


      }

      public DbSet<Airline> Airlines { get; set; }
      public DbSet<Airport> Airports { get; set; }
      public DbSet<Flight> Flights { get; set; }
      public DbSet<Plane> Planes { get; set; }
      public DbSet<Weather> Weather { get; set; }
      public DbSet<TableMetadata> TablesMetadata { get; set; }
      public DbSet<FlightsView.FlightsPerMonth> FlightsPerMonths { get; set; }
      public DbSet<FlightsView.FlightsPerMonthOrigin> FlightsPerMonthsOrigins { get; set; }
      public DbSet<FlightsView.MeanAirtimeOrigin> MeanAirtimeOrigins { get; set; }
      public DbSet<FlightsView.TopDestination> TopDestinations { get; set; }
      public DbSet<FlightsView.TopDestinationOrigin> TopDestinationOrigins { get; set; }
      public DbSet<WeatherView.NumberOfWeatherObservation> NumberOfWeatherObservations { get; set; }
      public DbSet<WeatherView.DailyMeanWeatherValues> DailyMeanWeatherValues { get; set; }
      public DbSet<WeatherView.MeanWeatherByOrigin> MeanWeatherByOrigin { get; set; }
      public DbSet<PlaneView.FlightsPerManufacturer> FlightsPerManufacturers { get; set; }
      public DbSet<PlaneView.PlanesPerAirbusModel> PlanesPerAirbusModels { get; set; }
      public DbSet<PlaneView.PlanesPerManufacturer> PlanesPerManufacturers { get; set; }
      public DbSet<FlightsView.MeanArrDepDelay> MeanArrDepDelays { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
            optionsBuilder.UseSqlServer(_connString);
         }
      }
   }
}
