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
            var validator = new ChecksumAccountValidator();
            var results = GlyphRowReader
                .Read(lines)
                .Select(glyphs =>
                {
                    var account = AccountNumber.Parse(glyphs);

                    var validationResult = validator.Validate(account);

                    if(!validationResult.IsValid)
                    {
                        var variants = AccountNumberVariantProvider
                            .GenerateVariants(account)
                            .Where(variant => validator.Validate(variant).IsValid)
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
            
            if(args.Length > 1 && !string.IsNullOrEmpty(args[1]))
            {
                await File.WriteAllLinesAsync(args[1], results.Select(r => $"{r.Account} {r.Validity.Description}"));
                Console.WriteLine("Output written to {0}", args[1]);
            }
            else
            {
                foreach(var (account, validity) in results)
                {
                    Console.WriteLine($"{account} {validity.Description}");
                }
            }
            Console.ReadLine();
        }
    }
}
