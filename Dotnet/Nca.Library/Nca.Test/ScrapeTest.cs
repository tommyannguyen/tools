using Nca.Scrape;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests
{
    public class ScrapeTest
    {

        [Test]
        public async Task TestCrape()
        {
            var parser = new HtmlScraper("https://batdongsan.com.vn/");
            await parser.ParseAsync();
            Assert.True(true);
        }
    }
}