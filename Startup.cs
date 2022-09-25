using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using Serilog;
using shortUrl.Common;
using ShortUrl.Common;
using ShortUrl.Databases;
using ShortUrl.Services;

namespace ShortUrl
{
    public class Startup
    {
        public Serilog.ILogger Logger { get; }
        public IWebHostEnvironment ENV { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            ENV = env;
            Configuration =
                new ConfigurationBuilder()
                .SetBasePath(ENV.ContentRootPath)
                .AddJsonFile("appsettings.json", optional : false, reloadOnChange : true)
                .AddJsonFile($"appsettings.{ENV.EnvironmentName}.json", optional : true)
                .AddJsonFile($"appsettings.local.json", optional : true)
                .Build();

            Logger = new Serilog.LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Destructure.AsScalar<JObject>()
                .Destructure.AsScalar<JArray>()
                .Enrich.FromLogContext()
                .CreateLogger();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<ShortUrlConfig>(Configuration.GetSection("ShortUrlConfig"));
            services.AddDbContext<short_urlContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("cs")));
            services.AddScoped<IUrlService, UrlService>();

            services.AddSingleton<Serilog.ILogger>(Logger);
            services.AddMvc(options => options.OutputFormatters.Add(new HtmlOutputFormatter()));
            services.AddControllers();
        }
        // https://www.arctek.dev/blog/make-a-quick-url-shortener
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog(Logger);
            Logger.Information("ShortUrl started");
            app.ConfigureExceptionHandler(Logger);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

// https://www.npgsql.org/efcore//
// dotnet ef dbcontext scaffold "Host=144.126.140.118;Database=short_url;Username=postgres;Password=Ld4t5555" Npgsql.EntityFrameworkCore.PostgreSQL

    // <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="5.0.7" />
    
    // <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="5.0.7">
    //   <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    //   <PrivateAssets>all</PrivateAssets>
    // </PackageReference>
    
    // <PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="5.0.7">
    //   <PrivateAssets>all</PrivateAssets>
    //   <IncludeAssets>runtime; build; native; contentfiles; analyzers; buildtransitive</IncludeAssets>
    // </PackageReference>
// dotnet ef migrations add InitialMigration
// dotnet ef database update
