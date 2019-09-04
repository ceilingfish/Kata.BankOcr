using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    public class ValidAccountNumberValidationResult : IAccountNumberValidationResult
    {
        public static ValidAccountNumberValidationResult Default { get; } = new ValidAccountNumberValidationResult();

        public bool IsValid => true;

        public string Description => string.Empty;
    }
}
