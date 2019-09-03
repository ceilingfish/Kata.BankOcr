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
            //    "000000000",
            //    "111111111",
            //    "222222222",
            //    "333333333",
            //    "444444444",
            //    "555555555",
            //    "666666666",
            //    "777777777",
            //    "888888888",
            //    "999999999"
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
