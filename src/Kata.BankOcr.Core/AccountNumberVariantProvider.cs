using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    public static class AccountNumberVariantProvider
    {
        public static IEnumerable<AccountNumber> GenerateVariants(AccountNumber account)
        {
            var variantChars = new[] { '|', '_', ' ' };
            for (var i = 0; i < account.Digits.Count; i++)
            {
                var number = account.Digits[i];
                foreach (var character in number.Glyph)
                {
                    foreach (var mutantChar in variantChars)
                    {
                        if (mutantChar == character.Character)
                        {
                            continue;
                        }

                        var mutatedGlyph = number.Glyph.Mutate(character, mutantChar);
                        if (Digit.TryParse(mutatedGlyph, out var mutantDigit))
                        {
                            var mutatedRow = new List<Digit>(account.Digits)
                            {
                                [i] = mutantDigit
                            };
                            yield return new AccountNumber(mutatedRow);
                        }
                    }
                }
            }
        }
    }
}
