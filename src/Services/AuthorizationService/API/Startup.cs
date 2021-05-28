using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Kwetter.Services.AuthorizationService.API.Infrastructure;
using Kwetter.Services.AuthorizationService.API.Infrastructure.Repositories;
using Kwetter.Services.AuthorizationService.API.Infrastructure.Services;
using Kwetter.Services.Common.API;
using Kwetter.Services.Common.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace Kwetter.Services.AuthorizationService.API
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDefaultApplicationServices(Assembly.GetAssembly(typeof(Startup)));

            var sqlConnectionString = _configuration.GetConnectionString("KweetDatabase");

            services.AddDbContextPool<IdentityDbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(sqlConnectionString, ServerVersion.AutoDetect(sqlConnectionString)));

            services.AddTransient<IIdentityRepository, IdentityRepository>();
            services.AddSingleton<IAuthorizationService, FirebaseAuthorizationService>();

            FirebaseApp firebaseApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") switch
                {
                    "Production" => GoogleCredential.FromJson(_configuration["Authorization:Credential"]),
                    "Development" => GoogleCredential.FromFile("C:/Users/Roy-t/Desktop/kwetter/kwetter-a60c1-firebase-adminsdk-3a0h3-3592a6c11e.json"),
                    _ => throw new ArgumentOutOfRangeException("Environment is not Production or Development."),
                }
            });
            services.AddSingleton(firebaseApp);

            services.AddMessagePublishing("AuthorizationService");

            services.AddCors();
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kwetter.Services.AuthorizationService.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kwetter.Services.AuthorizationService.API v1"));
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
