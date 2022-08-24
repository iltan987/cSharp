using Newtonsoft.Json;
using System.IO;

namespace Migros
{
    internal static class Globals
    {
        public static string dir;
        public static Settings settings { get; set; }
        public static Database database { get; set; }
    }

    internal class Settings
    {
        public decimal puanCarpani { get; set; } = 0.2M;
        public string cariNoFormat { get; set; } = "\\C0\\-00000";
        public string siparisNoFormat { get; set; } = "\\S000000";
        public string tarihFormat { get; set; } = "dd.MM.yyyy";
        public string tlFormat { get; set; } = "0.0## \\T\\L";
        public ulong lastCariId { get; set; } = 0;
        public ulong lastSiparisId { get; set; } = 0;

        public void Save() => File.WriteAllText(Path.Combine(Globals.dir, "settings.json"), JsonConvert.SerializeObject(this));

        public static void Read()
        {
            if (File.Exists(Path.Combine(Globals.dir, "settings.json")))
                Globals.settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Path.Combine(Globals.dir, "settings.json")));
            else
            {
                Globals.settings = new Settings();
                Globals.settings.Save();
            }
        }
    }
}