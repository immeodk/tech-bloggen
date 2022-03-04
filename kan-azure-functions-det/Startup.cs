using System;
using System.IO;
using Azure.Identity;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using StarWarsApi.Functions;
using StarWarsApi.Functions.StarWarsApi;

[assembly: FunctionsStartup(typeof(Startup))]
namespace StarWarsApi.Functions;

public class Startup : FunctionsStartup
{
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
    {
        var context = builder.GetContext();
        var environmentName = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT");

        var configBuilder = builder.ConfigurationBuilder
            .AddJsonFile(Path.Combine(context.ApplicationRootPath, "appsettings.json"), optional: false)
            .AddJsonFile(Path.Combine(context.ApplicationRootPath, $"appsettings.{environmentName}.json"), optional: true)
            .AddEnvironmentVariables()
            .AddUserSecrets<Startup>(true);

        var temporaryConfig = configBuilder.Build();

        if (!temporaryConfig.GetValue("KeyVault:UseKeyVault", true)) return;
        var keyVaultUrl = temporaryConfig.GetValue<Uri>("KeyVault:Url");
        configBuilder.AddAzureKeyVault(keyVaultUrl, new DefaultAzureCredential());
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {
        var configuration = builder.GetContext().Configuration;
        builder.Services
            .AddStarWarsApiFeature(configuration.GetSection("StarWarsApi"));
    }
}