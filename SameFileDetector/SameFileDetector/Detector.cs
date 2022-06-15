using IWshRuntimeLibrary;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SameFileDetector
{
    public partial class Detector : Form
    {
        public Detector()
        {
            InitializeComponent();
            cofd = new CommonOpenFileDialog() { IsFolderPicker = true, RestoreDirectory = true };
            errorProvider1.SetError(btnBrowse, "Klasör Seç");
        }

        string path;
        CommonOpenFileDialog cofd;

        void btnBrowse_Click(object sender, EventArgs e)
        {
            if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
            {
                errorProvider1.Clear();
                path = tbDirectory.Text = cofd.FileName;
                if (MessageBox.Show("Tarama Başlasın mı?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    btnScan.PerformClick();
            }
        }

        #region Scanning

        async void btnScan_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Emin misin?", "Tarama Başlıyor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            if (!Directory.Exists(path))
                return;

            listBox1.Items.Clear();
            btnBrowse.Enabled = btnScan.Enabled = btnClean.Enabled = false;

            bool success = false;

            State("Tarama Başladı");

            List<string> files = await GetFilesAsync(path);

            State($"Tarama Tamamlandı. {files.Count} dosya bulundu.");

            if (files.Count > 1)
            {
                State("Karşılaştırma Başladı");

                List<List<string>> sames = await CompareFilesAsync(files);

                State($"Karşılaştırma Tamamlandı. {sames.Count} kopya bulundu.");

                if (sames.Count > 0)
                {
                    State("Kayıt Başladı");

                    path = tbDirectory.Text = await Save(sames);

                    State("Kayıt Tamamlandı");

                    success = true;
                }
            }

            State("İşlem Tamamlandı");

            btnBrowse.Enabled = btnScan.Enabled = btnClean.Enabled = true;

            if (success && MessageBox.Show("Temizleme Başlasın mı?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                btnClean.PerformClick();
        }

        Task<List<string>> GetFilesAsync(string path) => Task.Run(async () =>
        {
            List<string> files = new List<string>();

            foreach (string item in Directory.GetFiles(path))
            {
                try
                {
                    files.Add(item);
                }
                catch (Exception)
                { }
            }

            foreach (string item in Directory.GetDirectories(path))
            {
                try
                {
                    files.AddRange(await GetFilesAsync(item));
                }
                catch (Exception)
                { }
            }

            return files;
        });

        Task<List<List<string>>> CompareFilesAsync(List<string> files) => Task.Run(async () =>
        {
            List<string> search = new List<string>(files);

            List<List<string>> sames = new List<List<string>>();

            Dictionary<string, byte[]> requiredHases = new Dictionary<string, byte[]>();

            int current = 0;

            MD5 md5 = MD5.Create();

            _ = listBox1.Invoke((Action)(() => listBox1.Items.Add("")));

            for (int i = 0; i < search.Count; i++)
            {
                _ = listBox1.Invoke((Action)(() => listBox1.Items[3] = GetTime() + "  Karşılaştırma " + Math.Round((double)current++ / files.Count * 100, 2) + "% Tamamlandı"));

                try
                {
                    using (FileStream f1 = new FileStream(search[i], FileMode.Open))
                    {
                        List<string> same = new List<string>();

                        for (int ii = i + 1; ii < search.Count; ii++)
                        {
                            try
                            {
                                using (FileStream f2 = new FileStream(search[ii], FileMode.Open))
                                {
                                    if (f1.Length == f2.Length)
                                    {
                                        if (f1.ReadByte() == f2.ReadByte())
                                        {
                                            f1.Position = 0;
                                            f2.Position = 0;

                                            byte[] h1, h2;
                                            bool success1 = requiredHases.TryGetValue(f1.Name, out h1),
                                            success2 = requiredHases.TryGetValue(f2.Name, out h2);
                                            if (!success1)
                                            {
                                                h1 = md5.ComputeHash(f1);
                                                requiredHases.Add(f1.Name, h1);
                                            }
                                            if (!success2)
                                            {
                                                h2 = md5.ComputeHash(f2);
                                                requiredHases.Add(f2.Name, h2);
                                            }
                                            if (await IsSame(h1, h2))
                                            {
                                                same.Add(f2.Name);
                                                search.RemoveAt(ii);
                                                ii--;
                                            }
                                        }
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                search.RemoveAt(ii);
                                ii--;
                                continue;
                            }
                        }

                        if (same.Count > 0)
                        {
                            same.Insert(0, f1.Name);
                            sames.Add(same);
                            search.RemoveAt(i);
                            i--;
                        }
                    }
                }
                catch (Exception)
                {
                    search.RemoveAt(i);
                    i--;
                    continue;
                }
            }
            _ = listBox1.Invoke((Action)(() => listBox1.Items[3] = GetTime() + "  Karşılaştırma 100% Tamamlandı"));

            return sames;
        });

        Task<bool> IsSame(byte[] h1, byte[] h2) => Task.Run(() =>
        {
            for (int i = 0; i < 16; i++)
                if (h1[i] != h2[i])
                    return false;

            return true;
        });

        Task<string> Save(List<List<string>> sames) => Task.Run(async () =>
        {
            string path, desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            while (Directory.Exists(path = Path.Combine(desktop, await GenerateRandom(15))))
            { }

            _ = Directory.CreateDirectory(path);

            _ = listBox1.Invoke((Action)(() => listBox1.Items.Add("")));

            for (int i = 0; i < sames.Count; i++)
            {
                _ = listBox1.Invoke((Action)(() => listBox1.Items[6] = GetTime() + "  Kayıt " + Math.Round((double)i / sames.Count * 100, 2) + "% Tamamlandı"));
                List<string> item = sames[i];

                for (int ii = 0; ii < item.Count; ii++)
                    await CreateShortcut(Path.Combine(path, i + ";" + ii + ".lnk"), item[ii]);
            }

            using (StreamWriter sw = new StreamWriter(Path.Combine(path, "count.txt")))
                sw.Write(sames.Count);

            _ = listBox1.Invoke((Action)(() => listBox1.Items[6] = GetTime() + "  Kayıt 100% Tamamlandı"));

            return path;
        });

        Random rnd = new Random();
        Task<string> GenerateRandom(int length) => Task.Run(() =>
        {
            string res = "";
            for (int i = 0; i < length; i++)
            {
                int a = rnd.Next(3);
                switch (a)
                {
                    case 0:
                        res += (char)rnd.Next('0', '9' + 1);
                        break;
                    case 1:
                        res += (char)rnd.Next('a', 'z' + 1);
                        break;
                    case 2:
                        res += (char)rnd.Next('A', 'Z' + 1);
                        break;
                }
            }
            return res;
        });

        Task CreateShortcut(string path, string target) => Task.Run(() =>
        {
            IWshShortcut shortcut = new WshShell().CreateShortcut(path);
            shortcut.TargetPath = target;
            shortcut.Save();
        });

        #endregion

        #region Cleaning

        async void btnClean_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            btnBrowse.Enabled = btnScan.Enabled = btnClean.Enabled = false;

            if (MessageBox.Show("Emin misin?", "Temizleme Başlıyor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                goto END;

            if (!Directory.Exists(path))
                goto END;

            if (!System.IO.File.Exists(Path.Combine(path, "count.txt")))
                goto END;

            State("Temizleme Başladı");

            await CleanAsync();

            State("Temizleme Tamamlandı");

            State("İşlem Tamamlandı");

        END:;

            btnBrowse.Enabled = btnScan.Enabled = btnClean.Enabled = true;
        }

        Task CleanAsync() => Task.Run(async () =>
        {
            _ = listBox1.Invoke((Action)(() => listBox1.Items.Add("")));

            int? count = await ReadCount();

            if (!count.HasValue)
            {
                _ = listBox1.Invoke((Action)(() => listBox1.Items[1] = GetTime() + "  Geçersiz Klasör"));
                return;
            }

            List<string> files = Directory.GetFiles(path).ToList();

            for (int i = count.Value - 1; i >= 0; i--)
            {
                _ = listBox1.Invoke((Action)(() => listBox1.Items[1] = GetTime() + "  Temizleme " + Math.Round((double)(count.Value - i - 1) / count.Value * 100, 2) + "% Tamamlandı"));

                string[] vs = files.Where(f => Path.GetFileName(f).StartsWith(i + ";")).ToArray();
                for (int ii = vs.Length - 1; ii > 0; ii--)
                {
                    System.IO.File.Delete(await GetTargetPath(vs[ii]));
                    System.IO.File.Delete(vs[ii]);
                }
            }

            System.IO.File.Delete(Path.Combine(path, "count.txt"));

            _ = listBox1.Invoke((Action)(() => listBox1.Items[1] = GetTime() + "  Temizleme 100% Tamamlandı"));
        });

        Task<int?> ReadCount() => Task.Run(() =>
        {
            int count = 0;
            bool success = false;
            using (StreamReader sr = new StreamReader(Path.Combine(path, "count.txt")))
                success = int.TryParse(sr.ReadToEnd(), out count);
            return success ? (int?)count : null;
        });

        Task<string> GetTargetPath(string path) => Task.Run(() => ((IWshShortcut)new WshShell().CreateShortcut(path)).TargetPath);

        #endregion

        void State(string state) => listBox1.Items.Add(GetTime() + "  " + state);

        string GetTime()
        {
            DateTime now = DateTime.Now;
            return $"{now.Hour}:{now.Minute}:{now.Second}";
        }
    }
}