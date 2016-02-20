using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace pidConstant
{
    public partial class sensors : Form
    {
        Form1 anaForm = new Form1();
        SerialPort mySerial;

        public sensors(SerialPort _s)
        {
            InitializeComponent();
            mySerial = _s;
            //anaForm.sPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.dataReceived);
            //mySerial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.dataReceived);

        }

        private void sensors_Load(object sender, EventArgs e)
        {
            if (mySerial.IsOpen) label10.Text = "Bağlandı"; else label10.Text = "Bağlı Değil";
            button1.Text = "Yenile";
            timer1.Interval = anaForm.timerInterval;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(mySerial.IsOpen)
            {
                label10.Text = "Bağlandı";
                timer1.Start();
            }
            else
            {
                label10.Text = "Bağlı Değil";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(mySerial.IsOpen && anaForm.sensorDegerleri[0] != -1)
            {
                progressBar1.Value = anaForm.sensorDegerleri[0];
                progressBar2.Value = anaForm.sensorDegerleri[1];
                progressBar3.Value = anaForm.sensorDegerleri[2];
                progressBar4.Value = anaForm.sensorDegerleri[3];
                progressBar8.Value = anaForm.sensorDegerleri[4];
                progressBar7.Value = anaForm.sensorDegerleri[5];
                progressBar6.Value = anaForm.sensorDegerleri[6];
                progressBar5.Value = anaForm.sensorDegerleri[7];

                label1.Text = anaForm.sensorDegerleri[0].ToString();
                label2.Text = anaForm.sensorDegerleri[1].ToString();
                label3.Text = anaForm.sensorDegerleri[2].ToString();
                label4.Text = anaForm.sensorDegerleri[3].ToString();
                label8.Text = anaForm.sensorDegerleri[4].ToString();
                label7.Text = anaForm.sensorDegerleri[5].ToString();
                label6.Text = anaForm.sensorDegerleri[6].ToString();
                label5.Text = anaForm.sensorDegerleri[7].ToString();

                label11.Text = anaForm.PIDval;

                if(ortalamaAl() > 2500 && ortalamaAl() < 5000)
                    trackBar1.Value = (int)ortalamaAl();
                label9.Text = trackBar1.Value.ToString();
            }
        }

        long ortalamaAl()
        {
            long pay = 0, payda = 0;

            for(int i = 0; i < 8; i++)
            {
                pay = pay + anaForm.sensorDegerleri[i] * 1000 * i;
                payda = payda + anaForm.sensorDegerleri[i];
            }

            if (payda != 0)
                return pay / payda;
            else
                return 0;
        }

        /*
        private void dataReceived(object sender, SerialDataReceivedEventArgs e)
        {

        }*/

    }
}
