using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyColorPicker
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        void MainForm_Load(object sender, EventArgs e) => TakeSS();

        Task TakeSS() => Task.Run(async () =>
        {
            IntPtr dc = GetWindowDC(IntPtr.Zero);

            while (true)
            {
                if (canContinue)
                {
                    Point position = Cursor.Position;
                    Color color = Color.FromArgb(GetPixel(dc, position.X, position.Y));
                    textBox1.Invoke(new MethodInvoker(() => textBox1.Text = string.Format("{0}, {1}, {2}", color.B, color.G, color.R)));
                }
                await Task.Delay(10);
            }
        });

        bool canContinue = true;

        [DllImport("gdi32")]
        public static extern int GetPixel(IntPtr hDC, int XPos, int YPos);

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                canContinue = !canContinue;
        }
    }
}