using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Daily
{
    class C
    {
        public static string GetGun(DateTime date)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    string path = @"daily\" + date.ToString("dd.MM.yyyy");
                    if (!Directory.Exists("daily"))
                        _ = Directory.CreateDirectory("daily");
                    return DecryptString(File.ReadAllText(path));
                }
                catch { }
            }
            return "";
        }

        public static void WriteGun(DateTime date, string val)
        {
            for (int i = 0; i < 10; i++)
            {
                try
                {
                    string path = @"daily\" + date.ToString("dd.MM.yyyy");
                    if (!Directory.Exists("daily"))
                        _ = Directory.CreateDirectory("daily");
                    using (StreamWriter sw = new StreamWriter(path))
                        sw.Write(EncryptString(val));
                    break;
                }
                catch
                { }
                if (i == 9)
                    _ = MessageBox.Show("Error while saving! Please try again");
            }
        }

        public static string EncryptString(string plainText)
        {
            byte[] array;

            using (Aes aes = Aes.Create())
            {
                aes.Key = new byte[16];
                aes.IV = new byte[16];

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                            streamWriter.Write(plainText);

                        array = memoryStream.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(array);
        }

        public static string DecryptString(string cipherText)
        {
            byte[] buffer = Convert.FromBase64String(cipherText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = new byte[16];
                aes.IV = new byte[16];
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream memoryStream = new MemoryStream(buffer))
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                using (StreamReader streamReader = new StreamReader(cryptoStream))
                    return streamReader.ReadToEnd();
            }
        }
    }
}