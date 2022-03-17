using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TCDev.Utilities.NetCore.Utility
{
    public class HttpResponseMessageResult : IActionResult
    {
        private readonly HttpResponseMessage _responseMessage;

        public HttpResponseMessageResult(HttpResponseMessage responseMessage)
        {
            _responseMessage = responseMessage; // could add throw if null
        }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var response = context.HttpContext.Response;


            if (_responseMessage == null)
            {
                var message = "Response message cannot be null";

                throw new InvalidOperationException(message);
            }

            using (_responseMessage)
            {
                response.StatusCode = (int)_responseMessage.StatusCode;

                var responseFeature = context.HttpContext.Features.Get<IHttpResponseFeature>();
                if (responseFeature != null)
                {
                    responseFeature.ReasonPhrase = _responseMessage.ReasonPhrase;
                }

                var responseHeaders = _responseMessage.Headers;

                // Ignore the Transfer-Encoding header if it is just "chunked".
                // We let the host decide about whether the response should be chunked or not.
                if (responseHeaders.TransferEncodingChunked == true &&
                    responseHeaders.TransferEncoding.Count == 1)
                {
                    responseHeaders.TransferEncoding.Clear();
                }

                foreach (var header in responseHeaders)
                {
                    if (!response.Headers.ContainsKey(header.Key))
                    {
                        response.Headers.Append(header.Key, header.Value.ToArray());
                    }
                    else if (response.Headers.ContainsKey(header.Key) && header.Key != "Set-Cookie")
                    {
                        response.Headers[header.Key] = header.Value.ToArray();
                    }
                    //if (header.Key == "Set-Cookie")
                    //{
                    //    Match match = Regex.Match(string.Join(",", header.Value), "(.+?)=(.+?);");
                    //    if (match.Captures.Count > 0)
                    //    {
                    //        response.Cookies.Append(
                    //            match.Groups[1].Value,
                    //            match.Groups[2].Value,
                    //            new CookieOptions()
                    //            {
                    //                HttpOnly = true,
                    //                Domain = context.HttpContext.Request.Headers["x-requested-for"].ToString(),
                    //                Path = "/",
                    //                Secure = true,
                    //                Expires = new DateTimeOffset(DateTime.Now.AddHours(1))
                    //            }
                    //        );
                    //    }
                    //}
                    //else
                    //{

                    //}
                }

                if (_responseMessage.Content != null)
                {
                    var contentHeaders = _responseMessage.Content.Headers;

                    // Copy the response content headers only after ensuring they are complete.
                    // We ask for Content-Length first because HttpContent lazily computes this
                    // and only afterwards writes the value into the content headers.
                    var unused = contentHeaders.ContentLength;

                    foreach (var header in contentHeaders)
                    {
                        response.Headers.Append(header.Key, header.Value.ToArray());
                    }

                    await _responseMessage.Content.CopyToAsync(response.Body);
                }
            }
        }
    }
}
