// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using Bar.Backend.Middleware;
using Bar.Data;
using Bar.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Bar.Backend.Controllers;


public sealed class TestStartup
{
    private readonly String mName;

    public IConfiguration Configuration { get; }


    public TestStartup(
        String name,
        IConfiguration configuration
    )
    {
        mName = name;
        Configuration = configuration;
    }


    public void ConfigureServices(
        IServiceCollection services
    )
    {
        services.AddControllers().AddApplicationPart(typeof(RumController).Assembly);

        services.AddScoped<IGinRepository, DbGinRepository>();
        services.AddScoped<IRumRepository, DbRumRepository>();
    }

    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env
    )
    {
        app.UseDeveloperExceptionPage();

        app.UseMiddleware<ApiKeyMiddleware>();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(x => x.MapControllers());
    }
}
