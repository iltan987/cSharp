using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace PngToIcoConverter
{
    public class Converter
    {
        static readonly int[] sizes = { 16, 24, 32, 40, 48, 56, 64, 72, 80, 88, 96, 104, 112, 120, 128, 136, 144, 152, 160, 168, 176, 184, 192, 200, 208, 216, 224, 232, 240, 248 };

        public static void Convert(string inputPath, string outputPath)
        {
            using (var org = Image.FromFile(inputPath))
            using (var output = new FileStream(outputPath, FileMode.Create))
            using (var bw = new BinaryWriter(output))
            {
                bw.Write(new byte[] { 0, 0, 1, 0, 31, 0 });
                bw.Write(new byte[496]);

                var current = 6;

                foreach (int size in sizes)
                {
                    bw.Seek(current, SeekOrigin.Begin);
                    using (Image image = Resize(org, size))
                    {
                        var width = (byte)image.Width;
                        bw.Write(width);
                        bw.Write(width);
                        bw.Write((byte)image.Palette.Entries.Length);
                        bw.Write((byte)0);
                        bw.Write((short)0);
                        bw.Write(GetBPP(image.PixelFormat));
                        using (MemoryStream ms = new MemoryStream())
                        {
                            image.Save(ms, ImageFormat.Bmp);
                            bw.Write((int)(ms.Length - 14)); // excluding the BITMAPFILEHEADER structure https://en.wikipedia.org/wiki/ICO_(file_format)
                            bw.Write((int)output.Length);
                            bw.Seek(0, SeekOrigin.End);
                            byte[] vs = ms.ToArray().Skip(14).ToArray();
                            Array.Copy(BitConverter.GetBytes(width * 2), 0, vs, 8, 4);
                            bw.Write(vs);
                        }
                    }
                    current += 16;
                }

                bw.Seek(current, SeekOrigin.Begin);

                using (Image image = Resize(org, 256))
                {
                    bw.Write((short)0);
                    bw.Write((byte)image.Palette.Entries.Length);
                    bw.Write((byte)0);
                    bw.Write((short)0);
                    bw.Write(GetBPP(image.PixelFormat));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        image.Save(ms, ImageFormat.Png);
                        bw.Write((int)ms.Length);
                        bw.Write((int)output.Length);
                        bw.Seek(0, SeekOrigin.End);
                        bw.Write(ms.ToArray());
                    }
                }
            }
        }

        static short GetBPP(PixelFormat pixelFormat)
        {
            switch (pixelFormat)
            {
                case PixelFormat.Format1bppIndexed:
                    return 1;
                case PixelFormat.Format4bppIndexed:
                    return 4;
                case PixelFormat.Format8bppIndexed:
                    return 8;
                case PixelFormat.Format16bppGrayScale:
                case PixelFormat.Format16bppRgb555:
                case PixelFormat.Format16bppRgb565:
                case PixelFormat.Format16bppArgb1555:
                    return 16;
                case PixelFormat.Format24bppRgb:
                    return 24;
                case PixelFormat.Format32bppRgb:
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                    return 32;
                case PixelFormat.Format48bppRgb:
                    return 48;
                case PixelFormat.Format64bppArgb:
                case PixelFormat.Format64bppPArgb:
                    return 64;
                default:
                    throw new Exception();
            }
        }

        static Image Resize(Image org, int newSize)
        {
            if (org.Width == newSize)
                return (Image)org.Clone();

            var destImage = new Bitmap(newSize, newSize);
            destImage.SetResolution(org.HorizontalResolution, org.VerticalResolution);
            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(org, new Rectangle(0, 0, newSize, newSize), 0, 0, org.Width, org.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            return destImage;
        }
    }
}