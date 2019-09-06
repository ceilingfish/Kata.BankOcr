using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    /// <summary>
    /// Represents the result of an account number validation
    /// </summary>
    public interface IAccountNumberValidationResult
    {
        /// <summary>
        /// True if valid, false otherwise
        /// </summary>
        bool IsValid { get; }

        /// <summary>
        /// An extended description of the validity state
        /// </summary>
        string Description { get; }
    }
}
