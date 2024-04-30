﻿using BeerApp;
using BeerApp.API;
using BeerApp.Output;
using BeerApp.Services;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using Polly;


var configuration = BuildConfiguration();
var serviceProvider = BuildServiceProvider(configuration);
var app = serviceProvider.GetRequiredService<BeerSearchApplication>();

await app.RunAsync(args);

static IConfigurationRoot BuildConfiguration()
{
    var configurationRoot = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build();

    return configurationRoot;
}

static ServiceProvider BuildServiceProvider(IConfigurationRoot configuration)
{
    var services = new ServiceCollection();
    ConfigureServices(configuration, services);
    var serviceProvider = services.BuildServiceProvider();
    return serviceProvider;
}

static void ConfigureServices(IConfigurationRoot configuration, IServiceCollection services)
{
    services.AddSingleton<BeerSearchApplication>();
    services.AddSingleton<IConsoleWriter, ConsoleWriter>();
    services.AddSingleton<IBeerSearchService, BeerSearchService>();

    services.AddValidatorsFromAssemblyContaining<Program>();
    services.AddRefitClient<IPunkIpaApi>()
        .ConfigureHttpClient(httpClient =>
        {
            httpClient.BaseAddress = new Uri(configuration["BeerApi:BaseAddress"]);
        })
        .AddTransientHttpErrorPolicy(builder =>
        {
            return builder.OrResult(response => response.StatusCode == System.Net.HttpStatusCode.NotFound)
                          .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        });
}
