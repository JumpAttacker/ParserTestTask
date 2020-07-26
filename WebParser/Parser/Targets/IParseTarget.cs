using System.Threading.Tasks;
using PuppeteerSharp;

namespace WebParser.Parser.Targets
{
    public interface IParseTarget
    {
        string Url { get; set; }
        Task<bool> Parse(Page page);
    }
}