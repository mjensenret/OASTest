using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TestTCPClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Parse("172.18.106.106"), 7734);

            sck.Connect(endPoint);

            Console.Write("Enter Message: ");
            string msg = Console.ReadLine();
            byte[] msgBuffer = Encoding.ASCII.GetBytes(msg);
            sck.Send(msgBuffer, 0, msgBuffer.Length, 0);

            byte[] buffer = new byte[255];
            int rec = sck.Receive(buffer, 0, buffer.Length, 0);

            Array.Resize(ref buffer, rec);

            Console.WriteLine("Recieved: {0}", Encoding.ASCII.GetString(buffer));

            sck.Close();
            

            Console.Read();

        }
    }
}
