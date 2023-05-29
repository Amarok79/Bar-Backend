// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using MongoDB.Driver;


namespace Bar.Data;


public interface IDatabaseService
{
    IMongoClient GetClient();
}
