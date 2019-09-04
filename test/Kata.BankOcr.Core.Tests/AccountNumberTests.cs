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
            var first = new Digit(1, Glyph.One);
            var second = new Digit(1, Glyph.One);

            var account = new AccountNumber(new[] { first, second });

            Assert.True(account.IsLegible);
        }

        [Fact]
        public void IsNotLegibleIfAnyDigitsAreIllegible()
        {
            var invalid = new char[3, 3]
            {
                { ' ', ' ', '|' },
                { ' ', ' ', '|' },
                { ' ', ' ', '|' }
            };

            var first = new Digit(1, Glyph.One);
            var second = new Digit(new Glyph(invalid));

            var account = new AccountNumber(new[] { first, second });

            Assert.False(account.IsLegible);
        }

        [Fact]
        public void IsNotLegibleIfAllDigitsAreIllegible()
        {
            var invalid = new char[3, 3]
            {
                { ' ', ' ', '|' },
                { ' ', ' ', '|' },
                { ' ', ' ', '|' }
            };

            var first = new Digit(new Glyph(invalid));
            var second = new Digit(new Glyph(invalid));

            var account = new AccountNumber(new[] { first, second });

            Assert.False(account.IsLegible);
        }

        [Fact]
        public void CanParseWithIllegibles()
        {
            var illegible = new char[3, 3]
            {
                { 'a', 'b', 'c' },
                { 'd', 'e', 'f' },
                { 'g', 'h', 'i' }
            };

            var account = AccountNumber.Parse(new[] { new Glyph(illegible), Glyph.One });
            Assert.False(account.IsLegible);
            Assert.Equal(2, account.Digits.Count);
            Assert.False(account.Digits[0].Number.HasValue);
            Assert.Equal(1, account.Digits[1].Number.Value);
        }

        [Fact]
        public void CanParseAllDigits()
        {
            var account = AccountNumber.Parse(new[]
            {
                Glyph.Zero,
                Glyph.One,
                Glyph.Two,
                Glyph.Three,
                Glyph.Four,
                Glyph.Five,
                Glyph.Six,
                Glyph.Seven,
                Glyph.Eight,
                Glyph.Nine
            });

            Assert.True(account.IsLegible);
            Assert.Equal(0, account.Digits[0].Number.Value);
            Assert.Equal(1, account.Digits[1].Number.Value);
            Assert.Equal(2, account.Digits[2].Number.Value);
            Assert.Equal(3, account.Digits[3].Number.Value);
            Assert.Equal(4, account.Digits[4].Number.Value);
            Assert.Equal(5, account.Digits[5].Number.Value);
            Assert.Equal(6, account.Digits[6].Number.Value);
            Assert.Equal(7, account.Digits[7].Number.Value);
            Assert.Equal(8, account.Digits[8].Number.Value);
            Assert.Equal(9, account.Digits[9].Number.Value);
        }
    }
}
