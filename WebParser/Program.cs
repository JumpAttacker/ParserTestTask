using System;
using System.Threading.Tasks;
using WebParser.Parser;
using WebParser.Parser.Targets;

namespace WebParser
{
    internal static class Program
    {
        private static async Task Main()
        {
            Console.WriteLine("Starting...");
            IParseTarget target = new KellySubaruParseTarget("https://www.kellysubaru.com/used-inventory/index.htm");
            IParser parser = new Parser.Parser(target);
            bool success = await parser.Parse();
            if (success)
                Console.WriteLine("Successfully done!");
            else
                Console.WriteLine("Got some errors in parser!");
        }
    }
}