using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DXBall
{
    class PXTitle
    {
        private LinearGradientBrush titleBrush;
        private int alpha, x, y, width, height;
        private Color color1, color2;
        public string Text 
        { 
            get; 
            set; 
        }
        public int Alpha 
        { 
            get { return alpha; } 
            set { alpha = value; } 
        }
        public int X 
        {
            get { return x; } 
            set { x = value; } 
        }
        public int Y 
        { 
            get { return y; } 
            set { y = value; } 
        }
        public int Width 
        {
            get { return width; } 
            set { width = value; } 
        }
        public int Height 
        { 
            get { return height; } 
            set { height = value; } 
        }
        public LinearGradientBrush TitleBrush 
        { 
            get { return titleBrush; } 
        }
        public PointF Position 
        { 
            get; 
            set; 
        }
        public bool Focused 
        { 
            get;
            set;
        }
        public Font TitleFont 
        { 
            get; 
            set; 
        }
        public event EventHandler AlphaIsMax;
        public event EventHandler AlphaIsZero;

        public PXTitle(Color _color1, Color _color2, float posX1, float posY1, float posX2, float posY2)
        {
            alpha = 0; 
            color1 = _color1;
            color2 = _color2;
            Focused = false;
            titleBrush = new LinearGradientBrush(new PointF(posX1, posY1), new PointF(posX2, posY2), 
                Color.FromArgb(alpha, color1), Color.FromArgb(alpha, color2));
        }

        public void OnAlphaIsMax()
        {
            EventHandler handler = AlphaIsMax;
            if (handler != null) { handler(this, EventArgs.Empty); }
        }

        public void OnAlphaIsZero()
        {
            EventHandler handler = AlphaIsZero;
            if (handler != null) { handler(this, EventArgs.Empty); }
        }

        public void IncreaseAlpha()
        {
            if (alpha < 255) alpha++;
            else { OnAlphaIsMax(); }
            titleBrush.LinearColors = new Color[2] { Color.FromArgb(alpha, color1), Color.FromArgb(alpha, color2) };
        }

        public void IncreaseAlpha(int add, int max)
        {
            if (alpha < max) alpha += add;
            else { OnAlphaIsMax(); }
            titleBrush.LinearColors = new Color[2] { Color.FromArgb(alpha, color1), Color.FromArgb(alpha, color2) };
        }

        public void DecreaseAlpha()
        {
            if (alpha > 0) alpha--;
            else { OnAlphaIsZero(); }
            titleBrush.LinearColors = new Color[2] { Color.FromArgb(alpha, color1), Color.FromArgb(alpha, color2) };
        }

        public void DecreaseAlpha(int dec, int min)
        {
            if (alpha > min) alpha -= dec;
            titleBrush.LinearColors = new Color[2] { Color.FromArgb(alpha, color1), Color.FromArgb(alpha, color2) };
        }

        public bool PositionInHere(int x, int y)
        {
            return x > X && x < X + Width && y > Y && y < Y + Height;
        }

        ~PXTitle() 
        { 
            GC.Collect();
        }
    }
}