using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DXBall
{
    public enum BoxKind
    {
        Red, Green, Blue, Purple, Yellow, Cyan, DarkRed, DarkGreen, DarkBlue, DarkGray, Gray, White
    }

    public class DXBox
    {
        private float posX, posY;
        public float PosX 
        {
            get { return posX; } 
            set { posX = value; } 
        }
        public float PosY 
        { 
            get { return posY; } 
            set { posY = value; } 
        }
        private Bitmap resPicture;
        private TextureBrush boxBrush;
        private RectangleF boxRectangle;
        private BoxKind kind;
        public TextureBrush BoxBrush 
        { 
            get { return boxBrush; } 
        }
        public RectangleF BoxRectangle 
        { 
            get { return boxRectangle; }
        }
        public bool Broken 
        { 
            get; 
            set; 
        }
        public bool Touched 
        { 
            get;
            set; 
        }
        public BoxKind Kind 
        { 
            get { return kind; } 
        }

        public DXBox(BoxKind _kind, float _posX, float _posY)
        {
            kind = _kind; 
			posX = _posX; 
			posY = _posY; 
			Broken = false; 
			Touched = false;
            _chooseResourcePicture(_kind);
            boxBrush = new TextureBrush(resPicture);
            boxRectangle = new RectangleF(posX, posY, 64f, 32f);
            PutBox();
        }

        public void PutBox()
        {
            boxBrush.ResetTransform();
            boxBrush.TranslateTransform(posX, posY);
            boxRectangle.X = posX; 
			boxRectangle.Y = posY;
        }

        private void _chooseResourcePicture(BoxKind _kind)
        {
            switch (_kind)
            {
                case BoxKind.Red: resPicture = Properties.Resources.redbox; 
                    break;
                case BoxKind.Green: resPicture = Properties.Resources.greenbox; 
                    break;
                case BoxKind.Blue: resPicture = Properties.Resources.bluebox; 
                    break;
                case BoxKind.Purple: resPicture = Properties.Resources.purplebox; 
                    break;
                case BoxKind.Yellow: resPicture = Properties.Resources.yellowbox; 
                    break;
                case BoxKind.Cyan: resPicture = Properties.Resources.cyanbox; 
                    break;
                case BoxKind.DarkRed: resPicture = Properties.Resources.darkredbox; 
                    break;
                case BoxKind.DarkGreen: resPicture = Properties.Resources.darkgreenbox;
                    break;
                case BoxKind.DarkBlue: resPicture = Properties.Resources.darkbluebox;
                    break;
                case BoxKind.DarkGray: resPicture = Properties.Resources.darkgraybox;
                    break;
                case BoxKind.White: resPicture = Properties.Resources.whitebox;
                    break;
            }
        }

        ~DXBox() { GC.Collect(); }
    }
}
