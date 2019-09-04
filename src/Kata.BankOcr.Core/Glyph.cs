using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    public class Glyph
    {
        public char[,] Characters { get; }

        public int Row {  get; }

        public int Column { get; }

        public Glyph(int row, int column, char[,] characters)
        {
            Row = row;
            Column = column;
            Characters = characters;
        }
    }
}
