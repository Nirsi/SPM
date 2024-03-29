﻿using System.Collections.Generic;
using System.IO;
using System.Net;

using System.Text.Json;
using SPM.Api.Responses;

namespace SPM.Api
{
    public static class Calls
    {
        private const string ApiBase = @"http://api.spiget.org/v2";

        public static IEnumerable<ResourceDetailsResponse> GetResourcesByName(string resourceName)
        {
            //https://api.spiget.org/v2/search/resources/<searched text>
            var apiResponse = GetApiResponse($"search/resources/{resourceName}");
            if (apiResponse == "404") return null;
            var jsonResponse = JsonSerializer.Deserialize<ResourceDetailsResponse[]>(apiResponse);

            return jsonResponse;
        }

        public static ResourceDetailsResponse GetResourceDetails(long resourceId)
        {
            var apiResponse = GetApiResponse($"resources/{resourceId}");
            if (apiResponse == "404") return null;
            var jsonResponse = JsonSerializer.Deserialize<ResourceDetailsResponse>(apiResponse);

            return jsonResponse;
        }

        public static MemoryStream GetResourceJar(long resourceId)
        {
            using var webClient = new WebClient();
            return new MemoryStream(webClient.DownloadData($"https://api.spiget.org/v2/resources/{resourceId}/download"));
        }
        
          
        //private methods
        private static string GetApiResponse(string action)
        {
            var webRequest = WebRequest.Create($"{ApiBase}/{action}");
            WebResponse webResponse = null;
            
            try
            {
                webResponse = webRequest.GetResponse();
            }
            catch (WebException exception)
            {
                if (exception.Status == WebExceptionStatus.ProtocolError)
                {
                    return "404";
                }
            }
            
            var stream = webResponse.GetResponseStream();
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}