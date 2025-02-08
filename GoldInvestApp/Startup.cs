using GoldInvestApp.Repository;
using GoldInvestApp.Repository.Interface;
using GoldInvestApp.Services;
using GoldInvestApp.Services.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GoldInvestApp
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
            services.AddControllers();



            #region Dependnncy Injection
            //GoldInvestRepository : IGoldInvest
            services.AddScoped<IGolsInvest, GoldInvestService>();
            services.AddScoped<IGoldInvest, GoldInvestRepository>();

            #endregion



            #region Swagger

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //else
            //{
            //    app.UseExceptionHandler("/Home/Error");
            //    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //    app.UseHsts();
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();

            });

            // For Swagger Start 
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.)
            //app.UseSwaggerUI();
            app.UseSwaggerUI(c => // UseSwaggerUI Protected by if (env.IsDevelopment())
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            // For Swagger End

        }
    }
}
