using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.IO.Ports;

namespace WindowsFormsApplication3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            timer1.Interval = 1000;         //timer değeri 1 sn olarak ayarlı
        }
        DateTime zaman = DateTime.Now;


        //form açıldığı andaki olaylar
        private void Form1_Load(object sender, EventArgs e)
        {
            linkLabel1.Text = "Oğuzhan Başer";
            button1.Text = "OTOMATİK BAĞLAN";
            button2.Text = "BAĞLAN";
            label1.Text = Convert.ToString(zaman.Hour);
            label2.Text = Convert.ToString(zaman.Minute);
            label3.Text = Convert.ToString(zaman.Second);
            timer1.Start();
        }

        //otomatik bağlan butonuna basınca com port u otomatik bulup saat bilgisini gönder
        private void button1_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();

            foreach(string portTara in SerialPort.GetPortNames()){
                 serialPort1.PortName = portTara;
                 serialPort1.BaudRate = 9600;
                 serialPort1.Open();
                
            }

            if (serialPort1.IsOpen)
            {
                zaman = DateTime.Now;
                serialPort1.Write(zaman.ToShortTimeString() +  zaman.Second.ToString() + "|");
                serialPort1.Close();
            }

        }


        //Her saniye labellere saat değerini yaz
        private void timer1_Tick(object sender, EventArgs e)
        {
            zaman = DateTime.Now;
            label1.Text = Convert.ToString(zaman.Hour);
            label2.Text = Convert.ToString(zaman.Minute);
            label3.Text = Convert.ToString(zaman.Second);
        }


        //otomatik bağlan işe yaramaması yada bir den çok com port bulunma durumunakarşı hazırlanan kısım
        //com bortu ve baud rate yi manuel girerek saat bilgisini arduino ya gönderir
        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen) serialPort1.Close();
            serialPort1.PortName = Interaction.InputBox("COM PORT GİRİNİZ: ");
            int baudRate;
            if (System.Int32.TryParse(Interaction.InputBox("BAUD RATE GİRİNİZ: "), out baudRate))
            {
                serialPort1.BaudRate = baudRate;

                try
                {
                    serialPort1.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Geçerli Baud Rate Giriniz!", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (serialPort1.IsOpen)
            {
                zaman = DateTime.Now;
                serialPort1.Write(zaman.ToShortTimeString() + zaman.Second.ToString() + "|");
                serialPort1.Close();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.elektrobot.net");
        }
    }
}
