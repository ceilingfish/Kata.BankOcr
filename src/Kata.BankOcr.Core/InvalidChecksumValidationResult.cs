using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    public class InvalidChecksumValidationResult : IAccountNumberValidationResult
    {
        public int Checksum { get; }

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
