using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.OpenApi.Models;

namespace StarWarsApi.Functions.StarWarsApi;

public class StarWarsFunctions
{
    private readonly IStarWarsApiClient _starWarsApiClient;

    public StarWarsFunctions(IStarWarsApiClient starWarsApiClient) => _starWarsApiClient = starWarsApiClient;

    [FunctionName(nameof(GetStarWarsPersonNameById))]
    [OpenApiOperation(nameof(GetStarWarsPersonNameById), "StarWars Personer", Summary = "Få navnet for Star Wars karakteren med det angivne ID", Visibility = OpenApiVisibilityType.Important)]
    [OpenApiParameter("id", Type = typeof(int), Required = true, In = ParameterLocation.Path)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string))]
    public async Task<string> GetStarWarsPersonNameById(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "starwars/person/{id:int}")] HttpRequest request,
        int id,
        CancellationToken cancellationToken
    ) => await _starWarsApiClient.GetPersonNameById(id, cancellationToken);
}