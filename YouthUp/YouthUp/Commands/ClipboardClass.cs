using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using YoutubeExplode.Playlists;
using YoutubeExplode.Videos;
using static YouthUp.Commands.RunClass;

namespace YouthUp.Commands
{
    class ClipboardClass
    {
        System.Timers.Timer timer;

        public ClipboardClass(IProgress<C1> videoProgress, IProgress<C2> playlistProgress)
        {
            timer = new System.Timers.Timer(1000);
            timer.Elapsed += async (s, e) => await Task.Run(() => 119.Err(async () =>
            {
                string st = await GetAsync();
                VideoId? vid = VideoId.TryParse(st);
                if (vid.HasValue)
                    videoProgress.Report(new C1(vid.Value, st));
                else
                {
                    PlaylistId? plist = PlaylistId.TryParse(st);
                    if (plist.HasValue)
                        playlistProgress.Report(new C2(plist.Value, st));
                }
            }));
        }

        public void Start() => 120.Err(() => timer.Start());

        public void Pause() => 121.Err(() => timer.Stop());

        async Task<string> GetAsync() => await RunAsync(Clipboard.GetText, ApartmentState.STA);
    }
}