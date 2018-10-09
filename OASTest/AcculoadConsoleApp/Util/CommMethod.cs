using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcculoadConsoleApp
{
    public abstract class CommMethod
    {
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

        public abstract bool IsConnected { get; }

        public abstract void Connect();

        public abstract void Disconnect();
    }
}
