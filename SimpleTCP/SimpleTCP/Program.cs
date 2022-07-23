using System;
using System.Net.Sockets;

namespace SimpleTCP
{
    internal class Program
    {
        static void Main()
        {
            TcpClient client = new TcpClient();
            client.Connect("192.168.1.158", 61);
            Console.WriteLine("Connected!");
            while (true)
            {
                char c = Console.ReadKey().KeyChar;
                Console.Write("\r ");
                Console.SetCursorPosition(0, 1);
                client.GetStream().WriteByte(BitConverter.GetBytes(c)[0]);
            }
        }
    }
}