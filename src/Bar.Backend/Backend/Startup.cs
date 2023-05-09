// Copyright (c) 2023, Olaf Kober <olaf.kober@outlook.com>

using Bar.Backend.Middleware;
using Bar.Data;


namespace Bar.Backend;


public sealed class Startup
{
    public IConfiguration Configuration { get; }


    public Startup(
        IConfiguration configuration
    )
    {
        Configuration = configuration;
    }


    public void ConfigureServices(
        IServiceCollection services
    )
    {
        services.AddApplicationInsightsTelemetry(
            options => options.ConnectionString = Configuration["ApplicationInsights:ConnectionString"]
        );

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddRepositories(Configuration);
    }

    public void Configure(
        IApplicationBuilder app,
        IWebHostEnvironment env,
        IDatabaseService databaseService
    )
    {
        databaseService.Migrate();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        else
        {
            app.UseMiddleware<ApiKeyMiddleware>();
        }

        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(x => x.MapControllers());
    }
}
