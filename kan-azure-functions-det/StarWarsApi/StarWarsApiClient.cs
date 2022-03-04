using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace StarWarsApi.Functions.StarWarsApi;

public class StarWarsApiClient : IStarWarsApiClient
{
    private readonly HttpClient _httpClient;

    public StarWarsApiClient(HttpClient httpClient, IOptions<StarWarsApiSettings> starWarsApiSettings)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = starWarsApiSettings.Value.BaseUrl;
    }

    public async Task<string> GetPersonNameById(int id, CancellationToken cancellationToken = default)
    {
        var httpResponse = await _httpClient.GetAsync($"/api/people/{id}/", cancellationToken);
        httpResponse.EnsureSuccessStatusCode();
        var response = await httpResponse.Content.ReadAsAsync<StarWarsPersonResponse>(cancellationToken);
        return response.name;
    }

    private record StarWarsPersonResponse(string name);
}