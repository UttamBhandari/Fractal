using System;
using System.Drawing;

namespace Fractal
{
    class HSB
    {
        public float rChan, gChan, bChan;
        public int r, g, a;

        //private int mandelColor;
        //private int unit;


        public HSB()
        {
            rChan = gChan = bChan = 0;
        }
        public void fromHSB(float h, float s, float b, int change = 0)
        {
            float red = b;
            float green = b;
            float blue = b;
            if (s != 0)
            {
                float max = b;
                float dif = b * s / 255;
                float min = b - dif;
                float h2 = h * 360 / 255;
                if (h2 < 60)
                {
                    red = max;
                    green = h2 * dif / 60 + min;
                    blue = min;
                }
                else if (h2 < 120)
                {
                    red = (h2 - 120) * dif / 60 * -1 + min;
                    green = max;
                    blue = min;
                }
                else if (h2 < 180)
                {
                    red = min;
                    green = max;
                    blue = (((h2 - 120)
                                * (dif / 60))
                                + min);
                }
                else if ((h2 < 240))
                {
                    red = min;
                    green = ((((h2 - 240)
                                * (dif / 60))
                                * -1)
                                + min);
                    blue = max;
                }
                else if ((h2 < 300))
                {
                    red = (((h2 - 240)
                                * (dif / 60))
                                + min);
                    green = min;
                    blue = max;
                }
                else if ((h2 <= 360))
                {
                    red = max;
                    green = min;
                    blue = ((((h2 - 360)
                                * (dif / 60))
                                * -1)
                                + min);
                }
                else {
                    red = 0;
                    green = 0;
                    blue = 0;
                }
            }
            
                rChan = (float)Math.Round(Math.Min(Math.Max(red, 0f), 255));
                gChan = (float)Math.Round(Math.Min(Math.Max(green, 0), 255));
                bChan = (float)Math.Round(Math.Min(Math.Max(blue, 0), 255));

            if (change == 1)
            {
                rChan = 50;
                bChan = 200;
            }

            else if (change == 2)
            {
                rChan = 10;
            }


            else if (change == 3)
            {
                bChan = 200;
            }


            else if (change == 4)
            {
                bChan = 100;
            }

            else if (change == 5)
            {
                rChan = 150;
            }

            else if (change == 6)
            {
                rChan = 10;
                bChan = 150;
            }

            else if (change == 7)
            {
                rChan = 255;
            }

            else if (change == 8)
            {
                bChan = 200;
            }

        }

    }
}
        /*
        private void nextRGB()
        {
            if (((r == 255)
                        && ((g < 255)
                        && (b == 0))))
            {
                g++;
            }

            if (((g == 255)
                        && ((r > 0)
                        && (b == 0))))
            {
                r--;
            }

            if (((g == 255)
                        && ((b < 255)
                        && (r == 0))))
            {
                b++;
            }

            if (((b == 255)
                        && ((g > 0)
                        && (r == 0))))
            {
                g--;
            }

            if (((b == 255)
                        && ((r < 255)
                        && (g == 0))))
            {
                r++;
            }

            if (((r == 255)
                        && ((b > 0)
                        && (g == 0))))
            {
                b--;
            }

        }

        public Color nextColor()
        {
            nextRGB();
            return makeColor();
        }

        private Color makeColor()
        {
            return new Color(r, g, b);
        } */
        /**
        private float UInt8(double v)
        {
            throw new NotImplementedException();
        }

      
        
    **/
       
    
