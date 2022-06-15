using System.Diagnostics;
using System.IO;

namespace YouthUp.Commands
{
    class FFMPEG
    {
        static string ffmpeg;

        public static void VideoToAudio(string input, string output) => 122.Err(() => Process.Start(new ProcessStartInfo() { Arguments = $"-i \"{input}\" \"{output}\"", FileName = ffmpeg, Verb = "runas", CreateNoWindow = true, UseShellExecute = false }).WaitForExit());

        public static void MergeAudioAndVideo(string audio, string video, string output) => 123.Err(() => Process.Start(new ProcessStartInfo() { Arguments = $"-i \"{video}\" -i \"{audio}\" -map 0:v -map 1:a -c:v copy \"{output}\"", FileName = ffmpeg, Verb = "runas", UseShellExecute = false, CreateNoWindow = true }).WaitForExit());

        public static void WriteFFMPEG() => 124.Err(() => File.WriteAllBytes(ffmpeg = Path.ChangeExtension(Path.GetTempFileName(), ".exe"), Properties.Resources.ffmpeg));
        public static void DeleteFFMPEG() => 125.Err(() => File.Delete(ffmpeg));
    }
}