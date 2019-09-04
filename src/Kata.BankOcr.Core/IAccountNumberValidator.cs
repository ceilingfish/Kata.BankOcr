using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    public interface IAccountNumberValidator
    {
        IAccountNumberValidationResult Validate(AccountNumber account);
    }
}
