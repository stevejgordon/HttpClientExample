using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;

namespace HttpClientExample.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IHttpClientWrapper _httpClientWrapper;
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(IHttpClientWrapper httpClientWrapper, ILogger<ValuesController> logger)
        {
            _httpClientWrapper = httpClientWrapper;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public async Task<string> Get()
        {
            const string url = "https://www.google.com";

            // normally this would be defined more centrally but including here for a quick example
            var retryPolicy = Policy
                .Handle<HttpRequestException>()
                .WaitAndRetryAsync(2, retryCount => TimeSpan.FromSeconds(2),
                    (exception, timeSpan, retryCount, context) =>
                    {
                        _logger.LogWarning(
                            $"Call to {url} failed - Retry {retryCount} : TimeSpan {timeSpan} : Ex {exception.Message}");
                    });

            var response = await retryPolicy.ExecuteAsync(() =>
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                return _httpClientWrapper.SendAsync(request);
            });

            return response.IsSuccessStatusCode ? "ok" : "not ok";
        }
    }
}