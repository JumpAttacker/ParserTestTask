using System.Threading.Tasks;
using PuppeteerSharp;

namespace WebParser.Parser.Targets
{
    public abstract class BaseParseTarget : IParseTarget
    {
        protected BaseParseTarget(string url)
        {
            Url = url;
        }

        public string Url { get; set; }
        public abstract Task<bool> Parse(Page page);
    }
}