using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Area : IGameObject
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public List<Block> Blocks { get; set; } = [];

        public List<Shape> Shapes { get; set; } = [];
        private Shape _fallingShape;
        public Shape FallingShape
        {
            get { return _fallingShape; }
            set
            {
                _fallingShape = value;
                this._fallingShape.Area = this;
            }
        }
        public bool IsRunning { get;  set; }

        public void Draw(IScreenDrawer screenDrawer)
        {
            for (int blockIndex = 0; blockIndex < Blocks.Count; blockIndex++)
            {
                Blocks[blockIndex].Draw(screenDrawer);
            }

            FallingShape.Draw(screenDrawer);
        }

        public void Update(double milliseconds, ConsoleKeyInfo? pressedKey)
        {
            FallingShape.Update(milliseconds, pressedKey);

            if (IsShapeOnFloor(FallingShape))
            {
                ProcessShapeStopped();
            }

            CheckXBoundaries(FallingShape);
        }

        private void CheckXBoundaries(Shape shape)
        {
            if (shape.GetMinX() < 1)
            {
                shape.X++;
            }
            else if (shape.GetMaxX() >= this.Width)
            {
                shape.X--;
            }
        }

        public void CheckCollision()
        {
            if (FallingShape != null)
            {
                foreach (var Block in Blocks)
                {
                    if (FallingShape.IsCollidingWithBlock(Block))
                    {
                        FallingShape.Y -= 1;
                        ProcessShapeStopped();
                        break;
                    }
                }
            }
        }

        private bool IsShapeOnFloor(Shape shape)
        {
            return shape.GetMaxY() >= Height - 1;
        }

        private void ProcessShapeStopped()
        {
            if (FallingShape.GetMinY() < 1)
            {
                IsRunning = false;
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Game Over");
            }
            Blocks.AddRange(FallingShape.Blocks);
            FallingShape = ShapeFactory.CreateRandomShape(5, 1);
        }
    }
}
