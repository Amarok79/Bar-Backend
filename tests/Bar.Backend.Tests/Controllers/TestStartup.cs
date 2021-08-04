// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using Bar.Backend.Controllers;
using Bar.Data;
using Bar.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Bar.Backend.Tests.Controllers
{
    public sealed class TestStartup
    {
        public IConfiguration Configuration { get; }


        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddApplicationPart(typeof(RumController).Assembly);

            services.AddDbContext<BarDbContext>(options => options.UseInMemoryDatabase("Bar"));

            services.AddScoped<IRumRepository, DbRumRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
