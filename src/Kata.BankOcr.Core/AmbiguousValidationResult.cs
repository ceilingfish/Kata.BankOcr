using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    public class AmbiguousValidationResult : IAccountNumberValidationResult
    {
        public bool IsValid => false;

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
