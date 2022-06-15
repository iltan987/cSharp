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
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        void btnBrowse_Click(object sender, EventArgs e)
        {
            CommonOpenFileDialog cofd = new CommonOpenFileDialog() { IsFolderPicker = true, RestoreDirectory = true };
            if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
                textBox1.Text = cofd.FileName;
        }

        void btnScan_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(textBox1.Text))
            {
                textBox1.Text = "";
                _ = MessageBox.Show("Directory not exists!");
            }
            else if (MessageBox.Show("Emin misin?", "Tarama Başlıyor", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                btnBrowse.Enabled = btnScan.Enabled = btnClean.Enabled = false;
                listBox1.Items.Clear();
                RunScan();
                btnBrowse.Enabled = btnScan.Enabled = btnClean.Enabled = true;
            }
        }

        async void btnClean_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Emin misin?", "Temizleme Başlıyor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                goto END;

            if (!Directory.Exists(textBox1.Text))
                goto END;

            if (!System.IO.File.Exists(Path.Combine(textBox1.Text, "count.txt")))
                goto END;

            listBox1.Items.Clear();
            btnBrowse.Enabled = btnScan.Enabled = btnClean.Enabled = false;

            await RunClean();

            btnBrowse.Enabled = btnScan.Enabled = btnClean.Enabled = true;

        END:;

            btnBrowse.Enabled = btnScan.Enabled = btnClean.Enabled = true;
        }

        async void RunScan()
        {
            Log("Tarama Başladı");

            _ = listBox1.Items.Add("Taranıyor. 0 dosya bulundu");
            Dictionary<FileStream, byte[]> hashes = new Dictionary<FileStream, byte[]>();
            try
            {
                await GetFilesAsync(textBox1.Text, hashes);

                listBox1.Items[1] = $"Tarama Tamamlandı. {hashes.Count} dosya bulundu.";

                if (hashes.Count > 1)
                {
                    Log("Hashing Başladı");

                    using (SHA512 sha = SHA512.Create())
                        await HashFiles(hashes, sha);

                    Log("Hashing Tamamlandı");

                    Log("Karşılaştırma Başladı");

                    List<List<string>> sames = new List<List<string>>();
                    await GetSames(hashes, sames);

                    Log($"Karşılaştırma Tamamlandı. {sames.Count} kopya bulundu.");

                    if (sames.Count > 0)
                    {
                        Log("Kayıt Başladı");

                        textBox1.Text = await Save(sames);

                        Log("Kayıt Tamamlandı");
                    }
                }

                Log("İşlem Tamamlanıyor. Lütfen bekleyiniz");
            }
            finally
            {
                for (int i = 0; i < hashes.Count; i++)
                {
                    var e = hashes.ElementAt(i);
                    e.Key.Close();
                    e.Key.Dispose();
                    _ = hashes.Remove(e.Key);
                    i--;
                }
            }

            Log("İşlem Tamamlandı!");
        }

        Task GetFilesAsync(string path, Dictionary<FileStream, byte[]> hashes) => Task.Run(async () =>
        {
            foreach (string item in Directory.GetFiles(path))
            {
                try
                {
                    hashes.Add(new FileStream(item, FileMode.Open), null);
                }
                catch
                { }
            }

            foreach (string item in Directory.GetDirectories(path))
            {
                try
                {
                    _ = listBox1.Invoke((Action)(() => listBox1.Items[1] = "Taranıyor. " + hashes.Count + " dosya bulundu."));
                    await GetFilesAsync(item, hashes);
                }
                catch
                { }
            }
        });

        Task HashFiles(Dictionary<FileStream, byte[]> hashes, SHA512 sha) => Task.Run(() =>
        {
            _ = listBox1.Invoke((Action)(() => listBox1.Items.Add("Hashing 0%")));
            int showEvery = (int)Math.Log(hashes.Count, 1.2);
            for (int i = 0; i < hashes.Count; i++)
            {
                if (i % showEvery == 0)
                    _ = listBox1.Invoke((Action)(() => listBox1.Items[3] = "Hashing " + Math.Round((double)i / hashes.Count * 100, 2) + "%"));
                var e = hashes.ElementAt(i);
                try
                {
                    hashes[e.Key] = sha.ComputeHash(e.Key);
                }
                catch
                {
                    _ = listBox1.Invoke((Action)(() => listBox1.Items[1] = $"Tarama Tamamlandı. {hashes.Count} dosya bulundu."));
                    e.Key.Close();
                    e.Key.Dispose();
                    _ = hashes.Remove(e.Key);
                    i--;
                }
            }
            _ = listBox1.Invoke((Action)(() => listBox1.Items[3] = "Hashing 100%"));
        });

        Task GetSames(Dictionary<FileStream, byte[]> hashes, List<List<string>> sames) => Task.Run(() =>
        {
            double current = 0;
            int max = hashes.Count;

            _ = listBox1.Invoke((Action)(() => listBox1.Items.Add("Karşılaştırma 0%")));

            for (int i = 0; i < hashes.Count; i++)
            {
                _ = listBox1.Invoke((Action)(() => listBox1.Items[6] = "Karşılaştırma " + Math.Round(current++ / max * 100, 2) + "%"));
                var v1 = hashes.ElementAt(i);

                List<string> vs = new List<string> { v1.Key.Name };

                for (int ii = i + 1; ii < hashes.Count; ii++)
                {
                    var v2 = hashes.ElementAt(ii);

                    if (v1.Value.SequenceEqual(v2.Value))
                    {
                        vs.Add(v2.Key.Name);
                        v2.Key.Close();
                        v2.Key.Dispose();
                        _ = hashes.Remove(v2.Key);
                        ii--;
                    }
                }

                if (vs.Count > 1)
                {
                    sames.Add(vs);
                    v1.Key.Close();
                    v1.Key.Dispose();
                    _ = hashes.Remove(v1.Key);
                    i--;
                }
            }
            _ = listBox1.Invoke((Action)(() => listBox1.Items[6] = "Karşılaştırma 100%"));
        });

        Task<string> Save(List<List<string>> sames) => Task.Run(async () =>
        {
            string path, desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            do
            {
                path = Path.Combine(desktop, await GenerateRandom(15));
            } while (Directory.Exists(path));

            _ = Directory.CreateDirectory(path);

            _ = listBox1.Invoke((Action)(() => listBox1.Items.Add("")));

            for (int i = 0; i < sames.Count; i++)
            {
                _ = listBox1.Invoke((Action)(() => listBox1.Items[9] = "Kayıt " + Math.Round((double)i / sames.Count * 100, 2) + "%"));
                List<string> item = sames[i];

                for (int ii = 0; ii < item.Count; ii++)
                    await CreateShortcut(Path.Combine(path, i + ";" + ii + ".lnk"), item[ii]);
            }

            using (StreamWriter sw = new StreamWriter(Path.Combine(path, "count.txt")))
                sw.Write(sames.Count);

            _ = listBox1.Invoke((Action)(() => listBox1.Items[9] = "Kayıt 100%"));

            return path;
        });

        readonly Random rnd = new Random();
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

        Task CleanAsync() => Task.Run(async () =>
        {
            _ = listBox1.Invoke((Action)(() => listBox1.Items.Add("")));

            int? count = await ReadCount();

            if (!count.HasValue)
            {
                _ = listBox1.Invoke((Action)(() => listBox1.Items[1] = "Geçersiz Klasör"));
                return;
            }

            List<string> files = Directory.GetFiles(textBox1.Text).ToList();

            for (int i = count.Value - 1; i >= 0; i--)
            {
                _ = listBox1.Invoke((Action)(() => listBox1.Items[1] = "Temizleme " + Math.Round((double)(count.Value - i - 1) / count.Value * 100, 2) + "%"));

                string[] vs = files.Where(f => Path.GetFileName(f).StartsWith(i + ";")).ToArray();
                for (int ii = vs.Length - 1; ii > 0; ii--)
                {
                    System.IO.File.Delete(await GetTargetPath(vs[ii]));
                    System.IO.File.Delete(vs[ii]);
                }
            }

            System.IO.File.Delete(Path.Combine(textBox1.Text, "count.txt"));

            _ = listBox1.Invoke((Action)(() => listBox1.Items[1] = "Temizleme 100%"));
        });

        Task<int?> ReadCount() => Task.Run(() =>
        {
            int count = 0;
            bool success = false;
            using (StreamReader sr = new StreamReader(Path.Combine(textBox1.Text, "count.txt")))
                success = int.TryParse(sr.ReadToEnd(), out count);
            return success ? (int?)count : null;
        });

        Task<string> GetTargetPath(string path) => Task.Run(() => ((IWshShortcut)new WshShell().CreateShortcut(path)).TargetPath);

        Task RunClean() => Task.Run(async () =>
        {
            Log("Temizleme Başladı");

            await CleanAsync();

            Log("Temizleme Tamamlandı");

            Log("İşlem Tamamlandı");
        });

        void Log(string msg) => _ = listBox1.InvokeRequired
                ? listBox1.Invoke((Action)(() => _ = listBox1.Items.Add(DateTime.Now.ToString("T") + "  " + msg)))
                : listBox1.Items.Add(DateTime.Now.ToString("T") + "  " + msg);
    }
}