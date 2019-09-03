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
                .ForEachAsync(glyphs =>
                {
                    foreach(var glyph in glyphs)
                    {
                        if (parser.TryParse(glyph, out var number))
                        {
                            Console.WriteLine("Found {0} at ({1},{2})", number, glyph.Row, glyph.Column);
                        }
                    }
                });
            Console.ReadLine();
        }
    }
}
