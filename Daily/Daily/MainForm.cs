using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Daily
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            monthCalendar1.MinDate = DateTime.MinValue;
            monthCalendar1.MaxDate = DateTime.MaxValue;
            monthCalendar1.TodayDate = DateTime.Today;
            textBox1.Text = C.GetGun(monthCalendar1.SelectionStart.Date);
        }

        async void textBox1_TextChanged(object sender, EventArgs e) => await Task.Run(() =>
        {
            string path = @"daily\" + monthCalendar1.SelectionStart.ToString("dd.MM.yyyy");
            if (textBox1.Text == "")
            {
                if (File.Exists(path))
                    File.Delete(path);
            }
            else
                C.WriteGun(monthCalendar1.SelectionStart.Date, textBox1.Text);
        });

        void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e) => textBox1.Text = C.GetGun(monthCalendar1.SelectionStart.Date);

        void button1_Click(object sender, EventArgs e)
        {
            string path = @"daily\" + monthCalendar1.SelectionStart.ToString("dd.MM.yyyy");
            if (File.Exists(path))
                if (MessageBox.Show("Emin misin?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    textBox1.Text = "";
                    File.Delete(path);
                }
        }
    }
}