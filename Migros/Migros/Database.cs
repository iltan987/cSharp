using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Migros
{
    internal class Database
    {
        public string CarilerDir { get; set; }
        public string SilinenCarilerDir;
        public string SiparislerDirName;
        public string SilinenSiparislerDirName;

        public Database()
        {
            CarilerDir = Path.Combine(Globals.dir, "Cariler");
            SilinenCarilerDir = Path.Combine(Globals.dir, "Silinen Cariler");
            SiparislerDirName = "Siparişler";
            SilinenSiparislerDirName = "Silinen Siparişler";
        }

        public List<Cari> GetCariler()
        {
            List<Cari> cariler = new List<Cari>();

            if (!Directory.Exists(CarilerDir))
                Directory.CreateDirectory(CarilerDir);
            if (!Directory.Exists(SilinenCarilerDir))
                Directory.CreateDirectory(SilinenCarilerDir);

            foreach (var item in Directory.GetDirectories(CarilerDir))
            {
                var cari = JsonConvert.DeserializeObject<Cari>(File.ReadAllText(Path.Combine(item, "info.json")));
                cari.Id = ulong.Parse(Path.GetFileName(item));
                cariler.Add(cari);
            }

            return cariler;
        }
    }
}