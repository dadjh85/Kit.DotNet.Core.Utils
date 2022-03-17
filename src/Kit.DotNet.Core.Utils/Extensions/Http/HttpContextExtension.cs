using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kit.DotNet.Core.Utils.Extensions.Http
{
    public static class HttpContextExtension
    {
        /// <summary>
        /// Get Ip Address remote from HttpContext.
        /// </summary>
        /// <param name="context">HttpContext object type</param>
        /// <returns>String object type</returns>
        public static string GetRemoteIpAddress(this HttpContext context)
        {
            string result = string.Empty;

            if (context.Connection.RemoteIpAddress != null)
            {
                result = context.Connection.RemoteIpAddress.ToString();
            }

            return result;
        }

        /// <summary>
        /// Get Ip Adress local from HttpContext.
        /// </summary>
        /// <param name="context">HttpContext object type</param>
        /// <returns>String object type</returns>
        public static string GetLocalIpAddress(this HttpContext context)
        {
            string result = string.Empty;

            if (context.Connection.LocalIpAddress != null)
            {
                result = context.Connection.LocalIpAddress.ToString();
            }

            return result;
        }

        /// <summary>
        /// Get Url Encode from HttpContext.
        /// </summary>
        /// <param name="context">HttpContext object type</param>
        /// <returns>String object type</returns>
        public static string GetEncodedUrl(this HttpContext context)
            => context.Request.GetEncodedUrl();

        /// <summary>
        /// Get CorrelationId of HttpContext.
        /// </summary>
        /// <param name="httpContext">HttpContext object type</param>
        /// <returns>String object type</returns>
        public static string GetTraceIdentifier(this HttpContext httpContext)
        {
            StringValues result;

            httpContext.Response.Headers.TryGetValue("CorrelationId", out result);

            return result;
        }

        /// <summary>
        /// Get Hedares of HttpContext.
        /// </summary>
        /// <param name="context">HttpContext object type</param>
        /// <returns>String object type</returns>
        public static string GetHeaders(this HttpContext context)
            => string.Join(";", context.Request.Headers.Select(x => x.Key + "=" + x.Value).ToArray());

        /// <summary>
        /// Extract the body of the request, from the Httpcontext object.
        /// </summary>
        /// <param name="context">HttpContext object type</param>
        /// <returns>Async String object type</returns>
        public static async Task<string> GetBodyRequestToStringAsync(this HttpContext context)
        {
            //Enable search
            context.Request.EnableBuffering();

            string request = await new StreamReader(context.Request.Body).ReadToEndAsync();

            //Set the position of the stream to 0 to enable rereading
            context.Request.Body.Position = 0;

            return request;
        }

        /// <summary>
        /// Check a response is Ok of httpcontext
        /// </summary>
        /// <param name="httpContext">a httpcontext for checked</param>
        /// <returns>a bool with the result</returns>
        public static bool IsCorrectHttpStatusCode(this HttpContext httpContext)
           => httpContext.Response.StatusCode > 199 && httpContext.Response.StatusCode < 300;

        /// <summary>
        /// Init response.
        /// </summary>
        /// <param name="httpContext">HttpContext object type</param>
        /// <param name="statusCode">Int object type</param>
        /// <returns>HttpContext object type</returns>
        public static HttpContext InitResponse(this HttpContext httpContext, int statusCode)
        {
            httpContext.Response.Clear();
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.Headers.Add("Content-Type", "application/json");

            return httpContext;
        }
    }
}
