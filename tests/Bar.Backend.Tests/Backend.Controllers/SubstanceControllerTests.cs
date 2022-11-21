// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NFluent;
using NUnit.Framework;


namespace Bar.Backend.Controllers;


[TestFixture]
public class SubstanceControllerTests
{
    private TestServer mServer;
    private FlurlClient mClient;


    [SetUp]
    public void Setup()
    {
        var webHostBuilder = new WebHostBuilder().UseStartup(x => new TestStartup("Substance", x.Configuration));

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
        var rsp = await mClient.Request("/api/substances").GetAsync();

        Check.That(rsp.StatusCode).IsEqualTo(200);

        Check.That(await rsp.GetJsonListAsync()).IsEmpty();
    }

    private async Task _GetSingle_Returns_NotFound()
    {
        var rsp = await mClient.Request("/api/substances/AAA").AllowAnyHttpStatus().GetAsync();

        Check.That(rsp.StatusCode).IsEqualTo(404);
    }

    private async Task _CreateItem_1()
    {
        var rsp = await mClient.Request("/api/substances/AAA")
       .PutJsonAsync(
            new {
                Name = "Grand Marnier",
                Category = "Liqueurs",
                Unit = "cl",
            }
        );

        var dto = await rsp.GetJsonAsync<SubstanceDto>();

        Check.That(rsp.StatusCode).IsEqualTo(200);

        Check.That(dto.Id).IsEqualTo("AAA");

        Check.That(dto.Name).IsEqualTo("Grand Marnier");

        Check.That(dto.Category).IsEqualTo("Liqueurs");

        Check.That(dto.Unit).IsEqualTo("cl");
    }

    private async Task _UpdateItem_1()
    {
        var rsp = await mClient.Request("/api/substances/AAA")
       .PutJsonAsync(
            new {
                Id = Guid.NewGuid(),
                Name = "Grand Marnier-2",
                Category = "Liqueurs-2",
                Unit = "cl-2",
            }
        );

        var dto = await rsp.GetJsonAsync<SubstanceDto>();

        Check.That(rsp.StatusCode).IsEqualTo(200);

        Check.That(dto.Id).IsEqualTo("AAA");

        Check.That(dto.Name).IsEqualTo("Grand Marnier-2");

        Check.That(dto.Category).IsEqualTo("Liqueurs-2");

        Check.That(dto.Unit).IsEqualTo("cl-2");
    }

    private async Task _GetSingle_Returns_Item_1()
    {
        var rsp = await mClient.Request("/api/substances/AAA").GetAsync();

        Check.That(rsp.StatusCode).IsEqualTo(200);

        var dto = await rsp.GetJsonAsync<SubstanceDto>();

        Check.That(rsp.StatusCode).IsEqualTo(200);

        Check.That(dto.Id).IsEqualTo("AAA");

        Check.That(dto.Name).IsEqualTo("Grand Marnier-2");

        Check.That(dto.Category).IsEqualTo("Liqueurs-2");

        Check.That(dto.Unit).IsEqualTo("cl-2");
    }

    private async Task _CreateItem_2()
    {
        var rsp = await mClient.Request("/api/substances/BBB")
       .PutJsonAsync(
            new {
                Name = "Limes",
            }
        );

        var dto = await rsp.GetJsonAsync<SubstanceDto>();

        Check.That(rsp.StatusCode).IsEqualTo(200);

        Check.That(dto.Id).IsEqualTo("BBB");

        Check.That(dto.Name).IsEqualTo("Limes");

        Check.That(dto.Category).IsNull();

        Check.That(dto.Unit).IsNull();
    }

    private async Task _GetAll_Returns_Items()
    {
        var rsp = await mClient.Request("/api/substances").GetAsync();

        Check.That(rsp.StatusCode).IsEqualTo(200);

        var dto = await rsp.GetJsonAsync<SubstanceDto[]>();

        Check.That(dto).HasSize(2);

        Check.That(dto[0].Id).IsEqualTo("AAA");

        Check.That(dto[0].Name).IsEqualTo("Grand Marnier-2");

        Check.That(dto[0].Category).IsEqualTo("Liqueurs-2");

        Check.That(dto[0].Unit).IsEqualTo("cl-2");

        Check.That(dto[1].Id).IsEqualTo("BBB");

        Check.That(dto[1].Name).IsEqualTo("Limes");

        Check.That(dto[1].Category).IsNull();

        Check.That(dto[1].Unit).IsNull();
    }

    private async Task _DeleteItem_1()
    {
        var rsp = await mClient.Request("/api/substances/AAA").DeleteAsync();

        Check.That(rsp.StatusCode).IsEqualTo(204);
    }

    private async Task _DeleteItem_2()
    {
        var rsp = await mClient.Request("/api/substances/BBB").DeleteAsync();

        Check.That(rsp.StatusCode).IsEqualTo(204);
    }

    private async Task _GetAll_ApiKey_Mismatch()
    {
        var rsp = await mClient.Request("/api/substances").WithHeader("Api-Key", "foo").AllowAnyHttpStatus().GetAsync();

        Check.That(rsp.StatusCode).IsEqualTo(401);
    }
}
