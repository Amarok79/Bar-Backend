// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Threading.Tasks;
using Bar.Backend.Controllers;
using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NFluent;
using NUnit.Framework;


namespace Bar.Backend.Tests.Controllers
{
    [TestFixture]
    public class RumControllerTests
    {
        private TestServer mServer;
        private FlurlClient mClient;


        [SetUp]
        public void Setup()
        {
            var webHostBuilder = new WebHostBuilder().UseStartup<TestStartup>();

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
        }


        private async Task _GetAll_Returns_EmptyList()
        {
            var rsp = await mClient.Request("/api/rums").GetAsync();

            Check.That(rsp.StatusCode).IsEqualTo(200);
            Check.That(await rsp.GetJsonListAsync()).IsEmpty();
        }

        private async Task _GetSingle_Returns_NotFound()
        {
            var rsp = await mClient.Request("/api/rums/6983cc47-047b-4e7c-8f17-af292ed80bd1")
               .AllowAnyHttpStatus()
               .GetAsync();

            Check.That(rsp.StatusCode).IsEqualTo(404);
        }

        private async Task _CreateItem_1()
        {
            var rsp = await mClient.Request("/api/rums/6983cc47-047b-4e7c-8f17-af292ed80bd1")
               .PutJsonAsync(
                    new {
                        Name   = "Clément Rhum Blanc",
                        Teaser = "Martinique",
                        Images = new[] { "KRO01084.jpg", "KRO00410.jpg" },
                    }
                );

            var dto = await rsp.GetJsonAsync<RumDto>();

            Check.That(rsp.StatusCode).IsEqualTo(200);
            Check.That(dto.Id).IsEqualTo(new Guid("6983cc47-047b-4e7c-8f17-af292ed80bd1"));
            Check.That(dto.Name).IsEqualTo("Clément Rhum Blanc");
            Check.That(dto.Teaser).IsEqualTo("Martinique");
            Check.That(dto.Images).ContainsExactly("KRO01084.jpg", "KRO00410.jpg");
        }

        private async Task _UpdateItem_1()
        {
            var rsp = await mClient.Request("/api/rums/6983cc47-047b-4e7c-8f17-af292ed80bd1")
               .PutJsonAsync(
                    new {
                        Id     = Guid.NewGuid(),
                        Name   = "Clément Rhum Blanc-2",
                        Teaser = "Martinique-2",
                        Images = new[] { "KRO01084.jpg", "KRO00410.jpg", "FOO.jpg" },
                    }
                );

            var dto = await rsp.GetJsonAsync<RumDto>();

            Check.That(rsp.StatusCode).IsEqualTo(200);
            Check.That(dto.Id).IsEqualTo(new Guid("6983cc47-047b-4e7c-8f17-af292ed80bd1"));
            Check.That(dto.Name).IsEqualTo("Clément Rhum Blanc-2");
            Check.That(dto.Teaser).IsEqualTo("Martinique-2");
            Check.That(dto.Images).ContainsExactly("KRO01084.jpg", "KRO00410.jpg", "FOO.jpg");
        }

        private async Task _GetSingle_Returns_Item_1()
        {
            var rsp = await mClient.Request("/api/rums/6983cc47-047b-4e7c-8f17-af292ed80bd1").GetAsync();

            Check.That(rsp.StatusCode).IsEqualTo(200);

            var dto = await rsp.GetJsonAsync<RumDto>();

            Check.That(rsp.StatusCode).IsEqualTo(200);
            Check.That(dto.Id).IsEqualTo(new Guid("6983cc47-047b-4e7c-8f17-af292ed80bd1"));
            Check.That(dto.Name).IsEqualTo("Clément Rhum Blanc-2");
            Check.That(dto.Teaser).IsEqualTo("Martinique-2");
            Check.That(dto.Images).ContainsExactly("KRO01084.jpg", "KRO00410.jpg", "FOO.jpg");
        }

        private async Task _CreateItem_2()
        {
            var rsp = await mClient.Request("/api/rums/f942f025-7970-4990-84b7-68afba4fc341")
               .PutJsonAsync(
                    new {
                        Name = "J. Wray Silver",
                    }
                );

            var dto = await rsp.GetJsonAsync<RumDto>();

            Check.That(rsp.StatusCode).IsEqualTo(200);
            Check.That(dto.Id).IsEqualTo(new Guid("f942f025-7970-4990-84b7-68afba4fc341"));
            Check.That(dto.Name).IsEqualTo("J. Wray Silver");
            Check.That(dto.Teaser).IsEmpty();
            Check.That(dto.Images).IsEmpty();
        }

        private async Task _GetAll_Returns_Items()
        {
            var rsp = await mClient.Request("/api/rums").GetAsync();

            Check.That(rsp.StatusCode).IsEqualTo(200);

            var dto = await rsp.GetJsonAsync<RumDto[]>();

            Check.That(dto).HasSize(2);

            Check.That(dto[0].Id).IsEqualTo(new Guid("6983cc47-047b-4e7c-8f17-af292ed80bd1"));
            Check.That(dto[0].Name).IsEqualTo("Clément Rhum Blanc-2");
            Check.That(dto[0].Teaser).IsEqualTo("Martinique-2");
            Check.That(dto[0].Images).ContainsExactly("KRO01084.jpg", "KRO00410.jpg", "FOO.jpg");

            Check.That(dto[1].Id).IsEqualTo(new Guid("f942f025-7970-4990-84b7-68afba4fc341"));
            Check.That(dto[1].Name).IsEqualTo("J. Wray Silver");
            Check.That(dto[1].Teaser).IsEmpty();
            Check.That(dto[1].Images).IsEmpty();
        }

        private async Task _DeleteItem_1()
        {
            var rsp = await mClient.Request("/api/rums/6983cc47-047b-4e7c-8f17-af292ed80bd1").DeleteAsync();

            Check.That(rsp.StatusCode).IsEqualTo(204);
        }

        private async Task _DeleteItem_2()
        {
            var rsp = await mClient.Request("/api/rums/f942f025-7970-4990-84b7-68afba4fc341").DeleteAsync();

            Check.That(rsp.StatusCode).IsEqualTo(204);
        }
    }
}
