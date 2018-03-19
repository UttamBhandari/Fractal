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

        private Image picture;
        private Graphics g1;
        private Cursor c1,c2;
        private HSB HSBcol;
        public Form1()
        {
            InitializeComponent();
            init();
            start();
        }
        public void init()
        {

        }
        public void start()
        {
            action = false;
            rectangle = false;
            initvalues();
            xzoom = (xende - xstart) / (double)x1;
            yzoom = (yende - ystart) / (double)y1;
            mandelbrot();
        }
        private void initvalues() //reset start value
        {
            xstart = SX;
            ystart = SY;
            xende = EX;
            yende = EY;
            if ((float)((xende - xstart) / (yende - ystart)) != xy)
                xstart = xende - (yende - ystart) * (double)xy;
        }

        private void mandelbrot() //calculate all points
        {
            int x, y;
            float h, b, alt = 0.0f;
            action = false;
            //pictureBox1.Cursor = c1; ( java cursor)
            pictureBox1.Cursor = c2;

            //showStatus("Mandelbrot-Set will be produced - please wait...");
            for (x = 0; x < x1; x += 2)
                for (y = 0; y < y1; y++)
                {
                    h = pointcolour(xstart + xzoom * (double)x, ystart + yzoom * (double)y); // color value
                    if (h != alt)
                    {
                        b = 1.0f - h * h; // brightnes
                                          //djm added
                        HSBcol.fromHSB(h, 0.8f, b);
                        Color col = new Color(0, HSBcol.rChan, HSBcol.gChan, HSBcol.bChan);
                    }

        private void pictureBox1_Click(object sender, EventArgs e)
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

        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
