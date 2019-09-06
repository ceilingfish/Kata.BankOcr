using System.Linq;

namespace Kata.BankOcr.Core
{
    /// <summary>
    /// Validates account numbers by generating a checksum.
    /// </summary>
    public static class ChecksumAccountValidator
    {
        /// <summary>
        /// Return a validation result for a given account number
        /// </summary>
        /// <param name="account">The account to validate</param>
        /// <returns>One of three results: 
        ///     <list type="number">
        ///         <item><see cref="IllegibleAccountNumberValidationResult"/> if we cannot read all digits in the account number</item>
        ///         <item><see cref="InvalidChecksumValidationResult"/> if the checksum fails</item>
        ///         <item><see cref="ValidAccountNumberValidationResult"/> if the checksum passes</item>
        ///     </list>
        /// </returns>
        public static IAccountNumberValidationResult Validate(AccountNumber account)
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
