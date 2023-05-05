// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Bar.Data;


/// <summary>
///     Extensions for services collection.
/// </summary>
public static class DataServiceCollectionExtensions
{
    /// <summary>
    ///     Adds the Rum and Gin repositories.
    /// </summary>
    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<BarDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("Database")));
        services.AddScoped<IGinRepository, DbGinRepository>();
        services.AddScoped<IRumRepository, DbRumRepository>();
        services.AddScoped<ISubstanceRepository, DbSubstanceRepository>();
        services.AddTransient<IDatabaseService, DatabaseServiceImpl>();

        return services;
    }
}
