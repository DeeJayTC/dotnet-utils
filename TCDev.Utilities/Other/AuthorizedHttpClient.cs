// TCDev 2022/03/17
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TeamworkIntegrationFramework.Utility;

public class AuthorizedHttpClient : HttpClient
{
   /// <summary>
   ///    Initialize a new Instance of the Client
   /// </summary>
   /// <param name="pApiKey">APIKey for Projects API</param>
   /// <param name="pBaseuri"></param>
   public AuthorizedHttpClient(string pApiKey, Uri pBaseuri)
   {
      this.BaseAddress = pBaseuri;
      this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{pApiKey}:x")));
      this.DefaultRequestHeaders.Accept.Clear();
      this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      this.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident / 6.0)");
   }

   /// <summary>
   ///    Initialize a new Instance of the Client
   /// </summary>
   /// <param name="pApiKey">APIKey for Projects API</param>
   /// <param name="pBaseuri"></param>
   public AuthorizedHttpClient(string pApiKey, Uri pBaseuri, HttpMessageHandler handler) : base(handler)
   {
      this.BaseAddress = pBaseuri;
      this.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{pApiKey}:x")));
      this.DefaultRequestHeaders.Accept.Clear();
      this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      this.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident / 6.0)");
   }

   public AuthorizedHttpClient()
   {
      this.DefaultRequestHeaders.Accept.Clear();
      this.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      this.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident / 6.0)");
   }
}