using System.Text;
using IoDit.WebAPI.Persistence;
using IoDit.WebAPI.Utilities;
using IoDit.WebAPI.Utilities.Loriot;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;

namespace IoDit.WebAPI;

public class Startup
{
    private readonly IConfiguration _configuration;

    private readonly bool _isDevelopment;
    private readonly bool _isStaging;

    public Startup(IWebHostEnvironment hostEnvironment, IConfiguration configuration)
    {
        _configuration = configuration;
        _isDevelopment = hostEnvironment.IsDevelopment();
        _isStaging = hostEnvironment.IsStaging();
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
            services.AddHttpClient<LoriotApiClient>();
            services.RegisterApplicationServices(_configuration);
            var connectionString = "";
            if (_isDevelopment)
            {
                connectionString = _configuration.GetConnectionString("PostgresSqlServer");
            }
            else if (_isStaging)
            {
                connectionString = _configuration.GetConnectionString("AzureAgroditPostgresSqlServer-Staging");
            }
            else
            {
                connectionString = _configuration.GetConnectionString("AzureAgroditPostgresSqlServer");
            }

            services.AddDbContext<AgroditDbContext>(opts =>
            {
                opts.UseNpgsql(connectionString, x =>
                x.UseNetTopologySuite()
                .UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                );

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
                        ValidIssuer = _configuration["BackEndUrl"],
                        // ValidAudience = "*",
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                    };
                });
            // swagger configuration
            if (_isDevelopment || _isStaging)
            {
                services.AddSwaggerGen((opt) =>
                {
                    var jwtSecurityScheme = new OpenApiSecurityScheme
                    {
                        BearerFormat = "JWT",
                        Name = "JWT Authentication",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = JwtBearerDefaults.AuthenticationScheme,
                        Description = "Get your token by login on the Auth/login route.\n Put **_ONLY_** your JWT Bearer token on textbox below!",

                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    };

                    opt.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        { jwtSecurityScheme, Array.Empty<string>() }
                    });
                });
            }
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
            app.UseStaticFiles();
        }
    }
}