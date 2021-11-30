// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Threading.Tasks;
using Bar.Domain;
using Microsoft.EntityFrameworkCore;
using NFluent;
using NUnit.Framework;


namespace Bar.Data;

[TestFixture]
public class DbSubstanceRepositoryTests
{
    private BarDbContext mContext;
    private ISubstanceRepository mRepository;


    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<BarDbContext>().UseInMemoryDatabase(
                Guid.NewGuid()
                   .ToString()
            )
           .Options;

        mContext = new BarDbContext(options);
        mContext.Database.EnsureDeleted();
        mContext.Database.EnsureCreated();

        mRepository = new DbSubstanceRepository(mContext);
    }

    [TearDown]
    public void Cleanup()
    {
        mContext.Database.EnsureDeleted();
        mContext.Dispose();
    }

    public async Task AddSampleItems()
    {
        mContext.Substances.Add(
            new SubstanceDbo {
                Id       = "AAA",
                Name     = "Grand Marnier",
                Category = "Liqueurs",
                Unit     = "cl",
            }
        );

        mContext.Substances.Add(
            new SubstanceDbo {
                Id   = "BBB",
                Name = "Limes",
            }
        );

        await mContext.SaveChangesAsync();

        mContext.ChangeTracker.Clear();
    }


    [Test]
    public async Task GetAllAsync_NoItems()
    {
        var result = await mRepository.GetAllAsync();

        Check.That(result)
           .IsEmpty();
    }

    [Test]
    public async Task GetAllAsync_WithItems()
    {
        await AddSampleItems();

        var result = await mRepository.GetAllAsync();

        Check.That(result)
           .HasSize(2);

        Check.That(
                result[0]
                   .Id
            )
           .IsEqualTo("AAA");

        Check.That(
                result[0]
                   .Name
            )
           .IsEqualTo("Grand Marnier");

        Check.That(
                result[0]
                   .Category
            )
           .IsEqualTo("Liqueurs");

        Check.That(
                result[0]
                   .Unit
            )
           .IsEqualTo("cl");

        Check.That(
                result[1]
                   .Id
            )
           .IsEqualTo("BBB");

        Check.That(
                result[1]
                   .Name
            )
           .IsEqualTo("Limes");

        Check.That(
                result[1]
                   .Category
            )
           .IsNull();

        Check.That(
                result[1]
                   .Unit
            )
           .IsNull();
    }


    [Test]
    public async Task GetOrDefaultAsync_NoItem()
    {
        var result = await mRepository.GetOrDefaultAsync("AAA");

        Check.That(result)
           .IsNull();
    }

    [Test]
    public async Task GetOrDefaultAsync_WithItem()
    {
        await AddSampleItems();

        var result = await mRepository.GetOrDefaultAsync("AAA");

        Check.That(result)
           .IsNotNull();

        Check.That(result.Id)
           .IsEqualTo("AAA");

        Check.That(result.Name)
           .IsEqualTo("Grand Marnier");

        Check.That(result.Category)
           .IsEqualTo("Liqueurs");

        Check.That(result.Unit)
           .IsEqualTo("cl");
    }


    [Test]
    public async Task DeleteAsync_NotExisting()
    {
        var result = await mRepository.DeleteAsync("AAA");

        Check.That(result)
           .IsFalse();
    }

    [Test]
    public async Task DeleteAsync_Existing()
    {
        await AddSampleItems();

        var result = await mRepository.DeleteAsync("AAA");

        Check.That(result)
           .IsTrue();

        result = await mRepository.DeleteAsync("AAA");

        Check.That(result)
           .IsFalse();
    }


    [Test]
    public async Task AddOrUpdateAsync_NotExisting()
    {
        var entity = new Substance("AAA", "Grand Marnier-2") {
            Category = "Liqueurs-2",
            Unit     = "ml",
        };

        var result = await mRepository.AddOrUpdateAsync(entity);

        Check.That(result)
           .IsTrue();

        var item = await mRepository.GetOrDefaultAsync("AAA");

        Check.That(item)
           .IsNotNull();

        Check.That(item.Id)
           .IsEqualTo("AAA");

        Check.That(item.Name)
           .IsEqualTo("Grand Marnier-2");

        Check.That(item.Category)
           .IsEqualTo("Liqueurs-2");

        Check.That(item.Unit)
           .IsEqualTo("ml");
    }

    [Test]
    public async Task UpdateAsync_Existing()
    {
        await AddSampleItems();

        var entity = new Substance("AAA", "Grand Marnier-2") {
            Category = "Liqueurs-2",
            Unit     = "ml",
        };

        var result = await mRepository.AddOrUpdateAsync(entity);

        Check.That(result)
           .IsFalse();

        var item = await mRepository.GetOrDefaultAsync("AAA");

        Check.That(item)
           .IsNotNull();

        Check.That(item.Id)
           .IsEqualTo("AAA");

        Check.That(item.Name)
           .IsEqualTo("Grand Marnier-2");

        Check.That(item.Category)
           .IsEqualTo("Liqueurs-2");

        Check.That(item.Unit)
           .IsEqualTo("ml");
    }
}
