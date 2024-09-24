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

        public List<Shape> Shapes { get; set; } = [];
        public Shape FallingShape { get; set; }

        public void Draw(IScreenDrawer screenDrawer)
        {
            for (int shapeIndex = 0; shapeIndex < Shapes.Count; shapeIndex++)
            {
                Shapes[shapeIndex].Draw(screenDrawer);
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
                foreach (var shape in Shapes)
                {
                    if (FallingShape.IsCollidingWithShape(shape))
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
            Shapes.Add(FallingShape);
            FallingShape = ShapeFactory.CreateRandomShape(5, 1);
        }
    }
}
