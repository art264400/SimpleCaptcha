using Microsoft.AspNetCore.Http;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace SimpleCaptcha
{
    public class SimpleCaptcha
    {
        private int Height { get; set; }

        private int Width { get; set; }

        private int CaptchaLenght { get; set; }
       
        private string Locale { get; set; }

        public SimpleCaptcha()
        {
            Height = 35;
            Width = 120;
            CaptchaLenght = 6;
            Locale = "en";
        }
        public SimpleCaptcha(int Height, int Width, string Locale,int CaptchaLenght)
        {
            this.Height = Height;
            this.Width = Width;
            this.Locale = Locale;
            this.CaptchaLenght = CaptchaLenght;
        }
        public string Generate(Stream stream)
        {
            string captcha = GenerateRandomString();

            using (Bitmap bmp = new Bitmap(Width, Height))
            {
                Random rnd = new Random();
                RectangleF rectf = new RectangleF(rnd.Next(10, 20), rnd.Next(4, 10), 0, 0);

                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.Clear(Color.White);
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                    Bezier(g, rnd);
                    Lines(g, rnd);
                    g.DrawString(captcha, new Font("Times New Roman", 18, FontStyle.Regular), Brushes.Black, rectf);
                    Noise(bmp, rnd);
                    g.Flush();

                    bmp.Save(stream, ImageFormat.Jpeg);
                }
            }
            return captcha;
        }
        private string GenerateRandomString()
        {
            Random rnd = new Random();
            var combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            if (Locale.ToLower() == "ru")
                combination = "0123456789АаБбВвГгДдЕеЖжЗзИиКкЛлМмНнОоПпРрСсТтУуФфХхЦцЧчШшЭэЮюЯя";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < CaptchaLenght; i++)
                sb.Append(combination[rnd.Next(combination.Length)]);

            return sb.ToString();
        }
        private Color RandomColor()
        {
            int r, g, b;
            byte[] bytes1 = new byte[3];
            Random rnd1 = new Random();
            rnd1.NextBytes(bytes1);
            r = Convert.ToInt16(bytes1[0]);
            g = Convert.ToInt16(bytes1[1]);
            b = Convert.ToInt16(bytes1[2]);
            return Color.FromArgb(r, g, b);
        }

        private void Bezier(Graphics g, Random rnd)
        {
            Point start = new Point(rnd.Next(10, 55), rnd.Next(1, 45));
            Point control1 = new Point(20, 20);
            Point control2 = new Point(25, 80);
            Point finish = new Point(rnd.Next(70, 130), 8);
            g.DrawBezier(new Pen(Color.Black, 2), start, control1, control2, finish);

        }
        private void Lines(Graphics g, Random rnd)
        {
            var pen = new Pen(Color.Black, 1.7F);
            var flag = rnd.Next(0, 1);
            if (flag == 0)
            {
                var width1 = 15 + rnd.Next(-20, 20);
                var height1 = 0;
                var width2 = 30 + rnd.Next(-20, 20);
                var height2 = 45;
                g.DrawLine(pen, width1, height1, width2, height2);
                g.DrawLine(pen, width1 + 30, height1, width2 + 30, height2);
                g.DrawLine(pen, width1 + 60, height1, width2 + 60, height2);
                g.DrawLine(pen, width1 + 90, height1, width2 + 90, height2);
            }

        }

        private void Noise(Bitmap bmp, Random rnd)
        {
            for (int x = 0; x < bmp.Width; x++)
                for (int y = 0; y < bmp.Height; y++)
                {
                    if (rnd.Next(100) <= 22) bmp.SetPixel(x, y, Color.Gray);
                }
        }
    }
}
