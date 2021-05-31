using Kwetter.Services.Common.API;
using Kwetter.Services.Common.Infrastructure;
using Kwetter.Services.FollowService.API.Application.IntegrationEventHandlers;
using Kwetter.Services.FollowService.API.Infrastructure;
using Kwetter.Services.FollowService.API.Infrastructure.Repositories;
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.API
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = _configuration.GetConnectionString("KweetDatabase");
            services.AddDbContext<FollowDbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(sqlConnectionString, ServerVersion.AutoDetect(sqlConnectionString)));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddDefaultApplicationServices(Assembly.GetAssembly(typeof(Startup)));
            services.AddMessagePublishing("FollowService", builder => {
                builder.WithHandler<UserCreatedIntegrationEventHandler>("UserCreated");
            });

            // TODO: Current implemenation is a dirty workaround. Fix self referencing loop
            services.AddControllers().AddNewtonsoftJson(x =>
            {
                x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kwetter.Services.FollowService.API", Version = "v1" });
            });
            services.VerifyDatabaseConnection<FollowDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kwetter.Services.FollowService.API v1"));
            }

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
