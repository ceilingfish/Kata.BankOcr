using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr
{
    public class OcrGlyph
    {
        public char[,] Characters { get; }

        public int Row {  get; }

        public int Column { get; }

        public OcrGlyph(int row, int column, char[,] characters)
        {
            Row = row;
            Column = column;
            Characters = characters;
        }
    }
}
