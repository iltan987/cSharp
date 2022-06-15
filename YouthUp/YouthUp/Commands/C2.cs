using YoutubeExplode.Playlists;

namespace YouthUp.Commands
{
    class C2
    {
        public C2(PlaylistId playlistId, string url)
        {
            PlaylistId = playlistId;
            Url = url;
        }

        public PlaylistId PlaylistId { get; set; }
        public string Url { get; set; }
    }
}