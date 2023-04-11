using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_Paint
{
    public partial class FPaint : Form
    {
        private Bitmap bmp;
        private Bitmap bmp2;
        private Bitmap bmp3;
        private Graphics graphics;
        private Pen pen;
        private Brush brush;
        private Point startPoint;
        private Point endPoint;
        private bool isMouseDown;
        private bool isFillShape;
        private int eraserSize = 10;
        private int index;

        private bool toolStripButtonResize_2 = false;

        public FPaint()
        {
            InitializeComponent();

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bmp);
            pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.White);
            isMouseDown = false;
            isFillShape = false;


            pictureBox1.Image = bmp;
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    bmp = new Bitmap(openFileDialog.FileName);
                    graphics = Graphics.FromImage(bmp);
                    pictureBox1.Image = bmp;
                }
                catch
                {
                    MessageBox.Show("Неможливо відкрити обраний файл");
                }
            }
            pictureBox1.Invalidate();
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JPEG Image|*.jpg|PNG Image|*.png|Bitmap Image|*.bmp";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog.FileName;
                ImageFormat format = ImageFormat.Jpeg;
                if (fileName.EndsWith(".png"))
                {
                    format = ImageFormat.Png;
                }
                else if (fileName.EndsWith(".bmp"))
                {
                    format = ImageFormat.Bmp;
                }
                pictureBox1.Image.Save(fileName, format);
            }
           
        }


        private void toolStripColors_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                brush = new SolidBrush(colorDialog.Color);
            }
        }
   
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStripRectangle_Click(object sender, EventArgs e)
        {
            toolStripRectangle.Checked = true;
            toolStripLine.Checked = false;
            toolStripEllips.Checked = false;
            toolStripPencil.Checked = false;
            toolStripErase.Checked = false;
            toolStripButtonResize.Checked = false;
            index = 2;
        }
        private void toolStripLine_Click_1(object sender, EventArgs e)
        {
            toolStripLine.Checked = true;
            toolStripEllips.Checked = false;
            toolStripRectangle.Checked = false;
            toolStripLine.Checked = false;
            toolStripPencil.Checked = false;
            toolStripErase.Checked = false;
            toolStripButtonResize.Checked = false;
            index = 1;
        }
        private void toolStripEllips_Click(object sender, EventArgs e)
        {
            toolStripEllips.Checked = true;
            toolStripRectangle.Checked = false;
            toolStripLine.Checked = false;
            toolStripPencil.Checked = false;
            toolStripErase.Checked = false;
            toolStripButtonResize.Checked = false;
            index = 3;
        }

        private void toolStripErase_Click(object sender, EventArgs e)
        {
            toolStripEllips.Checked = false;
            toolStripRectangle.Checked = false;
            toolStripLine.Checked = false;
            toolStripPencil.Checked = false;
            toolStripErase.Checked = true;
            toolStripButtonResize.Checked = false;
            index = 5;
        }

        private void toolStripPencil_Click(object sender, EventArgs e)
        {
            toolStripRectangle.Checked = false;
            toolStripLine.Checked = false;
            toolStripEllips.Checked = false;
            toolStripErase.Checked = false;
            toolStripPencil.Checked = true;
            toolStripButtonResize.Checked = false;
            index = 4;
        }
        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            isMouseDown = true;

            if (index == 4)
            {
                graphics.DrawLine(pen, startPoint, startPoint);
            }
            pictureBox1.Invalidate();
            startPoint = e.Location;
        }

        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (isMouseDown)
            {
                endPoint = e.Location;

                if (index == 4)
                {
                    graphics.DrawLine(pen, startPoint, endPoint);
                    startPoint = endPoint;

                }

                if (index == 5)
                {
                    Rectangle eraserRect = new Rectangle(e.Location.X - eraserSize / 2, e.Location.Y - eraserSize / 2, eraserSize, eraserSize);
                    graphics.FillRectangle(Brushes.White, eraserRect);
                }
                if (index == 6)
                {
                    int newWidth = e.Location.X - pictureBox1.Location.X;
                    int newHeight = e.Location.Y - pictureBox1.Location.Y;
                    if (newWidth > 0 && newHeight > 0)
                    {
                        pictureBox1.Width = newWidth;
                        pictureBox1.Height = newHeight;
                    }
                }

                pictureBox1.Invalidate();
            }
        }
        private void pictureBox1_MouseUp_1(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
            endPoint = e.Location;

            if (index == 1)
            {
                graphics.DrawLine(pen, startPoint, endPoint);
            }
            else if (index == 2)
            {
                Rectangle rect = new Rectangle(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), Math.Abs(startPoint.X - endPoint.X), Math.Abs(startPoint.Y - endPoint.Y));
                graphics.DrawRectangle(pen, rect);
            }
            else if (index == 3)
            {
                Rectangle rect = new Rectangle(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), Math.Abs(startPoint.X - endPoint.X), Math.Abs(startPoint.Y - endPoint.Y));

                graphics.DrawEllipse(pen, rect);
            }
            pictureBox1.Invalidate();
        }

        private void pictureBox1_Paint_1(object sender, PaintEventArgs e)
        {
            if (isMouseDown)
            {
                if (index == 1)
                {
                    e.Graphics.DrawLine(pen, startPoint, endPoint);
                }
                else if (index == 2)
                {
                    Rectangle rect = new Rectangle(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), Math.Abs(startPoint.X - endPoint.X), Math.Abs(startPoint.Y - endPoint.Y));

                    e.Graphics.DrawRectangle(pen, rect);
                }
                else if (index == 3)
                {
                    Rectangle rect = new Rectangle(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), Math.Abs(startPoint.X - endPoint.X), Math.Abs(startPoint.Y - endPoint.Y));

                    e.Graphics.DrawEllipse(pen, rect);
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = true;

            pen.Width = 1;
            eraserSize = 1;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = true;

            pen.Width = 3;
            eraserSize = 3;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = true;

            pen.Width = 5;
            eraserSize = 5;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = true;

            pen.Width = 8;
            eraserSize=8;
        }

        private void toolStripButtonResize_Click(object sender, EventArgs e)
        {
               
                toolStripButtonResize.Checked = true;
                index = 6;
                pictureBox1.Cursor = Cursors.SizeNWSE;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Red;
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Orange;
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            pen.Color= Color.Yellow;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            pen.Color = Color.LawnGreen;
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Blue;
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Fuchsia;
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            pen.Color = Color.DarkViolet;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Black;
        }

        private void toolStripButtonFill_Click(object sender, EventArgs e)
        {
            index = 7;
        }
    }
}
