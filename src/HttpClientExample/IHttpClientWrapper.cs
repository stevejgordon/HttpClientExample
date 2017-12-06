using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientExample
{
    public interface IHttpClientWrapper
    {
        /// <summary>
        /// Wraps the HttpClient.SendAsync method
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<HttpResponseMessage> SendAsync(HttpRequestMessage request);
    }
}