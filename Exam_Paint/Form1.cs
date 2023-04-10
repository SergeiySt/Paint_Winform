using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exam_Paint
{
    public partial class Form1 : Form
    {
        Bitmap picture_1;
        //Bitmap picture_2;


        // string mode;
        private Pen pen;

        private bool isDrawing = false;
        private Point lastPoint;

        private bool isResizing = false;
        private Point lastLocation;

        public Form1()
        {
           
            InitializeComponent();

            pictureBox1.Image = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            picture_1 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            //picture_2 = new Bitmap(pictureBox1.Width, pictureBox1.Height);
        }

        private void toolStripButtonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.ShowDialog();
            if (openFileDialog.FileName != "")
            {
                picture_1 = (Bitmap)Image.FromFile(openFileDialog.FileName);
                pictureBox1.Image = picture_1;
            }
        }

        private void toolStripButtonSave_Click(object sender, EventArgs e)
        {
           
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
       

        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = true;
                lastPoint = e.Location;
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDrawing && e.Button == MouseButtons.Left)
            {
                using (Graphics g = Graphics.FromImage(pictureBox1.Image))
                {
                    g.DrawLine(Pens.Black, lastPoint, e.Location);
                }
                pictureBox1.Invalidate();
                lastPoint = e.Location;
            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDrawing = false;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }

       

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            // pen.Color = pictureBox1.BackColor;
            //ToolStripButton toolStripButton = (ToolStripButton)sender;
            //toolStripBtnBlack.BackColor = toolStripBtnEraser.BackColor;
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            using (Graphics g = Graphics.FromImage(pictureBox1.Image))
            {
                Pen pen = new Pen(Color.Black);
                int x = 50; // координата x верхнего левого угла квадрата
                int y = 50; // координата y верхнего левого угла квадрата
                int size = 100; // размер квадрата
                g.DrawRectangle(pen, x, y, size, size);
            }
            pictureBox1.Invalidate();
        }
    }
}
