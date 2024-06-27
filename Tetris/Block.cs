﻿using System;
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

        public void Draw()
        {
            Console.SetCursorPosition(this.OffsetX + X, this.OffsetY + Y);
            Console.ForegroundColor = ConsoleColor.Green;

            Console.Write("\u2588");
        }

        public void Update(int milliseconds)
        {
        }
    }
}