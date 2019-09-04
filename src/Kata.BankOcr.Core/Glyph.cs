using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using static Kata.BankOcr.Core.Glyph;

namespace Kata.BankOcr.Core
{
    public class Glyph : IEnumerable<GlyphCharacter>
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

        private readonly GlyphCharacter[,] characters;

        public GlyphCharacter this[int x, int y] => characters[y, x];

        public int Width => characters.GetLength(1);

        public int Height => characters.GetLength(0);

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
            this.characters = new GlyphCharacter[3,3];
            for(var x=0;x<3;x++)
            {
                for (var y = 0; y < 3; y++)
                {
                    //we're transposing here as x, y is more common mathematics, but they're read into
                    //the character matrix the other way round in unit tests, as it's readable that way
                    this.characters[x, y] = new GlyphCharacter(x, y, characters[y, x]);
                }
            }
        }

        private Glyph(GlyphCharacter[,] characters)
        {
            this.characters = characters;
        }

        public Glyph Mutate(GlyphCharacter original, char replacement)
        {
            var newCharacters = (GlyphCharacter[,])characters.Clone();
            newCharacters[original.X, original.Y] = new GlyphCharacter(original.X, original.Y, replacement);
            return new Glyph(newCharacters);
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
                    if(!this[x,y].Equals(other[x,y]))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return 646361025 + EqualityComparer<GlyphCharacter[,]>.Default.GetHashCode(characters);
        }

        public IEnumerator<GlyphCharacter> GetEnumerator() => EnumerateCharacters().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private IEnumerable<GlyphCharacter> EnumerateCharacters()
        {
            for(var x=0;x<Width;x++)
            {
                for(var y=0;y<Height;y++)
                {
                    yield return this[x,y];
                }
            }
        }

        public class GlyphCharacter
        {
            public int X { get; }
            public int Y { get; }
            public char Character { get; }

            public GlyphCharacter(int x, int y, char character)
            {
                X = x;
                Y = y;
                Character = character;
            }

            public static implicit operator char(GlyphCharacter character) => character.Character;


            public override bool Equals(object obj)
            {
                if(obj is GlyphCharacter other)
                {
                    return Equals(other);
                }

                return false;
            }

            public bool Equals(GlyphCharacter other)
            {
                return X == other.X &&
                       Y == other.Y &&
                       Character == other.Character;
            }

            public override int GetHashCode()
            {
                var hashCode = -82981765;
                hashCode = hashCode * -1521134295 + X.GetHashCode();
                hashCode = hashCode * -1521134295 + Y.GetHashCode();
                hashCode = hashCode * -1521134295 + Character.GetHashCode();
                return hashCode;
            }
        }

    }
}
