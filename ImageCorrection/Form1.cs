using ImageCorrector;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageCorrection
{
    public partial class Form1 : Form
    {
        public string filename;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            filename = openFileDialog1.FileName;
            // читаем файл в строку
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(filename);
            a = b.AutoLevels(a);
            var c = b.GetBitmap(a);
            b.Save(c,"c:/1/1.bmp");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(filename);
            a = b.GreyWorld(a);
            var c = b.GetBitmap(a);
            b.Save(c, "c:/1/2.bmp");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(filename);
            a = b.GammaCorrection(a, (float)trackBar1.Value / 10);
            var c = b.GetBitmap(a);
            b.Save(c, "c:/1/3.bmp");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(filename);
            a = b.IdealFilter(a);
            var c = b.GetBitmap(a);
            b.Save(c, "c:/1/4.bmp");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(filename);
            a = b.Spreading(a);
            var c = b.GetBitmap(a);
            b.Save(c, "c:/1/5.bmp");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(filename);
            a = b.Harshness(a);
            var c = b.GetBitmap(a);
            b.Save(c, "c:/1/6.bmp");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(filename);
            a = b.Embossing(a);
            var c = b.GetBitmap(a);
            b.Save(c, "c:/1/7.bmp");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(filename);
            a = b.Watercolor(a);
            var c = b.GetBitmap(a);
            b.Save(c, "c:/1/8.bmp");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(filename);
            a = b.Negative(a);
            var c = b.GetBitmap(a);
            b.Save(c, "c:/1/8.bmp");
        }
    }
}
