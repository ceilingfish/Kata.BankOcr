using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr
{
    public interface IOcrGlphyParser
    {
        bool TryParse(OcrGlyph glyph, out int number);
    }
}
