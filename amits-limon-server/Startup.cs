using amits_limon_server.Data;
using amits_limon_server.Interfaces;
using amits_limon_server.Repositories;
using amits_limon_server.Services;
using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace amits_limon_server
{
    public class Startup
    {
        private readonly string _MyAllowSpecificOrigin = "myAlloworigin";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            #region Configure jwt for admin
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = Configuration["Jwt:Issuer"],
                      ValidAudience = Configuration["Jwt:Issuer"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                  };
              });
            #endregion

            #region Configure cache and rate to limit rate of requests per user
            services.AddMemoryCache();
            services.AddSingleton<IClientPolicyStore, MemoryCacheClientPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
            services.Configure<ClientRateLimitOptions>(options =>
                {
                    options.GeneralRules = new List<RateLimitRule>
                {
                     new RateLimitRule
                  {
                     Endpoint = "*",
                     Period = "1m",
                     Limit = 500,
                  },
               new RateLimitRule
                  {
                    Endpoint = "*",
                    Period = "1h",
                    Limit = 3600,
                  }
               };
                });
            #endregion

            #region Setting repository interfaces
            services.AddTransient<IGetSongs, Repository>();
            services.AddTransient<IGetSpotlights, Repository>();
            services.AddTransient<IAddComments, Repository>();
            services.AddTransient<IGetNotes, Repository>();
            services.AddTransient<IGetRecommendations, Repository>();
            services.AddTransient<IAdminOperations, Repository>();
            services.AddTransient<ObjectValuesChecker>();
            #endregion

            #region Setting DB configuration
            string connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DataContext>(options => options.UseLazyLoadingProxies().UseSqlServer(connectionString));
            #endregion

            services.AddControllers();

            #region Setting cors policy
            services.AddCors(options =>
            {
                options.AddPolicy(name: _MyAllowSpecificOrigin, builder =>
                {
                    builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200");
                });
            });
            #endregion

            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DataContext data)
        {
            //////// for test
            data.Database.EnsureDeleted();
            data.Database.EnsureCreated();
            ////////
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/error");
            }
            #region Setting route for images
            app.UseStaticFiles(
                new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "Images")),
                    RequestPath = "/api/Images"
                });
            #endregion

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors(_MyAllowSpecificOrigin);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
