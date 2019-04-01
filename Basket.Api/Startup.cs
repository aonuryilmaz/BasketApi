using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Basket.Api.Config;
using Basket.Api.Middleware;
using Basket.Api.Models;
using Basket.Api.Services;
using Basket.Core.Domain.Repositories;
using Basket.Core.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Swagger;

namespace Basket.Api
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ServerSettings>(Configuration);
            ConfigurationMongoDb(services);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API" ,Version = "V1"});
            });
            services.AddScoped<IUserRepository,UserRepository>();
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped<IBasketRepository,BasketRepository>();
            services.AddScoped<IBasketService,BasketService>();
            services.AddScoped<IProductService,ProductService>();
        }

        private void ConfigurationMongoDb(IServiceCollection service)
        {
            service.AddSingleton<MongoClient>(c=>
            {
                var options = c.GetRequiredService<IOptions<ServerSettings>>().Value;
                return new MongoClient(options.MongoDB.ConnectionString);
            });
            service.AddScoped<IMongoDatabase>(c =>
            {
                var options = c.GetService<IOptions<ServerSettings>>().Value;
                var client = c.GetService<MongoClient>();
                return client.GetDatabase(options.MongoDB.Database);
            });
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
                
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.ConfigureExceptionHandler();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}