using System;

namespace SnakeGame
{
    internal class Coordinates : ICloneable
    {
        public Coordinates()
        {
        }

        public Coordinates(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public int x { get; set; }
        public int y { get; set; }

        public object Clone()
        {
            return new Coordinates(x, y);
        }
    }

    internal enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }
}