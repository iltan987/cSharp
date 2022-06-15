using System;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form1 : Form
    {
        public bool justClose { get; set; } = false;

        public Form1()
        {
            InitializeComponent();
        }

        void btnStart_Click(object sender, EventArgs e)
        {
            Hide();
            new Form2(this).Show();
        }
        void btnExit_Click(object sender, EventArgs e) => Close();
        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!justClose && MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                e.Cancel = true;
        }
    }
}