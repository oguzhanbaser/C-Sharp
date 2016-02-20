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
    public partial class Form1 : Form
    {
        static int[] _sensorDegeri = new int[8];
        //P I D
        static double[] _PID = new double[3];
        static string _PIDVal;
        static int _timerInterval = 100;
        static String _hamveri = string.Empty;

        public String hamVeri
        {
            get { return _hamveri; }
            set { _hamveri = value; }
        }

        public int timerInterval
        {
            get { return _timerInterval; }
            set { _timerInterval = value; }
        }

        public int[] sensorDegerleri
        {
            get { return _sensorDegeri; }
            set { _sensorDegeri = value; }
        }

        public double[] PID
        {
            get { return _PID; }
            set { _PID = value; }
        }

        public string PIDval
        {
            get { return _PIDVal; }
            set { _PIDVal = value; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            timer1.Interval = _timerInterval;
            button1.Text = "OTO BAĞLAN";
            button2.Text = "BAĞLAN";
            comboBox1.DataSource = SerialPort.GetPortNames();
            label1.Text = "COM"; label2.Text = "BAUD";
            label3.Text = "Karadeniz Tenknik Üniversitesi \n IEEE RAS";
            linkLabel1.Text = "Oğuzhan Başer";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(!serialPort1.IsOpen)
            {
                foreach (String portName in SerialPort.GetPortNames())
                {
                    serialPort1.PortName = portName;
                    serialPort1.BaudRate = 9600;
                }

                try
                {
                    serialPort1.Open();
                    timer1.Start();
                    button1.Text = "Bağlantıyı Kes";
                    button2.Enabled = false;
                }
                catch (Exception ex){ MessageBox.Show(ex.Message.ToString(), "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else
            {
                if (serialPort1.IsOpen)
                {
                    serialPort1.Close();
                    button1.Text = "Oto Bağlan";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(!serialPort1.IsOpen)
            {
                try
                {
                    serialPort1.BaudRate = Convert.ToInt32(textBox1.Text);
                    serialPort1.PortName = comboBox1.SelectedItem.ToString();
                    serialPort1.Open();
                    button2.Enabled = false;
                    button1.Text = "Bağlantıyı Kes";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("COM Portu Yazarak yada Arduino Serial Monitörü Kapatarak Tekrar Deneyiniz \n\n" + ex.Message.ToString() + "\n\n" + ex.ToString(), "HATA!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void sensörDeğerleriToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sensors form1 = new sensors(serialPort1);
            form1.Show();
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            comboBox1.DataSource = SerialPort.GetPortNames();
        }

        private void sabitAyarlaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            constant form1 = new constant(serialPort1);
            form1.Show();
        }

        private void pIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            graphics form1 = new graphics(serialPort1);
            form1.Show();
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //String hamveri = serialPort1.ReadLine();
            try
            {
                _hamveri = serialPort1.ReadLine();
            }catch(Exception ex)
            {
                MessageBox.Show("Reading Error!");
            }

            String[] _gelenVeri = _hamveri.Split('|');
            
            
            if(_gelenVeri[0] == "#" && _gelenVeri.Length >= 12)
            {

                try
                {
                    Int32.TryParse(_gelenVeri[1], out _sensorDegeri[0]);
                    Int32.TryParse(_gelenVeri[2], out _sensorDegeri[1]);
                    Int32.TryParse(_gelenVeri[3], out _sensorDegeri[2]);
                    Int32.TryParse(_gelenVeri[4], out _sensorDegeri[3]);
                    Int32.TryParse(_gelenVeri[5], out _sensorDegeri[4]);
                    Int32.TryParse(_gelenVeri[6], out _sensorDegeri[5]);
                    Int32.TryParse(_gelenVeri[7], out _sensorDegeri[6]);
                    Int32.TryParse(_gelenVeri[8], out _sensorDegeri[7]);

                    _PID[0] = Convert.ToDouble(_gelenVeri[9]) / 1.0;     //pr
                    _PID[1] = Convert.ToDouble(_gelenVeri[10]) / 1.0;    //dr
                    _PID[2] = Convert.ToDouble(_gelenVeri[11]) / 1.0;    //in
                    _PIDVal = _gelenVeri[12];
                }
                catch (FormatException eX) { }

                /*
                Double.TryParse(_gelenVeri[9], out _PID[0]);
                Double.TryParse(_gelenVeri[10], out _PID[1]);
                Double.TryParse(_gelenVeri[11], out _PID[2]);*/
            }
            else
            {
                _sensorDegeri[0] = -1;
            }
            
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        private void serialPort1_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
        {
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(!serialPort1.IsOpen)
            {
                button2.Enabled = true;
                button1.Text = "OTO BAĞLAN";
            }
        }

        private void kontrolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            control form1 = new control(serialPort1);
            form1.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.elektrobot.net/");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }
    }
}
