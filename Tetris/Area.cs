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
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < Width; i++)
            {
                Console.Write("_");
            }

            Console.Clear();

            foreach (Shape shape in Shapes)
            {
                shape.Draw();
            }
        }

        public void Update(int milliseconds)
        {
            foreach (Shape shape in Shapes)
            {
                shape.Update(milliseconds);
            }
        }
    }
}
