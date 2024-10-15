using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public struct DrawCharacter
    {
        public DrawCharacter(char character, ConsoleColor color)
        {
            Character = character;
            Color = color;
        }

        public char Character { get; }
        public ConsoleColor Color { get; }

        public DrawCharacter Clone()
        {
            return new DrawCharacter(Character, Color);
        }
    }
}
