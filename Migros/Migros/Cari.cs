using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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
        public List<Siparis> Siparisler { get; set; }

        [JsonIgnore]
        private bool siparislerHasLoaded = false;

        public void Save()
        {
            string cariDir = Path.Combine(Globals.CarilerDirPath, Id.ToString());
            if (!Directory.Exists(cariDir))
            {
                Directory.CreateDirectory(cariDir);
                Directory.CreateDirectory(Path.Combine(cariDir, Globals.SiparislerDirName));
                Directory.CreateDirectory(Path.Combine(cariDir, Globals.SilinenSiparislerDirName));
            }
            File.WriteAllText(Path.Combine(cariDir, Globals.infoJsonFileName), JsonConvert.SerializeObject(this));
            if (Id > Globals.settings.lastCariId)
            {
                Globals.settings.lastCariId = Id;
                Globals.settings.Save();
            }
        }

        public void Delete()
        {
            string cariDir = Path.Combine(Globals.CarilerDirPath, Id.ToString());
            Directory.Move(cariDir, Path.Combine(Globals.SilinenCarilerDirPath, Id + "-" + DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss")));
        }

        public void GetSiparisler()
        {
            if (siparislerHasLoaded)
                return;
            Siparisler = new List<Siparis>();

            string siparislerPath = Path.Combine(Globals.CarilerDirPath, Id.ToString(), Globals.SiparislerDirName);
            if (!Directory.Exists(siparislerPath))
                Directory.CreateDirectory(siparislerPath);

            foreach (var item in Directory.GetDirectories(siparislerPath))
            {
                var siparis = JsonConvert.DeserializeObject<Siparis>(File.ReadAllText(Path.Combine(item, Globals.infoJsonFileName)));
                siparis.Id = ulong.Parse(Path.GetFileName(item));
                Siparisler.Add(siparis);
            }
            siparislerHasLoaded = true;
        }

        public void SaveSiparis(Siparis siparis)
        {
            string siparisDir = Path.Combine(Globals.CarilerDirPath, Id.ToString(), Globals.SiparislerDirName, siparis.Id.ToString());
            if (!Directory.Exists(siparisDir))
                Directory.CreateDirectory(siparisDir);
            File.WriteAllText(Path.Combine(siparisDir, Globals.infoJsonFileName), JsonConvert.SerializeObject(siparis));
            if (siparis.Id > Globals.settings.lastSiparisId)
            {
                Globals.settings.lastSiparisId = siparis.Id;
                Globals.settings.Save();
            }
        }

        public void DeleteSiparis(Siparis siparis)
        {
            Siparisler.Remove(siparis);
            string siparisDir = Path.Combine(Globals.CarilerDirPath, Id.ToString(), Globals.SiparislerDirName, siparis.Id.ToString());
            Directory.Move(siparisDir, Path.Combine(Globals.CarilerDirPath, Id.ToString(), Globals.SilinenSiparislerDirName, siparis.Id + "-" + DateTime.Now.ToString("dd.MM.yyyy HH.mm.ss")));
        }

        public Toplamlar GetToplamlar() => siparislerHasLoaded ? new Toplamlar(Siparisler.Select(f => f.Puan).Sum(), Siparisler.Select(f => f.Kullanilan).Sum()) : new Toplamlar(0, 0);

        public class Toplamlar
        {
            public Toplamlar(decimal tPuan, decimal tKullanilan)
            {
                this.tPuan = tPuan;
                this.tKullanilan = tKullanilan;
            }

            public decimal tPuan { get; set; }
            public decimal tTL => tPuan * Globals.settings.puanCarpani;
            public decimal tKullanilan { get; set; }
            public decimal kalan => tTL - tKullanilan;
        }
    }
}