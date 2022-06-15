using System;
using System.Drawing;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form2 : Form
    {
        public Form1 form1 { get; set; }
        public bool justClose { get; set; } = false;

        public Form2(Form1 form1)
        {
            InitializeComponent();
            this.form1 = form1;
        }

        readonly Snake snake = new Snake();
        readonly Random rnd = new Random();
        PictureBox apple;
        int score = 0;

        void Form2_Load(object sender, EventArgs e)
        {
            AddSnakePart(40, 40);
            CreateApple();
            timer1.Start();
        }

        void CreateApple()
        {
            apple = new PictureBox
            {
                Size = new Size(20, 20),
                Location = new Point(rnd.Next(panel1.Width / 20) * 20, rnd.Next(panel1.Height / 20) * 20),
                BackColor = Color.Red
            };
            panel1.Controls.Add(apple);
        }

        void AddSnakePart(int x, int y)
        {
            PictureBox pb = new PictureBox
            {
                Size = new Size(20, 20),
                Location = new Point(x, y),
                BackColor = Color.Lime,
                BorderStyle = BorderStyle.FixedSingle
            };
            snake.snakeParts.Add(pb);
            panel1.Controls.Add(pb);
        }

        void timer1_Tick(object sender, EventArgs e)
        {
            MoveSnake();
            CheckApple();
        }

        void MoveSnake()
        {
            for (int i = snake.snakeParts.Count - 1; i > 0; i--)
            {
                PictureBox old = snake.snakeParts[i - 1];
                PictureBox current = snake.snakeParts[i];

                if (old.Location.X > panel1.Width)
                    current.Location = new Point(0, old.Location.Y);
                else if (old.Location.X < 0)
                    current.Location = new Point(panel1.Width - (panel1.Width % 20), old.Location.Y);
                else if (old.Location.Y > panel1.Height)
                    current.Location = new Point(old.Location.X, 0);
                else if (old.Location.Y < 0)
                    current.Location = new Point(old.Location.X, panel1.Height - (panel1.Height % 20));
                else
                    current.Location = old.Location;
            }
            PictureBox pb = snake.snakeParts[0];
            if (pb.Location.X > panel1.Width)
                pb.Location = new Point(0, pb.Location.Y);
            else if (pb.Location.X < 0)
                pb.Location = new Point(panel1.Width - (panel1.Width % 20), pb.Location.Y);
            else if (pb.Location.Y > panel1.Height)
                pb.Location = new Point(pb.Location.X, 0);
            else if (pb.Location.Y < 0)
                pb.Location = new Point(pb.Location.X, panel1.Height - (panel1.Height % 20));
            else
            {
                switch (snake.direction)
                {
                    case Direction.LEFT:
                        pb.Location = new Point(pb.Location.X - 20, pb.Location.Y);
                        break;
                    case Direction.RIGHT:
                        pb.Location = new Point(pb.Location.X + 20, pb.Location.Y);
                        break;
                    case Direction.UP:
                        pb.Location = new Point(pb.Location.X, pb.Location.Y - 20);
                        break;
                    case Direction.DOWN:
                        pb.Location = new Point(pb.Location.X, pb.Location.Y + 20);
                        break;
                    default:
                        break;
                }
            }
        }

        void CheckApple()
        {
            PictureBox head = snake.snakeParts[0];
            if (head.Location == apple.Location)
            {
                if (timer1.Interval != 50)
                    timer1.Interval -= 50;
                panel1.Controls.Remove(apple);
                score += 100;
                label1.Text = "Score: " + score;
                CreateApple();

                PictureBox last = snake.snakeParts[0];
                AddSnakePart(last.Location.X, last.Location.Y);
            }
        }

        void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!justClose && MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;
        }

        void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                timer1.Stop();
                if (MessageBox.Show("Do you want to exit?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    justClose = true;
                    Close();
                    form1.Show();
                }
                else
                    timer1.Start();
            }
            else
            {
                if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                    snake.direction = Direction.UP;
                else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                    snake.direction = Direction.LEFT;
                else if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
                    snake.direction = Direction.DOWN;
                else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                    snake.direction = Direction.RIGHT;
            }
        }
    }
}