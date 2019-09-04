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

        public override string ToString() => string.Join(string.Empty, Digits);
    }
}
