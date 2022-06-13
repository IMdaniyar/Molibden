using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace diplom
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void ttt()
        {
            chart1.Series[0].Points.Clear();
            chart2.Series[0].Points.Clear();
            chart3.Series[0].Points.Clear();
            chart4.Series[0].Points.Clear();
        }
        int qwe, qwe1, qwe2, qwe3;
        double a1, a2, a3, a4; double h;
        private void button1_Click(object sender, EventArgs e)
        {
            qwe = 1; qwe2 = 1; qwe3 = 1; qwe1 = 1;
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "")
            {
                MessageBox.Show("Барлық өрісті толтырыңыз!");
            }
            else
            {
                ttt();
                double Time, x1, x2, x3;
                double Tp1, Rz1, To1, H;
                //int i;
                double i;
                double s1, s2, s3, s4;

                StreamWriter ouf1 = new StreamWriter("D:\\e\\P_T_prosep.dat");
                StreamWriter ouf2 = new StreamWriter("D:\\e\\P_H_tolpokr.dat");
                StreamWriter ouf3 = new StreamWriter("D:\\e\\P_Rz_werpove.dat");
                StreamWriter ouf4 = new StreamWriter("D:\\e\\P_T_tempotjig.dat");

                //Time=1800.0;
                Time = double.Parse(textBox1.Text);
                a1 = double.Parse(textBox2.Text);
                a2 = double.Parse(textBox3.Text);
                a3 = double.Parse(textBox4.Text);
                a4 = double.Parse(textBox5.Text);
                i = 1;
                while (i <= Time)
                //   for (i = 1; i <= Time; i++)
                {
                    //Zavisimost proshnosti sepleniya k temperature
                    Tp1 = ((double)i * (1500.0 / Time)) / 1500.0;
                    s1 = 0.025582553 + (0.68947296 / (1 + Math.Pow((Tp1 / 0.79000867), -19.39528)));
                    ouf1.WriteLine(i + " " + 1500.0 * Tp1 + " " + 60.0 * s1);
                    if (a1 == 1500.0 * Tp1) { textBox9.Text = (60.0 * s1).ToString(); qwe = 0; }
                    if (1500.0 * Tp1 >= 1100 && 1500.0 * Tp1 <= 1500) chart1.Series[0].Points.AddXY(1500.0 * Tp1, 60.0 * s1);


                    //Zavisimost proshnosti sepleniya k tolshinu pokritiya
                    H = ((double)i * 25.0 / Time) / 25.0;
                    s2 = -5.6415071 + 2.303763 * H - 0.69529111 * Math.Exp(H) + 3.5940978 * Math.Pow(H, 0.5) + 6.2987681 * Math.Exp(-H);
                    ouf2.WriteLine(i + " " + 25.0 * H + " " + 60.0 * s2);
                    if (a2 == 25.0 * H) { textBox7.Text = (60.0 * s2).ToString(); qwe1 = 0; }
                    chart2.Series[0].Points.AddXY(25.0 * H, 60.0 * s2);


                    // Zavisimost proshnosti sepleniya k sherohovatosti poverhnosti
                    if (i > 39)
                    {
                        Rz1 = ((double)i * 6.0 / Time) / 6.0;
                        x1 = Math.Sqrt(Rz1);
                        x2 = 1.0 / Rz1;
                        x3 = 1.0 / (Math.Pow(Rz1, 2));
                        s3 = 0.88413931 - (0.81172141 * x1) - (0.015359171 * x2) + (0.00071298626 * x3);
                        ouf3.WriteLine(i + " " + 6.0 * Rz1 + " " + 60.0 * s3);
                        if (i > 39) chart3.Series[0].Points.AddXY(6.0 * Rz1, 60.0 * s3);

                        if (a3 == 6.0 * Rz1) { textBox8.Text = (60.0 * s3).ToString(); qwe2 = 0; }

                    }
                    //Zavisimost proshnosti sepleniya k temperature otjiga poverhnosti

                    To1 = ((double)i * 2000.0 / Time) / 2000.0;
                    s4 = (0.13607025 - 0.10994617 * To1) / (1 - 2.0288591 * To1 + 1.0656063 * Math.Pow(To1, 2));
                    ouf4.WriteLine(i + " " + 2000.0 * To1 + " " + 52 * s4);
                    if (a4 == 2000.0 * To1) { textBox6.Text = (52.0 * s4).ToString(); qwe3 = 0; }
                    if (2000.0 * To1 >= 1700 && 2000.0 * To1 <= 2000) chart4.Series[0].Points.AddXY(2000.0 * To1, 52 * s4);


                    i = i + 1;


                }

                ouf1.Close(); ouf2.Close(); ouf3.Close(); ouf4.Close();
                Exs();
                ss();

            }
        }

        private void Exs()
        {
            StreamReader q1 = new StreamReader("D:\\e\\1.txt");
            StreamReader q2 = new StreamReader("D:\\e\\2.txt");
            StreamReader q3 = new StreamReader("D:\\e\\3.txt");
            StreamReader q4 = new StreamReader("D:\\e\\4.txt");
            {
                double x, y; string s;
                while ((s = q1.ReadLine()) != null)
                {

                    if (s != "")
                    {
                        x = double.Parse(s);
                        s = Convert.ToString(q1.ReadLine());
                        //  y = double.Parse(s);
                        y = Convert.ToDouble(s);
                        //  MessageBox.Show(y.ToString());
                        chart1.Series[1].Points.AddXY(x, y);

                    }
                    else break;
                }
            }

            {
                double x, y; string s;
                while ((s = q2.ReadLine()) != null)
                {
                    if (s != "")
                    {
                        x = double.Parse(s);
                        s = Convert.ToString(q2.ReadLine());
                        //  y = double.Parse(s);
                        y = Convert.ToDouble(s);
                        //  MessageBox.Show(y.ToString());
                        chart2.Series[1].Points.AddXY(x, y);

                    }
                    else break;
                }
            }
            {
                double x, y; string s;
                while ((s = q3.ReadLine()) != null)
                {
                    if (s != "")
                    {
                        x = double.Parse(s);
                        s = Convert.ToString(q3.ReadLine());
                        //  y = double.Parse(s);
                        y = Convert.ToDouble(s);
                        //  MessageBox.Show(y.ToString());
                        chart3.Series[1].Points.AddXY(x, y);

                    }
                    else break;
                }
            }
            {
                double x, y; string s;
                while ((s = q4.ReadLine()) != null)
                {
                    if (s != "")
                    {
                        x = double.Parse(s);
                        s = Convert.ToString(q4.ReadLine());
                        //  y = double.Parse(s);
                        y = Convert.ToDouble(s);
                        //  MessageBox.Show(y.ToString());
                        chart4.Series[1].Points.AddXY(x, y);

                    }
                    else break;
                }
            }
            q1.Close(); q2.Close(); q3.Close(); q4.Close();
        }

        public void ss()
        {
            if (qwe == 1)
            {
                h = 60 * (0.025582553 + (0.68947296 / (1 + Math.Pow(((a1 / 1500) / 0.79000867), -19.39528))));
                textBox9.Text = Convert.ToString(h);
            }
            if (qwe1 == 1)
            {
                h = 60 * (-5.6415071 + 2.303763 * (a2 / 25) - 0.69529111 * Math.Exp(a2 / 25) + 3.5940978 * Math.Pow(a2 / 25, 0.5) + 6.2987681 * Math.Exp(-a2 / 25));
                textBox7.Text = h.ToString();
            }
            if (qwe2 == 1)
            {
                double h1, h2, h3;
                h1 = Math.Sqrt(a3 / 6);
                h2 = 1.0 * 6.0 / a3;
                h3 = 1.0 / (Math.Pow((a3 / 6), 2));
                h = 60 * (0.88413931 - (0.81172141 * h1) - (0.015359171 * h2) + (0.00071298626 * h3));
                textBox8.Text = h.ToString();
            }
            if (qwe3 == 1)
            {
                h = 52 * (0.13607025 - 0.10994617 * (a4 / 2000)) / (1 - 2.0288591 * (a4 / 2000) + 1.0656063 * Math.Pow((a4 / 2000), 2));
                textBox6.Text = h.ToString();
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void chart4_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void chart3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
