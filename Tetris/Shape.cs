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
        private int _elapsedTime = 0;
        public int _xPosition = 0;
        public int X
        {
            get { return _xPosition; }
            set
            {
                _xPosition = value;
                foreach (var block in Blocks) {
                    block.X = value;
                }
            }
        }
        private int _yPosition = 0;
        public int Y
        {
            get { return _yPosition; }
            set
            {
                _yPosition = value;
                foreach (var block in Blocks) {
                    block.Y = value;
                }
            }
        }

        public int GetMinY()
        {
            return Blocks.Min(x => x.GetAbsoluteY());
        }
        public int GetMinX()
        {
            return Blocks.Min(x => x.GetAbsoluteX());
        }
        public int GetMaxX()
        {
            return Blocks.Max(x => x.GetAbsoluteX());
        }
        public int GetMaxY()
        {
            return Blocks.Max(x => x.GetAbsoluteY());
        }

        public Block[] Blocks { get; set; } = [];
        public Area Area { get; set; }

        public Shape(Area area)
        {
            this.Area = area;
        }
        public void Draw(IScreenDrawer screenDrawer)
        {
            foreach (var block in Blocks)
            {
                block.Draw(screenDrawer);
            }
        }

        public void Update(double milliseconds, ConsoleKeyInfo? pressedKey)
        {
            ProcessPressedKey(pressedKey);
            _elapsedTime += (int)milliseconds;
            int numberOfPositionsToMoveDown = _elapsedTime / 100;
            _elapsedTime %= 100;
            MoveDown(numberOfPositionsToMoveDown);
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
            MoveX(1);
        }

        private void MoveLeft()
        {
            MoveX(-1);
        }
        private void MoveX(int positions)
        {
            this.X += positions;
            if (this.Area.Blocks.Any(b => IsCollidingWithBlock(b)))
            {
                this.X-= positions;
            }
        }
        private void MoveDown(int positions)
        {
            this.Y+= positions;
        }

        public bool IsCollidingWithBlock(Block otherblock)
        {
                if (this.Blocks.Any(b => b.GetAbsoluteX() == otherblock.GetAbsoluteX() && b.GetAbsoluteY() == otherblock.GetAbsoluteY()))
                {
                    return true;
                }
            return false;
        }
    }
}
