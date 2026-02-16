using System;
using System.Collections.Generic;
using System.Text;

namespace cs8_con_Patterns
{
    class Point
    {
        public int X { get; }

        public int Y { get; }

        public Point(int x, int y) => (X, Y) = (x, y);

        public void Deconstruct(out int x, out int y) => (x, y) = (X, Y);

    }
}
