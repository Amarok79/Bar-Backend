// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using Bar.Backend.Middleware;
using Bar.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Bar.Backend
{
    public sealed class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddRumRepository(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMiddleware<ApiKeyMiddleware>();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(x => x.MapControllers());
        }
    }
}
