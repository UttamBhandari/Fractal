namespace Fractal
{
    internal class HSB
    {
        public float rChan, gChan, bChan;
        public HSB()
        {
            rChan = gChan = bChan = 0;
        }
        public void fromHSB(float h, float s, float b)
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
                else if(h2 < 120){
                    red = (h2 - 120) * dif / 60  * -1+ min;
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
            }
    }
}