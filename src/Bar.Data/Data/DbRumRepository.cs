// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.Domain;
using Microsoft.EntityFrameworkCore;


namespace Bar.Data;

internal sealed class DbRumRepository : IRumRepository
{
    private readonly BarDbContext mDbContext;


    public DbRumRepository(BarDbContext dbContext)
    {
        mDbContext = dbContext;
    }


    public async Task<IReadOnlyList<Rum>> GetAllAsync()
    {
        return await mDbContext.Rums.AsNoTracking()
           .Select(x => x.ToEntity())
           .ToListAsync();
    }

    public async Task<Rum?> GetOrDefaultAsync(Guid id)
    {
        var dbo = await mDbContext.Rums.AsNoTracking()
           .SingleOrDefaultAsync(x => x.Id == id);

        return dbo?.ToEntity();
    }

    public async Task<Boolean> DeleteAsync(Guid id)
    {
        var dbo = await mDbContext.Rums.SingleOrDefaultAsync(x => x.Id == id);

        if (dbo is null)
            return false;

        mDbContext.Rums.Remove(dbo);
        await mDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Boolean> AddOrUpdateAsync(Rum item)
    {
        var exists = await mDbContext.Rums.AsNoTracking()
           .AnyAsync(x => x.Id == item.Id);

        if (exists)
        {
            var dbo = mDbContext.Rums.Update(item.ToDbo());
            await mDbContext.SaveChangesAsync();

            return false;
        }

        mDbContext.Rums.Add(item.ToDbo());
        await mDbContext.SaveChangesAsync();

        return true;
    }
}
