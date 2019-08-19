using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Nca.Scrape
{
    public class HtmlScraper
    {
        private readonly string _url;

        public HtmlScraper(string url)
        {
            _url = url;
        }
        public async Task ParseAsync()
        {
            var web = new HtmlWeb();
            var document = await web.LoadFromWebAsync(_url);
            var mainContainer = document.DocumentNode.QuerySelectorAll("#ctl31_BodyContainer").FirstOrDefault();

        }
    }
}
