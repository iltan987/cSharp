using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

namespace IcoConverter
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            openFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        void button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button == button1)
                Convert();
            else
                Convert(false);
        }

        void Convert(bool direction = true)
        {
            if (direction)
            {
                openFileDialog1.Filter = "*.ico|*.ico";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    using (FileStream fs = new FileStream(GetAvailable(Path.ChangeExtension(openFileDialog1.FileName, ".png")), FileMode.Create))
                        new Icon(openFileDialog1.FileName, -1, -1).ToBitmap().Save(fs, ImageFormat.Png);
            }
            else
            {
                openFileDialog1.Filter = "*.png|*.png";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    ConvertToIco(Image.FromFile(openFileDialog1.FileName), GetAvailable(Path.ChangeExtension(openFileDialog1.FileName, ".ico")));
            }
        }

        void ConvertToIco(Image img, string file)
        {
            using (var msIco = new MemoryStream())
            {
                using (var bw = new BinaryWriter(msIco))
                {
                    bw.Write((short)0); //Reserved. Must always be 0.
                    bw.Write((short)1); //Specifies image type: 1 for icon (.ICO) image, 2 for cursor (.CUR) image. Other values are invalid.
                    bw.Write((short)9); //Specifies number of images in the file.



                    int[] vs = { 16, 24, 32, 48, 64, 96, 128, 192, 0 };

                    List<byte[]> images = new List<byte[]>();

                    int currentPos = 150;

                    for (int i = 0; i < 9; i++)
                    {
                        int current = vs[i];

                        bw.Write((byte)current); //Width
                        bw.Write((byte)current); //Height
                        bw.Write((byte)0); //Specifies number of colors in the color palette. Should be 0 if the image does not use a color palette.
                        bw.Write((byte)0); //Reserved. Should be 0
                        bw.Write((short)1); //Specifies color planes. Should be 0 or 1.
                        bw.Write((short)32); //Specifies bits per pixel.



                        Bitmap _b = new Bitmap(img, current == 0 ? 256 : current, current == 0 ? 256 : current);
                        byte[] _vs;
                        using (var _ms = new MemoryStream())
                        {
                            _b.Save(_ms, ImageFormat.Png);
                            _ms.Position = 0;
                            _vs = new byte[_ms.Length];
                            _ms.Read(_vs, 0, (int)_ms.Length);
                            images.Add(_vs);
                        }
                        bw.Write(_vs.Length);
                        bw.Write(currentPos);
                        currentPos += _vs.Length;
                    }

                    foreach (var item in images)
                        bw.Write(item);



                    bw.Seek(0, SeekOrigin.Begin);
                    Icon last = new Icon(msIco);
                    using (FileStream fs = new FileStream(file, FileMode.Create))
                        last.Save(fs);
                }
            }
        }

        string GetAvailable(string path)
        {
            string dir = Path.GetDirectoryName(path),
                fileName = Path.GetFileNameWithoutExtension(path),
                extension = Path.GetExtension(path),
                res = Path.Combine(dir, fileName + extension);
            for (ulong i = 1; File.Exists(res); i++)
                res = Path.Combine(dir, $"{fileName} ({i}){extension}");
            return res;
        }

        /*
        string Complete5(string val)
        {
            return val + "     ".Substring(0, 5 - val.Length);
        }
         */
    }
}