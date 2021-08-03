// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using Bar.Domain;
using Microsoft.EntityFrameworkCore;


namespace Bar.Data
{
    public class BarDbContext : DbContext
    {
        public DbSet<Rum>? Rums { get; set; }


        public BarDbContext()
        {
        }

        public BarDbContext(DbContextOptions<BarDbContext> options)
            : base(options)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=Bar");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
        }
    }
}
