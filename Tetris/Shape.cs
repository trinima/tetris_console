using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Shape : IGameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Block[] Blocks { get; set; }
        public bool IsFalling { get; set; } = true;

        public void Draw()
        {
            foreach (var block in Blocks)
            {
                block.Draw();
            }
        }

        public void Update(double milliseconds, ConsoleKeyInfo? pressedKey)
        {
            ProcessPressedKey(pressedKey);

            MoveDown(milliseconds);
            foreach (var block in Blocks)
            {
                block.X = this.X;
                block.Y = this.Y;
            }
        }

        private void ProcessPressedKey(ConsoleKeyInfo? pressedKey)
        {
            if (!pressedKey.HasValue)
            {
                return;
            }

            if (pressedKey.Value.Key == ConsoleKey.LeftArrow)
            {
                MoveLeft();
            }
            if (pressedKey.Value.Key == ConsoleKey.RightArrow)
            {
                MoveRight();
            }
            if (pressedKey.Value.Key == ConsoleKey.DownArrow)
            {
                MoveDown(0);
            }
        }

        private void MoveRight()
        {
            this.X += 1;
        }

        private void MoveLeft()
        {
            this.X -= 1;
        }

        private void MoveDown(double milliseconds)
        {
            this.Y++;
        }
    }
}
