using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    public class IllegibleAccountNumberValidationResult : IAccountNumberValidationResult
    {
        public bool IsValid => false;

        public IEnumerable<Digit> IllegibleDigits { get; }

        public string Description => "ILL";

        public IllegibleAccountNumberValidationResult(IEnumerable<Digit> illegibleDigits)
        {
            IllegibleDigits = illegibleDigits;
        }
    }
}
