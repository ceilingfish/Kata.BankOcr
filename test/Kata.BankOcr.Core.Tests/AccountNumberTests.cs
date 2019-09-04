using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Kata.BankOcr.Core.Tests
{
    public class AccountNumberTests
    {
        [Fact]
        public void IsLegibleIfAllDigitsLegible()
        {
            var matrix = new char[3,3]
            {
                { ' ', ' ', ' ' },
                { ' ', ' ', '|' },
                { ' ', ' ', '|' }
            };

            var first = new Digit(1, new Glyph(0, 0, matrix));
            var second = new Digit(1, new Glyph(0, 1, matrix));

            var account = new AccountNumber(new[] { first, second });

            Assert.True(account.IsLegible);
        }

        [Fact]
        public void IsNotLegibleIfAnyDigitsAreIllegible()
        {
            var matrix = new char[3, 3]
            {
                { ' ', ' ', ' ' },
                { ' ', ' ', '|' },
                { ' ', ' ', '|' }
            };

            var first = new Digit(1, new Glyph(0, 0, matrix));
            var second = new Digit(new Glyph(0, 1, matrix));

            var account = new AccountNumber(new[] { first, second });

            Assert.False(account.IsLegible);
        }

        [Fact]
        public void IsNotLegibleIfAllDigitsAreIllegible()
        {
            var matrix = new char[3, 3]
            {
                { ' ', ' ', ' ' },
                { ' ', ' ', '|' },
                { ' ', ' ', '|' }
            };

            var first = new Digit(new Glyph(0, 0, matrix));
            var second = new Digit(new Glyph(0, 1, matrix));

            var account = new AccountNumber(new[] { first, second });

            Assert.False(account.IsLegible);
        }
    }
}
