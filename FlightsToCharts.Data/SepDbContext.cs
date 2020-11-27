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

         //modelBuilder
         //       .Entity<Airline>(eb =>
         //       {
         //          eb.HasNoKey();
         //          eb.ToView("LoanTransactions");
         //          eb.Property(v => v.Carrier).HasColumnName("TransID");
         //       });
         // // Also add dbset with view model
      }

      public DbSet<Airline> Airlines { get; set; }
      public DbSet<Airport> Airports { get; set; }
      public DbSet<Flight> Flights { get; set; }
      public DbSet<Plane> Planes { get; set; }
      public DbSet<Weather> Weather { get; set; }
      public DbSet<TableMetadata> TablesMetadata { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         if (!optionsBuilder.IsConfigured)
         {
            optionsBuilder.UseSqlServer(_connString);
         }
      }
   }
}
