using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;

namespace Kata.BankOcr
{
    public class OcrRowReader
    {
        private readonly string file;

        public OcrRowReader(string file)
        {
            this.file = file;
        }
        public IObservable<IReadOnlyList<OcrGlyph>> Rows()
        {
            return Observable.Create<IReadOnlyList<OcrGlyph>>(observer =>
            {
                var lines = File.ReadLines(file).ToArray();

                var rows = lines.Length/4;
                for(int row=0;row<rows;row++)
                {
                    var yOffset = row * 4;
                    var line1 = lines[yOffset];
                    var line2 = lines[yOffset + 1];
                    var line3 = lines[yOffset + 2];
                    if(line1.Length != line2.Length || line2.Length != line3.Length)
                    {
                        observer.OnError(new InvalidDataException(FormattableString.Invariant($"Mismatching row lengths found starting at row {yOffset}")));
                        return Disposable.Empty;
                    }

                    var columns = line1.Length / 3;
                    var glyphs = new OcrGlyph[columns];
                    for(var column = 0; column<columns; column++)
                    {
                        var xOffset = column * 3;
                        var matrix = new char[3, 3]
                        {
                                { line1[xOffset], line1[xOffset+1], line1[xOffset+2] },
                                { line2[xOffset], line2[xOffset+1], line2[xOffset+2] },
                                { line3[xOffset], line3[xOffset+1], line3[xOffset+2] }
                        };
                        glyphs[column] = new OcrGlyph(row, column, matrix);
                    }

                    observer.OnNext(glyphs);
                }

                observer.OnCompleted();

                return Disposable.Empty;
            });
        } 
    }
}
