﻿// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using Microsoft.EntityFrameworkCore;


namespace Bar.Data
{
    internal sealed class BarDbContext : DbContext
    {
        internal DbSet<RumDbo> Rums { get; set; } = default!;


        public BarDbContext(DbContextOptions<BarDbContext> options)
            : base(options)
        {
        }
    }
}
