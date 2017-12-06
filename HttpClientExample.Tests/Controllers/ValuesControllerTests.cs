using Moq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using HttpClientExample.Controllers;
using Microsoft.Extensions.Logging;
using Xunit;

namespace HttpClientExample.Tests.Controllers
{
    public class ValuesControllerTests
    {
        [Fact]
        public async Task Get_ReturnsNotOkay_WhenHttpStatusCodeNot200()
        {
            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            var sut = new ValuesController(mockHttpClientWrapper.Object, Mock.Of<ILogger<ValuesController>>());

            var result = await sut.Get();

            Assert.Equal("not ok", result);    
        }

        [Fact]
        public async Task Get_ReturnsOkay_WhenHttpStatusCode200()
        {
            var mockHttpClientWrapper = new Mock<IHttpClientWrapper>();
            mockHttpClientWrapper.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            var sut = new ValuesController(mockHttpClientWrapper.Object, Mock.Of<ILogger<ValuesController>>());

            var result = await sut.Get();

            Assert.Equal("ok", result);
        }
    }
}