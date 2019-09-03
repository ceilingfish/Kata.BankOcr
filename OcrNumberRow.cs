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

        public RowValidity Validity
        {
            get
            {
                int sum = 0;
                for(var i=0;i<numbers.Count;i++)
                {
                    var accountNumber = numbers[numbers.Count - i - 1];
                    if(!accountNumber.Number.HasValue)
                    {
                        return RowValidity.Illegible;
                    }
                    sum += accountNumber.Number.Value * (i+1);
                }

                return sum % 11 == 0 ? RowValidity.Valid : RowValidity.Error;
            }
        }

        public int Count => numbers.Count;

        public OcrNumberRow(IReadOnlyList<OcrNumber> numbers)
        {
            this.numbers = numbers;
        }

        public IEnumerator<OcrNumber> GetEnumerator() => numbers.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => numbers.GetEnumerator();

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
            }
            
            return builder.ToString();
        }
    }
}
