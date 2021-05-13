using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kwetter.Gateway.Web.Gateway
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //FirebaseApp firebaseApp = FirebaseApp.Create(new AppOptions()
            //{
            //    Credential = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") switch
            //    {
            //        "Production" => GoogleCredential.FromJson(_configuration["Authorization:Credential"]),
            //        "Development" => GoogleCredential.FromFile(_configuration["Authorization:Credential"]),
            //        _ => throw new ArgumentOutOfRangeException("Environment is not Production or Development."),
            //    }
            //});
            //services.AddSingleton(firebaseApp);
            string authenticationProviderKey = "firebase";

            services
               .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(authenticationProviderKey, options =>
               {
                   options.Authority = "https://securetoken.google.com/kwetter-a60c1";
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidIssuer = "https://securetoken.google.com/kwetter-a60c1",
                       ValidateAudience = true,
                       ValidAudience = "kwetter-a60c1",
                       ValidateLifetime = true
                   };
               });

            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseOcelot().Wait();
        }
    }
}
