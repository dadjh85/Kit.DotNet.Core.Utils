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
        public void when_get_remote_ip_address_of_httpContext_object()
        {
            var httpContext = new DefaultHttpContext()
            {
                Connection = { RemoteIpAddress = new IPAddress(16885952) }
            };

            httpContext.GetRemoteIpAddress().Should().Be("192.168.1.1");
        }

        [Fact]
        public void when_get_remote_ip_address_of_httpContext_object_with_property_connection_null()
        {
            Mock<HttpContext> httpContext = new Mock<HttpContext>();

            httpContext.Object.GetRemoteIpAddress().Should().Be("");
        }

        [Fact]
        public void when_get_remote_ip_address_of_httpContext_object_with_null_value()
        {
            HttpContext httpContext = null;

            httpContext.GetRemoteIpAddress().Should().Be("");
        }

        [Fact]
        public void when_get_local_ip_address_of_httpContext_object()
        {
            var httpContext = new DefaultHttpContext()
            {
                Connection = { LocalIpAddress = new IPAddress(16885952) }
            };

            httpContext.GetLocalIpAddress().Should().Be("192.168.1.1");
        }

        [Fact]
        public void when_get_local_ip_address_of_httpContext_object_with_property_connection_null()
        {
            Mock<HttpContext> httpContext = new Mock<HttpContext>();

            httpContext.Object.GetLocalIpAddress().Should().Be("");
        }

        [Fact]
        public void when_get_local_ip_address_of_httpContext_object_with_null_value()
        {
            HttpContext httpContext = null;

            httpContext.GetLocalIpAddress().Should().Be("");
        }

        [Fact]
        public void when_get_EncodeUrl_of_httpContext()
        {
            var httpContext = new DefaultHttpContext();
            httpContext.Request.Path = "/localhost";

            httpContext.GetEncodedUrl().Should().Be(":///localhost");
        }

        [Fact]
        public void when_get_EncodeUrl_of_httpContext_with_null_value()
        {
            HttpContext httpContext = null;

            httpContext.GetEncodedUrl().Should().BeNull();
        }
    }
}
