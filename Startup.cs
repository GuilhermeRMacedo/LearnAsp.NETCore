using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Logging;
using hello_world_web.Services;
using Microsoft.Extensions.Configuration;
using hello_world_web.Entities;
using Microsoft.EntityFrameworkCore;

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

            //var connectionStringLocalDbMysqlServer = Startup.Configuration["ConnectionStrings:connectionStringLocalDbSqlServer"];
            var connectionStringLocalDbMysqlServer = Startup.Configuration["ConnectionStrings:connectionStringLocalDbMysqlServer"];


            //services.AddDbContext<CityInfoContext>(o => o.UseSqlServer(connectionStringLocalDbMysqlServer));
            services.AddDbContext<CityInfoContext>(o => o.UseMySQL(connectionStringLocalDbMysqlServer));

            //create one per request(scoped)
            services.AddScoped<ICityInfoRepository, CityInfoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
        ILoggerFactory loggerFactory, CityInfoContext cityInfoContext)
        {

            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            //loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            
            //fullfill 
            cityInfoContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg => 
            {
                //Entity, DTO. Property from DTO must be the same name of Entity to automapper works
                cfg.CreateMap<Entities.City, models.CityWithoutPointsOfInterestDto>();
                cfg.CreateMap<Entities.City, models.CityDto>();
                //cfg.CreateMap<Entities.PointOfInterest, models.PointsOfInterestDto>();    
            });

            app.UseMvc();

            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("Hello World!");
            // });
        }
    }
}
