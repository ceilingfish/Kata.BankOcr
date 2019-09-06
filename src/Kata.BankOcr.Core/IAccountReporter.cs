using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kata.BankOcr.Core
{
    /// <summary>
    /// A report mechanism that can be used to record account validation results
    /// </summary>
    public interface IAccountReporter : IDisposable
    {
        /// <summary>
        /// Record a single account result
        /// </summary>
        /// <param name="account">The account</param>
        /// <param name="validationResult">The validation result</param>
        /// <returns>An asynchronous task that completes once the result has been reported</returns>
        Task ReportAsync(AccountNumber account, IAccountNumberValidationResult validationResult);
    }
}
