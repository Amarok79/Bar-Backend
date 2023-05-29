// Copyright (c) 2023, Olaf Kober <olaf.kober@outlook.com>

using MongoDB.Driver;


namespace Bar.Data;


internal interface IDatabaseService
{
    IMongoClient GetClient();
}
