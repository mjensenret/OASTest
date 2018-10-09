using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace Client_GUI
{
    public partial class ClientForm : Form
    {
        Socket sock;

        public ClientForm()
        {
            InitializeComponent();
            sock = socket();
            FormClosing += new FormClosingEventHandler(ClientForm_FormClosing);
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            sock.Close();
        }

        Socket socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                sock.Connect(new IPEndPoint(IPAddress.Parse(txtIPAddress.Text), 7734));
                new Thread(() =>
                {
                    read();
                }).Start();

                MessageBox.Show("Connection Succeeded!");
            }
            catch
            {
                MessageBox.Show("Connection failed!");
            }



        }

        void read()
        {
            
            while (true)
            {
                try
                {

                    byte[] buffer = new byte[1024];
                    //int rec = sock.Receive(buffer, 0, buffer.Length, 0);
                    int rec = sock.Receive(buffer);
                    if (rec <= 0)
                    {
                        throw new SocketException();
                    }
                    Array.Resize(ref buffer, rec);
                    Console.WriteLine("rec: {0}", rec.ToString());
                    Console.WriteLine("Response: {0}", AsciiPacketToString(buffer));
                    Console.WriteLine("Trimmed Response: {0}", response(buffer));

                    Invoke((MethodInvoker)delegate
                    {
                        lstServerMessage.Items.Add(AsciiPacketToString(buffer));
                    });
                }
                catch
                {
                    MessageBox.Show("Disconnected");
                    sock.Close();
                    break;
                    
                }
            }
            Application.Exit();
        }



        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] data = Encoding.ASCII.GetBytes("\x02" + txtSendText.Text + "\x03");
            sock.Send(data, 0, data.Length, 0);
        }

        public static string AsciiPacketToString(byte[] bytes)
        {
            byte[] copy = new byte[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
                if (bytes[i] == 0x00)
                    copy[i] = (byte)'~';
                else if (bytes[i] == 0x20)
                    copy[i] = (byte)'-';
                else
                    copy[i] = bytes[i];
            return Encoding.ASCII.GetString(copy, 0, copy.Length);
        }

        public static string response(byte[] bytes)
        {
            byte[] copy = new byte[bytes.Length];

            string response = Encoding.ASCII.GetString(bytes, 1, bytes.Length -1);

            if (response.Contains("BD"))
                Console.WriteLine("Status: Idle");
            else if (response.Contains("FL"))
                Console.WriteLine("Status: Active");

            Console.WriteLine("ResponseString: {0}", response);

            if (bytes[4] == 82 && bytes[5] == 84)
            {
                Array.Resize(ref copy, 8);
                Buffer.BlockCopy(bytes, 15, copy, 0, 8);
            }
            else
            {
                Buffer.BlockCopy(bytes, 0, copy, 0, bytes.Length);
            }
            return Encoding.ASCII.GetString(copy, 0, copy.Length);
        }
    }
}
