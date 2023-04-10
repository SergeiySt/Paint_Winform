using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_Paint
{
    public partial class FPaint : Form
    {
        private Bitmap bitmap;
        private Graphics graphics;

        int PenSize = 2;
        int PenSizeErase = 10;

        private Point previousPoint;
        private Pen pen = new Pen(Color.Black, 2);
        private bool isDrawing = false;

        

        //  private Panel canvasPanel = new Panel();
        public FPaint()
        {
            InitializeComponent();

            //this.DoubleBuffered = true;
            //this.ResizeRedraw = true;
 
            this.bitmap = new Bitmap(this.pictureBox1.Width, this.pictureBox1.Height);
            this.graphics = Graphics.FromImage(this.bitmap);
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    pictureBox1.Image = new Bitmap(openFileDialog.FileName);
                }
                catch
                {
                    MessageBox.Show("Неможливо відкрити обраний файл");
                }
            }
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PNG Image|*.png|JPEG Image|*.jpg|Bitmap Image|*.bmp";

            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != "")
            {
                using (System.IO.FileStream fs = (System.IO.FileStream)saveFileDialog.OpenFile())
                {
                    if (pictureBox1.Image != null)
                    {
                        switch (saveFileDialog.FilterIndex)
                        {
                            case 0:
                                pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                            case 1:
                                pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            case 2:
                                pictureBox1.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                        }
                    }
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
       

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
          
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
           
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
        
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void toolStripColors_Click(object sender, EventArgs e)
        {
          
        }
   
        private void toolStripLine_Click(object sender, EventArgs e)
        {
           

        }

        private void toolStrip1_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
          
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStripRectangle_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripEllips_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStripErase_Click(object sender, EventArgs e)
        {
            toolStripErase.Checked = true;
            toolStripPencil.Checked = false;
            pen = new Pen(Color.White, PenSizeErase);
        }

        private void toolStripPencil_Click(object sender, EventArgs e)
        {
            toolStripErase.Checked = false;
            toolStripPencil.Checked = true;
            pen = new Pen(Color.Black, PenSize);
        }
        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            isDrawing = true;
            previousPoint = new Point(e.X, e.Y);
        }

        private void pictureBox1_MouseUp_1(object sender, MouseEventArgs e)
        {
            isDrawing = false;
        }

        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            if (isDrawing)
            {
                //Graphics g = this.pictureBox1.CreateGraphics();
                //g.DrawLine(pen, previousPoint, new Point(e.X, e.Y));
                //previousPoint = new Point(e.X, e.Y);
                this.graphics.DrawLine(pen, previousPoint, new Point(e.X, e.Y));
                this.pictureBox1.CreateGraphics().DrawImageUnscaled(bitmap, Point.Empty);
                previousPoint = new Point(e.X, e.Y);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = true;

            pen.Width = PenSize = 1;
            pen.Width = PenSizeErase = 1;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = true;

            pen.Width = PenSize = 3;
            pen.Width = PenSizeErase = 3;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = true;

            pen.Width = PenSize = 5;
            pen.Width = PenSizeErase = 5;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = false;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;

            pen.Width = PenSize = 8;
            pen.Width = PenSizeErase = 8;
        }
    }
}
