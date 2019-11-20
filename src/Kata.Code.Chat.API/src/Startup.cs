using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.PostgreSql;
using Kata.Code.Chat.API.Services;
using Kata.Code.Chat.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Graylog;
using Swashbuckle.AspNetCore.Swagger;

namespace Kata.Code.Chat.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string myAllowSpecificOrigins = "AllowMyOrigin";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ChatDbContext>(options => 
                options.UseNpgsql(
                    Configuration.GetConnectionString("ChatConnection"), 
                    m => m.MigrationsAssembly("Kata.Code.Chat.DataAccess")
                    )
                );

            var logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .MinimumLevel.Override("System", LogEventLevel.Error)
                .WriteTo.Console(
                    LogEventLevel.Information,
                    "{NewLine}{Timestamp:HH:mm:ss} [{Level:u3}] {Message} {Exception}")
                .WriteTo.File(
                    outputTemplate: "{NewLine}{Timestamp:HH:mm:ss} [{Level:u3}] ({SourceContext}): {Message} {Exception}",
                    path: "c:\\logs\\Kata.Code.Chat.API\\log.txt",
                    rollOnFileSizeLimit: true,
                    rollingInterval: RollingInterval.Day,
                    shared: true)
                .WriteTo.Graylog( new GraylogSinkOptions
                {
                    HostnameOrAddress = "graylog-test.voxelgroup.net",
                    Port = 12201,
                    Facility = "Kata.Code.Chat"
                })
                .CreateLogger();

            services.RemoveAll<ILoggerProvider>();

            services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog(logger: logger, dispose: true));

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseColouredConsoleLogProvider()
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors(options =>
            {
                options.AddPolicy(myAllowSpecificOrigins,
                    builder => builder.WithOrigins("http://localhost:4200"));
            });
            services.AddScoped<IRoom, Room>();
            services.AddSingleton<IUtc, utc>();
            services.AddScoped<IMessageRepository, IMessageRepository>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new Info { Title = "API Chat", Version = "v1"});
                c.DescribeAllEnumsAsStrings();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.XML";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
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

            app.UseCors(myAllowSpecificOrigins);

            app.UseHangfireServer();
            app.UseHangfireDashboard();

            //RecurringJob.AddOrUpdate("MyJob", () => Console.WriteLine("Minutely Job"), Cron.Minutely);
            RecurringJob.AddOrUpdate<IRoom>("Spam Message", r => r.AddMessage("Spam", "Remeber to visit http://www.voxelgroup.net"), Cron.Minutely);

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/V1/swagger.json", "API Chat v1");
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
