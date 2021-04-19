using Kwetter.Services.Common.API;
using Kwetter.Services.KweetService.API.DataAccess;
using Kwetter.Services.KweetService.API.DataAccess.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Linq;
using System.Reflection;

namespace Kwetter.Services.KweetService.API
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
            services.AddDbContext<KweetDbContext>(options => options.UseMySQL(sqlConnectionString));
            services.AddTransient<IKweetRepository, KweetRepository>();

            services.AddDefaultApplicationServices(Assembly.GetAssembly(typeof(Startup)));

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });
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

            ApplyDatabaseMigrations(app.ApplicationServices);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ApplyDatabaseMigrations(IServiceProvider applicationServices)
        {
            using var serviceScope = applicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var context = new KweetDbContext(serviceScope.ServiceProvider
                .GetRequiredService<DbContextOptions<KweetDbContext>>());

            context.Database.Migrate();
        }
    }
}
