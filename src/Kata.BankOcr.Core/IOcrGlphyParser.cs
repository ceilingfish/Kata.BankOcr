using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr
{
    public interface IGlyphParser
    {
        bool TryParse(Glyph glyph, out int number);
    }
}
