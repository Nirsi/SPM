using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using SPM.Api.Responses.Auxiliary;
using Version = SPM.Api.Responses.Auxiliary.Version;

namespace SPM.Api.Responses
{
    public class ResourceDetailsResponse
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
        
        [JsonPropertyName("name")] 
        public string Name { get; set; }
        public string tag { get; set; }
        [JsonPropertyName("version")]
        public Version Version { get; set; }
        public string contributors { get; set; }
        public long likes { get; set; }
        public File file { get; set; }
        
        [JsonPropertyName("testedVersions")]
        public List<string> TestedVersions { get; set; }
        public Links links { get; set; }
        public Rating rating { get; set; }
        public long releaseDate { get; set; }
        public long updateDate { get; set; }
        public long downloads { get; set; }
        public bool external { get; set; }
        public Icon icon { get; set; }
        public bool premium { get; set; }
        public long price { get; set; }
        public string currency { get; set; }
        public List<Review> reviews { get; set; }
        
        [JsonPropertyName("versions")]
        public List<Version> versions { get; set; }
    }
}