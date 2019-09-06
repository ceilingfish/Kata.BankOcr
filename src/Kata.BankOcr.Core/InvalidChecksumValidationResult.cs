using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    /// <summary>
    /// Represents a validation result that is returned if an account number has an invalid checksum
    /// </summary>
    public class InvalidChecksumValidationResult : IAccountNumberValidationResult
    {
        /// <summary>
        /// The calculated total checksum
        /// </summary>
        public int Checksum { get; }

        /// <summary>
        /// The modulus of the invalid checksum, which in valid cases should have been zero
        /// </summary>
        public int Modulus { get; }

        public bool IsValid => false;

        public string Description => "ERR";

        public InvalidChecksumValidationResult(int checksum, int modulus)
        {
            Checksum = checksum;
            Modulus = modulus;
        }
    }
}
