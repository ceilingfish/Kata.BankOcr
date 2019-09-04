using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kata.BankOcr.Core.Tests
{
    public class ChecksumAccountValidatorTests
    {
        [Fact]
        public void IllegibleAccountsAreInvalid()
        {
            var validator = new ChecksumAccountValidator();
            var illegibleAccount = new AccountNumber(new[]
            {
                new Digit(new Glyph(1,1,new char[3,3]
            {
                { 'a', 'b', 'c' },
                { 'd', 'e', 'f' },
                { 'g', 'h', 'i' }
            })),
                new Digit(1, new Glyph(0,1,new char[3,3]
            {
                { 'a', 'b', 'c' },
                { 'd', 'e', 'f' },
                { 'g', 'h', 'i' }
            }))
            });
            var result = validator.Validate(illegibleAccount);
            Assert.False(result.IsValid);
            var illegibleResult = Assert.IsType<IllegibleAccountNumberValidationResult>(result);
            var illegibleDigit = Assert.Single(illegibleResult.IllegibleDigits);
            Assert.Equal(1, illegibleDigit.Glyph.Row);
            Assert.Equal(1, illegibleDigit.Glyph.Column);
        }

        [Fact]
        public void BadChecksumsAreInvalid()
        {
            var glyph = new Glyph(1, 1, new char[3, 3]
            {
                { 'a', 'b', 'c' },
                { 'd', 'e', 'f' },
                { 'g', 'h', 'i' }
            });

            var validator = new ChecksumAccountValidator();
            var illegibleAccount = new AccountNumber(new[]
            {
                new Digit(8, glyph),
                new Digit(8, glyph),
                new Digit(8, glyph),
                new Digit(8, glyph),
                new Digit(8, glyph),
                new Digit(8, glyph),
                new Digit(8, glyph),
                new Digit(8, glyph),
                new Digit(8, glyph)
            });
            var result = validator.Validate(illegibleAccount);
            Assert.False(result.IsValid);
            Assert.IsType<InvalidChecksumValidationResult>(result);
        }

        [Fact]
        public void AccurateChecksumsAreValid()
        {
            var glyph = new Glyph(1, 1, new char[3, 3]
            {
                { 'a', 'b', 'c' },
                { 'd', 'e', 'f' },
                { 'g', 'h', 'i' }
            });

            var validator = new ChecksumAccountValidator();
            var illegibleAccount = new AccountNumber(new[]
            {
                new Digit(7, glyph),
                new Digit(1, glyph),
                new Digit(1, glyph),
                new Digit(1, glyph),
                new Digit(1, glyph),
                new Digit(1, glyph),
                new Digit(1, glyph),
                new Digit(1, glyph),
                new Digit(1, glyph)
            });
            var result = validator.Validate(illegibleAccount);
            Assert.True(result.IsValid);
            Assert.Equal(ValidAccountNumberValidationResult.Default, result);
        }
    }
}
