using System.Text.Json.Serialization;

namespace SPM.Api.Responses.Auxiliary
{
    public class Version
    {
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}