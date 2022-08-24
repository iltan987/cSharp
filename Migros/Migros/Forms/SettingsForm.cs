using System;
using System.Windows.Forms;

namespace Migros.Forms
{
    public partial class SettingsForm : Form
    {
        public SettingsForm() => InitializeComponent();

        private void button1_Click(object sender, EventArgs e)
        {
            Globals.settings.puanCarpani = numericUpDown1.Value;
            Globals.settings.cariNoFormat = textBox1.Text;
            Globals.settings.siparisNoFormat = textBox2.Text;
            Globals.settings.tarihFormat = textBox3.Text;
            Globals.settings.tlFormat = textBox4.Text;
            Globals.settings.Save();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = Globals.settings.puanCarpani;
            textBox1.Text = Globals.settings.cariNoFormat;
            textBox2.Text = Globals.settings.siparisNoFormat;
            textBox3.Text = Globals.settings.tarihFormat;
            textBox4.Text = Globals.settings.tlFormat;
        }
    }
}