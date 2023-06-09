﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
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
        private Graphics graphics;
        private Pen pen;
        private Brush brush;
        private Point startPoint;
        private Point endPoint;
        private bool isMouseDown;
        private bool isFillShape;
        private int eraserSize = 10;
        private int index;
        private bool statusPicture = false;

        private string filePath = "Untitled";
        

        public FPaint()
        {
            InitializeComponent();

            bmp = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            graphics = Graphics.FromImage(bmp);
            pen = new Pen(Color.Black);
            brush = new SolidBrush(Color.White);
            isMouseDown = false;
            isFillShape = false;

            toolStripStatusLabel3.Text = $"Розмір: {pictureBox1.Width}x{pictureBox1.Height} пікселів";

            graphics.Clear(Color.White);
            pictureBox1.Refresh();
          
            pictureBox1.Image = bmp;
            this.Text = Path.GetFileName(filePath) + " - Paint";
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
                    filePath = openFileDialog.FileName;
                    this.Text = Path.GetFileName(filePath) + " - Paint";
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
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(BUndo, "Undo (Ctrl+Z)");
            toolTip.SetToolTip(BRedo, "Redo (Ctrl+Y)");
        }
        private void toolStripRectangle_Click(object sender, EventArgs e)
        {
            toolStripRectangle.Checked = true;
            toolStripLine.Checked = false;
            toolStripEllips.Checked = false;
            toolStripPencil.Checked = false;
            toolStripErase.Checked = false;
            toolStripButtonResize.Checked = false;
            pictureBox1.Cursor = Cursors.Cross;
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
            pictureBox1.Cursor = Cursors.Cross;
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
            pictureBox1.Cursor = Cursors.Cross;
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
         // pictureBox1.Cursor = Cursor.;
        }
        private void pictureBox1_MouseDown_1(object sender, MouseEventArgs e)
        {
            isMouseDown = true;
            statusPicture = true;

         //  pictureBox1.Invalidate();
            startPoint = e.Location;
        }
        private void pictureBox1_MouseMove_1(object sender, MouseEventArgs e)
        {
            toolStripStatusLabel1.Text = $"X: {e.X}, Y: {e.Y}";
            toolStripStatusLabel2.Text = ""; 

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

                if(index == 7)
                {
                    graphics.FillRectangle(brush, 0, 0, pictureBox1.Width, pictureBox1.Height);
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
                
                if (isFillShape)
                {
                    graphics.FillRectangle(brush, rect);
                }
                graphics.DrawRectangle(pen, rect);
            }
            else if (index == 3)
            {
                Rectangle rect = new Rectangle(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), Math.Abs(startPoint.X - endPoint.X), Math.Abs(startPoint.Y - endPoint.Y));
                if (isFillShape)
                {
                    graphics.FillEllipse(brush, rect);
                }
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
                    if (isFillShape)
                    {
                        e.Graphics.FillRectangle(brush, rect);
                    }
                    e.Graphics.DrawRectangle(pen, rect);
                }
                else if (index == 3)
                {
                    Rectangle rect = new Rectangle(Math.Min(startPoint.X, endPoint.X), Math.Min(startPoint.Y, endPoint.Y), Math.Abs(startPoint.X - endPoint.X), Math.Abs(startPoint.Y - endPoint.Y));
                    if (isFillShape)
                    {
                        e.Graphics.FillEllipse(brush, rect);
                    }
                    e.Graphics.DrawEllipse(pen, rect);
                }
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            toolStripMenuItem2.Checked = true;
            toolStripMenuItem3.Checked = false;
            toolStripMenuItem4.Checked = false;
            toolStripMenuItem5.Checked = false;

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
            brush = new SolidBrush(Color.Red);

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Orange;
            brush = new SolidBrush(Color.Orange);        
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            pen.Color= Color.Yellow;
            brush = new SolidBrush(Color.Yellow);
        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            pen.Color = Color.LawnGreen;
            brush = new SolidBrush(Color.LawnGreen);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Blue;
            brush = new SolidBrush(Color.Blue);
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Fuchsia;
            brush = new SolidBrush(Color.Fuchsia);
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            pen.Color = Color.DarkViolet;
            brush = new SolidBrush(Color.DarkViolet);  
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            pen.Color = Color.Black;
            brush = new SolidBrush(Color.Black);
        }

        private void toolStripButtonFill_Click(object sender, EventArgs e)
        {
            toolStripButtonFill.Checked = true;
            index = 7;
        }
        private void toolStripButtonNewPaper_Click(object sender, EventArgs e)
        {
            if (statusPicture)
            {
                DialogResult savePicture = MessageBox.Show("Ви хочете зберегти ваші зміни?", "Примітка", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (savePicture == DialogResult.Yes)
                {
                    toolStripButtonSave_Click(sender, e);
                    graphics.Clear(Color.White);
                    pictureBox1.Refresh();
                }
                else if (savePicture == DialogResult.Cancel)
                {
                    // e.Cancel = true;
                }
                else
                {
                    graphics.Clear(Color.White);
                    pictureBox1.Refresh();
                }
            }
            else
            {
                graphics.Clear(Color.White);
                pictureBox1.Refresh();
            }
        }

        private void ToolStripMenuItemHelp_Click(object sender, EventArgs e)
        {
            FHelp form = new FHelp();
            form.ShowDialog();
        }

        private void ToolStripMenuItemNewPaper_Click(object sender, EventArgs e)
        {
            toolStripButtonNewPaper_Click(sender, e);
        }

        private void ToolStripMenuItemOpenFile_Click(object sender, EventArgs e)
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

        private void ToolStripMenuItemSave_Click(object sender, EventArgs e)
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

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void checkBoxColorFigure_CheckedChanged(object sender, EventArgs e)
        {
            isFillShape = checkBoxColorFigure.Checked;
        }
   
        private void FPaint_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (statusPicture)
            {
                DialogResult savePicture = MessageBox.Show("Ви хочете зберегти ваші зміни?", "Примітка", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

                if (savePicture == DialogResult.Yes)
                {
                    toolStripButtonSave_Click(sender, e);
                    e.Cancel = false;
                }
                else if (savePicture == DialogResult.Cancel)
                {
                    e.Cancel = true;
                }
                else
                {
                    e.Cancel = false;
                }
            }
            else
            {
                Application.Exit();
            }
        }

        private void ToolStripMenuItemPrint_Click(object sender, EventArgs e)
        {
            PrintImage(pictureBox1.Image);
        }

        private void PrintImage(Image image)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.PrintPage += (s, e) =>
            {
                e.Graphics.DrawImage(image, e.MarginBounds);
                e.HasMorePages = false; 
            };
            PrintDialog printDlg = new PrintDialog();
            printDlg.Document = printDoc;
            if (printDlg.ShowDialog() == DialogResult.OK)
            {
                printDoc.Print();
            }
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = $"Розмір: {pictureBox1.Width}x{pictureBox1.Height} пікселів";
        }

        private void BUndo_Click(object sender, EventArgs e)
        {

        }

        private void BRedo_Click(object sender, EventArgs e)
        {

        }

        private void FPaint_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z) // Ctrl+Z
            {
              
            }
            else if (e.Control && e.KeyCode == Keys.Y) // Ctrl+Y
            {
               
            }
        }

        private void FPaint_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Z || e.Control && e.KeyCode == Keys.Y)
            {
                e.Handled = true; 
            }
        }
    }
}
