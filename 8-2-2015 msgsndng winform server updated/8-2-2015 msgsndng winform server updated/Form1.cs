using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace _8_2_2015_msgsndng_winform_server_updated
{
    public partial class Form1 : Form
    {
        static TcpListener listner;
        static TcpClient client;
        static NetworkStream stream;
        static ASCIIEncoding aschi = new ASCIIEncoding();


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listner = new TcpListener(IPAddress.Any, 6060);
            listner.Start();
            client = listner.AcceptTcpClient();
            Control.CheckForIllegalCrossThreadCalls = false;
            new Thread(recivemsg).Start();

        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                stream = client.GetStream();
                byte[] data = new byte[100];
                string msg = textBox2.Text;
                data = aschi.GetBytes(msg);
                stream.Write(data, 0, data.Length);
                textBox1.Text += "\r\n" + "Server : " + msg;
                textBox2.Text = "";
                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.ScrollToCaret();
            }
        }




        public void recivemsg()
        {
            while (true)
            {

                stream = client.GetStream();
                byte[] data = new byte[100];
                stream.Read(data, 0, data.Length);
                string msg = aschi.GetString(data, 0, data.Length);
                textBox1.Text += "\r\n" + "Client : " + msg;
                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.ScrollToCaret();
            }
        }







    }
}
