using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HttpClientExample
{
    public class HttpClientWrapper : IHttpClientWrapper
    {
        private static readonly HttpClient HttpClient;

        /// <summary>
        /// Constructs a new static instance of the <see cref="HttpClientWrapper"/>
        /// </summary>
        static HttpClientWrapper()
        {
            HttpClient = new HttpClient
            {
                DefaultRequestHeaders = { Accept = { new MediaTypeWithQualityHeaderValue("application/json") } },
                Timeout = new TimeSpan(0, 0, 0, 2)
            };
        }

        /// <inheritdoc />
        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            return await HttpClient.SendAsync(request);
        }
    }
}