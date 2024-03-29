﻿// Copyright (c) 2022, Olaf Kober <olaf.kober@outlook.com>

using System.Net;


namespace Bar.Backend.Middleware;


internal sealed class ApiKeyMiddleware
{
    private readonly RequestDelegate mNext;


    public ApiKeyMiddleware(
        RequestDelegate next
    )
    {
        mNext = next;
    }


    public Task InvokeAsync(
        HttpContext httpContext
    )
    {
        var expectedApiKey = getApiKeySetting();
        var actualApiKey = getApiKeyHeader();

        if (String.Equals(actualApiKey, expectedApiKey))
        {
            return mNext(httpContext);
        }

        httpContext.Response.StatusCode = (Int32)HttpStatusCode.Unauthorized;

        return Task.CompletedTask;


        String? getApiKeyHeader()
        {
            if (httpContext.Request.Headers.TryGetValue("Api-Key", out var apiKey))
            {
                return apiKey;
            }

            return null;
        }

        String? getApiKeySetting()
        {
            var config = httpContext.RequestServices.GetRequiredService<IConfiguration>();

            return config.GetValue<String>("ApiKey");
        }
    }
}
