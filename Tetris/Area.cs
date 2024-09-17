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

        public List<Shape> Shapes { get; set; }

        public void Draw()
        {
            //Console.SetCursorPosition(0, 0);

            //for (int i = 0; i < Width; i++)
            //{
            //    Console.Write("_");
            //}

            Console.Clear();

            foreach (Shape shape in Shapes)
            {
                shape.Draw();
            }
        }

        public void Update(double milliseconds, ConsoleKeyInfo? pressedKey)
        {
            bool hasShapeHitFloor = false;

            foreach (Shape shape in Shapes.Where(s => s.IsFalling))
            {
                shape.Update(milliseconds, pressedKey);

                if (IsShapeOnFloor(shape))
                {
                    hasShapeHitFloor = true;
                    ProcessShapeHitFloor(shape);
                }
                CheckXBoundaries(shape);
            }

            if (hasShapeHitFloor)
            {
                SpawnNewShape();
            }
        }

        private void CheckXBoundaries(Shape shape)
        {
            if (shape.X < 1)
            {
                shape.X = 1;
            }
            else if (shape.X > Width - 2)
            {
                shape.X = Width - 2;
            }
        }

        public void CheckCollision()
        {
            var fallingShape = Shapes.FirstOrDefault(s => s.IsFalling);

            if (fallingShape != null)
            {
                foreach (var shape in Shapes.Where(s => !s.IsFalling))
                {
                 if (fallingShape.IsCollidingWIthShape(shape))
                    {
                        fallingShape.IsFalling = false;
                        break;
                    }
                }
            }
        }

        private bool IsShapeOnFloor(Shape shape)
        {
            return shape.Y > Height;
        }

        private void ProcessShapeHitFloor(Shape shape)
        {
            shape.Y = Height;
            shape.IsFalling = false;

        }

        private void SpawnNewShape()
        {
            this.Shapes.Add(ShapeFactory.CreateRandomShape(5, 1));
        }
    }
}
