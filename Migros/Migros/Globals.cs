using Newtonsoft.Json;
using System.IO;

namespace Migros
{
    public static class Globals
    {
        public static Settings settings { get; set; }
        public static Database database { get; set; }


        public const string ApplicationDirName = "Migros";
        public static string ApplicationDirPath { get; set; }

        public const string CarilerDirName = "Cariler";
        public static string CarilerDirPath { get; set; }

        public const string SilinenCarilerDirName = "Silinen Cariler";
        public static string SilinenCarilerDirPath { get; set; }

        public const string YedeklerDirName = "Yedekler";
        public static string YedeklerDirPath { get; set; }

        public const string SettingsFileName = "settings.json";
        public static string SettingsFilePath { get; set; }

        public const string SiparislerDirName = "Siparişler";
        public const string SilinenSiparislerDirName = "Silinen Siparişler";
        public const string infoJsonFileName = "info.json";
    }

    public class Settings
    {
        public decimal puanCarpani { get; set; } = 0.2M;
        public string cariNoFormat { get; set; } = "\\C0\\-00000";
        public string siparisNoFormat { get; set; } = "\\S000000";
        public string tarihFormat { get; set; } = "dd.MM.yyyy";
        public string tlFormat { get; set; } = "0.0## \\T\\L";
        public ulong lastCariId { get; set; } = 0;
        public ulong lastSiparisId { get; set; } = 0;

        public void Save() => File.WriteAllText(Globals.SettingsFilePath, JsonConvert.SerializeObject(this));

        public static void Read() => Globals.settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Globals.SettingsFilePath));
    }
}