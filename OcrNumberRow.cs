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

        public bool IsValid
        {
            get
            {
                int sum = 0;
                for(var i=0;i<numbers.Count;i++)
                {
                    var accountNumber = numbers[numbers.Count - i - 1];
                    if(!accountNumber.Number.HasValue)
                    {
                        return false;
                    }
                    sum += accountNumber.Number.Value * (i+1);
                }

                return sum % 11 == 0;
            }
        }

        public int Count => numbers.Count;

        public OcrNumberRow(IReadOnlyList<OcrNumber> numbers)
        {
            this.numbers = numbers;
        }

        public IEnumerator<OcrNumber> GetEnumerator() => numbers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => numbers.GetEnumerator();

        public override string ToString() => string.Join("", numbers);
    }
}
