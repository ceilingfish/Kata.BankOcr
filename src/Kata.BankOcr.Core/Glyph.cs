using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.BankOcr.Core
{
    public class Glyph
    {
        public static Glyph Zero { get; } = new Glyph(new char[,]
        {
            { ' ', '_', ' ' },
            { '|', ' ', '|' },
            { '|', '_', '|' }
        });

        public static Glyph One { get; } = new Glyph(new char[,]
        {
            { ' ', ' ', ' ' },
            { ' ', ' ', '|' },
            { ' ', ' ', '|' }
        });

        public static Glyph Two { get; } = new Glyph(new char[,]
        {
            { ' ', '_', ' ' },
            { ' ', '_', '|' },
            { '|', '_', ' ' }
        });

        public static Glyph Three { get; } = new Glyph(new char[,]
        {
            { ' ', '_', ' ' },
            { ' ', '_', '|' },
            { ' ', '_', '|' }
        });

        public static Glyph Four { get; } = new Glyph(new char[,]
        {
            { ' ', ' ', ' ' },
            { '|', '_', '|' },
            { ' ', ' ', '|' }
        });

        public static Glyph Five { get; } = new Glyph(new char[,]
        {
            { ' ', '_', ' ' },
            { '|', '_', ' ' },
            { ' ', '_', '|' }
        });

        public static Glyph Six { get; } = new Glyph(new char[,]
        {
            { ' ', '_', ' ' },
            { '|', '_', ' ' },
            { '|', '_', '|' }
        });

        public static Glyph Seven { get; } = new Glyph(new char[,]
        {
            { ' ', '_', ' ' },
            { ' ', ' ', '|' },
            { ' ', ' ', '|' }
        });

        public static Glyph Eight { get; } = new Glyph(new char[,]
        {
            { ' ', '_', ' ' },
            { '|', '_', '|' },
            { '|', '_', '|' }
        });


        public static Glyph Nine { get; } = new Glyph(new char[,]
        {
            { ' ', '_', ' ' },
            { '|', '_', '|' },
            { ' ', '_', '|' }
        });

        private readonly char[,] characters;

        public char this[int x, int y]
        {
            get => this.characters[y,x];
        }

        public Glyph(char[,] characters)
        {
            if(characters == null)
            {
                throw new ArgumentNullException(nameof(characters));
            }
            if(characters.GetLength(1) != 3 || characters.GetLength(1) != 3)
            {
                throw new ArgumentException("Characters must be a 3x3 matrix");
            }
            this.characters = characters;
        }

        public override bool Equals(object obj)
        {
            if(obj is Glyph other)
            {
                return Equals(other);
            }

            return false;
        }

        public bool Equals(Glyph other)
        {
            for(var x=0;x<3;x++)
            {
                for(var y=0;y<3;y++)
                {
                    if(this[x,y] != other[x,y])
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return 646361025 + EqualityComparer<char[,]>.Default.GetHashCode(characters);
        }
    }
}
