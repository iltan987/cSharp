using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode;
using YoutubeExplode.Common;
using YoutubeExplode.Videos;
using YoutubeExplode.Videos.Streams;

namespace YouthUp.Commands
{
    public partial class VideoInfo : UserControl
    {
        Progress<double> progress1, progress2, progress3;
        Video video;
        List<AudioOnlyStreamInfo> audios = new List<AudioOnlyStreamInfo>();
        List<VideoOnlyStreamInfo> videos = new List<VideoOnlyStreamInfo>();
        AudioOnlyStreamInfo bestAudio;
        bool success = false, cbReady = false;
        public VideoId vid { get; set; }
        public PlaceholderTB.PlaceholderTB1 folderTB { get; set; }

        public VideoInfo(VideoId vid, PlaceholderTB.PlaceholderTB1 folderTB)
        {
            InitializeComponent();
            this.vid = vid;
            this.folderTB = folderTB;
            folderTB.TextChanged += (s, e) => CheckAvailability();
        }

        public Task LoadInfo(SemaphoreSlim semaphore) => Task.Run(() => 107.Err(async () =>
        {
            await semaphore.WaitAsync();
            try
            {
                progress1 = new Progress<double>((val) => 108.Err(() =>
                {
                    _ = pbPercentage.Invoke((Action)(() => pbPercentage.Value = (int)(val * 75)));
                    if (val == 1) return;
                    _ = lblState.Invoke((Action)(() => lblState.Text = "State: Downloading " + Math.Round(val * 75, 2) + "%"));
                }));
                progress2 = new Progress<double>((val) => 109.Err(() =>
                {
                    _ = pbPercentage.Invoke((Action)(() => pbPercentage.Value = (int)(val * 35)));
                    _ = lblState.Invoke((Action)(() => lblState.Text = "State: Downloading " + Math.Round(val * 35, 2) + "%"));
                }));
                progress3 = new Progress<double>((val) => 110.Err(() =>
                {
                    _ = pbPercentage.Invoke((Action)(() => pbPercentage.Value = (int)(35 + (val * 35))));
                    _ = lblState.Invoke((Action)(() => lblState.Text = "State: Downloading " + Math.Round(35 + (val * 35), 2) + "%"));
                }));

                for (int i = 0; i < 3; i++)
                {
                    try
                    {
                        Task<Video> task = new YoutubeClient().Videos.GetAsync(vid).AsTask();
                        if (await Task.WhenAny(task, Task.Delay(10000)) == task)
                        {
                            video = task.Result;

                            SetPBImage(video.Thumbnails);
                            _ = lblAuthor.Invoke((Action)(() => lblAuthor.Text = video.Author.Title));
                            _ = lblTitle.Invoke((Action)(() => lblTitle.Text = video.Title));
                            _ = lblState.Invoke((Action)(() => lblState.Text = "State: Loading Stream Info..."));

                            StreamManifest manifest = await new YoutubeClient().Videos.Streams.GetManifestAsync(vid);

                            audios.AddRange(manifest.GetAudioOnlyStreams().Where(f => f.Container == YoutubeExplode.Videos.Streams.Container.Mp4));
                            videos.AddRange(manifest.GetVideoOnlyStreams().Where(f => f.Container == YoutubeExplode.Videos.Streams.Container.Mp4 && f.VideoCodec.StartsWith("avc")));

                            if (audios.Count == 0 || videos.Count == 0)
                            {
                                Failed();
                                break;
                            }

                            var _try = audios.TryGetWithHighestBitrate();
                            if (_try == null)
                            {
                                Failed();
                                break;
                            }
                            else
                                bestAudio = (AudioOnlyStreamInfo)_try;

                            _ = tableLayoutPanel2.Invoke((Action)(() => tableLayoutPanel2.Enabled = true));
                            _ = tableLayoutPanel4.Invoke((Action)(() => tableLayoutPanel4.Enabled = true));
                            _ = rbMP4.Invoke((Action)(() => rbMP4.Checked = true));

                            success = true;
                            break;
                        }
                    }
                    catch (Exception) { }
                }

                if (!success)
                    Failed();

                _ = CheckAvailability();
            }
            finally
            {
                _ = semaphore.Release();
            }
        }));

