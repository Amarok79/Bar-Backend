// Copyright (c) 2023, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;
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
        this IServiceCollection services
    )
    {
        services.AddSingleton<IDatabaseService, DatabaseService>();
        services.AddScoped<IGinRepository, DbGinRepository>();
        services.AddScoped<IRumRepository, DbRumRepository>();

        return services;
    }
}
