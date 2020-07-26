using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PuppeteerSharp;

namespace WebParser.Parser.Targets
{
    public class KellySubaruParseTarget : BaseParseTarget
    {
        public KellySubaruParseTarget(string url) : base(url)
        {
        }

        private static string VinXPath => @"//div[1]/div/div[3]/dl[3]/dd";
        private static string ImageXPath => @"//div[1]/div/div[1]/a[1]/img";
        private static string PriceXPath => @"//div[1]/div/div[2]/ul/li[1]/span/span[2]";
        private static string PatternForImageUrl => @"\?.*$";

        public override async Task<bool> Parse(Page page)
        {
            const string jsCode = @"() => {return Array.from(document.querySelectorAll('.item')).map(x=>x.outerHTML);}";
            string[] results = await page.EvaluateFunctionAsync<string[]>(jsCode);
            if (results.Length < 2)
            {
                Console.WriteLine("[Error] Cant select second car in list");
                return false;
            }
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(results[1]);

            #region Image
            
            HtmlNode node = doc.DocumentNode.SelectSingleNode(ImageXPath);

            if (node != null)
            {
                string url = node.GetAttributeValue("src", string.Empty);
                if (string.IsNullOrEmpty(url))
                {
                    Console.WriteLine("[Error] cant get attribute src for image");
                    return false;
                }

                string replace = Regex.Replace(url, PatternForImageUrl, string.Empty);
                Console.WriteLine($"Image Url: {replace}");
            }
            else
            {
                Console.WriteLine("[Error] cant find image by XPath");
                return false;
            }
            
            #endregion

            #region Vin
            
            node = doc.DocumentNode.SelectSingleNode(VinXPath);

            if (node != null)
            {
                string vin = node.GetDirectInnerText();
                Console.WriteLine($"Vin: {vin}");
            }
            else
            {
                Console.WriteLine("[Error] cant find vin by XPath");
                return false;
            }
            
            #endregion

            #region Price
            
            node = doc.DocumentNode.SelectSingleNode(PriceXPath);

            if (node != null)
            {
                string price = node.GetDirectInnerText();
                Console.WriteLine($"Price: {price}");
            }
            else
            {
                Console.WriteLine("[Error] cant find price by XPath");
                return false;
            }
            
            #endregion

            return true;
        }
    }
}