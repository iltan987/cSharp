using YoutubeExplode.Videos.Streams;

namespace YouthUp.Commands
{
    class VideoInfoItem
    {
        public VideoOnlyStreamInfo videoOnlyStreamInfo { get; set; }
        public long audioOnlyStreamInfoBytes { get; set; }

        public VideoInfoItem(VideoOnlyStreamInfo videoOnlyStreamInfo, long audioOnlyStreamInfoBytes)
        {
            this.videoOnlyStreamInfo = videoOnlyStreamInfo;
            this.audioOnlyStreamInfoBytes = audioOnlyStreamInfoBytes;
        }

        public override string ToString() => videoOnlyStreamInfo.VideoQuality.Label + " ~" + new FileSize(videoOnlyStreamInfo.Size.Bytes + audioOnlyStreamInfoBytes).ToString();
    }
}