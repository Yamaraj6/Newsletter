using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace Lab5
{
    internal sealed class RandomImage
    {
        #region Private Definitions  
        private readonly int width;
        private readonly int height;
        private readonly Random random = new Random();
        private string text;
        private Bitmap image;
        #endregion

        public RandomImage(string text, int width, int height)
        {
            this.text = text; this.width = width;
            this.height = height; GenerateImage();
        }
        public Bitmap Image { get { return image; } }
        private void GenerateImage()
        {
            //stworzenie bitmapy o zadanym rozmiarze     
            var bitmap = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            var graphics = Graphics.FromImage(bitmap);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            var rect = new Rectangle(0, 0, width, height);
            var hatchBrush = new HatchBrush(HatchStyle.SmallConfetti,
                Color.LightGray, Color.White);
            graphics.FillRectangle(hatchBrush, rect);
            SizeF size;
            float fontSize = rect.Height + 1;
            Font font;
            do
            {
                fontSize--;
                font = new Font(FontFamily.GenericSansSerif,fontSize, FontStyle.Bold);
                size = graphics.MeasureString(text, font);
            } while (size.Width > rect.Width);
            var format = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            var path = new GraphicsPath();
            path.AddString(text, font.FontFamily, (int)font.Style, font.Size, rect, format);
            PointF[] points = {
                new PointF(random.Next(rect.Width)/4f, random.Next(
                    rect.Height)/4f),
                new PointF(rect.Width - random.Next(rect.Width)/4f,
                random.Next(rect.Height)/4f),
                new PointF(random.Next(rect.Width)/4f,
                rect.Height - random.Next(rect.Height)/4f),
                new PointF(rect.Width - random.Next(rect.Width)/4f,
                rect.Height - random.Next(rect.Height)/4f)
            }; var matrix = new Matrix();
            matrix.Translate(0F, 0F);
            path.Warp(points, rect, matrix, WarpMode.Perspective, 0F);
            hatchBrush = new HatchBrush(HatchStyle.Percent10, Color.Black, Color.SkyBlue);
            graphics.FillPath(hatchBrush, path);
            var max = Math.Max(rect.Width, rect.Height);
            for (var i = 0; i < (int)(rect.Width * rect.Height / 30F); i++)
            {
                var x = random.Next(rect.Width);
                var y = random.Next(rect.Height);
                var w = random.Next(max / 50);
                var h = random.Next(max / 50);
                graphics.FillEllipse(hatchBrush, x, y, w, h);
            }
            font.Dispose();
            hatchBrush.Dispose();
            graphics.Dispose();
            image = bitmap;
        }
    }
}