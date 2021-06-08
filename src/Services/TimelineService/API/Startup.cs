using Kwetter.Services.Common.API;
using Kwetter.Services.Common.Infrastructure;
using Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.KweetCreated;
using Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.KweetLiked;
using Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.KweetUnliked;
using Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.UserCreated;
using Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.UserFollowed;
using Kwetter.Services.TimelineService.API.Application.IntegrationEventHandlers.UserUnfollowed;
using Kwetter.Services.TimelineService.API.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Neo4jClient;
using System;
using System.Reflection;

namespace Kwetter.Services.TimelineService.API
{
    public class Startup
    {
        private IConfiguration Config { get; }

        public Startup(IConfiguration configuration)
        {
            Config = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            IGraphClient graphClient = InitNeo4jClient(Config.GetSection("NEO4J"));
            services.AddSingleton(graphClient);
            services.AddTransient<ITimelineGraphRepository, TimelineGraphRepository>();
            services.AddDefaultApplicationServices(Assembly.GetAssembly(typeof(Startup)));

            services.AddMessagePublishing("TimelineService", builder => {
                builder.WithHandler<KweetCreatedIntegrationEventHandler>("KweetCreated");
                builder.WithHandler<KweetLikedIntegrationEventHandler>("KweetLiked");
                builder.WithHandler<KweetUnlikedIntegrationEventHandler>("KweetUnliked");
                builder.WithHandler<UserCreatedIntegrationEventHandler>("UserCreated");
                builder.WithHandler<UserFollowedIntegrationEventHandler>("UserFollowed");
                builder.WithHandler<UserUnfollowedIntegrationEventHandler>("UserUnfollowed");
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kwetter.Services.TimelineService.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kwetter.Services.TimelineService.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static IGraphClient InitNeo4jClient(IConfigurationSection config)
        {
            IGraphClient client = new GraphClient(new Uri(config["URL"]), config["USERNAME"], config["PASSWORD"]);
            client.ConnectAsync().Wait();
            return client;
        }
    }
}
