using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageCorrector
{
    public class BitMapProcessor
    {

        public Pixel[,] GetPixels(Bitmap bmp)
        {
            var result = new Pixel[bmp.Height, bmp.Width];
            for (int i = 0; i < bmp.Height; i++)
                for (int j = 0; j < bmp.Width; j++)
                {
                    var c = bmp.GetPixel(j, i);
                    result[i, j] = new Pixel(c.R, c.G, c.B);
                }
            return result;
        }

        public Pixel[,] GetPixels(string filename)
        {
            using (FileStream fstream = File.OpenRead(filename))
            {
                var bmp = (Bitmap)Bitmap.FromStream(fstream);
                return GetPixels(bmp);
            }
        }

        public Bitmap GetBitmap(Pixel[,] pixelMap)
        {
            Bitmap bmp = new Bitmap(pixelMap.GetUpperBound(1) + 1, pixelMap.GetUpperBound(0) + 1);
            for (int i = 0; i < pixelMap.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < pixelMap.GetUpperBound(1) + 1; j++)
                {
                    bmp.SetPixel(j, i, Color.FromArgb(pixelMap[i, j].R, pixelMap[i, j].G, pixelMap[i, j].B));
                }
            return bmp;
        }

        public void Save(Bitmap bmp, string filename)
        {
            bmp.Save("c:/1/1.bmp");
        }

        //Автоконтраст
        public Pixel[,] AutoLevels(Pixel[,] pixelMap)
        {
            var max = GetMaxLevel(pixelMap);
            var min = GetMinLevel(pixelMap);
            for (int i = 0; i < pixelMap.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < pixelMap.GetUpperBound(1) + 1; j++)
                {
                    pixelMap[i, j].R = (byte)((pixelMap[i, j].R - min.R) * (255 / (max.R - min.R)));
                    pixelMap[i, j].G = (byte)((pixelMap[i, j].G - min.G) * (255 / (max.G - min.G)));
                    pixelMap[i, j].B = (byte)((pixelMap[i, j].B - min.B) * (255 / (max.B - min.B)));
                }
            return pixelMap;
        }
        public Pixel GetMaxLevel(Pixel[,] pixelMap)
        {
            byte R = 0, G = 0, B = 0;
            for (int i = 0; i < pixelMap.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < pixelMap.GetUpperBound(1) + 1; j++)
                {
                    if (pixelMap[i, j].R > R)
                        R = pixelMap[i, j].R;
                    if (pixelMap[i, j].G > G)
                        G = pixelMap[i, j].G;
                    if (pixelMap[i, j].B > B)
                        B = pixelMap[i, j].B;
                }
            return new Pixel(R, G, B);
        }
        public Pixel GetMinLevel(Pixel[,] pixelMap)
        {
            byte R = 255, G = 255, B = 255;
            for (int i = 0; i < pixelMap.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < pixelMap.GetUpperBound(1) + 1; j++)
                {
                    if (pixelMap[i, j].R < R)
                        R = pixelMap[i, j].R;
                    if (pixelMap[i, j].G < G)
                        G = pixelMap[i, j].G;
                    if (pixelMap[i, j].B < B)
                        B = pixelMap[i, j].B;
                }
            return new Pixel(R, G, B);
        }
        public Pixel GetAverageLevel(Pixel[,] pixelMap, out byte av)
        {
            long R = 0, G = 0, B = 0;
            for (int i = 0; i < pixelMap.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < pixelMap.GetUpperBound(1) + 1; j++)
                {
                    R += pixelMap[i, j].R;
                    G += pixelMap[i, j].G;
                    B += pixelMap[i, j].B;
                }
            R = R / pixelMap.Length;
            G = G / pixelMap.Length;
            B = B / pixelMap.Length;
            av = (byte)((R + G + B) / 3);
            return new Pixel((byte)R, (byte)G, (byte)B);
        }


        public Pixel[,] GreyWorld(Pixel[,] pixelMap)
        {
            byte av = 0;
            var AvLevel = GetAverageLevel(pixelMap, out av);
            for (int i = 0; i < pixelMap.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < pixelMap.GetUpperBound(1) + 1; j++)
                {
                    pixelMap[i, j].R = (byte)(pixelMap[i, j].R * av / AvLevel.R);
                    pixelMap[i, j].G = (byte)(pixelMap[i, j].G * av / AvLevel.G);
                    pixelMap[i, j].B = (byte)(pixelMap[i, j].B * av / AvLevel.B);
                }
            return pixelMap;
        }

        public Pixel[,] Negative(Pixel[,] pixelMap)
        {
            for (int i = 0; i < pixelMap.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < pixelMap.GetUpperBound(1) + 1; j++)
                {
                    pixelMap[i, j].R = (byte)(255 - pixelMap[i, j].R);
                    pixelMap[i, j].G = (byte)(255 - pixelMap[i, j].G);
                    pixelMap[i, j].B = (byte)(255 - pixelMap[i, j].B);
                }
            return pixelMap;
        }

        public Pixel[,] GammaCorrection(Pixel[,] pixelMap, float gamma)
        {
            for (int i = 0; i < pixelMap.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < pixelMap.GetUpperBound(1) + 1; j++)
                {
                    pixelMap[i, j].R = (byte)(Math.Pow((float)pixelMap[i, j].R / 255, gamma) * 255);
                    pixelMap[i, j].G = (byte)(Math.Pow((float)pixelMap[i, j].G / 255, gamma) * 255);
                    pixelMap[i, j].B = (byte)(Math.Pow((float)pixelMap[i, j].B / 255, gamma) * 255);
                }
            return pixelMap;
        }

        public Pixel[,] IdealFilter(Pixel[,] pixelMap)
        {
            var max = GetMaxLevel(pixelMap);
            for (int i = 0; i < pixelMap.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < pixelMap.GetUpperBound(1) + 1; j++)
                {
                    pixelMap[i, j].R = (byte)(pixelMap[i, j].R * 255 / max.R);
                    pixelMap[i, j].G = (byte)(pixelMap[i, j].G * 255 / max.G);
                    pixelMap[i, j].B = (byte)(pixelMap[i, j].B * 255 / max.B);
                }
            return pixelMap;
        }

        public Pixel[,] BoxFilter(Pixel[,] pixelMap, float[,] mask)
        {
            float denom = 0;
            float R = 0, G = 0, B = 0;
            var maskY = mask.GetUpperBound(0) + 1;
            var maskX = mask.GetUpperBound(1) + 1;
            for (int i = 0; i < maskY; i++)
                for (int j = 0; j < maskX; j++)
                {
                    denom += mask[i, j];
                }
            if (denom == 0) denom = 1;
            for (int i = maskY / 2; i < pixelMap.GetUpperBound(0) + 1 - maskY / 2; i++)
                for (int j = maskX / 2; j < pixelMap.GetUpperBound(1) + 1 - maskX / 2; j++)
                {
                    R = 0; G = 0; B = 0;
                    for (int l = 0; l < maskY; l++)
                        for (int m = 0; m < maskX; m++)
                        {
                            R += pixelMap[i + l - maskY / 2, j + m - maskX / 2].R * mask[l, m];
                            G += pixelMap[i + l - maskY / 2, j + m - maskX / 2].G * mask[l, m];
                            B += pixelMap[i + l - maskY / 2, j + m - maskX / 2].B * mask[l, m];
                        }
                    R = R / denom; G = G / denom; B = B / denom;
                    if (R > 255) R = 255; if (G > 255) G = 255; if (B > 255) B = 255;
                    if (R < 0) R = 0; if (G < 0) G = 0; if (B < 0) B = 0;
                    pixelMap[i, j] = new Pixel((byte)R, (byte)G, (byte)B);
                }
            return pixelMap;
        }

        public Pixel[,] Spreading(Pixel[,] pixelMap)
        {
            return BoxFilter(pixelMap, new float[5, 5] {
                { 0.000789f, 0.006581f, 0.013347f, 0.006581f, 0.000789f },
                { 0.006581f, 0.054901f, 0.111345f, 0.054901f, 0.006581f},
                {0.013347f, 0.111345f, 0.225821f, 0.111345f, 0.013347f},
                { 0.006581f, 0.054901f, 0.111345f, 0.054901f, 0.006581f},
                { 0.000789f, 0.006581f, 0.013347f, 0.006581f, 0.000789f }
            });
        }

        public Pixel[,] Harshness(Pixel[,] pixelMap)
        {
            return BoxFilter(pixelMap, new float[3, 3] { { 0, -1, 0 }, { -1, 5, -1 }, { 0, -1, 0 } });
        }

        public Pixel[,] Embossing(Pixel[,] pixelMap)
        {
            return BoxFilter(pixelMap, new float[3, 3] { { 0, 1, 0 }, { -1, 0, 1 }, { 0, -1, 0 } });
        }

        public Pixel[,] Watercolor(Pixel[,] pixelMap)
        {
            return BoxFilter(pixelMap, new float[3, 3] { { -1, -1, -1 }, { -1, 9, -1 }, { -1, -1, -1 } });
        }
    }


}
