using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class ShapeFactory
    {
        //TODO: Make methods for the following shapes:

        // Horizontal Bar:   ####

        // ZigZag top left   ##
        //                    ##

        // ZigZag top right   ##
        //                   ##

        // Left L
        // Right L

        public static Shape CreatePyramid(int x, int y)
        {
            return new Shape()
            {
                X = x,
                Y = y,
                Blocks =
                [
                    new Block() { OffsetX = 0, OffsetY = 0 },

                    new Block() { OffsetX = -1, OffsetY = 1 },
                    new Block() { OffsetX = 0, OffsetY = 1 },
                    new Block() { OffsetX = 1, OffsetY = 1 }
                ]
            };
        }

        public static Shape CreateSquare(int x, int y)
        {
            return new Shape()
            {
                X = x,
                Y = y,
                Blocks =
                [
                    new Block() { OffsetX = 0, OffsetY = 0 },
                    new Block() { OffsetX = 0, OffsetY = 1 },
                    new Block() { OffsetX = 1, OffsetY = 0 },
                    new Block() { OffsetX = 1, OffsetY = 1 }
                ]
            };
        }

    }
}
