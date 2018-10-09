using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AcculoadConsoleApp
{
    class Program
    {
        public static void StartClient()
        {
            byte[] bytes = new byte[1024];

            //Connect to the AccuLoad
            try
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse("172.18.106.106"), 7734);

                Socket sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Connected to: {0}", sender.RemoteEndPoint.ToString());

                    byte[] msg = Encoding.ASCII.GetBytes("\x02" + "01RQ" + "\x03");



                    Console.WriteLine("msg: {0}", AsciiPacketToString(msg));

                    int bytesSent = sender.Send(msg);

                    Console.WriteLine("bytesSent: {0}", bytesSent.ToString());


                    int bytesRec = sender.Receive(bytes);

                    Console.WriteLine("bytesRec: {0}", bytesRec.ToString());
                    Console.WriteLine("Response: {0}", AsciiPacketToString(bytes));

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                    Console.Read();


                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected Exception : {0}", e.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static string AsciiPacketToString(byte[] bytes)
        {
            byte[] copy = new byte[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
                if (bytes[i] == 0x00)
                    copy[i] = (byte)'~';
                else
                    copy[i] = bytes[i];
            return Encoding.ASCII.GetString(copy, 0, copy.Length);
        }

        public static int Main(string[] args)
        {


            //Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            //sck.Bind(new IPEndPoint(0, 7734));
            //sck.Listen(0);

            //Socket acc = sck.Accept();

            //byte[] buffer = Encoding.ASCII.GetBytes("Hello World!");
            //acc.Send(buffer, 0, buffer.Length, 0);

            //buffer = new byte[255];
            //int rec = acc.Receive(buffer, 0, buffer.Length, 0);

            //Array.Resize(ref buffer, rec);

            //Console.WriteLine("Received: {0} ", Encoding.ASCII.GetString(buffer));

            //sck.Close();
            //acc.Close();

            //Console.Read();


            StartClient();
            return 0;

        }
    }
}
