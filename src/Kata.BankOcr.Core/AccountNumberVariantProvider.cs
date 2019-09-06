using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    /// <summary>
    /// A utility that can generate every variant of a glyph in a given account number. It does this by sequential replacement of each character through the list of account digits. Each
    /// variant only differs from the original by a single character.
    /// </summary>
    public static class AccountNumberVariantProvider
    {
        /// <summary>
        /// Generates every variant of an account number where a glyph can be altered by a single character change to turn it into a valid digit. If a glyph is already valid, we still attempt to turn
        /// it into a different valid glyph, but we won't return the original valid glyph.
        /// </summary>
        /// <param name="account">The original account number</param>
        /// <returns>A list of variant account numbers</returns>
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
