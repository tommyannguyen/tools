using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.PowerBI.Api.V2;
using Microsoft.Rest;
using Nca.WebApp.Models;
using Newtonsoft.Json;

namespace Nca.WebApp.Controllers
{
    public partial class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            //var tokenCredentials = await GetAccessToken();

            //using (var powerBiclient = new PowerBIClient(new Uri("https://api.powerbi.com/"), tokenCredentials))
            //{
            //    var reports = powerBiclient.Groups.GetGroups();
            //}
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<TokenCredentials> GetAccessToken()
        {
            using (HttpClient client = new HttpClient())
            {
                var tenantId = "d0b0aeb0-e767-47a7-b574-8758c6382cb2";
                var tokenEndpoint = $"https://login.microsoftonline.com/{tenantId}/oauth2/token";
                var accept = "application/json";
                var userName = "tommy.an.nguyen@gmail.com";
                var password = "Bebe1234156";
                var clientId = "";
                client.DefaultRequestHeaders.Add("Accept", accept);

                var content = new FormUrlEncodedContent(new[]
                   {
                      new KeyValuePair<string, string>("grant_type", "password"),
                      new KeyValuePair<string, string>("username", userName),
                      new KeyValuePair<string, string>("password", password),
                      new KeyValuePair<string, string>("client_id", clientId),
                      new KeyValuePair<string, string>("scope", "openid"),
                      new KeyValuePair<string, string>("resource", "https://analysis.windows.net/powerbi/api")
                   });

                var tokenResult = await client.PostAsync(tokenEndpoint, content).ContinueWith<string>((response) =>
                {
                    var result = response.Result.Content.ReadAsStringAsync().Result;
                    AzureAdTokenResponse tokenRes = JsonConvert.DeserializeObject<AzureAdTokenResponse>(result);
                    return tokenRes?.AccessToken;
                });
                return new TokenCredentials(tokenResult, "Bearer");
            }
        }
    }
}
