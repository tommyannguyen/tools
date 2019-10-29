using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Nca.Test
{
    public class BiTest
    {
        [Test]
        public async Task TestLogin()
        {
            using (HttpClient client = new HttpClient())
            {
                var tenantId = "1a3a4261-a577-48ec-beb1-71cd3a14e413";
                var tokenEndpoint = $"https://login.microsoftonline.com/{tenantId}/oauth2/token";
                var accept = "application/json";
                var userName = "adam.davis@ariaresorts.co.uk";
                var password = "*";
                var clientId = "68fcad01-31ed-4fe6-b280-73305c3f6e68";
                client.DefaultRequestHeaders.Add("Accept", accept);

                var content = new FormUrlEncodedContent(new[]
                   {
                      new KeyValuePair<string, string>("grant_type", "password"),
                      new KeyValuePair<string, string>("username", userName),
                      new KeyValuePair<string, string>("password", password),
                      new KeyValuePair<string, string>("client_id", clientId),
                      new KeyValuePair<string, string>("client_secret", "[Tf8m2/@4-m4.lFcsza.JrOD9snjBXm@"),
                      new KeyValuePair<string, string>("scope", "openid"),
                      new KeyValuePair<string, string>("resource", "https://analysis.windows.net/powerbi/api")
                   });

                var tokenResult = await client.PostAsync(tokenEndpoint, content).ContinueWith<string>((response) =>
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;
                    AzureAdTokenResponse tokenRes = JsonConvert.DeserializeObject<AzureAdTokenResponse>(result);
                    return tokenRes?.AccessToken;
                });
                Assert.True(!string.IsNullOrEmpty(tokenResult));
            }
        }
    }
    public class AzureAdTokenResponse
    {
        public string AccessToken { get; set; }
    }
}
