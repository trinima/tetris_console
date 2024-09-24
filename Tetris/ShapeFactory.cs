using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris;

public static class ShapeFactory
{
    private static Shape MakeNewShape(int x, int y, params Block[] blocks)
    {
        var shape = new Shape();

        shape.Blocks = blocks;
        shape.X = x;
        shape.Y = y;

        return shape;
    }

    public static Shape CreatePyramid(int x, int y)
    {
        return MakeNewShape(
            x,
            y,
            new Block() { OffsetX = 0, OffsetY = 1 },
            new Block() { OffsetX = 1, OffsetY = 1 },
            new Block() { OffsetX = 2, OffsetY = 1 },
            new Block() { OffsetX = 1, OffsetY = 0 }
        );
    }

    public static Shape CreateSquare(int x, int y)
    {
        return MakeNewShape(
            x,
            y,
            new Block() { OffsetX = 0, OffsetY = 0 },
            new Block() { OffsetX = 0, OffsetY = 1 },
            new Block() { OffsetX = 1, OffsetY = 0 },
            new Block() { OffsetX = 1, OffsetY = 1 }
        );
    }
    public static Shape CreateBar(int x, int y)
    {
        return MakeNewShape(
            x,
            y,
            new Block() { OffsetX = 0, OffsetY = 0 },
            new Block() { OffsetX = 1, OffsetY = 0 },
            new Block() { OffsetX = 2, OffsetY = 0 },
            new Block() { OffsetX = 3, OffsetY = 0 }
        );
    }

    public static Shape CreateZigZagtopleft(int x, int y)
    {
        return MakeNewShape(
            x,
            y,
            new Block() { OffsetX = 0, OffsetY = 0 },
            new Block() { OffsetX = 1, OffsetY = 0 },
            new Block() { OffsetX = 1, OffsetY = 1 },
            new Block() { OffsetX = 2, OffsetY = 1 }
        );
    }

    public static Shape CreateZigZagTopRight(int x, int y)
    {
        return MakeNewShape(
            x,
            y,
            new Block() { OffsetX = 2, OffsetY = 0 },
            new Block() { OffsetX = 3, OffsetY = 0 },
            new Block() { OffsetX = 1, OffsetY = 1 },
            new Block() { OffsetX = 2, OffsetY = 1 }
        );
    }
    public static Shape CreateLeftL(int x, int y)
    {
        return MakeNewShape(
            x,
            y,
            new Block() { OffsetX = 0, OffsetY = 2 },
            new Block() { OffsetX = 1, OffsetY = 0 },
            new Block() { OffsetX = 1, OffsetY = 1 },
            new Block() { OffsetX = 1, OffsetY = 2 }
        );
    }
    public static Shape CreateRightL(int x, int y)
    {
        return MakeNewShape(
            x,
            y,
            new Block() { OffsetX = 0, OffsetY = 0 },
            new Block() { OffsetX = 0, OffsetY = 1 },
            new Block() { OffsetX = 0, OffsetY = 2 },
            new Block() { OffsetX = 1, OffsetY = 2 }
        );
    }

    public static Shape? CreateRandomShape(int x, int y)
    {
        Random random = new Random();
        int randomShape = random.Next(0, 7);
        switch (randomShape)
        {
            case 0:
                return CreateSquare(x, y);
            case 1:
                return CreateBar(x, y);
            case 2:
                return CreateLeftL(x, y);
            case 3:
                return CreateRightL(x, y);
            case 4:
                return CreateZigZagtopleft(x, y);
            case 5:
                return CreateZigZagTopRight(x, y);
            case 6:
                return CreatePyramid(x, y);
            default:
                return null;
        }
    }
}
