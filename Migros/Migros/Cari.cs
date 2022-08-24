using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Migros
{
    public class Cari
    {
        public Cari(ulong id, ulong cariNo, string isim, string kartNo)
        {
            Id = id;
            CariNo = cariNo;
            Isim = isim;
            KartNo = kartNo;
        }

        [JsonIgnore]
        public ulong Id { get; set; }

        public ulong CariNo { get; set; }
        public string Isim { get; set; }
        public string KartNo { get; set; }

        [JsonIgnore]
        public List<Siparis> Siparisler { get; set; }

        public void Save()
        {
            string cariDir = Path.Combine(Globals.database.CarilerDir, Id.ToString());
            if (!Directory.Exists(cariDir))
            {
                Directory.CreateDirectory(cariDir);
                Directory.CreateDirectory(Path.Combine(cariDir, Globals.database.SiparislerDirName));
                Directory.CreateDirectory(Path.Combine(cariDir, Globals.database.SilinenSiparislerDirName));
            }
            File.WriteAllText(Path.Combine(cariDir, "info.json"), JsonConvert.SerializeObject(this));
            if (Id > Globals.settings.lastCariId)
            {
                Globals.settings.lastCariId = Id;
                Globals.settings.Save();
            }
        }

        public void Delete()
        {
            string cariDir = Path.Combine(Globals.database.CarilerDir, Id.ToString());
            Directory.Move(cariDir, Path.Combine(Globals.database.SilinenCarilerDir, Id + "-" + DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss")));
        }

        public void GetSiparisler()
        {
            Siparisler = new List<Siparis>();

            string siparislerPath = Path.Combine(Globals.dir, Globals.database.CarilerDir, Id.ToString(), Globals.database.SiparislerDirName);
            if (!Directory.Exists(siparislerPath))
                Directory.CreateDirectory(siparislerPath);

            foreach (var item in Directory.GetDirectories(siparislerPath))
            {
                var siparis = JsonConvert.DeserializeObject<Siparis>(File.ReadAllText(Path.Combine(item, "info.json")));
                siparis.Id = ulong.Parse(Path.GetFileName(item));
                Siparisler.Add(siparis);
            }
        }

        public void SaveSiparis(Siparis siparis)
        {
            string siparisDir = Path.Combine(Globals.database.CarilerDir, Id.ToString(), Globals.database.SiparislerDirName, siparis.Id.ToString());
            if (!Directory.Exists(siparisDir))
                Directory.CreateDirectory(siparisDir);
            File.WriteAllText(Path.Combine(siparisDir, "info.json"), JsonConvert.SerializeObject(siparis));
            if (siparis.Id > Globals.settings.lastSiparisId)
            {
                Globals.settings.lastSiparisId = siparis.Id;
                Globals.settings.Save();
            }
        }

        internal void DeleteSiparis(Siparis siparis)
        {
            Siparisler.Remove(siparis);
            string siparisDir = Path.Combine(Globals.database.CarilerDir, Id.ToString(), Globals.database.SiparislerDirName, siparis.Id.ToString());
            Directory.Move(siparisDir, Path.Combine(Globals.database.CarilerDir, Id.ToString(), Globals.database.SilinenSiparislerDirName, siparis.Id + "-" + DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss")));
        }
    }
}