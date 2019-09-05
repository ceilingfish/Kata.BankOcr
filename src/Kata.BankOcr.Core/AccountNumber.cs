using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata.BankOcr.Core
{
    /// <summary>
    /// A set of contiguous digits that represent an account number
    /// </summary>
    public class AccountNumber
    {
        /// <summary>
        /// The digits in the account number
        /// </summary>
        public IReadOnlyList<Digit> Digits { get; }

        /// <summary>
        /// Whether the account number is legible
        /// </summary>
        public bool IsLegible => Digits.All(d => d.IsLegible);

        public AccountNumber(IReadOnlyList<Digit> numbers)
        {
            this.Digits = numbers;
        }

        /// <summary>
        /// Parse glyphs into numbers
        /// </summary>
        /// <param name="glyphs">The glyphs to parse</param>
        /// <returns>An account number that may or may not be legible</returns>
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
