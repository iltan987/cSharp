using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Migros
{
    internal class Database
    {
        internal List<Cari> GetCariler()
        {
            List<Cari> cariler = new List<Cari>();

            foreach (var item in Directory.GetDirectories(Globals.CarilerDirPath))
            {
                var cari = JsonConvert.DeserializeObject<Cari>(File.ReadAllText(Path.Combine(item, Globals.infoJsonFileName)));
                cari.Id = ulong.Parse(Path.GetFileName(item));
                cariler.Add(cari);
            }

            return cariler;
        }

        internal void Backup()
        {
            var now = DateTime.Now;
            var target = Directory.CreateDirectory(Path.Combine(Globals.YedeklerDirPath, now.ToString("dd.MM.yyyy HH.mm.ss.fff")));
            File.Copy(Globals.SettingsFilePath, Path.Combine(target.FullName, Globals.SettingsFileName));
            CopyFilesRecursively(new DirectoryInfo(Globals.CarilerDirPath), target.CreateSubdirectory(Globals.CarilerDirName));
            CopyFilesRecursively(new DirectoryInfo(Globals.SilinenCarilerDirPath), target.CreateSubdirectory(Globals.SilinenCarilerDirName));

            var dirNames = Directory.GetDirectories(Globals.YedeklerDirPath).ToList().ConvertAll(f => new { path = f, time = DateTime.ParseExact(Path.GetFileName(f), "dd.MM.yyyy HH.mm.ss.fff", System.Globalization.CultureInfo.InvariantCulture) });

            var hmm = dirNames.Where(f => f.time.AddDays(9) < DateTime.Today).OrderBy(f => f.time).Take(Math.Max(dirNames.Count - 10, 0)).ToList();

            foreach (var item in hmm)
                Directory.Delete(item.path, true);
        }

        internal void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
        {
            foreach (DirectoryInfo dir in source.GetDirectories())
                CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
            foreach (FileInfo file in source.GetFiles())
                file.CopyTo(Path.Combine(target.FullName, file.Name));
        }
    }
}