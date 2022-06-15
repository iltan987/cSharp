using YoutubeExplode.Videos.Streams;

namespace YouthUp.Commands
{
    class AudioInfoItem
    {
        public AudioOnlyStreamInfo Stream { get; set; }

        public AudioInfoItem(AudioOnlyStreamInfo stream) => Stream = stream;

        public override string ToString() => Stream.Bitrate + " " + Stream.Size;
    }
}