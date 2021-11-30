// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.Domain;
using Microsoft.EntityFrameworkCore;


namespace Bar.Data;

internal sealed class DbSubstanceRepository : ISubstanceRepository
{
    private readonly BarDbContext mDbContext;


    public DbSubstanceRepository(BarDbContext dbContext)
    {
        mDbContext = dbContext;
    }


    public async Task<IReadOnlyList<Substance>> GetAllAsync()
    {
        return await mDbContext.Substances.AsNoTracking()
           .Select(x => x.ToEntity())
           .ToListAsync();
    }

    public async Task<Substance?> GetOrDefaultAsync(String id)
    {
        var dbo = await mDbContext.Substances.AsNoTracking()
           .SingleOrDefaultAsync(x => x.Id == id);

        return dbo?.ToEntity();
    }

    public async Task<Boolean> DeleteAsync(String id)
    {
        var dbo = await mDbContext.Substances.SingleOrDefaultAsync(x => x.Id == id);

        if (dbo is null)
            return false;

        mDbContext.Substances.Remove(dbo);
        await mDbContext.SaveChangesAsync();

        return true;
    }

    public async Task<Boolean> AddOrUpdateAsync(Substance item)
    {
        var exists = await mDbContext.Substances.AsNoTracking()
           .AnyAsync(x => x.Id == item.Id);

        if (exists)
        {
            var dbo = mDbContext.Substances.Update(item.ToDbo());
            await mDbContext.SaveChangesAsync();

            return false;
        }

        mDbContext.Substances.Add(item.ToDbo());
        await mDbContext.SaveChangesAsync();

        return true;
    }
}
