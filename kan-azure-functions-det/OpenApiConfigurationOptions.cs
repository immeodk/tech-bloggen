using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace StarWarsApi.Functions;

public class OpenApiConfigurationOptions : DefaultOpenApiConfigurationOptions
{
    public override OpenApiInfo Info { get; set; } = new()
    {
        Version = "0.1.0",
        Title = "Star Wars API",
        Description = "Et fantastisk API der dækker alle dine Star Wars behov. Prøv det inden din nabo."
    };

    public override OpenApiVersionType OpenApiVersion { get; set; } = OpenApiVersionType.V3;
}