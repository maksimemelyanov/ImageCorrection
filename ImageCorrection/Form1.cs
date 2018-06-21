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
        public Bitmap bmp;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            filename = openFileDialog1.FileName;
            bmp = (Bitmap)Bitmap.FromFile(filename);
            pictureBox1.Image = bmp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(bmp);
            a = b.AutoLevels(a);
            bmp = b.GetBitmap(a);
            pictureBox1.Image = bmp;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(bmp);
            a = b.GreyWorld(a);
            bmp = b.GetBitmap(a);
            pictureBox1.Image = bmp;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(bmp);
            a = b.GammaCorrection(a, (float)trackBar1.Value / 10);
            bmp = b.GetBitmap(a);
            pictureBox1.Image = bmp;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(bmp);
            a = b.IdealFilter(a);
            bmp = b.GetBitmap(a);
            pictureBox1.Image = bmp;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(bmp);
            a = b.Spreading(a);
            bmp = b.GetBitmap(a);
            pictureBox1.Image = bmp;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(bmp);
            a = b.Harshness(a);
            bmp = b.GetBitmap(a);
            pictureBox1.Image = bmp;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(bmp);
            a = b.Embossing(a);
            bmp = b.GetBitmap(a);
            pictureBox1.Image = bmp;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(bmp);
            a = b.Watercolor(a);
            bmp = b.GetBitmap(a);
            pictureBox1.Image = bmp;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(bmp);
            a = b.Negative(a);
            bmp = b.GetBitmap(a);
            pictureBox1.Image = bmp;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(bmp);
            a = b.Grey(a);
            bmp = b.GetBitmap(a);
            pictureBox1.Image = bmp;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(bmp);
            a = b.Sepia(a, trackBar2.Value);
            bmp = b.GetBitmap(a);
            pictureBox1.Image = bmp;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            BitMapProcessor b = new BitMapProcessor();
            var a = b.GetPixels(bmp);
            a = b.Noise(a, trackBar3.Value);
            bmp = b.GetBitmap(a);
            pictureBox1.Image = bmp;
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (bmp == null)
                return;
            saveFileDialog1.Filter = "Изображение|*.bmp";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            var savename = saveFileDialog1.FileName;
            bmp.Save(savename);
        }
    }
}
