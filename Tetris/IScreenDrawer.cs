using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public interface IScreenDrawer
    {
        void Draw(int x, int y, char character);
        public char[] DrawFrame();
    }
}
