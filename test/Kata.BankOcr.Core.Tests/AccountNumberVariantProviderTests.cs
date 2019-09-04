using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Kata.BankOcr.Core.Tests
{
    public class AccountNumberVariantProviderTests
    {
        [Fact]
        public void ReturnsVariantsByAddingPipe()
        {
            var account = new AccountNumber(new []
            {
                new Digit(new Glyph(new char[,]
                {
                    { ' ', ' ',  ' '},
                    { ' ', ' ',  ' '},
                    { ' ', ' ',  '|'}
                }))
            });

            var variants = AccountNumberVariantProvider.GenerateVariants(account).ToArray();
            var variant = Assert.Single(variants);
            var digit = Assert.Single(variant.Digits);
            Assert.Equal(1, digit.Number);
            Assert.Equal(Glyph.One, digit.Glyph);
            Assert.True(digit.IsLegible);
        }

        [Fact]
        public void ReturnsVariantsByRemovingPipe()
        {
            var account = new AccountNumber(new[]
            {
                new Digit(new Glyph(new char[,]
                {
                    { ' ', ' ',  ' '},
                    { ' ', '|',  '|'},
                    { ' ', ' ',  '|'}
                }))
            });

            var variants = AccountNumberVariantProvider.GenerateVariants(account).ToArray();
            var variant = Assert.Single(variants);
            var digit = Assert.Single(variant.Digits);
            Assert.Equal(1, digit.Number);
            Assert.Equal(Glyph.One, digit.Glyph);
            Assert.True(digit.IsLegible);
        }

        [Fact]
        public void ReturnsVariantsByAddingUnderscore()
        {
            var account = new AccountNumber(new[]
            {
                new Digit(new Glyph(new char[,]
                {
                    { ' ', ' ', ' ' },
                    { ' ', '_', '|' },
                    { ' ', '_', '|' }
                }))
            });

            var variants = AccountNumberVariantProvider.GenerateVariants(account).ToArray();
            var variant = Assert.Single(variants);
            var digit = Assert.Single(variant.Digits);
            Assert.Equal(3, digit.Number);
            Assert.Equal(Glyph.Three, digit.Glyph);
            Assert.True(digit.IsLegible);
        }

        [Fact]
        public void ReturnsVariantsByRemovingUnderscore()
        {
            var account = new AccountNumber(new[]
{
                new Digit(new Glyph(new char[,]
                {
                    { '_', '_', ' ' },
                    { ' ', ' ', '|' },
                    { ' ', ' ', '|' }
                }))
            });

            var variants = AccountNumberVariantProvider.GenerateVariants(account).ToArray();
            var variant = Assert.Single(variants);
            var digit = Assert.Single(variant.Digits);
            Assert.Equal(7, digit.Number);
            Assert.Equal(Glyph.Seven, digit.Glyph);
            Assert.True(digit.IsLegible);
        }

        [Fact]
        public void ReturnsNoVariants()
        {
            var account = new AccountNumber(new[]
{
                new Digit(new Glyph(new char[,]
                {
                    { ' ', ' ', ' ' },
                    { ' ', ' ', ' ' },
                    { ' ', ' ', ' ' }
                }))
            });

            var variants = AccountNumberVariantProvider.GenerateVariants(account).ToArray();
            Assert.Empty(variants);
        }

        [Fact]
        public void ReturnsMultipleVariants()
        {
            var account = new AccountNumber(new[]
{
                new Digit(new Glyph(new char[,]
                {
                    { ' ', ' ', ' ' },
                    { ' ', '_', '|' },
                    { ' ', ' ', '|' }
                }))
            });

            var variants = AccountNumberVariantProvider.GenerateVariants(account).ToArray();
            Assert.Equal(2, variants.Length);
            Assert.Single(variants, v => v.Digits[0].Number == 1);
            Assert.Single(variants, v => v.Digits[0].Number == 4);
        }
    }
}
