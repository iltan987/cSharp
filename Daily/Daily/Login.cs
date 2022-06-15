using System;
using System.IO;
using System.Windows.Forms;

namespace Daily
{
    public partial class Login : Form
    {
        public Login() => InitializeComponent();

        void button1_Click(object sender, EventArgs e)
        {
            string pass = C.DecryptString(File.ReadAllText("login.txt"));
            if (pass == textBox1.Text)
            {
                MainForm mainForm = new MainForm();
                Hide();
                _ = mainForm.ShowDialog();
                Close();
            }
            else
            {
                MessageBox.Show("Hatalı Şifre!");
                Application.Exit();
            }
        }
    }
}