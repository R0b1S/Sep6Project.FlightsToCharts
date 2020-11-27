using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightsToCharts.Data;
using FlightsToCharts.Data.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FlightsToCharts.Core
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.AddRazorPages();
         services.AddSignalR();
         services.AddEntityFrameworkSqlServer().AddDbContext<SepDbContext>(options =>
             options.UseSqlServer(Configuration.GetConnectionString("SepAirlineDbConnectionString") ?? Configuration.GetConnectionString("Sep6DbConnString"))
         );
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }

         app.UseHttpsRedirection();
         app.UseStaticFiles();

         app.UseRouting();

         app.UseAuthorization();


         app.UseEndpoints(endpoints =>
         {
            endpoints.MapRazorPages();
            endpoints.MapHub<AirlinesHub>("/airlinesHub");
            endpoints.MapHub<AirportsHub>("/airportsHub");
            endpoints.MapHub<FlightsHub>("/flightsHub");
            endpoints.MapHub<PlanesHub>("/planesHub");
            endpoints.MapHub<WeatherHub>("/weatherHub");
            endpoints.MapHub<SharedHub>("/sharedHub");
         });
      }
   }
}
