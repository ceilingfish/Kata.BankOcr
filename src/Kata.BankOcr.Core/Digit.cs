using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr
{
    public class Digit
    {
        public Glyph Glyph { get; }
        public int? Number { get; }
        public bool IsLegible => Number.HasValue;

        public Digit(int number, Glyph glyph)
        {
            Number = number;
            Glyph = glyph;
        }

        public Digit(Glyph glyph)
        {
            Glyph = glyph;
        }

        public override string ToString()
        {
            return Number.HasValue ? Number.Value.ToString() : "?";
        }
    }
}
