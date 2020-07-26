using System;
using System.Threading.Tasks;
using PuppeteerSharp;
using WebParser.Parser.Targets;

namespace WebParser.Parser
{
    public class Parser : IParser
    {
        public Parser(IParseTarget target)
        {
            ParseTarget = target ?? throw new ArgumentNullException(nameof(target));
        }

        private IParseTarget ParseTarget { get; }

        public async Task<bool> Parse()
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            LaunchOptions options = new LaunchOptions
            {
                Headless = true
            };
            await using Browser browser = await Puppeteer.LaunchAsync(options);
            await using Page page = await browser.NewPageAsync();
            await page.GoToAsync(ParseTarget.Url);
            var success = await ParseTarget.Parse(page);
            return success;
        }
    }
}