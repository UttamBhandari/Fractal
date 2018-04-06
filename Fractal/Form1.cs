using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.IO;

namespace Fractal
{
    public partial class Form1 : Form
    {
        private int MAX = 256; // max iterations
        private double SX = -2.025;  // start value real
        private double SY = -1.125; // start value imaginary
        private double EX = 0.6;  // end value real
        private double EY = 1.125;  // end value imaginary
        private static int x1 ,y1,xs,ys,xe,ye;
        private static double xstart, ystart, xende, yende, xzoom, yzoom;
        private static bool action, rectangle, finished;
        private static float xy;

        private Image image;
        private Graphics g1;
        private Cursor c1, c2;
        private HSB HSBcol = new HSB();
        private Pen pen;  
        private bool boxclicked, RunFirst;


        public Form1()
        {
            InitializeComponent();

            //SX = Convert.ToDouble(readState()[0]);
            //SY = Convert.ToDouble(readState()[1]);
            //EX = Convert.ToDouble(readState()[2]);
            //EY = Convert.ToDouble(readState()[3]);

            init();

            start();
           // pictureBox1.Cursor = Cursors.Cross;
            
        }
        public void init()  //all instances will be prepared
        {
            finished = false;
            //c1 = new Cursor(Cursor.WAIT_CURSOR);
            //c2 = new Cursor(Cursor.CROSSHAIR_CURSOR);
            x1 = pictureBox1.Width;
            y1 = pictureBox1.Height;
            xy = (float)x1 / (float)y1;
            image = new Bitmap(x1, y1);
            g1 = Graphics.FromImage(image);
            finished = true;
            RunFirst = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult close = MessageBox.Show("Do you sure want to exit?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            if (close == DialogResult.OK)
            {
                this.Close();
            }
            if(close== DialogResult.Cancel)
            {

                this.DialogResult = DialogResult.Cancel;
            }
        }


        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult res = MessageBox.Show("Are you sure you want to Restart ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (res == DialogResult.Yes)
            {
                 
                saveState(-2.025, -1.125, 0.6, 1.125);

                using (StreamWriter sw = File.CreateText("colState.txt"))
                {
                    sw.WriteLine(0);
                }

                Application.Restart();
            }
            if (res == DialogResult.No)
            {
                this.DialogResult = DialogResult.Cancel; 
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            start();
            mandelbrot();
            rectangle = false;
            update();
            StreamWriter writefile = new StreamWriter("uttamsave.txt");
            writefile.Write("-2.025" + Environment.NewLine);
            writefile.Write("-1.125" + Environment.NewLine);
            writefile.Write("0.6" + Environment.NewLine);
            writefile.Write("1.125" + Environment.NewLine);
            writefile.Close();
        }

        private void saveImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveimage = new SaveFileDialog();
            saveimage.Filter = "jpg(*.jpg)|*.jpg|bmp (*.bmp)|*.bmp| png(*.png) |*.png| gif(*.gif)|*.gif";
            if (saveimage.ShowDialog() == DialogResult.OK)
            {
                image.Save(saveimage.FileName);
            }
       }

        private void stopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Dispose();
        }

        private void cloneToolStripMenuItem_Click(object sender, EventArgs e)
        {
           // object Clone;
            Form1 newform = new Form1();
            newform.Show();
        }

        public void destroy()//delete all instances
        {
            if (finished)
            {
                image = null;
                g1 = null;

                //System.gc();   garbage collection
            }
        }

        private void colorPaletteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            int num = new Random().Next(1, 8);
            mandelbrot(num);
            update();

            using (StreamWriter sw = File.CreateText("colState.txt"))
            {
                sw.WriteLine(num);
            }

        }

        public void start()
        {
            action = false;
            rectangle = false;
            initvalues();
            xzoom = (xende - xstart) / (double)x1;
            yzoom = (yende - ystart) / (double)y1;

            int a = 0;
            using (StreamReader sr = File.OpenText("colState.txt"))
            {
                int num = 0;
                while ((num = Convert.ToInt32(sr.ReadLine())) != 0)
                {
                    a = num;
                }
            }

            mandelbrot(a);
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private int num = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            num++;

            if (num >= 8)
            {
                num = 1;
            } else
            {
                using (StreamWriter sw = File.CreateText("colState.txt"))
                {
                    sw.WriteLine(num);
                }
                mandelbrot(num);
                update();
            }
        }

