using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr
{
    public class OcrNumber
    {
        public OcrGlyph Glyph { get; }
        public int? Number { get; }

        public OcrNumber(int number, OcrGlyph glyph)
        {
            Number = number;
            Glyph = glyph;
        }

        public OcrNumber(OcrGlyph glyph)
        {
            Glyph = glyph;
        }

        public override string ToString()
        {
            return Number.HasValue ? Number.Value.ToString() : "?";
        }
    }
}
