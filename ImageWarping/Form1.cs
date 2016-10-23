using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageWarping
{
    public partial class ImageWarping : Form
    {
        private Bitmap bitmap;
        private Bitmap bitmapBuffer;
        private List<Point> startPoints;
        private List<Point> endPoints;
        private Point tmpStartPoint;
        private Point tmpEndPoint;
        private bool isDrawing;
        private bool isLoad;
        public ImageWarping()
        {
            InitializeComponent();
            startPoints = new List<Point>();
            endPoints = new List<Point>();
            isDrawing = false;
            isLoad = false;
            ImagePictureBox.MouseDown += new MouseEventHandler(ImagePictureBox_MouseDown);
            ImagePictureBox.MouseUp += new MouseEventHandler(ImagePictureBox_MouseUp);
            ImagePictureBox.MouseMove += new MouseEventHandler(ImagePictureBox_MouseMove);
        }

        private void ImagePictureBox_MouseDown(Object sender, MouseEventArgs e)
        {
            if (isLoad)
            {
                isDrawing = true;
                tmpStartPoint = new Point(e.X, e.Y);
            }
        }

        private void ImagePictureBox_MouseUp(Object sender, MouseEventArgs e)
        {
            if (isDrawing && isLoad)
            {
                isDrawing = false;
                tmpEndPoint = new Point(e.X, e.Y);
                startPoints.Add(tmpStartPoint);
                endPoints.Add(tmpEndPoint);
                Graphics.FromImage(bitmapBuffer).DrawImage(ImagePictureBox.Image, new Point(0, 0));
            }
        }

        private void ImagePictureBox_MouseMove(Object sender, MouseEventArgs e)
        {
            if (isDrawing && isLoad)
            {
                tmpEndPoint = new Point(e.X, e.Y);
                Bitmap buffer = new Bitmap(bitmapBuffer.Width, bitmapBuffer.Height);
                Graphics.FromImage(buffer).DrawImage(bitmapBuffer, new Point(0, 0));
                Graphics.FromImage(buffer).DrawLine(new Pen(Color.Red, 3), tmpStartPoint, tmpEndPoint);
                ImagePictureBox.Image = buffer;
            }
        }

        private void LoadImageButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Multiselect = false;
            open.Title = "请选择图片";
            open.Filter = "bmp files (*.bmp)|*.bmp";
            if (open.ShowDialog() == DialogResult.OK)
            {
                bitmap = new Bitmap(open.FileName);
                ImagePictureBox.Image = bitmap;
                bitmapBuffer = new Bitmap(bitmap.Width, bitmap.Height);
                Graphics.FromImage(bitmapBuffer).DrawImage(bitmap, new Point(0, 0));
                isLoad = true;
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (isLoad)
            {
                startPoints.Clear();
                endPoints.Clear();
                ImagePictureBox.Image = bitmap;
                bitmapBuffer = new Bitmap(bitmap.Width, bitmap.Height);
                Graphics.FromImage(bitmapBuffer).DrawImage(bitmap, new Point(0, 0));
            }       
        }

        private void IDWButton_Click(object sender, EventArgs e)
        {
            IDWButton.Enabled = false;
            if (startPoints.Count == endPoints.Count && startPoints.Count > 0 && endPoints.Count > 0)
            {
                IDW idw = new IDW(startPoints, endPoints);
                Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                unsafe
                {
                    byte* ptr = (byte*)bitmapData.Scan0;
                    for (int i = 1; i <= bitmap.Height; i++)
                    {
                        for (int j = 1; j <= bitmap.Width; j++)
                        {
                            
                            int r, g, b;
                            b = Convert.ToInt32(*ptr);
                            ptr++;
                            g = Convert.ToInt32(*ptr);
                            ptr++;
                            r = Convert.ToInt32(*ptr);
                            ptr++;
                            Color color = Color.FromArgb(r, g, b);
                            Point point = idw.F(new Point(j, i)); 
                            int x = point.X < 0 ? 0 : (point.X >= newBitmap.Width ? (newBitmap.Width - 1) : point.X); 
                            int y = point.Y < 0 ? 0 : (point.Y >= newBitmap.Height ? (newBitmap.Height - 1) : point.Y); 
                            newBitmap.SetPixel(x, y, color); 
                        }
                        ptr += bitmapData.Stride - bitmap.Width * 3;
                    }
                }
                bitmap.UnlockBits(bitmapData);
                bitmap = newBitmap;
                ImagePictureBox.Image = bitmap;      
                Bitmap newBitmapBuffer = new Bitmap(newBitmap.Width, newBitmap.Height);
                Graphics.FromImage(newBitmapBuffer).DrawImage(bitmap, new Point(0, 0));
                bitmapBuffer = newBitmapBuffer;
                startPoints.Clear();
                endPoints.Clear();              
            }
            else
            {
                MessageBox.Show("请选择控制点对!", "提示");
            }
            IDWButton.Enabled = true;
        }

        private void RBFButton_Click(object sender, EventArgs e)
        {
            RBFButton.Enabled = false;
            if (startPoints.Count == endPoints.Count && startPoints.Count > 1 && endPoints.Count > 1)
            {
                RBF rbf = new RBF(startPoints, endPoints);
                Bitmap newBitmap = new Bitmap(bitmap.Width, bitmap.Height);
                BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                unsafe
                {
                    byte* ptr = (byte*)bitmapData.Scan0;
                    for (int i = 1; i <= bitmap.Height; i++)
                    {
                        for (int j = 1; j <= bitmap.Width; j++)
                        {

                            int r, g, b;
                            b = Convert.ToInt32(*ptr);
                            ptr++;
                            g = Convert.ToInt32(*ptr);
                            ptr++;
                            r = Convert.ToInt32(*ptr);
                            ptr++;
                            Color color = Color.FromArgb(r, g, b);
                            Point point = rbf.F(new Point(j, i));
                            int x = point.X < 0 ? 0 : (point.X >= newBitmap.Width ? (newBitmap.Width - 1) : point.X);
                            int y = point.Y < 0 ? 0 : (point.Y >= newBitmap.Height ? (newBitmap.Height - 1) : point.Y);
                            newBitmap.SetPixel(x, y, color);
                        }
                        ptr += bitmapData.Stride - bitmap.Width * 3;
                    }
                }
                bitmap.UnlockBits(bitmapData);
                bitmap = newBitmap;
                ImagePictureBox.Image = bitmap;
                Bitmap newBitmapBuffer = new Bitmap(newBitmap.Width, newBitmap.Height);
                Graphics.FromImage(newBitmapBuffer).DrawImage(bitmap, new Point(0, 0));
                bitmapBuffer = newBitmapBuffer;
                startPoints.Clear();
                endPoints.Clear();
            }
            else
            {
                MessageBox.Show("请选择足够的控制点对!", "提示");
            }
            RBFButton.Enabled = true;
        }
    }
}
