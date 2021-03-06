using Kwetter.Services.Common.API;
using Kwetter.Services.Common.Infrastructure;
using Kwetter.Services.UserService.API.Application.IntegrationEventHandlers.IdentityCreated;
using Kwetter.Services.UserService.API.Infrastructure;
using Kwetter.Services.UserService.API.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.Threading;

namespace Kwetter.Services.UserService.API
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var sqlConnectionString = _configuration.GetConnectionString("KweetDatabase");
            services.AddDbContextPool<UserDbContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(sqlConnectionString, ServerVersion.AutoDetect(sqlConnectionString)));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddDefaultApplicationServices(Assembly.GetAssembly(typeof(Startup)));

            services.AddMessagePublishing("UserService", builder => {
                builder.WithHandler<IdentityCreatedIntegrationEventHandler>("IdentityCreated");
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
            services.VerifyDatabaseConnection<UserDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
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
