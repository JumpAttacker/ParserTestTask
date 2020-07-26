using System.Threading.Tasks;

namespace WebParser.Parser
{
    public interface IParser
    {
        Task<bool> Parse();
    }
}