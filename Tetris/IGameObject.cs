using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public interface IGameObject
    {
        void Draw();
        void Update(int milliseconds);

    }
}
