using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    public interface IGlyphParser
    {
        bool TryParse(Glyph glyph, out int number);
    }
}
