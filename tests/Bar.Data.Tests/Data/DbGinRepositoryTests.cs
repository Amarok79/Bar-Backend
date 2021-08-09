// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Threading.Tasks;
using Bar.Domain;
using Microsoft.EntityFrameworkCore;
using NFluent;
using NUnit.Framework;


namespace Bar.Data
{
    [TestFixture]
    public class DbGinRepositoryTests
    {
        private BarDbContext mContext;
        private IGinRepository mRepository;


        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<BarDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options;

            mContext = new BarDbContext(options);
            mContext.Database.EnsureDeleted();
            mContext.Database.EnsureCreated();

            mRepository = new DbGinRepository(mContext);
        }

        [TearDown]
        public void Cleanup()
        {
            mContext.Database.EnsureDeleted();
            mContext.Dispose();
        }

        public async Task AddSampleItems()
        {
            mContext.Gins.Add(
                new GinDbo {
                    Id     = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55"),
                    Name   = "The Stin Dry Gin",
                    Teaser = "Styrian Dry Gin",
                    Images = "KRO01046.jpg;KRO00364.jpg",
                }
            );

            mContext.Gins.Add(
                new GinDbo {
                    Id   = new Guid("01691cd5-1102-4593-9c27-72b567871338"),
                    Name = "The Duke",
                }
            );

            await mContext.SaveChangesAsync();

            mContext.ChangeTracker.Clear();
        }


        [Test]
        public async Task GetAllAsync_NoItems()
        {
            var result = await mRepository.GetAllAsync();

            Check.That(result).IsEmpty();
        }

        [Test]
        public async Task GetAllAsync_WithItems()
        {
            await AddSampleItems();

            var result = await mRepository.GetAllAsync();

            Check.That(result).HasSize(2);

            Check.That(result[0].Id).IsEqualTo(new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55"));
            Check.That(result[0].Name).IsEqualTo("The Stin Dry Gin");
            Check.That(result[0].Teaser).IsEqualTo("Styrian Dry Gin");
            Check.That(result[0].Images).HasSize(2);
            Check.That(result[0].Images[0].FileName).IsEqualTo("KRO01046.jpg");
            Check.That(result[0].Images[1].FileName).IsEqualTo("KRO00364.jpg");

            Check.That(result[1].Id).IsEqualTo(new Guid("01691cd5-1102-4593-9c27-72b567871338"));
            Check.That(result[1].Name).IsEqualTo("The Duke");
            Check.That(result[1].Teaser).IsEmpty();
            Check.That(result[1].Images).IsEmpty();
        }


        [Test]
        public async Task GetOrDefaultAsync_NoItem()
        {
            var id     = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55");
            var result = await mRepository.GetOrDefaultAsync(id);

            Check.That(result).IsNull();
        }

        [Test]
        public async Task GetOrDefaultAsync_WithItem()
        {
            await AddSampleItems();

            var id     = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55");
            var result = await mRepository.GetOrDefaultAsync(id);

            Check.That(result).IsNotNull();
            Check.That(result.Id).IsEqualTo(new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55"));
            Check.That(result.Name).IsEqualTo("The Stin Dry Gin");
            Check.That(result.Teaser).IsEqualTo("Styrian Dry Gin");
            Check.That(result.Images).HasSize(2);
            Check.That(result.Images[0].FileName).IsEqualTo("KRO01046.jpg");
            Check.That(result.Images[1].FileName).IsEqualTo("KRO00364.jpg");
        }


        [Test]
        public async Task DeleteAsync_NotExisting()
        {
            var id     = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55");
            var result = await mRepository.DeleteAsync(id);

            Check.That(result).IsFalse();
        }

        [Test]
        public async Task DeleteAsync_Existing()
        {
            await AddSampleItems();

            var id     = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55");
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
            Check.That(item.Images).HasSize(1);
            Check.That(item.Images[0].FileName).IsEqualTo("KRO01046-2.jpg");
        }

        [Test]
        public async Task UpdateAsync_Existing()
        {
            await AddSampleItems();

            var id = new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55");

            var entity = new Gin(id, "The Stin Dry Gin-2") {
                Teaser = "Styrian Dry Gin-2",
                Images = new[] { new Image("KRO01046-2.jpg") },
            };

            var result = await mRepository.AddOrUpdateAsync(entity);

            Check.That(result).IsFalse();

            var item = await mRepository.GetOrDefaultAsync(id);

            Check.That(item).IsNotNull();
            Check.That(item.Id).IsEqualTo(new Guid("a8ca512d-bb7b-4fd8-9e4d-c9bb12ce2b55"));
            Check.That(item.Name).IsEqualTo("The Stin Dry Gin-2");
            Check.That(item.Teaser).IsEqualTo("Styrian Dry Gin-2");
            Check.That(item.Images).HasSize(1);
            Check.That(item.Images[0].FileName).IsEqualTo("KRO01046-2.jpg");
        }
    }
}
