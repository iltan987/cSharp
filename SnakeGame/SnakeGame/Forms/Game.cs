using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace SnakeGame.Forms
{
    public partial class Game : Form
    {
        public Game()
        {
            InitializeComponent();
        }

        System.Threading.Timer timer;
        Coordinates apple;
        List<Coordinates> snake;
        Direction direction;
        Random rnd;
        int pixelWidth = 16, pixelHeight = 16;
        int dueTime = 250, period = 250;
        int score = 0;
        int maxWidth, maxHeight;

        void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Game_Load(object sender, EventArgs e)
        {
            timer = new System.Threading.Timer(TimerCallBack);

            RestartGame();
        }

        private void RestartGame()
        {
            rnd = new Random();
            apple = new Coordinates();
            snake = new List<Coordinates>();
            direction = Direction.RIGHT;


            maxWidth = pictureBox1.Width / pixelWidth;
            maxHeight = pictureBox1.Height / pixelHeight;
            apple.x = rnd.Next(0, maxWidth);
            apple.y = rnd.Next(0, maxHeight);

            Coordinates head = new Coordinates();

            do
            {
                head.x = rnd.Next(3, maxWidth - 3);
                head.y = rnd.Next(0, maxHeight);
            } while (head.x == apple.x && head.y == apple.y);
            snake.Add(head);

            Coordinates before;
            for (int i = 1; i <= 2; i++)
            {
                before = snake[i - 1];
                snake.Add(new Coordinates(before.x - 1, before.y));
            }
            pictureBox1.Invalidate();

            timer.Change(dueTime, period);
        }

        void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(Brushes.DarkRed, apple.x * pixelWidth, apple.y * pixelHeight, pixelWidth, pixelHeight);

            Brush b = Brushes.DarkGreen;

            Coordinates item;
            for (int i = snake.Count - 1; i >= 0; i--)
            {
                item = snake[i];
                e.Graphics.FillRectangle(b, item.x * pixelWidth + 1, item.y * pixelHeight + 1, pixelWidth - 1, pixelHeight - 1);
            }
        }

        void TimerCallBack(object state)
        {
            timer.Change(Timeout.Infinite, Timeout.Infinite);

            for (int i = snake.Count - 1; i > 0; i--)
            {
                Coordinates coords = snake[i], coords2 = snake[i - 1];
                coords.x = coords2.x;
                coords.y = coords2.y;
            }

            Coordinates head = snake[0];

            if (direction == Direction.DOWN)
                head.y++;
            else if (direction == Direction.LEFT)
                head.x--;
            else if (direction == Direction.RIGHT)
                head.x++;
            else
                head.y--;

            if (head.x == apple.x && head.y == apple.y)
            {
                apple.x = rnd.Next(0, maxWidth);
                apple.y = rnd.Next(0, maxHeight);

                score += 100;
                label1.Invoke(new MethodInvoker(() => label1.Text = $"Score: {score}"));
                snake.Add(new Coordinates(snake[snake.Count - 1].x - 1, snake[snake.Count - 1].y));
            }

            pictureBox1.Invalidate();

            timer.Change(dueTime, period);
        }

        private void Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                return;
            }
            else if (e.KeyCode == Keys.Down || e.KeyCode == Keys.S)
            {
                if (direction != Direction.UP)
                    direction = Direction.DOWN;
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (direction != Direction.RIGHT)
                    direction = Direction.LEFT;
            }
            else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (direction != Direction.LEFT)
                    direction = Direction.RIGHT;
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.W)
            {
                if (direction != Direction.DOWN)
                    direction = Direction.UP;
            }
        }
    }
}