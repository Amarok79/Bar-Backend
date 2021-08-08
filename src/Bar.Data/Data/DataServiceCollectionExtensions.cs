// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using Bar.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Bar.Data
{
    /// <summary>
    ///     Extensions for services collection.
    /// </summary>
    public static class DataServiceCollectionExtensions
    {
        /// <summary>
        ///     Adds the Rum repository.
        /// </summary>
        public static IServiceCollection AddRumRepository(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<BarDbContext>(x => x.UseSqlServer(configuration.GetConnectionString("Database")));
            services.AddScoped<IRumRepository, DbRumRepository>();

            return services;
        }
    }
}
