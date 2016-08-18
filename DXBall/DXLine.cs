using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DXBall
{
    class DXLine
    {
        private float posX, posY;
        public float PosX { get { return posX; } set { posX = value; } }
        public float PosY { get { return posY; } set { posY = value; } }
        private TextureBrush lineBrush;
        private RectangleF lineRectangle;
        public TextureBrush LineBrush { get { return lineBrush; } }
        public RectangleF LineRectangle { get { return lineRectangle; } }

        private Bitmap resPicture;

        public DXLine(float _posX, float _posY)
        {
            resPicture = Properties.Resources.linenormal;
            lineBrush = new TextureBrush(resPicture);
            lineBrush.TranslateTransform(posX, posY);
            lineRectangle = new RectangleF(_posX, _posY, 128f, 32f); posX = _posX; posY = _posY;
        }

        public void MoveLine()
        {
            if (posX < 64f) posX = 64f;
            else if (posX > 768f) posX = 768f;
            lineBrush.ResetTransform();
            lineBrush.TranslateTransform(posX, posY);
            lineRectangle.X = posX; lineRectangle.Y = posY;
        }


    }
}
