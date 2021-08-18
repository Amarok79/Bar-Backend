// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NFluent;
using NUnit.Framework;


namespace Bar.Backend.Controllers
{
    [TestFixture]
    public class GinControllerTests
    {
        private TestServer mServer;
        private FlurlClient mClient;


        [SetUp]
        public void Setup()
        {
            var webHostBuilder = new WebHostBuilder().UseStartup(x => new TestStartup("Gin", x.Configuration));

            mServer = new TestServer(webHostBuilder);
            mClient = new FlurlClient(mServer.CreateClient());
        }

        [TearDown]
        public void Cleanup()
        {
            mClient.Dispose();
            mServer.Dispose();
        }


        [Test]
        public async Task Usage()
        {
            await _GetAll_Returns_EmptyList();
            await _GetSingle_Returns_NotFound();
            await _CreateItem_1();
            await _UpdateItem_1();
            await _GetSingle_Returns_Item_1();
            await _CreateItem_2();
            await _GetAll_Returns_Items();
            await _DeleteItem_1();
            await _GetSingle_Returns_NotFound();
            await _DeleteItem_1();
            await _DeleteItem_2();
            await _GetAll_Returns_EmptyList();
            await _GetAll_ApiKey_Mismatch();
        }


        private async Task _GetAll_Returns_EmptyList()
        {
            var rsp = await mClient.Request("/api/gins").GetAsync();

            Check.That(rsp.StatusCode).IsEqualTo(200);
            Check.That(await rsp.GetJsonListAsync()).IsEmpty();
        }

        private async Task _GetSingle_Returns_NotFound()
        {
            var rsp = await mClient.Request("/api/gins/6983cc47-047b-4e7c-8f17-af292ed80bd1")
               .AllowAnyHttpStatus()
               .GetAsync();

            Check.That(rsp.StatusCode).IsEqualTo(404);
        }

        private async Task _CreateItem_1()
        {
            var rsp = await mClient.Request("/api/gins/6983cc47-047b-4e7c-8f17-af292ed80bd1")
               .PutJsonAsync(
                    new {
                        Name   = "The Stin Dry Gin",
                        Teaser = "Styrian Dry Gin",
                        Images = new[] { "KRO01046.jpg", "KRO00364.jpg" },
                    }
                );

            var dto = await rsp.GetJsonAsync<GinDto>();

            Check.That(rsp.StatusCode).IsEqualTo(200);
            Check.That(dto.Id).IsEqualTo(new Guid("6983cc47-047b-4e7c-8f17-af292ed80bd1"));
            Check.That(dto.Name).IsEqualTo("The Stin Dry Gin");
            Check.That(dto.Teaser).IsEqualTo("Styrian Dry Gin");
            Check.That(dto.Images).ContainsExactly("KRO01046.jpg", "KRO00364.jpg");
        }

        private async Task _UpdateItem_1()
        {
            var rsp = await mClient.Request("/api/gins/6983cc47-047b-4e7c-8f17-af292ed80bd1")
               .PutJsonAsync(
                    new {
                        Id     = Guid.NewGuid(),
                        Name   = "The Stin Dry Gin-2",
                        Teaser = "Styrian Dry Gin-2",
                        Images = new[] { "KRO01046.jpg", "KRO00364.jpg", "FOO.jpg" },
                    }
                );

            var dto = await rsp.GetJsonAsync<GinDto>();

            Check.That(rsp.StatusCode).IsEqualTo(200);
            Check.That(dto.Id).IsEqualTo(new Guid("6983cc47-047b-4e7c-8f17-af292ed80bd1"));
            Check.That(dto.Name).IsEqualTo("The Stin Dry Gin-2");
            Check.That(dto.Teaser).IsEqualTo("Styrian Dry Gin-2");
            Check.That(dto.Images).ContainsExactly("KRO01046.jpg", "KRO00364.jpg", "FOO.jpg");
        }

        private async Task _GetSingle_Returns_Item_1()
        {
            var rsp = await mClient.Request("/api/gins/6983cc47-047b-4e7c-8f17-af292ed80bd1").GetAsync();

            Check.That(rsp.StatusCode).IsEqualTo(200);

            var dto = await rsp.GetJsonAsync<GinDto>();

            Check.That(rsp.StatusCode).IsEqualTo(200);
            Check.That(dto.Id).IsEqualTo(new Guid("6983cc47-047b-4e7c-8f17-af292ed80bd1"));
            Check.That(dto.Name).IsEqualTo("The Stin Dry Gin-2");
            Check.That(dto.Teaser).IsEqualTo("Styrian Dry Gin-2");
            Check.That(dto.Images).ContainsExactly("KRO01046.jpg", "KRO00364.jpg", "FOO.jpg");
        }

        private async Task _CreateItem_2()
        {
            var rsp = await mClient.Request("/api/gins/f942f025-7970-4990-84b7-68afba4fc341")
               .PutJsonAsync(
                    new {
                        Name = "Toplitz Gin",
                    }
                );

            var dto = await rsp.GetJsonAsync<GinDto>();

            Check.That(rsp.StatusCode).IsEqualTo(200);
            Check.That(dto.Id).IsEqualTo(new Guid("f942f025-7970-4990-84b7-68afba4fc341"));
            Check.That(dto.Name).IsEqualTo("Toplitz Gin");
            Check.That(dto.Teaser).IsEmpty();
            Check.That(dto.Images).IsEmpty();
        }

        private async Task _GetAll_Returns_Items()
        {
            var rsp = await mClient.Request("/api/gins").GetAsync();

            Check.That(rsp.StatusCode).IsEqualTo(200);

            var dto = await rsp.GetJsonAsync<GinDto[]>();

            Check.That(dto).HasSize(2);

            Check.That(dto[0].Id).IsEqualTo(new Guid("6983cc47-047b-4e7c-8f17-af292ed80bd1"));
            Check.That(dto[0].Name).IsEqualTo("The Stin Dry Gin-2");
            Check.That(dto[0].Teaser).IsEqualTo("Styrian Dry Gin-2");
            Check.That(dto[0].Images).ContainsExactly("KRO01046.jpg", "KRO00364.jpg", "FOO.jpg");

            Check.That(dto[1].Id).IsEqualTo(new Guid("f942f025-7970-4990-84b7-68afba4fc341"));
            Check.That(dto[1].Name).IsEqualTo("Toplitz Gin");
            Check.That(dto[1].Teaser).IsEmpty();
            Check.That(dto[1].Images).IsEmpty();
        }

        private async Task _DeleteItem_1()
        {
            var rsp = await mClient.Request("/api/gins/6983cc47-047b-4e7c-8f17-af292ed80bd1").DeleteAsync();

            Check.That(rsp.StatusCode).IsEqualTo(204);
        }

        private async Task _DeleteItem_2()
        {
            var rsp = await mClient.Request("/api/gins/f942f025-7970-4990-84b7-68afba4fc341").DeleteAsync();

            Check.That(rsp.StatusCode).IsEqualTo(204);
        }

        private async Task _GetAll_ApiKey_Mismatch()
        {
            var rsp = await mClient.Request("/api/gins").WithHeader("Api-Key", "foo").AllowAnyHttpStatus().GetAsync();

            Check.That(rsp.StatusCode).IsEqualTo(401);
        }
    }
}
