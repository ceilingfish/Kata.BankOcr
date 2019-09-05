using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kata.BankOcr.Core
{
    public interface IAccountReporter : IDisposable
    {
        Task ReportAsync(AccountNumber account, IAccountNumberValidationResult validationResult);
    }
}
