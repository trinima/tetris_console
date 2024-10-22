using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class Block : IGameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int OffsetX { get; set; }
        public int OffsetY { get; set; }

        public void Draw(IScreenDrawer screenDrawer)
        {
            screenDrawer.Draw(this.OffsetX + X, this.OffsetY + Y, 'x', ConsoleColor.Yellow);
        }

        public void Update(double milliseconds, ConsoleKeyInfo? keyInfo)
        {
        }

        public int GetAbsoluteX()
        {
            return this.OffsetX + this.X;
        }
        public int GetAbsoluteY()
        {
            return this.OffsetY + this.Y;
        }
    }
}
