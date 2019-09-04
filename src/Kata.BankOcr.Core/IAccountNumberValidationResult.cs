using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    public interface IAccountNumberValidationResult
    {
        bool IsValid { get; }

        string Description { get; }
    }
}
