using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kata.BankOcr
{
    public class CascadingGlyphParser : IGlyphParser
    {
        public  static CascadingGlyphParser Default { get; } = new CascadingGlyphParser(CharacterMatchingGlyphParser.Numbers.ToArray());


        private readonly IEnumerable<IGlyphParser> parsers;

        public CascadingGlyphParser(params IGlyphParser[] parsers)
        {
            this.parsers = parsers;
        }

        public bool TryParse(Glyph glyph, out int number)
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
