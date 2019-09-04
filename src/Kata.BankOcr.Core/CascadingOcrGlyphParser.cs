using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata.BankOcr
{
    public class CascadingOcrGlyphParser : IOcrGlphyParser
    {
        public  static CascadingOcrGlyphParser Default { get; } = new CascadingOcrGlyphParser(CharacterMatchingGlyphParser.Numbers.ToArray());


        private readonly IEnumerable<IOcrGlphyParser> parsers;

        public CascadingOcrGlyphParser(params IOcrGlphyParser[] parsers)
        {
            this.parsers = parsers;
        }

        public bool TryParse(OcrGlyph glyph, out int number)
        {
            foreach(var parser in parsers)
            {
                if(parser.TryParse(glyph, out number))
                {
                    return true;
                }
            }

            number = default;
            return false;
        }
    }
}
