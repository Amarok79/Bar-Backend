// Copyright (c) 2023, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;
using MongoDB.Driver;


namespace Bar.Data;


internal sealed class DbGinRepository : IGinRepository
{
    private readonly IDatabaseService mDatabaseService;


    public DbGinRepository(
        IDatabaseService databaseService
    )
    {
        mDatabaseService = databaseService;
    }


    private IMongoCollection<GinDbo> _GetCollection()
    {
        return mDatabaseService.GetClient().GetDatabase("bar-db").GetCollection<GinDbo>("gins");
    }


    public async Task<IReadOnlyList<Gin>> GetAllAsync(
        Boolean includeDrafts = false
    )
    {
        var filter = includeDrafts
            ? Builders<GinDbo>.Filter.Empty
            : Builders<GinDbo>.Filter.Eq(x => x.IsDraft, false);

        var gins = await _GetCollection().Find(filter).ToListAsync();

        return gins.Select(x => x.ToEntity()).ToList();
    }

    public async Task<Gin?> GetOrDefaultAsync(
        Guid id
    )
    {
        var filter = Builders<GinDbo>.Filter.Eq(x => x.Id, id.ToString());

        var dbo = await _GetCollection().Find(filter).FirstOrDefaultAsync();

        return dbo?.ToEntity();
    }

    public async Task<Boolean> DeleteAsync(
        Guid id
    )
    {
        var filter = Builders<GinDbo>.Filter.Eq(x => x.Id, id.ToString());

        var result = await _GetCollection().DeleteOneAsync(filter);

        return result.DeletedCount > 0;
    }

    public async Task<Boolean> AddOrUpdateAsync(
        Gin item
    )
    {
        var filter = Builders<GinDbo>.Filter.Eq(x => x.Id, item.Id.ToString());

        var result = await _GetCollection()
            .ReplaceOneAsync(filter, item.ToDbo(),
                new ReplaceOptions {
                    IsUpsert = true,
                });

        return result.ModifiedCount == 0;
    }
}
