using Xunit;

namespace Kata.BankOcr.Core.Tests
{
    public class ChecksumAccountValidatorTests
    {
        [Fact]
        public void IllegibleAccountsAreInvalid()
        {
            var illegibleAccount = new AccountNumber(new[]
            {
                new Digit(new Glyph(new char[3,3]
                {
                    { 'a', 'b', 'c' },
                    { 'd', 'e', 'f' },
                    { 'g', 'h', 'i' }
                })),
                new Digit(1, Glyph.One)
            });
            var result = ChecksumAccountValidator.Validate(illegibleAccount);
            Assert.False(result.IsValid);
            var illegibleResult = Assert.IsType<IllegibleAccountNumberValidationResult>(result);
            Assert.Single(illegibleResult.IllegibleDigits);
        }

        [Fact]
        public void BadChecksumsAreInvalid()
        {
            var illegibleAccount = new AccountNumber(new[]
            {
                new Digit(8, Glyph.Eight),
                new Digit(8, Glyph.Eight),
                new Digit(8, Glyph.Eight),
                new Digit(8, Glyph.Eight),
                new Digit(8, Glyph.Eight),
                new Digit(8, Glyph.Eight),
                new Digit(8, Glyph.Eight),
                new Digit(8, Glyph.Eight),
                new Digit(8, Glyph.Eight)
            });
            var result = ChecksumAccountValidator.Validate(illegibleAccount);
            Assert.False(result.IsValid);
            Assert.IsType<InvalidChecksumValidationResult>(result);
        }

        [Fact]
        public void AccurateChecksumsAreValid()
        {
            var illegibleAccount = new AccountNumber(new[]
            {
                new Digit(7, Glyph.Seven),
                new Digit(1, Glyph.One),
                new Digit(1, Glyph.One),
                new Digit(1, Glyph.One),
                new Digit(1, Glyph.One),
                new Digit(1, Glyph.One),
                new Digit(1, Glyph.One),
                new Digit(1, Glyph.One),
                new Digit(1, Glyph.One)
            });
            var result = ChecksumAccountValidator.Validate(illegibleAccount);
            Assert.True(result.IsValid);
            Assert.Equal(ValidAccountNumberValidationResult.Default, result);
        }
    }
}
