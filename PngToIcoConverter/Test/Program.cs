using System.IO;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            foreach (var item in args)
                PngToIcoConverter.Converter.Convert(item, Path.ChangeExtension(item, "ico"));
        }
    }
}