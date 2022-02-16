using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using CoinApi.Application;
using CoinApi.Background.Queue;
using CoinApi.Background.Queue.Core.Enums;
using CoinApi.Background.Queue.Core.Interfaces;
using CoinApi.Background.Queue.Core.Requests;
using CoinApi.DataAccess;
using CoinApi.Domain.Common.Configurations;
using CoinApi.Domain.Logic;
using CoinApi.Filters;
using CoinApi.Hangfire;
using CoinApi.Integration;
using CoinApi.Mapping;
using E.S.Simple.MemoryCache;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace CoinApi
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private string _dbConnectionString;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
            SetupConnectionStrings();
            ConfigureLogging(configuration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var generalConfig = _configuration.GetSection("CoinGeneralConfig").Get<CoinGeneralConfiguration>();

            services.Configure<CoinApiConfiguration>(_configuration.GetSection("CoinApiConfig"));
            services.Configure<CoinApiCronJobsConfiguration>(_configuration.GetSection("CoinApiCronJobsConfig"));
            services.Configure<CoinGeneralConfiguration>(_configuration.GetSection("CoinGeneralConfig"));

            services.AddControllers(options => { options.Filters.Add<ApiExceptionFilterAttribute>(); })
                .AddNewtonsoftJson(
                    options =>
                    {
                        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    });
            services.AddMvc()
                .AddFluentValidation(x =>
                {
                    x.AutomaticValidationEnabled = false;
                    x.ImplicitlyValidateChildProperties = true;
                });

            services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

            SetupCors(services);
            SetupSwagger(services);
            SetupResponseCompression(services);

            services.AddHttpContextAccessor();
            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
            services.AddMemoryCache();
            services.AddSimpleMemoryCache();
            services.AddDataAccess(_configuration, generalConfig.UseInMemoryDatabase);
            services.AddMapping();
            services.AddApplication();
            services.AddIntegration(_configuration);
            services.AddBackgroundQueue();
            services.AddHangfire(_configuration, generalConfig.UseInMemoryDatabase);
            services.AddDomainLogic();
            services.AddOptions();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IBackgroundQueue backgroundQueue)
        {
            app.UseCors("AllowAnyCorsPolicy");

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseResponseCompression();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "swagger/{documentName}/swagger.json";

                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                    swaggerDoc.Servers =
                        new List<OpenApiServer> {new() {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/"}});
            });

            app.UseStaticFiles();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "Coin Api Service");
            });

            app.UseHangfireDashboard();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });

            //Enqueue background tasks

            backgroundQueue.QueueAsync(
                new BackgroundQueueRequest(BackgroundQueueRequestTypeEnum.SyncCoinsInBackground));
            backgroundQueue.QueueAsync(
                new BackgroundQueueRequest(BackgroundQueueRequestTypeEnum.SyncCurrenciesInBackground));
            backgroundQueue.QueueAsync(
                new BackgroundQueueRequest(BackgroundQueueRequestTypeEnum.SyncCryptoCurrenciesInBackground));
            
            backgroundQueue.QueueAsync(
                new BackgroundQueueRequest(BackgroundQueueRequestTypeEnum.SyncCoins));
            backgroundQueue.QueueAsync(
                new BackgroundQueueRequest(BackgroundQueueRequestTypeEnum.SyncCurrencies));
        }

        #region Private Methods

        private void SetupConnectionStrings()
        {
            _dbConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ??
                                  _configuration.GetConnectionString("local");
            _configuration["Serilog:WriteTo:0:Args:connectionString"] = _dbConnectionString;
        }

        private static void ConfigureLogging(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        }

        private static void SetupCors(IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        "AllowAnyCorsPolicy",
                        builder => builder.WithOrigins(
                                "http://localhost:4200",
                                "http://127.0.0.1:4200",
                                "http://127.0.0.1:4201",
                                "https://127.0.0.1:4201")
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials());
                });
        }

        private static void SetupSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                        {Title = "App Store - Coin API", Version = "v1", Description = "Coin Api"});

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            });
        }

        private static void SetupResponseCompression(IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.EnableForHttps = true;
            });

            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
        }

        #endregion
    }
}