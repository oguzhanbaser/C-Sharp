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
    public partial class constant : Form
    {
        Form1 anaForm = new Form1();
        SerialPort mySerial;

        public constant(SerialPort _s)
        {
            InitializeComponent();
            mySerial = _s;
            button1.Text = "Yenile";
        }

        private void constant_Load(object sender, EventArgs e)
        {
            timer1.Interval = anaForm.timerInterval;
            listBox1.Enabled = false;
            checkBox1.Text = "GÖNDER";
            checkBox2.Text = "Sabit Oku";
            label1.Text = "KP"; label7.Text = "0";
            label2.Text = "KD"; label6.Text = "0";
            label3.Text = "KI"; label5.Text = "0";
            label9.Text = "HIZ";
            numericUpDown1.Maximum = 10000; numericUpDown1.Minimum = 0;
            numericUpDown2.Maximum = 10000; numericUpDown2.Minimum = 0;
            numericUpDown3.Maximum = 10000; numericUpDown3.Minimum = 0;
            numericUpDown1.Increment = 0.01M; numericUpDown2.Increment = 0.01M; numericUpDown3.Increment = 0.01M;
            if (mySerial.IsOpen) label4.Text = "Bağlandı"; else label4.Text = "Bağlı Değil";

            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (mySerial.IsOpen)
            {
                label4.Text = "Bağlandı"; 
            }
            else
            {
                label4.Text = "Bağlı Değil";
            }
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(mySerial.IsOpen && checkBox1.Checked)
            {
                mySerial.WriteLine("$" + numericUpDown1.Value * 1000 +"|");
            }
            label7.Text = ((float)numericUpDown1.Value).ToString();
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (mySerial.IsOpen && checkBox1.Checked)
            {
                mySerial.WriteLine("%" + numericUpDown2.Value * 1000 + "|");
            }
            label6.Text = ((float)numericUpDown2.Value).ToString();
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (mySerial.IsOpen && checkBox1.Checked)
            {
                mySerial.WriteLine("!" + numericUpDown3.Value * 1000 + "|");
            }
            label5.Text = ((float)numericUpDown3.Value).ToString();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox2.Checked)
            {
                listBox1.Enabled = true;
                if(mySerial.IsOpen)
                {
                    mySerial.WriteLine("&0|");
                    timer1.Start();
                }
                else
                {
                    listBox1.Items.Add("HATA!");
                }
            }
            else
            {
                listBox1.Items.Clear();
                listBox1.Enabled = false;
                if(mySerial.IsOpen)
                {
                    mySerial.WriteLine("&1|");
                    timer1.Stop();
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            listBox1.Items.Add(anaForm.hamVeri);
            listBox1.SelectedIndex = listBox1.SelectedIndex + 1;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (mySerial.IsOpen && checkBox1.Checked)
            {
                mySerial.WriteLine("+" + numericUpDown4.Value + "|");
            }
        }
    }
}
