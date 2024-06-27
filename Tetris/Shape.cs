using System;
using System.Collections.Generic;
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

        public void Draw()
        {
            foreach (var block in Blocks)
            {
                block.Draw();
            }
        }

        public void Update(int milliseconds)
        {
            MoveDown(milliseconds);
            foreach (var block in Blocks)
            {
                block.X = this.X;
                block.Y = this.Y;
            }
        }

        private void MoveDown(int milliseconds)
        {
            this.Y++;
        }
    }
}
