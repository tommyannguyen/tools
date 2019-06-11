using Hangfire;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Nca.Test.Jobs
{
    public class MyHangfireJobs
    {
        [AutomaticRetry(Attempts = 5)]
        public async Task<string> SendGetRequest()
        {
            //var client = new HttpClient();
            //var result = await client.GetStringAsync("https://google.com.vn");

            Trace.WriteLine("xxx");
            return "ten ten ten";
        }
    }
}
