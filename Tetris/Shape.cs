using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Shape : IGameObject, IShapeCollidable
    {
        private int elapsedTime = 0;
        public int X { get; set; }
        private int yPosition = 0;
        public int Y
        {
            get { return yPosition; }
            set
            {
                yPosition = value;
                foreach (var block in Blocks) { block.Y = value; }
            }
        }
        public Block[] Blocks { get; set; } = [];
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
            elapsedTime += (int)milliseconds;
            int numberOfPositionsToMoveDown = elapsedTime / 100;
            elapsedTime %= 100;
            MoveDown(numberOfPositionsToMoveDown);
            foreach (var block in Blocks)
            {
                block.X = this.X;
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
                MoveDown(1);
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

        private void MoveDown(int positions)
        {
            this.Y+= positions;
        }

        public bool IsCollidingWIthShape(Shape othershape)
        {
   foreach (var otherblock in othershape.Blocks)
            {
                if (this.Blocks.Any(b => b.GetAbsoluteX() == otherblock.GetAbsoluteX() && b.GetAbsoluteY() == otherblock.GetAbsoluteY() + 1))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
