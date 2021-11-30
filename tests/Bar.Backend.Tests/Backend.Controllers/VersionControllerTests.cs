// Copyright (c) 2021, Olaf Kober <olaf.kober@outlook.com>

using System;
using System.Threading.Tasks;
using Flurl.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NFluent;
using NUnit.Framework;


namespace Bar.Backend.Controllers;

[TestFixture]
public class VersionControllerTests
{
    private TestServer mServer;
    private FlurlClient mClient;


    [SetUp]
    public void Setup()
    {
        var webHostBuilder = new WebHostBuilder().UseStartup(x => new TestStartup("System", x.Configuration));

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
        await _Get();
    }


    private async Task _Get()
    {
        var rsp = await mClient.Request("/api/version")
           .GetAsync();

        Check.That(rsp.StatusCode)
           .IsEqualTo(200);

        var json = await rsp.GetJsonAsync<VersionDto>();

        Check.That(json.ServerVersion)
           .IsNotNull();
    }
}
