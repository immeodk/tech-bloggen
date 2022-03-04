using System.Threading;
using System.Threading.Tasks;

namespace StarWarsApi.Functions.StarWarsApi;

public interface IStarWarsApiClient
{
    Task<string> GetPersonNameById(int id, CancellationToken cancellationToken = default);
}