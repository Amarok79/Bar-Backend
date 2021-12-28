// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using Microsoft.EntityFrameworkCore;


namespace Bar.Data;

internal sealed class DatabaseServiceImpl : IDatabaseService
{
    private readonly BarDbContext mDataContext;


    public DatabaseServiceImpl(BarDbContext dataContext)
    {
        mDataContext = dataContext;
    }


    public void Migrate()
    {
        mDataContext.Database.Migrate();
    }
}
