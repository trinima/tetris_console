using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public interface IGameObject
    {
        void Draw(IScreenDrawer screenDrawer);
        void Update(double milliseconds, ConsoleKeyInfo? keyInfo);

    }
}
