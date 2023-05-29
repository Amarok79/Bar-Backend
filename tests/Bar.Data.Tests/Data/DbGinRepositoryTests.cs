// Copyright (c) 2023, Olaf Kober <olaf.kober@outlook.com>

using Bar.Domain;
using EphemeralMongo;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using NFluent;
using NUnit.Framework;


namespace Bar.Data;


[TestFixture]
public class DbGinRepositoryTests
{
    private IGinRepository mRepository;
    private IMongoRunner mRunner;
    private IMongoCollection<GinDbo> mCollection;


    [SetUp]
    public void Setup()
    {
        mRunner = MongoRunner.Run();

        var configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<String, String> {
                { "ConnectionStrings:Database", mRunner.ConnectionString },
            })
            .Build();

        var databaseService = new DatabaseService(configuration);

        var db = databaseService.GetClient().GetDatabase("bar-db");
        db.CreateCollection("gins");

        mCollection = db.GetCollection<GinDbo>("gins");

        mRepository = new DbGinRepository(databaseService);
    }

    [TearDown]
    public void Cleanup()
    {
        mRunner?.Dispose();
    }


    public async Task AddSampleItems()
    {
        await mCollection.InsertOneAsync(new GinDbo {
                Id = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55").ToString(),
                Name = "The Stin Dry Gin",
                Teaser = "Styrian Dry Gin",
                Description = "Made in Styria",
                Images = new[] { "KRO01046.jpg", "KRO00364.jpg" },
                IsDraft = false,
            }
        );

        await mCollection.InsertOneAsync(new GinDbo {
                Id = new Guid("01691cd5-1102-4593-9c27-72b567871338").ToString(),
                Name = "The Duke",
                IsDraft = true,
            }
        );
    }


    [Test]
    public async Task GetAllAsync_NoItems()
    {
        var result = await mRepository.GetAllAsync(true);

        Check.That(result).IsEmpty();

        result = await mRepository.GetAllAsync();

        Check.That(result).IsEmpty();
    }

    [Test]
    public async Task GetAllAsync_WithItems_IncludingDrafts()
    {
        await AddSampleItems();

        var result = await mRepository.GetAllAsync(true);

        Check.That(result).HasSize(2);

        Check.That(result[0].Id).IsEqualTo(new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55"));
        Check.That(result[0].Name).IsEqualTo("The Stin Dry Gin");
        Check.That(result[0].Teaser).IsEqualTo("Styrian Dry Gin");
        Check.That(result[0].Description).IsEqualTo("Made in Styria");
        Check.That(result[0].Images).HasSize(2);
        Check.That(result[0].Images[0].FileName).IsEqualTo("KRO01046.jpg");
        Check.That(result[0].Images[1].FileName).IsEqualTo("KRO00364.jpg");
        Check.That(result[0].IsDraft).IsFalse();

        Check.That(result[1].Id).IsEqualTo(new Guid("01691cd5-1102-4593-9c27-72b567871338"));
        Check.That(result[1].Name).IsEqualTo("The Duke");
        Check.That(result[1].Teaser).IsEmpty();
        Check.That(result[1].Description).IsEmpty();
        Check.That(result[1].Images).IsEmpty();
        Check.That(result[1].IsDraft).IsTrue();
    }

    [Test]
    public async Task GetAllAsync_WithItems_Not_IncludingDrafts()
    {
        await AddSampleItems();

        var result = await mRepository.GetAllAsync();

        Check.That(result).HasSize(1);
        Check.That(result[0].Id).IsEqualTo(new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55"));
        Check.That(result[0].Name).IsEqualTo("The Stin Dry Gin");
        Check.That(result[0].Teaser).IsEqualTo("Styrian Dry Gin");
        Check.That(result[0].Description).IsEqualTo("Made in Styria");
        Check.That(result[0].Images).HasSize(2);
        Check.That(result[0].Images[0].FileName).IsEqualTo("KRO01046.jpg");
        Check.That(result[0].Images[1].FileName).IsEqualTo("KRO00364.jpg");
        Check.That(result[0].IsDraft).IsFalse();
    }


    [Test]
    public async Task GetOrDefaultAsync_NoItem()
    {
        var id = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55");
        var result = await mRepository.GetOrDefaultAsync(id);

        Check.That(result).IsNull();
    }

    [Test]
    public async Task GetOrDefaultAsync_WithItem()
    {
        await AddSampleItems();

        var id = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55");
        var result = await mRepository.GetOrDefaultAsync(id);

        Check.That(result).IsNotNull();
        Check.That(result.Id).IsEqualTo(new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55"));
        Check.That(result.Name).IsEqualTo("The Stin Dry Gin");
        Check.That(result.Teaser).IsEqualTo("Styrian Dry Gin");
        Check.That(result.Description).IsEqualTo("Made in Styria");
        Check.That(result.Images).HasSize(2);
        Check.That(result.Images[0].FileName).IsEqualTo("KRO01046.jpg");
        Check.That(result.Images[1].FileName).IsEqualTo("KRO00364.jpg");
        Check.That(result.IsDraft).IsFalse();
    }


    [Test]
    public async Task DeleteAsync_NotExisting()
    {
        var id = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55");
        var result = await mRepository.DeleteAsync(id);

        Check.That(result).IsFalse();
    }

    [Test]
    public async Task DeleteAsync_Existing()
    {
        await AddSampleItems();

        var id = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55");
        var result = await mRepository.DeleteAsync(id);

        Check.That(result).IsTrue();

        result = await mRepository.DeleteAsync(id);

        Check.That(result).IsFalse();
    }


    [Test]
    public async Task AddOrUpdateAsync_NotExisting()
    {
        var id = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55");

        var entity = new Gin(id, "The Stin Dry Gin-2") {
            Teaser = "Styrian Dry Gin-2",
            Images = new[] { new Image("KRO01046-2.jpg") },
        };

        var result = await mRepository.AddOrUpdateAsync(entity);

        Check.That(result).IsTrue();

        var item = await mRepository.GetOrDefaultAsync(id);

        Check.That(item).IsNotNull();
        Check.That(item.Id).IsEqualTo(new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55"));
        Check.That(item.Name).IsEqualTo("The Stin Dry Gin-2");
        Check.That(item.Teaser).IsEqualTo("Styrian Dry Gin-2");
        Check.That(item.Description).IsEqualTo("");
        Check.That(item.Images).HasSize(1);
        Check.That(item.Images[0].FileName).IsEqualTo("KRO01046-2.jpg");
        Check.That(item.IsDraft).IsTrue();
    }

    [Test]
    public async Task UpdateAsync_Existing()
    {
        await AddSampleItems();

        var id = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55");

        var entity = new Gin(id, "The Stin Dry Gin-2") {
            Teaser = "Styrian Dry Gin-2",
            Description = "Foo",
            Images = new[] { new Image("KRO01046-2.jpg") },
            IsDraft = false,
        };

        var result = await mRepository.AddOrUpdateAsync(entity);

        Check.That(result).IsFalse();

        var item = await mRepository.GetOrDefaultAsync(id);

        Check.That(item).IsNotNull();
        Check.That(item.Id).IsEqualTo(new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55"));
        Check.That(item.Name).IsEqualTo("The Stin Dry Gin-2");
        Check.That(item.Teaser).IsEqualTo("Styrian Dry Gin-2");
        Check.That(item.Description).IsEqualTo("Foo");
        Check.That(item.Images).HasSize(1);
        Check.That(item.Images[0].FileName).IsEqualTo("KRO01046-2.jpg");
        Check.That(item.IsDraft).IsFalse();
    }
}
