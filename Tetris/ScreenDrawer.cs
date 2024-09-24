using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class ScreenDrawer : IScreenDrawer
    {
        private char[] _defaultBuffer;
        private int[] _yOffsets;

        public ScreenDrawer(int height, int width)
        {
            Height = height;
            Width = width;

            _yOffsets = CalculateYOffsets(height, width);

            _defaultBuffer = ConstructDefaultBuffer(height, width, _yOffsets);
            Buffer = GetCopyOfBuffer(_defaultBuffer);
        }

        public char[] Buffer { get; private set; }
        public int Height { get; }
        public int Width { get; }

        public void Draw(int x, int y, char character)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return;
            }

            int bufferIndex = GetBufferIndex(x, y);
            Buffer[bufferIndex] = character;
        }

        public char[] DrawFrame()
        {
            var outputBuffer = Buffer;
            Buffer = GetCopyOfBuffer(_defaultBuffer);
            return outputBuffer;
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

        private char[] ConstructDefaultBuffer(int height, int width, int[] yOffsets)
        {
            char[] buffer = new char[width * height + height];

            SetBufferToCharacter(buffer, ' ');

            for (int y = 0; y < height; y++)
            {
                buffer[yOffsets[y]] = '\n';
            }

            return buffer;
        }

        private int GetBufferIndex(int x, int y) =>
            x + _yOffsets[y];

        private char[] GetCopyOfBuffer(char[] original)
        {
            var copy = new char[original.Length];
            original.CopyTo(copy, 0);
            return copy;
        }

        private void SetBufferToCharacter(char[] buffer, char character)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = character;
            }
        }
    }
}
