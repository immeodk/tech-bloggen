using System;

namespace StarWarsApi.Functions.StarWarsApi;

public record StarWarsApiSettings
{
    public Uri BaseUrl { get; set; } = new("https://swapi.dev"); // Sæt eventuel default værdi
}