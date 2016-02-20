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
using System.Windows.Forms.DataVisualization.Charting;

namespace pidConstant
{
    public partial class graphics : Form
    {
        Form1 anaForm = new Form1();

        SerialPort mySerial;

        Series series1;
        Series series2;
        Series series3;
        Series seriesPosition;

        public graphics(SerialPort _s)
        {
            InitializeComponent();
            mySerial = _s;
        }

        private void graphics_Load(object sender, EventArgs e)
        {
            button1.Text = "Yenile";
            timer1.Interval = anaForm.timerInterval;

            graphReload();
            if (mySerial.IsOpen) label1.Text = "Bağlandı"; else label1.Text = "Bağlı Değil";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (mySerial.IsOpen)
            {
                label1.Text = "Bağlandı";
                timer1.Start();
            }
            else
            {
                label1.Text = "Bağlı Değil";
            }
        }

        int sayac = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if (mySerial.IsOpen && anaForm.sensorDegerleri[0] != -1)
            {
                try
                {
                    series1.Points.AddXY(sayac, anaForm.PID[0]);
                    series2.Points.AddXY(sayac, anaForm.PID[2]);
                    series3.Points.AddXY(sayac, anaForm.PID[1]);
                    seriesPosition.Points.AddXY(sayac, anaForm.PID[0] + anaForm.PID[1] + anaForm.PID[2]);
                }catch { }
            }

            if(sayac % 500 == 0)
            {
                graphReload();
            }
        }

        int x = 0;
        Random rnd = new Random();

        void graphReload()
        {
            chart1.Series.Clear();
            chart1.Legends.Clear();

            series1 = chart1.Series.Add("P");
            series2 = chart1.Series.Add("I");
            series3 = chart1.Series.Add("D");
            seriesPosition = chart1.Series.Add("Position");

            series1.BorderWidth = 2;
            series2.BorderWidth = 2;
            series3.BorderWidth = 2;
            seriesPosition.BorderWidth = 2;

            seriesPosition.Color = Color.Green;
            
            series1.ChartType = SeriesChartType.Spline;
            series2.ChartType = SeriesChartType.Spline;
            series3.ChartType = SeriesChartType.Spline;
            seriesPosition.ChartType = SeriesChartType.Spline;

            if (checkBox1.Checked)
            {
                series1.Enabled = true;
            }
            else
            {
                series1.Enabled = false;
            }

            if (checkBox2.Checked)
            {
                series2.Enabled = true;
            }else
            {
                series2.Enabled = false;
            }
            if (checkBox3.Checked)
            {
                series3.Enabled = true;
            }
            else
            {
                series3.Enabled = false;
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            {
                series1.Enabled = true;
            }
            else
            {
                series1.Enabled = false;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                series2.Enabled = true;
            }
            else
            {
                series2.Enabled = false;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                series3.Enabled = true;
            }
            else
            {
                series3.Enabled = false;
            }
        }
    }
}
