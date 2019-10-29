using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Nca.Web.React.Spa.Controllers
{
    [Route("[controller]")]
    public class IdentityController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var baseUrl = "https://id.moap.vn";
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", "tommy.an.nguyen@gmail.com"),
                new KeyValuePair<string, string>("password","Bebe1234156"),
                new KeyValuePair<string, string>("client_secret","AKUeyXPGR01RN5nZLvoKB5p0Pciy3Yv1"),
                new KeyValuePair<string, string>("client_id","d5369772-3632-4dd4-8fd2-d75bfe5ea285"),
                new KeyValuePair<string, string>("grant_type", "password")
            };
            var request = new HttpRequestMessage(HttpMethod.Post, $"{baseUrl}/connect/token")
            {
                Content = new FormUrlEncodedContent(keyValues)
            };

            AuthenticationToken result;
            using (HttpClient client = new HttpClient())
            {
                var response = await client.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();
                result = JsonConvert.DeserializeObject<AuthenticationToken>(content);
            }
            return Ok(result);
        }
    }
    public class Token
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("userName")]
        public string Username { get; set; }

        [JsonProperty(".issued")]
        public string IssuedAt { get; set; }

        [JsonProperty(".expires")]
        public string ExpiresAt { get; set; }
    }
}
