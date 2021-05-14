using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Database.DB>(opt => opt.UseMySql(GetConnectionString(), ServerVersion.Parse("10.3.0-mariadb")));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            app.UseEndpoints(routes => routes.MapControllerRoute("default", "{controller}/{action}"));
        }

        private string GetConnectionString()
        {
            var result = _configuration.GetConnectionString("DefaultConnection");

            // get password from env
            var userId = Environment.GetEnvironmentVariable("DB_USER");
            var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
            if (userId != null && password != null)
                result = $"{result};userid={userId};password={password}";

            return result;
        }
    }
}
