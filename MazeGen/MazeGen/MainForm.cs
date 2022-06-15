using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MazeGen
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        Bitmap b;

        int big = 30;
        int width = 600, height = 600;

        async void MainForm_Load(object sender, EventArgs e)
        {
            ClientSize = new Size(width + 50, height + 50);
            pictureBox1.Width = width;
            pictureBox1.Height = height;
            b = new Bitmap(width, height);
            await GenerateMaze();
            pictureBox1.Image = b;
        }

        Random rnd = new Random();
        List<(int, int)> visitedCells = new List<(int, int)>();

        Task GenerateMaze() => Task.Run(() =>
        {
            visitedCells.Clear();
            visitedCells.Add((0, 0));
            using (Graphics g = Graphics.FromImage(b))
                g.Clear(Color.White);

            using (Graphics g = Graphics.FromImage(b))
            {
                int x = big, y = 0;
                while (y < height)
                {
                    x += big;
                    if (x >= width)
                    {
                        x = 0;
                        y += big;
                    }
                    List<(int, int)> neighnoringCells = NeighboringCells(x, y);
                    var randomCell = neighnoringCells[rnd.Next(neighnoringCells.Count)];
                    if (!visitedCells.Any(f => f.Item1 == randomCell.Item1 && f.Item2 == randomCell.Item2))
                        g.DrawLine(Pens.Blue, x, y, randomCell.Item1, randomCell.Item2);

                    visitedCells.Add(randomCell);
                }
            }
        });

        List<(int, int)> NeighboringCells(int x, int y)
        {
            List<(int, int)> neighboringCells = new List<(int, int)>();

            if (x - big >= 0)
                neighboringCells.Add((x - big, y));
            if (x + big <= width)
                neighboringCells.Add((x + big, y));
            if (y - big >= 0)
                neighboringCells.Add((x, y - big));
            if (y + big <= height)
                neighboringCells.Add((x, y + big));

            return neighboringCells;
        }

        void DrawPixel(int x, int y, Color c)
        {
            for (int i = 0; i < big; i++)
                for (int ii = 0; ii < big; ii++)
                    b.SetPixel(x + i, y + ii, c);
        }

        List<Point> _currStroke = new List<Point>();
        bool draw = false;

        void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;

            Cursor.Position = pictureBox1.PointToScreen(new Point(3, 3));
        }

        void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Kaybettiniz!");
            Application.Exit();
        }

        Color color = Color.FromArgb(0, 0, 255);

        void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (!draw)
                return;
            int x = e.Location.X, y = e.Location.Y;
            if (x < 0 || x >= pictureBox1.Width || y < 0 || y >= pictureBox1.Height)
                return;
            else if (x > pictureBox1.Width - big && y > pictureBox1.Height - big)
            {
                MessageBox.Show("Kazandınız!");
                Application.Exit();
            }

            if (b.GetPixel(x, y).Equals(color))
            {
                MessageBox.Show("Kaybettiniz!");
                Application.Exit();
            }
            else
            {
                _currStroke.Add(e.Location);
                Refresh();
            }
        }

        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (_currStroke.Count > 1)
                e.Graphics.DrawLines(Pens.Black, _currStroke.ToArray());
        }

        async void button1_Click(object sender, EventArgs e)
        {
            Cursor.Position = pictureBox1.PointToScreen(Point.Empty);
            await GenerateMaze();
            pictureBox1.Image = b;
        }
    }
}