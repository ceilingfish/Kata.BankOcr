using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    /// <summary>
    /// Represents the result of validation on an account number where we've found that multiple variants on the account number could result in a valid checksum
    /// </summary>
    public class AmbiguousValidationResult : IAccountNumberValidationResult
    {
        public bool IsValid => false;

        /// <summary>
        /// The variations from the original account number that are also valid
        /// </summary>
        public IEnumerable<AccountNumber> Possibilities { get; }

        public AmbiguousValidationResult(IEnumerable<AccountNumber> possibilities)
        {
            Possibilities = possibilities;
        }

        public string Description
        {
            get
            {
                return FormattableString.Invariant($@"AMB [""{string.Join("\", \"", Possibilities)}""]");
            }
        }
    }
}
