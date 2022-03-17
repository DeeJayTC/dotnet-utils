// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TCDev.Utilities.NetCore.Utility;

namespace TCDev.Utilities.NetCore.Controllers;

/// <summary>
///    Example for a controller that can be used to proxy requests to web services from client apps
///    this can be helpful to prevent CORS issues but you should never allow this being used unauthenticated!
///    to use this you need to pass a header "x-requested-for" with the target domain being used
/// </summary>
[Produces("application/json")]
[Route("/proxy/{*url}")]
[Authorize]
public class ProxyController : Controller
{
   [HttpGet]
   public async Task<IActionResult> Get(string url)
   {
      try
      {
         var validatedRequest = ValidateRequest(this.Request);
         if (validatedRequest.HasErrors) return StatusCode(400, validatedRequest.Errors);


         using (var client = CreateClient(this.Request.Headers, validatedRequest.Domain))
         {
            // Call API and return result
            var result = await client.GetAsync(url);

            // Here we ask the framework to dispose the response object a the end of the user resquest
            this.HttpContext.Response.RegisterForDispose(result);
            return new HttpResponseMessageResult(result);
         }
      }
      catch (Exception ex)
      {
         return StatusCode(500, ex);
      }
   }

   [HttpPost]
   public async Task<IActionResult> Post(string url)
   {
      try
      {
         var validatedRequest = ValidateRequest(this.Request);
         if (validatedRequest.HasErrors) return StatusCode(400, validatedRequest.Errors);


         using (var client = CreateClient(this.Request.Headers, validatedRequest.Domain))
         {
            // Call API and return result
            var content = new StreamContent(this.Request.Body);
            var result = await client.PostAsync(url, content);

            // Here we ask the framework to dispose the response object a the end of the user resquest
            this.HttpContext.Response.RegisterForDispose(result);
            return new HttpResponseMessageResult(result);
         }
      }
      catch (Exception ex)
      {
         return StatusCode(500, ex);
      }
   }

   [HttpPut]
   public async Task<IActionResult> Put(string url)
   {
      try
      {
         var validatedRequest = ValidateRequest(this.Request);
         if (validatedRequest.HasErrors) return StatusCode(400, validatedRequest.Errors);


         using (var client = CreateClient(this.Request.Headers, validatedRequest.Domain))
         {
            // Call API and return result
            var content = new StreamContent(this.Request.Body);
            var result = await client.PutAsync(url, content);

            // Here we ask the framework to dispose the response object a the end of the user resquest
            this.HttpContext.Response.RegisterForDispose(result);
            return new HttpResponseMessageResult(result);
         }
      }
      catch (Exception ex)
      {
         return StatusCode(500, ex);
      }
   }

   [HttpPatch]
   public async Task<IActionResult> Patch(string url)
   {
      try
      {
         var validatedRequest = ValidateRequest(this.Request);
         if (validatedRequest.HasErrors) return StatusCode(400, validatedRequest.Errors);


         using (var client = CreateClient(this.Request.Headers, validatedRequest.Domain))
         {
            // Call API and return result
            var content = new StreamContent(this.Request.Body);
            var result = await client.PutAsync(url, content);

            // Here we ask the framework to dispose the response object a the end of the user resquest
            this.HttpContext.Response.RegisterForDispose(result);
            return new HttpResponseMessageResult(result);
         }
      }
      catch (Exception ex)
      {
         return StatusCode(500, ex);
      }
   }

   [HttpDelete]
   public async Task<IActionResult> Delete(string url)
   {
      try
      {
         var validatedRequest = ValidateRequest(this.Request);
         if (validatedRequest.HasErrors) return StatusCode(400, validatedRequest.Errors);

         using var client = CreateClient(this.Request.Headers, validatedRequest.Domain);
         // Call API and return result
         var result = await client.DeleteAsync(url);

         // Here we ask the framework to dispose the response object a the end of the user resquest
         this.HttpContext.Response.RegisterForDispose(result);
         return new HttpResponseMessageResult(result);
      }
      catch (Exception ex)
      {
         return StatusCode(500, ex);
      }
   }

   private HttpClient CreateClient(IHeaderDictionary headers, Uri baseUrl)
   {
      var client = new HttpClient();
      client.BaseAddress = baseUrl;

      // Copy Headers
      client.DefaultRequestHeaders.Add("Authorization", this.Request.Headers["Authorization"].ToString());
      client.DefaultRequestHeaders.UserAgent.ParseAdd(this.Request.Headers["User-Agent"].ToString());
      client.DefaultRequestHeaders.Add("Accept", "Application/Json");
      return client;
   }

   private ProxyRequest ValidateRequest(HttpRequest req)
   {
      try
      {
         var validatedRequest = new ProxyRequest();

         // Get domain header, if not exist discard
         var forDomain = this.Request.Headers["x-requested-for"];
         if (forDomain == string.Empty) validatedRequest.Errors.Add("Need to pass for-domain header");

         var isDomainValid = forDomain != string.Empty && Uri.IsWellFormedUriString(forDomain, UriKind.RelativeOrAbsolute);
         if (!isDomainValid) validatedRequest.Errors.Add("Need to pass a valid domain for x-requested-for");
         if (isDomainValid) validatedRequest.Domain = new Uri(forDomain);

         var authHeader = this.Request.Headers["Authorization"];
         if (authHeader == string.Empty) validatedRequest.Errors.Add("Authentication header missing, pass an empty 'Authentication: ' header for unauthed calls");

         if (validatedRequest.Errors.Count > 0) validatedRequest.HasErrors = true;

         return validatedRequest;
      }
      catch (Exception Ex)
      {
         var res = new ProxyRequest {HasErrors = true};
         res.Errors.Add(Ex.Message);
         return res;
      }
   }
}

public class ProxyRequest
{
   public Uri Domain { get; set; }
   public bool HasErrors { get; set; }
   public List<string> Errors { get; set; } = new();
   public Dictionary<string, string> Headers;
}