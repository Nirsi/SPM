using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

using System.Text.Json;
using System.Text.Json.Serialization;
using SPM.Api.Responses;
using SPM.Api.Responses.Auxiliary;

namespace SPM.Api
{
    public class Calls
    {
        private string _apiAddress = @"http://api.spiget.org/v2";

        public IEnumerable<ResourceDetailsResponse> GetSearchedResources(string resourceName)
        {
            //https://api.spiget.org/v2/search/resources/<searched text>
            var apiResponse = GetApiResponse($"search/resources/{resourceName}");
            if (apiResponse == "404") return null;
            var jsonResponse = JsonSerializer.Deserialize<ResourceDetailsResponse[]>(apiResponse);

            return jsonResponse;
        }

        public ResourceDetailsResponse GetResourceDetails(int resourceId)
        {
            var apiResponse = GetApiResponse($"resources/{resourceId}");
            if (apiResponse == "404") return null;
            var jsonResponse = JsonSerializer.Deserialize<ResourceDetailsResponse>(apiResponse);

            return jsonResponse;
        }
          
        //private methods
        private string GetApiResponse(string action)
        {
            var webRequest = WebRequest.Create($"{_apiAddress}/{action}");
            if (((HttpWebResponse) webRequest.GetResponse()).StatusCode == HttpStatusCode.NotFound)
            {
                return "404";
            }
            var stream = webRequest.GetResponse().GetResponseStream();
            var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
    }
}