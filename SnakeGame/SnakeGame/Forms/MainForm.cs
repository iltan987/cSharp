using System;
using System.Windows.Forms;

namespace SnakeGame.Forms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new Game().ShowDialog();
            Show();
        }
    }
}