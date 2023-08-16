using TesodevAPI.Ads.Controllers;
using TesodevAPI.Ads.Services;
using Microsoft.AspNetCore.Mvc;
using TesodevAPI.Ads.Configs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore;
using TesodevAPI.Ads.Repositories;
using ZstdSharp.Unsafe;

namespace TesodevAPI.Ads
{
    public class Startup
    {
        private readonly string _environment;
        private readonly Settings _settings;

        public Startup(IWebHostEnvironment env)
        {
            Console.WriteLine($"Environment: {env.EnvironmentName}");
            var builder = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddEnvironmentVariables();
            Configuration = builder.Build();
            var envVariables = Environment.GetEnvironmentVariables();
            if (string.IsNullOrWhiteSpace(envVariables["ASPNETCORE_ENVIRONMENT"]?.ToString()))
                throw new ArgumentNullException("ASPNETCORE_ENVIRONMENT");
            _environment = envVariables["ASPNETCORE_ENVIRONMENT"].ToString();
            _settings = Configuration.GetSection("Settings").Get<Settings>();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => // Cross Origin Source
            {
                options.AddPolicy("AllowAll", builder => {
                    builder
                .WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials();
                });
            });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //services.AddSwaggerDocument();
            services.AddResponseCompression();
            services.AddSingleton<IService, Service>();
            services.AddSingleton<IRepository, Repository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseOpenApi();
                app.UseSwaggerUi3();
               // app.UseSwaggerUI();
            }

            app.UseCors("AllowAll");
            app.UseResponseCompression();
            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
            
        }
    }
}
