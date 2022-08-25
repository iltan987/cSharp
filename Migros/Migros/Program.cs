using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Migros
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;


            Globals.ApplicationDirPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Globals.ApplicationDirName);
            if (!Directory.Exists(Globals.ApplicationDirPath))
                Directory.CreateDirectory(Globals.ApplicationDirPath);

            Globals.CarilerDirPath = Path.Combine(Globals.ApplicationDirPath, Globals.CarilerDirName);
            if (!Directory.Exists(Globals.CarilerDirPath))
                Directory.CreateDirectory(Globals.CarilerDirPath);

            Globals.SilinenCarilerDirPath = Path.Combine(Globals.ApplicationDirPath, Globals.SilinenCarilerDirName);
            if (!Directory.Exists(Globals.SilinenCarilerDirPath))
                Directory.CreateDirectory(Globals.SilinenCarilerDirPath);

            Globals.YedeklerDirPath = Path.Combine(Globals.ApplicationDirPath, Globals.YedeklerDirName);
            if (!Directory.Exists(Globals.YedeklerDirPath))
                Directory.CreateDirectory(Globals.YedeklerDirPath);

            Globals.SettingsFilePath = Path.Combine(Globals.ApplicationDirPath, Globals.SettingsFileName);
            if (!File.Exists(Globals.SettingsFilePath))
            {
                Globals.settings = new Settings();
                Globals.settings.Save();
            }

            Globals.database = new Database();

            Application.Run(new Forms.MainForm());
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string resourceName = new AssemblyName(args.Name).Name + ".dll", resource = Array.Find(Assembly.GetExecutingAssembly().GetManifestResourceNames(), element => element.EndsWith(resourceName));
            using (var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(resource))
            {
                byte[] assemblyData = new byte[stream.Length];
                _ = stream.Read(assemblyData, 0, assemblyData.Length);
                return Assembly.Load(assemblyData);
            }
        }
    }
}