        public Task DownloadAsync(SemaphoreSlim semaphore, bool enumerate, int id) => Task.Run(() => 111.Err(() =>
        {
            try
            {
                if (CheckAvailability())
                {
                    _ = Invoke((Action)(() =>
                    {
                        tableLayoutPanel1.BackColor = Color.Green;
                        Size = new Size(750, 223);
                        pbPercentage.Visible = true;
                        tableLayoutPanel2.Enabled = tableLayoutPanel4.Enabled = btnCancel.Visible = false;
                        lblState.Text = "State: Downloading";
                    }));

                    semaphore.WaitAsync().Wait();

                    string tempMP3Location = Path.GetTempFileName();

                    if (rbMP3.Checked)
                    {
                        try
                        {
                            string mp3Location = Class1.GetFileName(folderTB.Text, (enumerate ? id + "- " : "") + string.Join("_", video.Title.Split(Path.GetInvalidFileNameChars())), ".mp3");

                            new YoutubeClient().Videos.Streams.DownloadAsync(((AudioInfoItem)cbRes.Invoke((Func<AudioInfoItem>)(() => (AudioInfoItem)cbRes.SelectedItem))).Stream, tempMP3Location, progress1).AsTask().Wait();

                            FFMPEG.VideoToAudio(tempMP3Location, mp3Location);
                        }
                        catch (Exception) { }
                    }
                    else
                    {
                        try
                        {
                            string tempMP4Location = Path.GetTempFileName(),
                            outputLocation = Class1.GetFileName(folderTB.Text, (enumerate ? id + "- " : "") + string.Join("_", video.Title.Split(Path.GetInvalidFileNameChars())), ".mp4");

                            new YoutubeClient().Videos.Streams.DownloadAsync(((VideoInfoItem)cbRes.Invoke((Func<VideoInfoItem>)(() => (VideoInfoItem)cbRes.SelectedItem))).videoOnlyStreamInfo, tempMP4Location, progress2).AsTask().Wait();
                            new YoutubeClient().Videos.Streams.DownloadAsync(bestAudio, tempMP3Location, progress3).AsTask().Wait();

                            FFMPEG.MergeAudioAndVideo(tempMP3Location, tempMP4Location, outputLocation);

                            File.Delete(tempMP4Location);
                        }
                        catch (Exception) { }
                    }

                    File.Delete(tempMP3Location);
                    _ = Invoke((Action)(() => Dispose()));
                }
            }
            catch (Exception)
            {
                _ = Invoke((Action)(() =>
                {
                    tableLayoutPanel1.BackColor = Color.Red;
                    Size = new Size(750, 200);
                    pbPercentage.Visible = false;
                    btnCancel.Visible = true;
                    lblState.Text = "State: Downloading Failed";
                }));
            }
            finally
            {
                _ = semaphore.Release();
            }
        }));

        void radioButton_CheckedChanged(object sender, EventArgs e) => 112.Err(() =>
        {
            cbReady = tableLayoutPanel2.Enabled = tableLayoutPanel4.Enabled = false;

            cbRes.Items.Clear();
            if (rbMP3.Checked)
            {
                audios.ForEach(f => cbRes.Items.Add(new AudioInfoItem(f)));

                AudioInfoItem best = (AudioInfoItem)cbRes.Items[0];
                for (int i = 1; i < cbRes.Items.Count; i++)
                {
                    AudioInfoItem item = (AudioInfoItem)cbRes.Items[i];
                    if (item.Stream.Bitrate > best.Stream.Bitrate)
                        best = item;
                }
                cbRes.SelectedItem = best;
            }
            else
            {
                videos.ForEach(f => cbRes.Items.Add(new VideoInfoItem(f, bestAudio.Size.Bytes)));

                if (cbRes.Items.Count > 0)
                {
                    VideoInfoItem best = (VideoInfoItem)cbRes.Items[0];
                    for (int i = 1; i < cbRes.Items.Count; i++)
                    {
                        VideoInfoItem item = (VideoInfoItem)cbRes.Items[i];
                        if (item.videoOnlyStreamInfo.VideoQuality > best.videoOnlyStreamInfo.VideoQuality)
                            best = item;
                    }
                    cbRes.SelectedItem = best;
                }
            }

            cbReady = tableLayoutPanel2.Enabled = tableLayoutPanel4.Enabled = true;
            _ = CheckAvailability();
        });

        void pbThumbnail_DoubleClick(object sender, EventArgs e) => 113.Err(() => Process.Start($"https://www.youtube.com/watch?v={vid.Value}"));

        void btnCancel_Click(object sender, EventArgs e) => 114.Err(() => Dispose());

        public bool CheckAvailability() => 115.Err(() =>
        {
            if (!success || !cbReady)
                return false;
            else if (string.IsNullOrWhiteSpace(folderTB.Text))
            {
                _ = lblState.Invoke((Action)(() => lblState.Text = "State: READY!\nWaiting for user to select folder to save..."));
                return false;
            }
            else if (!Directory.Exists(folderTB.Text))
            {
                _ = lblState.Invoke((Action)(() => lblState.Text = "State: READY!\nDownload location is not exists!"));
                return false;
            }

            _ = lblState.Invoke((Action)(() => lblState.Text = "State: READY!\nWaiting for user to start downloading..."));
            return true;
        });

        void Failed() => 116.Err(() =>
        {
            _ = pbThumbnail.Invoke((Action)(() => pbThumbnail.Image = Properties.Resources.error));
            _ = lblAuthor.Invoke((Action)(() => lblAuthor.Text = "Invalid Video!"));
            _ = lblTitle.Invoke((Action)(() => lblTitle.Text = "Invalid Video!"));
            _ = lblState.Invoke((Action)(() => lblState.Text = "State: Invalid Video!"));
            _ = tableLayoutPanel2.Invoke((Action)(() => tableLayoutPanel2.Enabled = false));
            _ = tableLayoutPanel4.Invoke((Action)(() => tableLayoutPanel4.Enabled = false));
        });

        void SetPBImage(IReadOnlyList<Thumbnail> set) => 117.Err(() => _ = pbThumbnail.Invoke((Action)(() =>
        {
            List<Thumbnail> vs = set.OrderByDescending(f => f.Resolution.Area).ToList();

            bool succes = false;

            foreach (var item in vs)
            {
                try
                {
                    pbThumbnail.Load(item.Url);
                    succes = true;
                    break;
                }
                catch (Exception)
                { }
            }

            if (!succes)
                pbThumbnail.Image = Properties.Resources.error;
        })));
    }
}