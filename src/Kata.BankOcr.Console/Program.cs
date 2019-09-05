using Kata.BankOcr.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Kata.BankOcr
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var lines = await File.ReadAllLinesAsync(args[0]);
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
            if(args.Length > 1 && !string.IsNullOrEmpty(args[1]))
            {
                reporter = new FileReporter(args[1]);
            }
            else
            {
                reporter = new ConsoleReporter();
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
