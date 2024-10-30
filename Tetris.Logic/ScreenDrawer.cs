using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace Tetris
{
    public class ScreenDrawer : IScreenDrawer
    {
        private ConsoleColor DEFAULT_COLOR = ConsoleColor.White;
        private DrawCharacter[,] _defaultBuffer;
        private int[] _yOffsets;
        private Graphics _graphics;
        private IntPtr _desktopPtr;

        public ScreenDrawer(int height, int width, Graphics graphics)
        {
            Height = height;
            Width = width;

            _graphics = graphics;
            _yOffsets = CalculateYOffsets(height, width);

            //_defaultBuffer = ConstructDefaultBuffer(height, width, _yOffsets);
            //Buffer = GetCopyOfBuffer(_defaultBuffer);
        }

        public DrawCharacter[,] Buffer { get; private set; }
        public int Height { get; }
        public int Width { get; }

        public void Draw(int x, int y, char character, Color color)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                return;
            }

            RectangleF rectangle = new RectangleF(x * 10, y * 10, 10, 10);
            this._graphics.FillRectangle(new SolidBrush(color), rectangle);
        }


        public void Clear()
        {
            this._graphics.Clear(Color.White);
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
