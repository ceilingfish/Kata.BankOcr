using Kata.BankOcr.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace Kata.BankOcr
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var writer = new OcrRowWriter(args[0]);
            //await writer.WriteAsync(new[]
            //{
            //    "711111111",
            //    "123456789",
            //    "490867715",

            //    "888888888",
            //    "490067715",
            //    "012345678",
            //});
            //return;



            var parser = CascadingGlyphParser.Default;
            var reader = new OcrRowReader(args[0]);
            var validator = new ChecksumAccountValidator();
            var results = await reader
                .Rows()
                .Select(glyphs =>
                {
                    var numbers = new Digit[glyphs.Count];

                    for(int i=0;i<numbers.Length;i++)
                    {
                        var glyph = glyphs[i];
                        if (parser.TryParse(glyph, out var number))
                        {
                            numbers[i] = new Digit(number, glyph);
                        }
                        else
                        {
                            numbers[i] = new Digit(glyph);
                        }
                    }
                    var account = new AccountNumber(numbers);

                    //if(row.Validity != RowValidity.Valid)
                    //{
                    //    var variants = row
                    //        .GenerateVariants()
                    //        .Where(v => v.Validity == RowValidity.Valid)
                    //        .ToArray();

                    //    switch(variants.Length)
                    //    {
                    //        case 0: return row;
                    //        case 1: return variants[0];
                    //        default:
                    //            row.Validity = RowValidity.Ambiguous;
                    //            return row;
                    //    }
                    //}

                    var validationResult = validator.Validate(account);

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
