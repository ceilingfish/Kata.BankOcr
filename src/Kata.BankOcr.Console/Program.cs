using Kata.BankOcr.Core;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kata.BankOcr.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build()
                .Get<Options>();

            if(string.IsNullOrEmpty(config?.Input))
            {
                System.Console.Error.WriteLine("No input file defined");
                return;
            }
            else if(!File.Exists(config.Input))
            {
                System.Console.Error.WriteLine("Cannot find input file {0}", config.Input);
                return;
            }


            var lines = await File.ReadAllLinesAsync(config.Input);
            var results = GlyphRowReader
                .Read(lines)
                .Select(glyphs =>
                {
                    var account = AccountNumber.Parse(glyphs);

                    var validationResult = ChecksumAccountValidator.Validate(account);

                    if(!validationResult.IsValid)
                    {
                        var variants = AccountNumberVariantProvider
                            .GenerateVariants(account)
                            .Where(variant => ChecksumAccountValidator.Validate(variant).IsValid)
                            .ToArray();
                        if(variants.Length == 1)
                        {
                            return (Account: variants[0], Validity: ValidAccountNumberValidationResult.Default);
                        }
                        else if(variants.Length > 1)
                        {
                            return (Account: account, Validity: new AmbiguousValidationResult(variants));
                        }
                    }

                    return (Account: account, Validity: validationResult);
                })
                .ToArray();
            
            IAccountReporter reporter;
            if(!string.IsNullOrEmpty(config.Output))
            {
                reporter = new FileReporter(config.Output);
            }
            else
            {
                reporter = ConsoleReporter.Default;
            }
            
            using(reporter)
            {
                foreach (var (account, validity) in results)
                {
                    await reporter.ReportAsync(account, validity);
                }
            }
        }
    }
}
