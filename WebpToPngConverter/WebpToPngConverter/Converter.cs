using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace WebpToPngConverter
{
    public class Converter
    {
        [DllImport("libwebp", CallingConvention = CallingConvention.Cdecl)]
        static extern int WebPGetInfo(IntPtr data, uint data_size, out int width, out int height);

        [DllImport("libwebp", CallingConvention = CallingConvention.Cdecl)]
        static extern IntPtr WebPDecodeBGRAInto(IntPtr data, uint data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        [DllImport("kernel32.dll")]
        static extern void CopyMemory(IntPtr Destination, IntPtr Source, uint Length);

        public static Bitmap Convert(byte[] webpData)
        {
            GCHandle pinnedWebP = GCHandle.Alloc(webpData, GCHandleType.Pinned);

            Bitmap bitmap = null;
            BitmapData bitmapData = null;
            IntPtr outputBuffer = IntPtr.Zero;

            try
            {
                IntPtr ptrData = pinnedWebP.AddrOfPinnedObject();
                if (WebPGetInfo(ptrData, (uint)webpData.Length, out int width, out int height) != 1)
                    throw new Exception("Error 100");

                bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);

                int outputBufferSize = bitmapData.Stride * height;
                outputBuffer = Marshal.AllocHGlobal(outputBufferSize);

                outputBuffer = WebPDecodeBGRAInto(ptrData, (uint)webpData.Length, outputBuffer, outputBufferSize, bitmapData.Stride);

                CopyMemory(bitmapData.Scan0, outputBuffer, (uint)outputBufferSize);
            }
            finally
            {
                bitmap?.UnlockBits(bitmapData);
                pinnedWebP.Free();
                Marshal.FreeHGlobal(outputBuffer);
            }
            return bitmap;
        }

        public static Bitmap Convert(string path) => Convert(File.ReadAllBytes(path));

        public static Bitmap Convert(Stream stream)
        {
            byte[] vs;
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] read = new byte[1024 * 10];
                int count;
                while ((count = stream.Read(read, 0, read.Length)) > 0)
                    ms.Write(read, 0, count);
                vs = ms.ToArray();
            }
            return Convert(vs);
        }
    }
}