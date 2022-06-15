using YoutubeExplode.Videos;

namespace YouthUp.Commands
{
    class C1
    {
        public C1(VideoId videoId, string url)
        {
            VideoId = videoId;
            Url = url;
        }

        public VideoId VideoId { get; set; }
        public string Url { get; set; }
    }
}