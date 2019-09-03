using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata.BankOcr
{
    public class OcrNumberRow : IReadOnlyList<OcrNumber>
    {
        private readonly IReadOnlyList<OcrNumber> numbers;

        public OcrNumber this[int index]
        {
            get => numbers[index];
        }

        private RowValidity? validity;
        public RowValidity Validity
        {
            get
            {
                if(!validity.HasValue)
                {
                    validity = CalculateValidity(numbers);
                }

                return validity.Value;
            }

            set => validity = value;
        }

        public int Count => numbers.Count;

        public OcrNumberRow(IReadOnlyList<OcrNumber> numbers)
        {
            this.numbers = numbers;
        }

        public IEnumerable<OcrNumberRow> GenerateVariants()
        {
            var variantChars = new[] { '|', '_', ' ' };
            for(var i=0;i<numbers.Count;i++)
            {
                var number = numbers[i];
                for(var y=0;y<number.Glyph.Characters.GetLength(0);y++)
                {
                    for(var x=0;x<number.Glyph.Characters.GetLength(1);x++)
                    {
                        foreach(var mutantChar in variantChars)
                        {
                            if(mutantChar == number.Glyph.Characters[y,x])
                            {
                                continue;
                            }

                            var mutated = (char[,])number.Glyph.Characters.Clone();
                            mutated[y,x] = mutantChar;
                            var mutatedGlyph = new OcrGlyph(number.Glyph.Row, number.Glyph.Column, mutated);
                            if(CascadingOcrGlyphParser.Default.TryParse(mutatedGlyph, out int mutantNumber))
                            {
                                var mutatedNumber = new OcrNumber(mutantNumber, mutatedGlyph);
                                var mutatedRow = new List<OcrNumber>(numbers);
                                mutatedRow[i] = mutatedNumber;
                                yield return new OcrNumberRow(mutatedRow);
                            }
                        }
                    }
                }
            }
        }

        public IEnumerator<OcrNumber> GetEnumerator() => numbers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => numbers.GetEnumerator();

        private static RowValidity CalculateValidity(IReadOnlyList<OcrNumber> numbers)
        {
            int sum = 0;
            for (var i = 0; i < numbers.Count; i++)
            {
                var accountNumber = numbers[numbers.Count - i - 1];
                if (!accountNumber.Number.HasValue)
                {
                    return RowValidity.Illegible;
                }
                sum += accountNumber.Number.Value * (i + 1);
            }

            return sum % 11 == 0 ? RowValidity.Valid : RowValidity.Error;
        }

        public override string ToString() {
            
            var builder = new StringBuilder();
            foreach(var number in numbers)
            {
                builder.Append(number);
            }
            switch(Validity)
            {
                case RowValidity.Error:
                    builder.Append(" ERR");
                    break;
                case RowValidity.Illegible:
                    builder.Append(" ILL");
                    break;
                case RowValidity.Ambiguous:
                    builder.Append(" AMB");
                    break;
            }
            
            return builder.ToString();
        }
    }
}
