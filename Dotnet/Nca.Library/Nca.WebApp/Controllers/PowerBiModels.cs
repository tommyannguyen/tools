using Newtonsoft.Json;
using System;

namespace Nca.WebApp.Models
{
    public class PowerBiEmbedToken
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "tokenId")]
        public string TokenId { get; set; }

        [JsonProperty(PropertyName = "expiration")]
        public DateTime? Expiration { get; set; }
    }

    public class AzureAdTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
    public class PowerBiReport
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "webUrl")]
        public string WebUrl { get; set; }

        [JsonProperty(PropertyName = "embedUrl")]
        public string EmbedUrl { get; set; }

        [JsonProperty(PropertyName = "datasetId")]
        public string DatasetId { get; set; }
    }
}
