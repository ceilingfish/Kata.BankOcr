using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kata.BankOcr
{
    public class OcrRowWriter
    {
        private readonly string file;

        public OcrRowWriter(string file)
        {
            this.file = file;
        }

        public async Task WriteAsync(IEnumerable<string> accounts)
        {
            using(var stream = File.OpenWrite(file))
            using(var writer = new StreamWriter(stream))
            {
                foreach (var account in accounts)
                {
                    var parsers = account.Select(c =>
                    {
                        var number = int.Parse(c.ToString());
                        var parser = CharacterMatchingGlyphParser.Numbers.SingleOrDefault(p => p.Result == number);
                        return parser;
                    })
                    .ToArray();

                    for(int row = 0;row < parsers[0].Matrix.GetLength(0);row++)
                    {
                        foreach(var parser in parsers)
                        {
                            for(int column = 0;column < parser.Matrix.GetLength(1);column++)
                            {
                                await writer.WriteAsync(parser.Matrix[row, column]);
                            }
                        }
                        writer.WriteLine();
                    }

                    await writer.WriteLineAsync(Enumerable.Repeat(' ', parsers.Sum(p => p.Matrix.GetLength(1))).ToArray());
                }
            }
        }
    }
}
