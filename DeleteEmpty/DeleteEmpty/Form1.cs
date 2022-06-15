using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace DeleteEmpty
{
    public partial class Form1 : Form
    {
        public Form1() => InitializeComponent();

        void button1_Click(object sender, EventArgs e)
        {
            List<string> folders = new List<string>();
            GetFolders(textBox1.Text, folders);
            if (folders.Count == 0)
                return;
            if (MessageBox.Show(folders.Count + " folders found. Delete all?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (var item in folders)
                    Directory.Delete(item);
                _ = MessageBox.Show(folders.Count + " folders deleted");
            }
        }

        void GetFolders(string path, List<string> list)
        {
            string[] vs = Directory.GetFiles(path),
                vs1 = Directory.GetDirectories(path);

            if (!vs.Any() && !vs1.Any())
                list.Add(path);
            else
                foreach (var item in vs1)
                    GetFolders(item, list);
        }

        void button2_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog cofd = new CommonOpenFileDialog { IsFolderPicker = true };
            if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
                textBox1.Text = cofd.FileName;
        }
    }
}