﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Garage2._0MVC.Data;
using jsreport.AspNetCore;
using jsreport.Local;
using jsreport.Binary;
using Garage2._0MVC.Services;


namespace Garage2._0MVC
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
            services.AddControllersWithViews();

            // New NuGet
            services.AddMvc();
            services.AddJsReport(new LocalReporting()
            .UseBinary(JsReportBinary.GetBinary())
            .AsUtility()
            .Create());

            services.AddScoped<ITypeSelectService, TypeSelectService>();
            services.AddScoped<IMemberSelectService, MemberSelectService>();
            services.AddScoped<IParkingService, ParkingService>();
            services.AddTransient<IParkingCapacityService, ParkingCapacityService>();
            services.AddTransient<IFilterService, FilterService>();

            services.AddDbContext<Garage2_0MVCContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("Garage2_0MVCContext")).EnableSensitiveDataLogging());
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=VehicleModels2}/{action=Index}/{id?}");
            });
            //RotativaConfiguration.Setup(env, "Rotativa");
        }
    }
}
