// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bar.Domain;
using Microsoft.EntityFrameworkCore;


namespace Bar.Data
{
    internal sealed class DbGinRepository : IGinRepository
    {
        private readonly BarDbContext mDbContext;


        public DbGinRepository(BarDbContext dbContext)
        {
            mDbContext = dbContext;
        }


        public async Task<IReadOnlyList<Gin>> GetAllAsync()
        {
            return await mDbContext.Gins.AsNoTracking().Select(x => x.ToEntity()).ToListAsync();
        }

        public async Task<Gin?> GetOrDefaultAsync(Guid id)
        {
            var dbo = await mDbContext.Gins.AsNoTracking().SingleOrDefaultAsync(x => x.Id == id);

            return dbo?.ToEntity();
        }

        public async Task<Boolean> DeleteAsync(Guid id)
        {
            var dbo = await mDbContext.Gins.SingleOrDefaultAsync(x => x.Id == id);

            if (dbo is null)
                return false;

            mDbContext.Gins.Remove(dbo);
            await mDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Boolean> AddOrUpdateAsync(Gin item)
        {
            var exists = await mDbContext.Gins.AsNoTracking().AnyAsync(x => x.Id == item.Id);

            if (exists)
            {
                var dbo = mDbContext.Gins.Update(item.ToDbo());
                await mDbContext.SaveChangesAsync();

                return false;
            }

            mDbContext.Gins.Add(item.ToDbo());
            await mDbContext.SaveChangesAsync();

            return true;
        }
    }
}
