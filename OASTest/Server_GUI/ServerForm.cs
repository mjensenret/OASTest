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

namespace Server_GUI
{
    public partial class ServerForm : Form
    {
        Socket sock;
        Socket acc;

        public ServerForm()
        {
            InitializeComponent();
        }

        Socket socket()
        {
            return new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            sock = socket();

            sock.Bind(new IPEndPoint(0, 7734));
            sock.Listen(0);

            new Thread(() =>
            {
                acc = sock.Accept();
                MessageBox.Show("Connect Accepted!");
                sock.Close();

                while (true)
                {
                    try
                    {
                        byte[] buffer = new byte[255];
                        int rec = acc.Receive(buffer, 0, buffer.Length, 0);

                        if (rec <= 0)
                        {
                            throw new SocketException();
                        }

                        Array.Resize(ref buffer, rec);

                        Invoke((MethodInvoker)delegate
                        {
                            lstClientMessage.Items.Add(Encoding.ASCII.GetString(buffer));
                        });
                    }
                    catch
                    {
                        MessageBox.Show("Disconnected!");
                        break;
                    }

                }
                Application.Exit();
            }).Start();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            byte[] data = Encoding.ASCII.GetBytes(txtSendText.Text);
            acc.Send(data, 0, data.Length, 0);

        }
    }
}
