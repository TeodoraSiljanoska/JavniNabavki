using JavniNabavki.Models;
using JavniNabavki.Repositories;
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
//using Microsoft.TodoApi.Nodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;









namespace JavniNabavki
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
            services.AddScoped<IExamRepository, ExamRepository>();
            //services.AddDbContext<BookContext>(o => o.UseSqlite("Data source=exams.db"));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "JavniNabavki", Version = "v1" });
            }
            );
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ExamContext>(o => o.UseSqlServer(connectionString));

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "JavniNabavki V");
                c.RoutePrefix = "";
            }
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();



        }
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940



    }
}