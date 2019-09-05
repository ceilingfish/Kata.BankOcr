using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    /// <summary>
    /// A validation result that represents an account number that could not be read entirely or partially
    /// </summary>
    public class IllegibleAccountNumberValidationResult : IAccountNumberValidationResult
    {
        public bool IsValid => false;

        /// <summary>
        /// The digits that were not recognised
        /// </summary>
        public IEnumerable<Digit> IllegibleDigits { get; }

        public string Description => "ILL";

        public IllegibleAccountNumberValidationResult(IEnumerable<Digit> illegibleDigits)
        {
            IllegibleDigits = illegibleDigits;
        }
    }
}
