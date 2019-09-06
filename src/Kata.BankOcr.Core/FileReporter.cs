using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kata.BankOcr.Core
{
    /// <summary>
    /// Write the results of account number validation to a file
    /// </summary>
    public class FileReporter : IAccountReporter
    {
        private readonly Lazy<StreamWriter> fileStream;

        public FileReporter(string fileName)
        {
            this.fileStream = new Lazy<StreamWriter>(() => 
            {
                var stream = File.OpenWrite(fileName);
                var writer = new StreamWriter(stream,Encoding.UTF8);
                return writer;
            },LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public Task ReportAsync(AccountNumber account, IAccountNumberValidationResult validationResult)
        {
            return fileStream.Value.WriteLineAsync(FormattableString.Invariant($"{account} {validationResult.Description}"));
        }

        public void Dispose()
        {
            if(fileStream.IsValueCreated)
            {
                fileStream.Value.Dispose();
            }
        }
    }
}
