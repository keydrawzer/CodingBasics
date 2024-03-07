using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CodingBasics.Data;
using CodingBasics.Services;

namespace CodingBasics
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Enable Cross-Origin
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                        .WithOrigins("http://localhost:8080") // Replace with url Vue.js
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            
            services.AddControllers(); // Controllers MVC support

            // DB Configuration
            services.AddDbContext<BasicDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("localServer")));


            // Register services
            services.AddScoped<PersonService>(); // Person service
        }

       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // Enable developer exception
                app.UseDeveloperExceptionPage();
            }

            // Apply CORS
            app.UseCors("CorsPolicy");
            // Configure routing
            app.UseRouting();
            // Use authorization
            app.UseAuthorization();
            // Define endpoints
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
