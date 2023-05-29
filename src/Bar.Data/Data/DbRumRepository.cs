// Copyright (c) 2023, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;
using MongoDB.Driver;


namespace Bar.Data;


internal sealed class DbRumRepository : IRumRepository
{
    private readonly IDatabaseService mDatabaseService;


    public DbRumRepository(
        IDatabaseService databaseService
    )
    {
        mDatabaseService = databaseService;
    }


    private IMongoCollection<RumDbo> _GetCollection()
    {
        return mDatabaseService.GetClient().GetDatabase("bar-db").GetCollection<RumDbo>("rums");
    }


    public async Task<IReadOnlyList<Rum>> GetAllAsync(
        Boolean includeDrafts = false
    )
    {
        var filter = includeDrafts
            ? Builders<RumDbo>.Filter.Empty
            : Builders<RumDbo>.Filter.Eq(x => x.IsDraft, false);

        var rums = await _GetCollection().Find(filter).ToListAsync();

        return rums.Select(x => x.ToEntity()).ToList();
    }

    public async Task<Rum?> GetOrDefaultAsync(
        Guid id
    )
    {
        var filter = Builders<RumDbo>.Filter.Eq(x => x.Id, id.ToString());

        var dbo = await _GetCollection().Find(filter).FirstOrDefaultAsync();

        return dbo?.ToEntity();
    }

    public async Task<Boolean> DeleteAsync(
        Guid id
    )
    {
        var filter = Builders<RumDbo>.Filter.Eq(x => x.Id, id.ToString());

        var result = await _GetCollection().DeleteOneAsync(filter);

        return result.DeletedCount > 0;
    }

    public async Task<Boolean> AddOrUpdateAsync(
        Rum item
    )
    {
        var filter = Builders<RumDbo>.Filter.Eq(x => x.Id, item.Id.ToString());

        var result = await _GetCollection()
            .ReplaceOneAsync(filter, item.ToDbo(),
                new ReplaceOptions {
                    IsUpsert = true,
                });

        return result.ModifiedCount == 0;
    }
}
