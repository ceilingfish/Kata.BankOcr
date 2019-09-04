using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata.BankOcr.Core
{
    public class AccountNumber
    {
        public IReadOnlyList<Digit> Digits { get; }

        public bool IsLegible => Digits.All(d => d.IsLegible);

        public AccountNumber(IReadOnlyList<Digit> numbers)
        {
            this.Digits = numbers;
        }

        public static AccountNumber Parse(IReadOnlyList<Glyph> glyphs)
        {
            var digits = new List<Digit>();
            foreach(var glyph in glyphs)
            {
                if(Digit.TryParse(glyph, out var digit))
                {
                    digits.Add(digit);
                }
                else
                {
                    digits.Add(new Digit(glyph));
                }
            }

            return new AccountNumber(digits);
        }

        public IEnumerable<AccountNumber> GenerateVariants()
        {
            var variantChars = new[] { '|', '_', ' ' };
            for (var i = 0; i < Digits.Count; i++)
            {
                var number = Digits[i];
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
                            var mutatedRow = new List<Digit>(Digits)
                            {
                                [i] = mutantDigit
                            };
                            yield return new AccountNumber(mutatedRow);
                        }
                    }
                }
            }
        }

        public override string ToString() => string.Join(string.Empty, Digits);
    }
}
