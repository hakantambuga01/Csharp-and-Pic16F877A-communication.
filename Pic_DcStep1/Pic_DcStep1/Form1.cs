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
using System.Threading;

namespace Pic_DcStep1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string[] portlar = SerialPort.GetPortNames();
        int pwm;
        int step;

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string port in portlar)
            {
                comboBox1.Items.Add(port);
            }
            label4.Text = "0";
            label6.Text = "0";
        }

        //-----------------------> Port Aç <------------------------------
        private void button1_Click(object sender, EventArgs e)
        {
            if(serialPort1.IsOpen==false)
            {
                if (comboBox1.Text == "")
                    return;
                serialPort1.PortName = comboBox1.Text;
                serialPort1.BaudRate = Convert.ToInt32("9600");
                try
                {
                    serialPort1.Open();
                    button1.BackColor = Color.Green;
                    button2.BackColor = Color.WhiteSmoke;
                    serialPort1.DiscardOutBuffer();
                    serialPort1.DiscardInBuffer();
                }
                catch (Exception hata)
                {

                    MessageBox.Show("Hata:" + hata.Message);
                }
             
            }
        }
        //---------------------> Port Kapat <--------------------------------
        private void button2_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen == true)
            {
                serialPort1.Close();
                button2.BackColor = Color.Red;
                button1.BackColor = Color.WhiteSmoke;
                label4.Text = "0";
                pwm = 0;
            }
        }

        //------------------> DC PWM ARTTIR <-----------------------------
        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.Write("W");
            button3.BackColor = Color.WhiteSmoke;
            pwm = pwm + 10;
            if (pwm >= 250) pwm = 250;
            label4.Text = Convert.ToString(pwm);
        }

        //------------------> DC PWM AZALT <-----------------------------
        private void button4_Click(object sender, EventArgs e)
        {
            serialPort1.Write("S");
            button4.BackColor = Color.WhiteSmoke;
            pwm = pwm - 10;
            if (pwm <= 0) pwm = 0;
            label4.Text = Convert.ToString(pwm);
        }

        //------------------> DC SAĞA DÖN <-----------------------------
        private void button5_Click(object sender, EventArgs e)
        {
            serialPort1.Write("D");
            button5.BackColor = Color.Red;
            button6.BackColor = Color.WhiteSmoke;
            label4.Text = "0";
            pwm = 0;
        }

        //------------------> DC SOLA DÖN <-----------------------------
        private void button6_Click(object sender, EventArgs e)
        {
            serialPort1.Write("A");
            button6.BackColor = Color.Red;
            button5.BackColor = Color.WhiteSmoke;
            label4.Text = "0";
            pwm = 0;
        }

        //------------------> DC STOP <-----------------------------
        private void button7_Click(object sender, EventArgs e)
        {
            serialPort1.Write("Z");
            button5.BackColor = Color.WhiteSmoke;
            button6.BackColor = Color.WhiteSmoke;
            label4.Text = "0";
            pwm = 0;
        }

        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////////////////
        //------------------> STEP HIZ ARTTIR <-----------------------------
        private void button8_Click(object sender, EventArgs e)
        {
            serialPort1.Write("T");
            button8.BackColor = Color.WhiteSmoke;
            step = step + 1;
            if (step >= 5) step = 5;
            label6.Text = Convert.ToString(step);

        }
        //------------------> STEP HIZ AZALT <-----------------------------
        private void button9_Click(object sender, EventArgs e)
        {
            serialPort1.Write("G");
            button9.BackColor = Color.WhiteSmoke;
            step = step - 1;
            if (step <= 1) step = 1;
            label6.Text = Convert.ToString(step);
        }
        //------------------> STEP SAGA DÖN<-----------------------------
        private void button10_Click(object sender, EventArgs e)
        {
            serialPort1.Write("H");
            button10.BackColor = Color.Red;
            button11.BackColor = Color.WhiteSmoke;
        }
        //------------------> STEP SOLA DÖN <-----------------------------
        private void button11_Click(object sender, EventArgs e)
        {
            serialPort1.Write("F");
            button11.BackColor = Color.Red;
            button10.BackColor = Color.WhiteSmoke;
        }
        //------------------> STEP DUR <-----------------------------
        private void button12_Click(object sender, EventArgs e)
        {
            serialPort1.Write("X");
            button11.BackColor = Color.WhiteSmoke;
            button10.BackColor = Color.WhiteSmoke;
            label6.Text = Convert.ToString(step);
            step = 0;
        }





        //------------ MOUSE DOWN
        // DC PWM ARTTIR
        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            button3.BackColor = Color.Turquoise;
        }
        // DC PWM AZALT
        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            button4.BackColor = Color.Turquoise;
        }
        // STEP HIZ ARTTIR
        private void button8_MouseDown(object sender, MouseEventArgs e)
        {
            button8.BackColor = Color.Turquoise;
        }
        // STEP HIZ AZALT
        private void button9_MouseDown(object sender, MouseEventArgs e)
        {
            button9.BackColor = Color.Turquoise;
        }

    }
}
