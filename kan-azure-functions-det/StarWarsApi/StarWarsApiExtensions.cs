using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using System;
using System.Net;

namespace StarWarsApi.Functions.StarWarsApi;

public static class StarWarsApiExtensions
{
    public static IServiceCollection AddStarWarsApiFeature(this IServiceCollection services, IConfiguration configuration)
        =>
            services
                .Configure<StarWarsApiSettings>(configuration)
                .AddHttpClient<IStarWarsApiClient, StarWarsApiClient>()
                    .AddTransientHttpErrorPolicy(policy =>
                        policy
                            .OrResult(res => res.StatusCode == HttpStatusCode.TooManyRequests)
                            .WaitAndRetryAsync(5, retryNumber =>
                                TimeSpan.FromMilliseconds(Math.Pow(2, retryNumber) * 50 + Random.Shared.Next(1000))
                            )
                    )
                .Services;
}