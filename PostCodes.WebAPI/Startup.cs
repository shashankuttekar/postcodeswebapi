using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PostCodes.Common.Model;
using PostCodes.Common.Services;
using PostCodes.Common.Services.Interfaces;
using PostCodes.WebAPI.Middleware;
using PostCodes.WebAPI.Services;
using PostCodes.WebAPI.Services.Interfaces;

namespace PostCodes.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                       .AllowAnyHeader()
                       .AllowAnyMethod();
                    });
            });

            services.AddLogging(config =>
            {
                config.AddAWSProvider(Configuration.GetAWSLoggingConfigSection());
                config.SetMinimumLevel(LogLevel.Debug);
            });
            services.AddControllers();
            var config = new PostCodesEnvironmentConfig(Configuration.GetSection("PostalCode").Get<PostCodeURIBase>());

            services.AddSingleton<IPostCodesEnvironmentConfig>(_ => config);
            string baseURI = config.GetPostCodesBaseURI();
            services.AddSingleton<IPostCodesService, PostCodesService>();
            services.AddSingleton<IHttpClientRepository, HttpClientRepository>();
            services.AddHttpClient("PostCodesWebAPI", c => c.BaseAddress = new Uri(baseURI));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
            });
        }
    }
}
