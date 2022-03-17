// TCDev 2022/03/15
// Apache 2.0 License
// https://www.github.com/deejaytc/dotnet-utils

using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace TCDev.Extensions
{
   /// <summary>
   ///    Extend HTTPRequest with our own functions
   /// </summary>
   public static class RequestExtension
   {
      /// <summary>
      ///    Parse TWRequest Data from the Request
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      public static TWRequestData GetData(this HttpRequest request)
      {
         try
         {
            if (request.IsValid())
            {
               var data = new TWRequestData(request, true);
               return data;
            }
         }
         catch (Exception ex)
         {
            return null;
         }

         return null;
      }

      /// <summary>
      ///    Check if the request is valid
      /// </summary>
      /// <param name="request"></param>
      public static bool IsValid(this HttpRequest request)
      {
         try
         {
            string providername = request.Query["provider"];
            request.HttpContext.Items["csp"] = providername;
            if (string.IsNullOrEmpty(providername)) return false;
            return true;
         }
         catch (Exception ex)
         {
            return false;
         }
      }

      /// <summary>
      ///    Return ProviderName for the request
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      public static string GetProviderName(this HttpRequest request)
      {
         return request.HttpContext.Items["csp"] as string;
      }

      /// <summary>
      ///    Parse extra query parameters in the request
      /// </summary>
      /// <param name="request"></param>
      /// <returns></returns>
      public static ExtraOptions GetExtraOptions(this HttpRequest request)
      {
         try
         {
            if (request.IsValid())
            {
               if (!string.IsNullOrEmpty(request.Query["extraOptions"]))
               {
                  var extraOptions = JsonConvert.DeserializeObject<ExtraOptions>(request.Query["extraOptions"]);
                  return extraOptions;
               }

               return new ExtraOptions();
            }
         }
         catch (Exception ex)
         {
            return null;
         }

         return null;
      }
   }


   /// <summary>
   ///    Returns data specific to Teamwork Requests
   /// </summary>
   public class TWRequestData
   {
      /// <summary>
      /// </summary>
      /// <param name="request"></param>
      /// <param name="isValid"></param>
      public TWRequestData(HttpRequest request, bool isValid)
      {
         Request = request;
      }

      /// <summary>
      ///    Type of Provider the call is for
      /// </summary>
      public HttpRequest Request { get; }
   }

   /// <summary>
   ///    Retreives extra options passed in the Teamwork Requests
   /// </summary>
   public class ExtraOptions : Dictionary<string, string>
   {
      public T GetValue<T>(string key, T defaultValue) // key is case sensitive
      {
         var value = defaultValue;
         try
         {
            if (ContainsKey(key))
            {
               var converter = TypeDescriptor.GetConverter(typeof(T));
               try
               {
                  value = (T) converter.ConvertFromString(this[key]);
               }
               catch (FormatException)
               {
                  return defaultValue;
               }
            }
         }
         catch (NotSupportedException)
         {
            return defaultValue;
         }

         return value;
      }
   }
}