using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace tcpClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        TcpClient myClient;
        NetworkStream ns;
        bool connected = false;

        //gönderilecek karakterler
        char[] led1 = {'a'};
        char[] lm35 = { 'b' };
        char[] led2 = { 'c' };
        char[] ldr = { 'd' };
        byte[] readData = new byte[256];
        string[] stringSeparators = new string[] { "|" };

        private void Form1_Load(object sender, EventArgs e)
        {
            //timer lar 200ms' de tetikleniyor
            timer1.Interval = 200;  
            timer2.Interval = 200;
        }

        //bağlanma butonu
        private void button1_Click(object sender, EventArgs e)
        {
            //bağlı ise bağlantıyı kes
            if (!connected)
            {
                try
                {
                    myClient = new TcpClient("169.254.145.11", 61);         //server IP ve port
                    label1.Text = "Bağlandı";
                    connected = true;
                    ns = myClient.GetStream();
                    button1.Text = "DURDUR";
                }catch(Exception ex)
                {
                    MessageBox.Show("Bağlantı Kurulamadı!\n\r" + ex.Message, "Baglanti Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connected = false;
                    button1.Text = "Bağlan";
                }
            }else
            {
                try
                {
                    myClient.Close();               //server ile bağlantıyı kes
                    button1.Text = "Bağlan";
                    label1.Text = "Bağlantı Yok";
                    connected = false;
                }catch(Exception ex)
                {
                    MessageBox.Show("Bağlantı Kurulamadı!\n\r" + ex.Message, "Baglanti Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connected = false;
                    button1.Text = "Bağlan";
                    label1.Text = "Bağlantı Yok";
                }
            }
        }

        //led1 durum değiştirme ve durum görüntüleme
        private void button2_Click(object sender, EventArgs e)
        {
            if(connected)
            {
                ns.Write(Encoding.GetEncoding("UTF-8").GetBytes(led1), 0, led1.Length);         //led1 karakterini gönderdik
                int recv = ns.Read(readData, 0, readData.Length);                               //gelen veriyi aldık
                String s = Encoding.ASCII.GetString(readData, 0, recv);
                String[] ss = s.Split(stringSeparators, StringSplitOptions.None);               //parseleyip lable' e yazdırdık
                label2.Text = ss[1];
            }
        }

        //led2 durum değiştirme ve durum görüntüleme
        private void button3_Click(object sender, EventArgs e)
        {
            if(connected)
            {
                ns.Write(Encoding.GetEncoding("UTF-8").GetBytes(led2), 0, led2.Length);         //led2 karakterini gönderdik
                int recv = ns.Read(readData, 0, readData.Length);                               //gelen veriyi aldık
                String s = Encoding.ASCII.GetString(readData, 0, recv);
                String[] ss = s.Split(stringSeparators, StringSplitOptions.None);               //parseleyip lable' e yazdırdık
                label3.Text = ss[1];
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(connected)
            {
                ns.Write(Encoding.GetEncoding("UTF-8").GetBytes(lm35), 0, lm35.Length);         //lm35 karakterini gönderdik
                int recv = ns.Read(readData, 0, readData.Length);                               //gelen veriyi aldık
                String s = Encoding.ASCII.GetString(readData, 0, recv);
                String[] ss = s.Split(stringSeparators, StringSplitOptions.None);               //parseleyip lable' e yazdırdık
                label5.Text = ss[1];
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(connected)
            {
                ns.Write(Encoding.GetEncoding("UTF-8").GetBytes(ldr), 0, ldr.Length);           //ldr karakterini gönderdik
                int recv = ns.Read(readData, 0, readData.Length);                               //gelen veriyi aldık
                String s = Encoding.ASCII.GetString(readData, 0, recv);
                String[] ss = s.Split(stringSeparators, StringSplitOptions.None);               //parseleyip lable' e yazdırdık
                label4.Text = ss[1];
            }
        }

        //checkbox1 işaretlenince timer1' ı başlat
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                if(connected)
                {
                    timer1.Start();
                }
                else
                {
                    checkBox1.Checked = false;
                }
            }
            else
            {
                timer1.Stop();
            }
        }

        //200ms 'de bir lm35 değerini iste
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (connected)
            {
                ns.Write(Encoding.GetEncoding("UTF-8").GetBytes(lm35), 0, led1.Length);
                int recv = ns.Read(readData, 0, readData.Length);
                String s = Encoding.ASCII.GetString(readData, 0, recv);
                String[] ss = s.Split(stringSeparators, StringSplitOptions.None);
                label5.Text = ss[1];
            }
            else
            {
                timer1.Stop();
            }
        }

        //200ms 'de bir ldr değerini iste
        private void timer2_Tick(object sender, EventArgs e)
        {
            if (connected)
            {
                ns.Write(Encoding.GetEncoding("UTF-8").GetBytes(ldr), 0, led1.Length);
                int recv = ns.Read(readData, 0, readData.Length);
                String s = Encoding.ASCII.GetString(readData, 0, recv);
                String[] ss = s.Split(stringSeparators, StringSplitOptions.None);
                label4.Text = ss[1];
            }
            else
            {
                timer2.Stop();
            }
        }

        //checkbox2 işaretlenince timer2' ı başlat
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                if (connected)
                {
                    timer2.Start();
                }
                else
                {
                    checkBox2.Checked = false;
                }
            }
            else
            {
                timer2.Stop();
            }
        }

    }
}
