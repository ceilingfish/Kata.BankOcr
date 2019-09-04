using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata.BankOcr.Core
{
    public class ChecksumAccountValidator : IAccountNumberValidator
    {
        public IAccountNumberValidationResult Validate(AccountNumber account)
        {
            if(!account.IsLegible)
            {
                return new IllegibleAccountNumberValidationResult(account.Digits.Where(i => !i.IsLegible));
            }

            int sum = 0;
            var digits = account.Digits;
            for (var i = 0; i < digits.Count; i++)
            {
                var accountNumber = account.Digits[digits.Count - i - 1];
                sum += accountNumber.Number.Value * (i + 1);
            }

            var modulus = sum % 11;

            if(modulus == 0)
            {
                return ValidAccountNumberValidationResult.Default;
            }
            else
            {
                return new InvalidChecksumValidationResult(sum, modulus);
            }
        }
    }
}
