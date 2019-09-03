using System;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Kata.BankOcr
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var parser = CascadingOcrGlyphParser.Default;
            var reader = new OcrRowReader(args[0]);
            await reader
                .Rows()
                .Select(glyphs =>
                {
                    return glyphs.Select(glyph => 
                    {
                        if (parser.TryParse(glyph, out var number))
                        {
                            return new OcrNumber(number, glyph);
                        }
                        else
                        {
                            return new OcrNumber(glyph);
                        }
                    });
                })
                .ForEachAsync(row => 
                {
                    var accountNumber = string.Join("", row);
                    Console.WriteLine(accountNumber);
                });
            Console.ReadLine();
        }
    }
}
