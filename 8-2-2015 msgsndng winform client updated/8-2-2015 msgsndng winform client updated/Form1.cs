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

namespace _8_2_2015_msgsndng_winform_client_updated
{
    public partial class Form1 : Form
    {
        
        static TcpClient client;
        static NetworkStream stream;
        static ASCIIEncoding aschi = new ASCIIEncoding();
        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            client = new TcpClient(txt_ipaddress.Text, 6060);
            stream = client.GetStream();
            Control.CheckForIllegalCrossThreadCalls = false;
            new Thread(rcvmsg).Start();


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
                textBox1.Text += "\r\n"+"Client : " + msg;
                textBox2.Text = "";
                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.ScrollToCaret();
            }
        }


        public void rcvmsg()
        {
            while (true)
            {
                
                stream = client.GetStream();
                byte[] data = new byte[100];
                stream.Read(data, 0, data.Length);
                string msg = aschi.GetString(data, 0, data.Length);
                textBox1.Text += "\r\n"+"Server : " + msg;
                textBox1.SelectionStart = textBox1.TextLength;
                textBox1.ScrollToCaret();
            }
        }

    }
}
