// Copyright (c) 2023, Olaf Kober <olaf.kober@outlook.com>

using Microsoft.Extensions.Configuration;
using MongoDB.Driver;


namespace Bar.Data;


internal sealed class DatabaseService : IDatabaseService
{
    private readonly IConfiguration mConfiguration;


    public DatabaseService(
        IConfiguration configuration
    )
    {
        mConfiguration = configuration;
    }


    public IMongoClient GetClient()
    {
        var connectionString = mConfiguration["ConnectionStrings:Database"];

        return new MongoClient(connectionString);
    }
}
