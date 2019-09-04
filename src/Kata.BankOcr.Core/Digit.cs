using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    public class Digit
    {
        private static IReadOnlyList<(Glyph Symbol, int Result)> numbers = new[]
        {
            (Glyph.Zero, 0),
            (Glyph.One, 1),
            (Glyph.Two, 2),
            (Glyph.Three, 3),
            (Glyph.Four, 4),
            (Glyph.Five, 5),
            (Glyph.Six, 6),
            (Glyph.Seven, 7),
            (Glyph.Eight, 8),
            (Glyph.Nine, 9)
        };

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

        public static bool TryParse(Glyph input, out Digit match)
        {
            foreach (var (digitGlyph, number) in numbers)
            {
                if (digitGlyph.Equals(input))
                {
                    match = new Digit(number, input);
                    return true;
                }
            }

            match = null;
            return false;
        }
    }
}