        private void cycleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void stopToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            stop();
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "-Mandelbrot Program\n-Drag the particular area with the cursor to zoom in.\n-One click to zoom out./n-Change the color from Option.\n-From ColorCycle the color changes automatically.\n-Save the image you zoom in.";
            MessageBox.Show(message);
            
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aboutprogram = "Author :Uttam Bhandari\nMandelbrot C#\nEmail:uttambhandari75@gmail.com\nContact Number: +9779841254148";
            MessageBox.Show(aboutprogram);
        }

        private void startToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void stop()
        {
        }
        



        public void update()
        {
            saveState(xstart, ystart, xende, yende);
            Graphics g = pictureBox1.CreateGraphics();
            g.DrawImage(image, 0, 0);
            if(rectangle)
            {
                Pen pen = new Pen(Color.White, 1);
                if (xs < xe)
                {
                    if (ys < ye) g.DrawRectangle(new Pen(Color.White), xs, ys, (xe - xs), (ye - ys));
                    else g.DrawRectangle(new Pen(Color.White), xs, ye, (xe - xs), (ys - ye));
                }
                else
                {
                    if (ys < ye) g.DrawRectangle(new Pen(Color.White), xe, ys, (xs - xe), (ye - ys));
                    else g.DrawRectangle(new Pen(Color.White), xe, ye, (xs - xe), (ys - ye));
                }
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics obj = e.Graphics;
            obj.DrawImage(image, new Point(0, 0));
        }


        public void paint()
        {
            update();
        }
      
        private void mandelbrot(int change=0) //calculate mandelbrot points
        {
            int x, y;
            float h, b, alt = 0.0f;
            action = false;
            //pictureBox1.Cursor = c1; ( java cursor)
            //pictureBox1.Cursor = c2;

            //showStatus("Mandelbrot-Set will be produced - please wait...");
            for (x = 0; x < x1; x += 2)
                for (y = 0; y < y1; y++)
                {
                    h = pointcolour(xstart + xzoom * (double)x, ystart + yzoom * (double)y); // color value
                    if (h != alt)
                    {
                       
                        b = 1.0f - h * h; // brightnes
                                          //djm added
                         HSBcol.fromHSB(h * 255, 0.8f * 255, b * 255, change);

                        Color col = Color.FromArgb((int)HSBcol.rChan, (int)HSBcol.gChan, (int)HSBcol.bChan);//g1.setColor(col);
                        
                        pen = new Pen(col);


                        //djmg1.setColor(col);

                        alt = h;
                    }
                    g1.DrawLine(pen, x, y, x + 1, y);
                }
            //showStatus("Mandelbrot-Set ready - please select zoom area with pressed mouse.");
            textBox1.Text = "Mandelbrot-Set ready - please select zoom area with pressed mouse.";
            textBox1.Enabled = false;
            action = true;
        }
              private float pointcolour(double xwert, double ywert)
        {
            double m = 0.0, r = 0.0, i = 0.0;
            int j = 0;
            while ((j < MAX) && (m < 4.0))
            {
                j++;
                m = r * r - i * i;
                i = 2.0 * r * i + ywert;
                r = m + xwert;

            }
            return (float)j / (float)MAX;
        }

        private void initvalues() //reset start value
        {
            if (RunFirst == true)
            {
                List<string> co = new List<string>();

                using (StreamReader strRdr = File.OpenText("state.txt"))
                {
                    string s = "";
                    while ((s = strRdr.ReadLine()) != null)
                    {
                        co.Add(s);
                    }
                }
                    xstart = Convert.ToDouble(readState()[0]);
                ystart = Convert.ToDouble(readState()[1]);
                xende = Convert.ToDouble(readState()[2]);
                yende = Convert.ToDouble(readState()[3]);
                RunFirst = false;
                
            }
            else {
                xstart = SX;
                ystart = SY;
                xende = EX;
                yende = EY;
            }
            if ((float)((xende - xstart) / (yende - ystart)) != xy)
                xstart = xende - (yende - ystart) * (double)xy;
        }

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            boxclicked = true;
            {
                action = true;

                if (action)
                {
                    xs = e.X;
                    ys = e.Y;
                }
            }
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (boxclicked)
            {
                if (action)
                {
                    xe = e.X;
                    ye = e.Y;
                    rectangle = true;

                    update();
                }
            }

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            int z, w;

            if (action)
            {
                xe = e.X;
                ye = e.Y;
                if (xs > xe)
                {
                    z = xs;
                    xs = xe;
                    xe = z;
                }

                if (ys > ye)
                {
                    z = ys;
                    ys = ye;
                    ye = z;
                }
                w = (xe - xs);
                z = (ye - ys);
                if ((w < 2) && (z < 2)) initvalues();
                else
                {
                    if (((float)w > (float)z * xy)) ye = (int)((float)ys + (float)w / xy);
                    else xe = (int)((float)xs + (float)z * xy);
                    xende = xstart + xzoom * (double)xe;
                    yende = ystart + yzoom * (double)ye;
                    xstart += xzoom * (double)xs;
                    ystart += yzoom * (double)ys;
                }
                xzoom = (xende - xstart) / (double)x1;
                yzoom = (yende - ystart) / (double)y1;

                int a = 0;
                using (StreamReader sr = File.OpenText("colState.txt"))
                {
                    int num = 0;
                    while ((num = Convert.ToInt32(sr.ReadLine())) != 0)
                    {
                        a = num;
                    }
                }

                mandelbrot(a);

                rectangle = false;
                boxclicked = false;


                update();

            }
        }
    
       

        private List<string> readState()
        {
            string path = Directory.GetCurrentDirectory() + "\\state.txt";

            List<string> l = new List<string>();

            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    l.Add(s);
                }
            }

            return l;
        }

        private void saveState(double xstart, double ystart, double xend, double yend)
        {
            string path = Directory.GetCurrentDirectory() + "\\state.txt";
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(xstart);
                sw.WriteLine(ystart);
                sw.WriteLine(xend);
                sw.WriteLine(yend);
            }

        }

    }

    

}
