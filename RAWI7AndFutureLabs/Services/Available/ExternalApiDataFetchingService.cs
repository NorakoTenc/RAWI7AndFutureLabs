using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
namespace RAWI7AndFutureLabs.Services.Available
{
     public class ExternalApiDataFetchingService : BackgroundService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;

        public ExternalApiDataFetchingService(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _httpClient = httpClientFactory.CreateClient();
            _cache = cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var result = await _cache.GetOrCreateAsync("ExchangeRates", async entry =>
                {
                    entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                    return await FetchExchangeRatesAsync();
                });

                Console.WriteLine($"Exchange rates: {result}");

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }

        private async Task<string> FetchExchangeRatesAsync()
        {
            var response = await _httpClient.GetAsync("https://api.exchangeratesapi.io/latest");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }

}
