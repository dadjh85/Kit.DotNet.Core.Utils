using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;
using Kit.DotNet.Core.Utils.Extensions.Http;
using System.Net;
using FluentAssertions;

namespace UnitTests.Extensions
{
    public class HttpContextExtensionTest
    {
        [Fact]
        public void when_get_ip_address_of_httpContext_object()
        {
            var httpContext = new DefaultHttpContext()
            {
                Connection = { RemoteIpAddress = new IPAddress(16885952) }
            };

            httpContext.GetRemoteIpAddress().Should().Be("192.168.1.1");
        }

        [Fact]
        public void when_get_ip_address_of_httpContext_object_with_property_connection_null()
        {
            Mock<HttpContext> httpContext = new Mock<HttpContext>();

            httpContext.Object.GetRemoteIpAddress().Should().Be("");
        }

        [Fact]
        public void when_get_ip_address_of_httpContext_object_with_null_value()
        {
            HttpContext httpContext = null;

            httpContext.GetRemoteIpAddress().Should().Be("");
        }
    }
}
