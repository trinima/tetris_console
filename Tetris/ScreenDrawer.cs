using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class ScreenDrawer : IScreenDrawer
    {
        private ConsoleColor DEFAULT_COLOR = ConsoleColor.White;
        private DrawCharacter[] _defaultBuffer;
        private int[] _yOffsets;

        public ScreenDrawer(int height, int width)
        {
            Height = height;
            Width = width;

            _yOffsets = CalculateYOffsets(height, width);

            _defaultBuffer = ConstructDefaultBuffer(height, width, _yOffsets);
            Buffer = GetCopyOfBuffer(_defaultBuffer);
        }

        public DrawCharacter[] Buffer { get; private set; }
        public int Height { get; }
        public int Width { get; }

        public void Draw(int x, int y, char character, ConsoleColor? color)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return;
            }

            int bufferIndex = GetBufferIndex(x, y);
            Buffer[bufferIndex] = new DrawCharacter(character, color ?? DEFAULT_COLOR);
        }

        public DrawCharacter[] DrawFrame()
        {
            var outputBuffer = Buffer;
            Buffer = GetCopyOfBuffer(_defaultBuffer);
            return outputBuffer;
            //return new DrawCharacter [] 
            //{ 
            //new DrawCharacter('\n', DEFAULT_COLOR),
            //  new DrawCharacter('n', DEFAULT_COLOR),
            //    new DrawCharacter('j', DEFAULT_COLOR),
            //      new DrawCharacter('\n', DEFAULT_COLOR),
            //        new DrawCharacter('p', DEFAULT_COLOR),
            //};
        }

        private int[] CalculateYOffsets(int height, int width)
        {
            var offsets = new int[height];

            for (int i = 0; i < height; i++)
            {
                offsets[i] = ((i * width) + i);
            }

            return offsets;
        }

        private DrawCharacter[] ConstructDefaultBuffer(int height, int width, int[] yOffsets)
        {
            DrawCharacter[] buffer = new DrawCharacter[width * height + height];

            SetBufferToCharacter(buffer, ' ');

            for (int y = 0; y < height; y++)
            {
                buffer[yOffsets[y]] = new DrawCharacter('\n', DEFAULT_COLOR);
            }

            return buffer;
        }

        private int GetBufferIndex(int x, int y) =>
            x + _yOffsets[y];

        private DrawCharacter[] GetCopyOfBuffer(DrawCharacter[] original)
        {
            var copy = new DrawCharacter[original.Length];
            original.CopyTo(copy, 0);
            return copy;
        }

        private void SetBufferToCharacter(DrawCharacter[] buffer, char character)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = new DrawCharacter(character, DEFAULT_COLOR);
            }
        }
    }
}
