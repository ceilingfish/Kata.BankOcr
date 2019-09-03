using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr
{
    public class CharacterMatchingGlyphParser : IOcrGlphyParser
    {
        public static CharacterMatchingGlyphParser One { get; } = new CharacterMatchingGlyphParser(new char[,]
        {
            { ' ', ' ', ' ' },
            { ' ', ' ', '|' },
            { ' ', ' ', '|' }
        }, 1);

        public static CharacterMatchingGlyphParser Two { get; } = new CharacterMatchingGlyphParser(new char[,]
        {
            { ' ', '_', ' ' },
            { ' ', '_', '|' },
            { '|', '_', ' ' }
        }, 2);

        public static CharacterMatchingGlyphParser Three { get; } = new CharacterMatchingGlyphParser(new char[,]
        {
            { ' ', '_', ' ' },
            { ' ', '_', '|' },
            { ' ', '_', '|' }
        }, 3);

        public static CharacterMatchingGlyphParser Four { get; } = new CharacterMatchingGlyphParser(new char[,]
        {
            { ' ', ' ', ' ' },
            { '|', '_', '|' },
            { ' ', ' ', '|' }
        }, 4);

        public static CharacterMatchingGlyphParser Five { get; } = new CharacterMatchingGlyphParser(new char[,]
        {
            { ' ', '_', ' ' },
            { '|', '_', ' ' },
            { ' ', '_', '|' }
        }, 5);

        public static CharacterMatchingGlyphParser Six { get; } = new CharacterMatchingGlyphParser(new char[,]
        {
            { ' ', '_', ' ' },
            { '|', '_', ' ' },
            { '|', '_', '|' }
        }, 6);

        public static CharacterMatchingGlyphParser Seven { get; } = new CharacterMatchingGlyphParser(new char[,]
        {
            { ' ', '_', ' ' },
            { ' ', ' ', '|' },
            { ' ', ' ', '|' }
        }, 7);

        public static CharacterMatchingGlyphParser Eight { get; } = new CharacterMatchingGlyphParser(new char[,]
        {
            { ' ', '_', ' ' },
            { '|', '_', '|' },
            { '|', '_', '|' }
        }, 8);

        public static CharacterMatchingGlyphParser Nine { get; } = new CharacterMatchingGlyphParser(new char[,]
        {
            { ' ', '_', ' ' },
            { '|', '_', '|' },
            { ' ', '_', '|' }
        }, 9);

        private readonly int matchResult;
        private readonly char[,] matchMatrix;

        public CharacterMatchingGlyphParser(char[,] matrix, int result)
        {
            this.matchMatrix = matrix;
            this.matchResult = result;
        }

        public bool TryParse(OcrGlyph glyph, out int number)
        {
            if (glyph.Characters.GetLength(0) != matchMatrix.GetLength(0) || glyph.Characters.GetLength(1) != matchMatrix.GetLength(1))
            {
                number = default;
                return false;
            }
            for(int y = 0;y<matchMatrix.GetLength(0);y++)
            {
                for (int x = 0; x < matchMatrix.GetLength(1); x++)
                {
                    if(matchMatrix[y,x] != glyph.Characters[y,x])
                    {
                        number = default;
                        return false;
                    }
                }
            }

            number = matchResult;
            return true;
        }
    }
}
