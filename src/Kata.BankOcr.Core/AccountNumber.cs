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

        //public IEnumerable<AccountNumber> GenerateVariants()
        //{
        //    var variantChars = new[] { '|', '_', ' ' };
        //    for(var i=0;i<Digits.Count;i++)
        //    {
        //        var number = Digits[i];
        //        for(var y=0;y<number.Glyph.Characters.GetLength(0);y++)
        //        {
        //            for(var x=0;x<number.Glyph.Characters.GetLength(1);x++)
        //            {
        //                foreach(var mutantChar in variantChars)
        //                {
        //                    if(mutantChar == number.Glyph.characters[y,x])
        //                    {
        //                        continue;
        //                    }

        //                    var mutated = (char[,])number.Glyph.Characters.Clone();
        //                    mutated[y,x] = mutantChar;
        //                    var mutatedGlyph = new Glyph(number.Glyph.Row, number.Glyph.Column, mutated);
        //                    if(CascadingGlyphParser.Default.TryParse(mutatedGlyph, out int mutantNumber))
        //                    {
        //                        var mutatedNumber = new Digit(mutantNumber, mutatedGlyph);
        //                        var mutatedRow = new List<Digit>(Digits)
        //                        {
        //                            [i] = mutatedNumber
        //                        };
        //                        yield return new AccountNumber(mutatedRow);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        public override string ToString() => string.Join(string.Empty, Digits);
    }
}
