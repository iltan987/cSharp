using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlaceholderTextBox
{
    public class PlaceholderTextBox : TextBox
    {
        const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        string placeHolderText;

        public string PlaceholderText
        {
            get { return placeHolderText; }
            set { placeHolderText = value; SendMessage(Handle, EM_SETCUEBANNER, 0, value); }
        }

    }
}