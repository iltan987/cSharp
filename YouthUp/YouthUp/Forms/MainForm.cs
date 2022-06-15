using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YouthUp.Commands;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;

namespace YouthUp.Forms
{
    public partial class MainForm : Form
    {
        public MainForm() => InitializeComponent();

        Progress<C1> vIdProgress;
        Progress<C2> pIdProgress;
        ClipboardClass clipboard;

        string lastVId, lastPId;

        SemaphoreSlim downloadManager, loadManager;
        List<Task> funcs1 = new List<Task>(), funcs2 = new List<Task>();

        void MainForm_Load(object sender, EventArgs e) => 100.Err(() =>
        {
            downloadManager = new SemaphoreSlim(3);
            loadManager = new SemaphoreSlim(3);
            vIdProgress = new Progress<C1>((val) =>
            {
                if (val.VideoId.Value == lastVId)
                    return;
                else
                {
                    VideoId? asd = VideoId.TryParse(placeholderTB1.Text);
                    if (asd.HasValue && asd.Value == val.VideoId)
                        return;
                }
                lastVId = val.VideoId.Value;
                lastPId = "";

                _ = Invoke((Action)(() =>
                {
                    if (WindowState != FormWindowState.Maximized)
                        WindowState = FormWindowState.Maximized;
                    TopMost = true;
                    Focus();
                    BringToFront();
                    TopMost = false;
                    if (MessageBox.Show("Do you want to add this video to queue?\n" + val.Url, "I detected an URL", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        placeholderTB1.Text = val.Url;
                        button1.PerformClick();
                    }
                }));
            });
            pIdProgress = new Progress<C2>((val) =>
            {
                if (val.PlaylistId.Value == lastPId)
                    return;
                else
                {
                    VideoId? asd = VideoId.TryParse(placeholderTB1.Text);
                    if (asd.HasValue && asd.Value == val.PlaylistId)
                        return;
                }
                lastVId = "";
                lastPId = val.PlaylistId.Value;

                _ = Invoke((Action)(() =>
                {
                    if (WindowState != FormWindowState.Maximized)
                        WindowState = FormWindowState.Maximized;
                    TopMost = true;
                    Focus();
                    BringToFront();
                    TopMost = false;
                    if (MessageBox.Show("Do you want to add this playlist to queue?\n" + val.Url, "I detected an URL", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        placeholderTB1.Text = val.Url;
                        button1.PerformClick();
                    }
                }));
            });
            clipboard = new ClipboardClass(vIdProgress, pIdProgress);
            clipboard.Start();
        });

        void button1_Click(object sender, EventArgs e) => 101.Err(async () =>
        {
            clipboard.Pause();
            placeholderTB1.Enabled = button1.Enabled = button2.Enabled = menuStrip1.Enabled = false;

            VideoId? vid = VideoId.TryParse(placeholderTB1.Text);
            if (vid.HasValue)
            {
                foreach (VideoInfo item in panel1.Controls)
                    if (item.vid == vid.Value)
                    {
                        _ = MessageBox.Show("It's already exists!");
                        goto SON;
                    }

                VideoInfo inf = new VideoInfo(vid.Value, placeholderTB2) { Dock = DockStyle.Top };
                panel1.Controls.Add(inf);
                funcs1.Add(inf.LoadInfo(loadManager));
            }
            else
            {
                PlaylistId? pid = PlaylistId.TryParse(placeholderTB1.Text);
                if (pid.HasValue)
                {
                    List<PlaylistVideo> asd = (await new YoutubeClient().Playlists.GetVideosAsync(pid.Value)).ToList();
                    foreach (var item in asd)
                    {
                        bool exists = false;
                        foreach (VideoInfo item2 in panel1.Controls)
                            if (item2.vid == item.Id)
                            {
                                exists = true;
                                break;
                            }
                        if (exists)
                            continue;
                        VideoInfo inf = new VideoInfo(item.Id, placeholderTB2) { Dock = DockStyle.Top };
                        panel1.Controls.Add(inf);
                        funcs1.Add(inf.LoadInfo(loadManager));
                    }
                }
                else
                    _ = MessageBox.Show("Invalid URL!");
            }
            await Task.WhenAll(funcs1);
            funcs1 = new List<Task>();

        SON:;
            placeholderTB1.Enabled = button1.Enabled = button2.Enabled = menuStrip1.Enabled = true;
            clipboard.Start();
        });

        void button2_Click(object sender, EventArgs e) => 102.Err(() =>
        {
            CommonOpenFileDialog cofd = new CommonOpenFileDialog() { InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop), IsFolderPicker = true, RestoreDirectory = true };
            if (cofd.ShowDialog() == CommonFileDialogResult.Ok)
                placeholderTB2.Text = cofd.FileName;
        });

        void downloadAllToolStripMenuItem_Click(object sender, EventArgs e) => 103.Err(async () =>
        {
            clipboard.Pause();
            menuStrip1.Enabled = placeholderTB1.Enabled = button1.Enabled = button2.Enabled = false;
            FFMPEG.WriteFFMPEG();
            Control.ControlCollection list = panel1.Controls;
            for (int i = 0; i < list.Count; i++)
                funcs2.Add(((VideoInfo)list[i]).DownloadAsync(downloadManager, enumerateFilesToolStripMenuItem.Checked, i + 1));
            await Task.WhenAll(funcs2);
            funcs2 = new List<Task>();
            FFMPEG.DeleteFFMPEG();
            clipboard.Start();
            menuStrip1.Enabled = placeholderTB1.Enabled = button1.Enabled = button2.Enabled = true;
        });

        void enumerateFilesToolStripMenuItem_Click(object sender, EventArgs e) => enumerateFilesToolStripMenuItem.Checked = !enumerateFilesToolStripMenuItem.Checked;

        void mP3ToolStripMenuItem_Click(object sender, EventArgs e) => 104.Err(() =>
        {
            foreach (VideoInfo item in panel1.Controls)
                item.rbMP3.Checked = true;
        });

        void mP4ToolStripMenuItem_Click(object sender, EventArgs e) => 105.Err(() =>
        {
            foreach (VideoInfo item in panel1.Controls)
                item.rbMP4.Checked = true;
        });

        void cleanQueueToolStripMenuItem_Click(object sender, EventArgs e) => 106.Err(() => panel1.Controls.Clear());
    }
}