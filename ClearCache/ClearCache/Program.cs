using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace ClearCache
{
    static class Program
    {
        static NotifyIcon notifyIcon = new NotifyIcon();

        static void Main()
        {
            if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
            {
                notifyIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                notifyIcon.Visible = true;
                new DirectoryInfo(Path.GetTempPath()).DeleteMe();
                new DirectoryInfo(@"C:\Windows\Temp").DeleteMe();
                new DirectoryInfo(@"C:\Windows\Prefetch").DeleteMe();
                notifyIcon.ShowBalloonTip(0, "Clear Cache", "Cache cleanup successfully completed.", ToolTipIcon.Info);
            }
            else
                Process.Start(new ProcessStartInfo
                {
                    UseShellExecute = true,
                    WorkingDirectory = Environment.CurrentDirectory,
                    FileName = Application.ExecutablePath,
                    Verb = "runas"
                });
        }

        static void DeleteMe(this DirectoryInfo dir)
        {
            try
            {
                dir.Delete(true);
                dir.Create();
            }
            catch { }
        }
    }
}