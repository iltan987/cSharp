using System.Collections.Generic;
using System.Windows.Forms;

namespace Snake
{
    class Snake
    {
        public List<PictureBox> snakeParts { get; set; } = new List<PictureBox>();
        public Direction direction { get; set; } = Direction.RIGHT;


    }

    enum Direction
    {
        LEFT,
        RIGHT,
        UP,
        DOWN
    }
}