using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Kata.BankOcr.Core.Tests
{
    public class CharacterMatchingGlyphParserTests
    {
        [Fact]
        public void CanParseMatrixMatch()
        {
            var matrix = new char[3, 3]
            {
                { 'a', 'b', 'c' },
                { 'd', 'e', 'f' },
                { 'g', 'h', 'i' }
            };

            var parser = new CharacterMatchingGlyphParser(matrix, 4);

            Assert.True(parser.TryParse(new Glyph(0,0, matrix), out var result));
            Assert.Equal(4, result);
        }

        [Fact]
        public void CannotParseMatrixMismatch()
        {
            var comparand = new char[3, 3]
            {
                { 'a', 'b', 'c' },
                { 'd', 'e', 'f' },
                { 'g', 'h', 'i' }
            };

            var parser = new CharacterMatchingGlyphParser(comparand, 4);

            var input = new char[3, 3]
            {
                { 'b', 'b', 'c' },
                { 'd', 'e', 'f' },
                { 'g', 'h', 'i' }
            };

            Assert.False(parser.TryParse(new Glyph(0, 0, input), out var result));
            Assert.Equal(0, result);
        }
    }
}
