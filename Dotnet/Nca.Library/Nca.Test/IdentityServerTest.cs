using Microsoft.AspNetCore.Authentication;
using Nca.Scrape;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Tests
{
    public class IdentityServerTest
    {

        [Test]
        public async Task TestGetToken()
        {
            var baseUrl = "https://id.moap.vn";
            var keyValues = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("username", "tommy.an.nguyen@gmail.com"),
                new KeyValuePair<string, string>("password",""),
                new KeyValuePair<string, string>("client_secret","6SpYSDcYvrKTxoP1lz1CHCfaL6NdcDZr"),
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
            Assert.True(true);
        }
    }
}