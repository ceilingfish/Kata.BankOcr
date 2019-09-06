using System;
using System.Collections.Generic;
using System.IO;

namespace Kata.BankOcr.Core
{
    /// <summary>
    /// A utility that reads rows of characters into a list of glyphs
    /// </summary>
    public static class GlyphRowReader
    {
        /// <summary>
        /// Interpret the character rows as glyphs
        /// </summary>
        /// <param name="lines">The rows from which to interpret glyphs</param>
        /// <returns>An enumerable that contains each row of glyphs</returns>
        /// <exception cref="InvalidDataException">Thrown if the characters don't conform to the format expected by the glyph specification</exception>
        public static IEnumerable<IReadOnlyList<Glyph>> Read(IReadOnlyList<string> lines)
        {
            if (lines.Count % 4 != 0)
            {
                throw new InvalidDataException("Invalid row count");
            }

            var rows = lines.Count / 4;
            for (int row = 0; row < rows; row++)
            {
                var yOffset = row * 4;
                var line1 = lines[yOffset];
                var line2 = lines[yOffset + 1];
                var line3 = lines[yOffset + 2];
                var line4 = lines[yOffset + 3];
                if (line1.Length != line2.Length || line2.Length != line3.Length || line3.Length != line4.Length)
                {
                    throw new InvalidDataException(FormattableString.Invariant($"Mismatching column lengths found starting at row {yOffset}"));
                }

                var columns = line1.Length / 3;
                var glyphs = new Glyph[columns];
                for (var column = 0; column < columns; column++)
                {
                    var xOffset = column * 3;
                    var matrix = new char[3, 3]
                    {
                                { line1[xOffset], line1[xOffset+1], line1[xOffset+2] },
                                { line2[xOffset], line2[xOffset+1], line2[xOffset+2] },
                                { line3[xOffset], line3[xOffset+1], line3[xOffset+2] }
                    };
                    glyphs[column] = new Glyph(matrix);
                }

                yield return glyphs;
            }

        }
    }
}
