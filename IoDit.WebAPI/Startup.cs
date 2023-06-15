using System.Text;
using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Utilities;
using IoDit.WebAPI.Utilities.Loriot;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace IoDit.WebAPI;

public class Startup
{
  private readonly IConfiguration _configuration;

  private readonly bool _isDevelopment;

  public Startup(IWebHostEnvironment hostEnvironment, IConfiguration configuration)
  {
    _configuration = configuration;
    _isDevelopment = hostEnvironment.IsDevelopment();
  }

  [UsedImplicitly]
  public void ConfigureServices(IServiceCollection services)
  {
    {
      services.AddHealthChecks();
      services.AddControllers().AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        foreach (var converter in NetTopologySuite.IO.GeoJsonSerializer.Create(new GeometryFactory(new PrecisionModel(), 4326)).Converters)
        {
          options.SerializerSettings.Converters.Add(converter);
        }
      });
      services.AddEndpointsApiExplorer();
      services.AddSwaggerGen();
      services.AddHttpClient<LoriotApiClient>();
      services.RegisterApplicationServices(_configuration);
      var connectionString = "";
      if (_isDevelopment)
      {
        connectionString = _configuration.GetConnectionString("PostgresSqlServer");
      } else {
        connectionString = _configuration.GetConnectionString("AzureAgroditPostgresSqlServer");
      }

      services.AddDbContext<IoDitDbContext>(opts =>
      {
        opts.UseNpgsql(connectionString, x => x.UseNetTopologySuite());
      });
      // var issuer = configuration["JwtSettings-Issuer"];
      // var audience = configuration["JwtSettings-Audience"];
      var secretKey = Encoding.ASCII.GetBytes(_configuration["JwtSettings-SecretKey"]);
      services.AddCors();
      services.AddAuthentication(options =>
          {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
          })
          .AddJwtBearer(options =>
          {
            options.TokenValidationParameters = new TokenValidationParameters
            {
              //todo change issuer to https://agrodit-api.azurewebsites.net on deploy 
              //todo change audience on deploy
              ValidateIssuer = false,
              ValidateAudience = false,
              ValidIssuer = "https://localhost:7161",
              ValidAudience = "http://localhost:8100",
              ValidateIssuerSigningKey = true,
              ValidateLifetime = true,
              ClockSkew = TimeSpan.Zero,
              IssuerSigningKey = new SymmetricSecurityKey(secretKey)
            };
          });
    }
  }

  [UsedImplicitly]
  public void Configure(IApplicationBuilder app)
  {
    {
      if (_isDevelopment)
      {
        app.UseSwagger();
        app.UseSwaggerUI();
      }
      else
      {
        app.UseHttpsRedirection();
      }

      app.UseCors(x =>
      {
        x.AllowAnyHeader();
        x.AllowAnyMethod();
        x.AllowAnyOrigin();
      });
      app.UseRouting();
      app.UseAuthentication();
      app.UseAuthorization();
      app.UseEndpoints(endpoints =>
      {
        endpoints.MapHealthChecks("/healthz");
        endpoints.MapControllers();
      });
    }
  }
}