using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Kata.BankOcr
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var writer = new OcrRowWriter(args[0]);
            //await writer.WriteAsync(new[]
            //{
            //    "711111111",
            //    "123456789",
            //    "490867715",

            //    "888888888",
            //    "490067715",
            //    "012345678",
            //});
            //return;

            var parser = CascadingOcrGlyphParser.Default;
            var reader = new OcrRowReader(args[0]);
            await reader
                .Rows()
                .Select(glyphs =>
                {
                    var row = new OcrNumber[glyphs.Count];

                    for(int i=0;i<row.Length;i++)
                    {
                        var glyph = glyphs[i];
                        if (parser.TryParse(glyph, out var number))
                        {
                            row[i] = new OcrNumber(number, glyph);
                        }
                        else
                        {
                            row[i] = new OcrNumber(glyph);
                        }
                    }

                    return new OcrNumberRow(row);
                })
                .ForEachAsync(row => 
                {
                    Console.WriteLine(row);
                });
            Console.ReadLine();
        }
    }
}
