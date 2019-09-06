using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Kata.BankOcr.Core
{
    /// <summary>
    /// Reports validation results to the console
    /// </summary>
    public class ConsoleReporter : IAccountReporter
    {
        public static ConsoleReporter Default { get; } = new ConsoleReporter();

        public Task ReportAsync(AccountNumber account, IAccountNumberValidationResult validationResult)
        {
            Console.WriteLine($"{account} {validationResult.Description}");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Console.ReadLine();
        }
    }
}
