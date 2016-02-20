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
    public partial class control : Form
    {
        Form1 anaForm = new Form1();
        SerialPort mySerial;
        bool status = true;

        public control(SerialPort _s)
        {
            InitializeComponent();
            mySerial = _s;
        }

        private void control_Load(object sender, EventArgs e)
        {
            button1.Text = "DURDUR";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(mySerial.IsOpen)
            {
                mySerial.WriteLine("a");
                status = !status;
            }

            if(status && mySerial.IsOpen)
            {
                button1.Text = "DURDUR";
            }
            else if(mySerial.IsOpen)
            {
                button1.Text = "BAŞLAT";
            }
        }
    }
}
