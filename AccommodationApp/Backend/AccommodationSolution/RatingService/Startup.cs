using Jaeger.Reporters;
using Jaeger.Samplers;
using Jaeger.Senders.Thrift;
using Jaeger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Neo4j.Driver;
using RatingService.Neo4J;
using OpenTracing.Contrib.NetCore.Configuration;
using OpenTracing;
using RatingService.Repository;
using RatingService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RatingService
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

            services.AddCors();

            //services.AddSingleton<IDbContext, MongoDbContext>();

            services.Configure<Neo4jSettings>(Configuration.GetSection("Neo4jSettings"));
            var settings = new Neo4jSettings();
            Configuration.GetSection("Neo4jSettings").Bind(settings);

            services.AddSingleton(GraphDatabase.Driver(settings.Neo4jConnection, AuthTokens.Basic(settings.Neo4jUser, settings.Neo4jPassword)));
            services.AddSingleton<INeo4jDataAccess, Neo4jDataAccess>();
            services.AddScoped<IRatingRepository, Neo4jRatingRepository>();

            services.AddScoped<RatingService.Service.RatingService>();

            services.AddScoped<Neo4jRecommendationRepository>();
            services.AddScoped<RecommendationService>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "RatingService", Version = "v1" });
            });

            services.AddOpenTracing();
            services.AddSingleton<ITracer>(sp =>
            {
                var serviceName = sp.GetRequiredService<IWebHostEnvironment>().ApplicationName;
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                var reporter = new RemoteReporter.Builder().WithLoggerFactory(loggerFactory).WithSender(new UdpSender())
                    .Build();
                var tracer = new Tracer.Builder(serviceName)
                    // The constant sampler reports every span.
                    .WithSampler(new ConstSampler(true))
                    // LoggingReporter prints every reported span to the logging framework.
                    .WithReporter(reporter)
                    .Build();

                return tracer;
            });

            services.Configure<HttpHandlerDiagnosticOptions>(options =>
                options.OperationNameResolver =
                    request => $"{request.Method.Method}: {request?.RequestUri?.AbsoluteUri}");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RatingService v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}