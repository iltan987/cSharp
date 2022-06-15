using System.IO;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            WebpToPngConverter.Converter.Convert(args[0]).Save(Path.ChangeExtension(args[0], "png"));
            //WebpToPngConverter.Converter.Convert(new FileStream(args[0], FileMode.Open)).Save(Path.ChangeExtension(args[0], "png"));
        }
    }
}