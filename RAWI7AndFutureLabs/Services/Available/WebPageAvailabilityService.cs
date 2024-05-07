using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace RAWI7AndFutureLabs.Services.Available
{
    public class WebPageAvailabilityService : BackgroundService
    {
        private readonly string _webPageUrl;
        private readonly string _logFilePath;
        private readonly HttpClient _httpClient;

        public WebPageAvailabilityService(string webPageUrl, string logFilePath, IHttpClientFactory httpClientFactory)
        {
            _webPageUrl = webPageUrl;
            _logFilePath = logFilePath;
            _httpClient = httpClientFactory.CreateClient();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await _httpClient.GetAsync(_webPageUrl);
                var availability = response.IsSuccessStatusCode ? "Available" : "Not Available";

                var logMessage = $"{DateTime.UtcNow}: Web Page {_webPageUrl} is {availability}";
                File.AppendAllText(_logFilePath, logMessage + Environment.NewLine);

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }
}