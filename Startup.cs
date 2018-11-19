﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using hello_world_web.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using hello_world_web.Entities;
using Microsoft.EntityFrameworkCore;

using MySql.Data.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;

namespace hello_world_web
{
    public class Startup
    {
        public static IConfiguration Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }

        // public Startup(IConfiguration configuration)
        // {
        //     Configuration = configuration;
        // }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()));

            services.AddTransient<IMailService, LocalMailService>();
            //services.AddTransient<IMailService, CloudMailService>();

            //var connectionStringLocalDbSqlServer = "Server=(localdb)\\mssqllocaldb;Database=curso_asp.netcoreTESTANDO;Trusted_Connection=True;MultipleActiveResultSets=true";
            var connectionStringLocalDbMysqlServer = "Data Source=Localhost;Database=curso_asp.netcore;User Id=root;Password=root";


            //services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionStringLocalDbMysqlServer));
            services.AddDbContext<CityInfoContext>(o => o.UseMySQL(connectionStringLocalDbMysqlServer));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            //loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStatusCodePages();

            app.UseMvc();

            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });
        }
    }
}
