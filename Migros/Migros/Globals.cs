using Newtonsoft.Json;
using System.IO;

namespace Migros
{
    internal static class Globals
    {
        internal static Settings settings { get; set; }
        internal static Database database { get; set; }


        internal const string ApplicationDirName = "Migros";
        internal static string ApplicationDirPath { get; set; }

        internal const string CarilerDirName = "Cariler";
        internal static string CarilerDirPath { get; set; }

        internal const string SilinenCarilerDirName = "Silinen Cariler";
        internal static string SilinenCarilerDirPath { get; set; }

        internal const string YedeklerDirName = "Yedekler";
        internal static string YedeklerDirPath { get; set; }

        internal const string SettingsFileName = "settings.json";
        internal static string SettingsFilePath { get; set; }

        internal const string SiparislerDirName = "Siparişler";
        internal const string SilinenSiparislerDirName = "Silinen Siparişler";
        internal const string infoJsonFileName = "info.json";
    }

    internal class Settings
    {
        internal decimal puanCarpani { get; set; } = 0.2M;
        internal string cariNoFormat { get; set; } = "\\C0\\-00000";
        internal string siparisNoFormat { get; set; } = "\\S000000";
        internal string tarihFormat { get; set; } = "dd.MM.yyyy";
        internal string tlFormat { get; set; } = "0.0## \\T\\L";
        internal ulong lastCariId { get; set; } = 0;
        internal ulong lastSiparisId { get; set; } = 0;

        internal void Save() => File.WriteAllText(Globals.SettingsFilePath, JsonConvert.SerializeObject(this));

        internal static void Read() => Globals.settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(Globals.SettingsFilePath));
    }
}