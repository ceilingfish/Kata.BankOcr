using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    /// <summary>
    /// Represents a single digit
    /// </summary>
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

        /// <summary>
        /// The glyph that this digit was represented by
        /// </summary>
        public Glyph Glyph { get; }

        /// <summary>
        /// The number (if the glyph matched one of the known patterns)
        /// </summary>
        public int? Number { get; }

        /// <summary>
        /// True if the glyph is recognised as a digit, false otherwise
        /// </summary>
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

        /// <summary>
        /// Try and convert a glyph into a digit
        /// </summary>
        /// <param name="input">The glyph</param>
        /// <param name="match">The digit to store the result into</param>
        /// <returns>True if it matched a digit pattern, false otherwise</returns>
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
