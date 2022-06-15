using System;
using System.IO;
using System.Windows.Forms;

namespace YouthUp.Commands
{
    static class Class1
    {
        public static string GetFileName(string dir, string fileName, string extension) => 118.Err(() =>
        {
            string res = Path.Combine(dir, fileName + extension);
            for (long i = 2; File.Exists(res); i++)
                res = Path.Combine(dir, fileName + " (" + i + ")" + extension);
            return res;
        });

        public static void Err(this int errorCode, Action action)
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "YouthUp - Error: " + errorCode);
                throw ex;
            }
        }

        public static T Err<T>(this int errorCode, Func<T> func)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                _ = MessageBox.Show(ex.Message, "YouthUp - Error: " + errorCode);
                throw ex;
            }
        }
    }
}