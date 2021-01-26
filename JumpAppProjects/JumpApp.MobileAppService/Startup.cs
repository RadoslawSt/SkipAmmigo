﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.EntityFrameworkCore;
using JumpApp.MobileAppService.Extensions;
using System.IO;

namespace JumpApp.MobileAppService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureIISIntegration();
            
            services.ConfigureMySqlContext(Configuration);
            services.ConfigureRepositoryWrapper();
            services.ConfigureSwaggerGen();
            services.AddMvc();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            //});


            // services.AddSingleton<IItemRepository, ItemRepository>();
            //services.AddDbContext<MobileServiceContext>(options =>
            //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        //public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            {
                //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
                //loggerFactory.AddDebug();
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                //app.Use(async (context, next) =>
                //{
                //    await next();

                //    if (context.Response.StatusCode == 404
                //        && !Path.HasExtension(context.Request.Path.Value))
                //    {
                //        context.Request.Path = "/swagger/index.html";
                //        await next();
                //    }
                //});
                app.UseMvc();

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });

                app.Run(async (context) => await Task.Run(() => context.Response.Redirect("/swagger")));
            }
        }
    }
}
