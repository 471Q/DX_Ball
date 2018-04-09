using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DXBall
{
	/// <summary>
	/// Class for Paddel
	/// </summary>
	class DXLine
	{
		private float posX, posY;
		public float PosX 
        { 
            get { return posX; } 
            set { posX = value; } 
        }
		public float PosY { get { return posY; } set { posY = value; } }
		private TextureBrush lineBrush;
		private RectangleF lineRectangle;
		public TextureBrush LineBrush 
        { 
            get { return lineBrush; } 
        }
		public RectangleF LineRectangle 
        { 
            get { return lineRectangle; }
        }

		private bool moveRight, moveLeft;

		private Bitmap resPicture;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DXBall.DXLine"/> class.
		/// </summary>
		/// <param name="_posX">Position x.</param>
		/// <param name="_posY">Position y.</param>
		public DXLine(float _posX, float _posY)
		{
			resPicture = Properties.Resources.linenormal;
			lineBrush = new TextureBrush(resPicture);
			lineBrush.TranslateTransform(posX, posY);
			lineRectangle = new RectangleF(_posX, _posY, 128f, 32f / 2);
			posX = _posX;
			posY = _posY;
		}

		/// <summary>
		/// Moves the line.
		/// </summary>
		public void MoveLine()
		{
			if (posX < 64f) posX = 64f;
			else if (posX > 768f) posX = 768f;
			lineBrush.ResetTransform();
			lineBrush.TranslateTransform(posX, posY);
			lineRectangle.X = posX;

			if (moveRight)
			{
				posX += 20;
			}

			if (moveLeft)
			{
				posX -= 20;
			}
		}

		public bool MoveRight
		{
			get { return moveRight; }
			set { moveRight = value; }
		}

		public bool MoveLeft
		{
			get { return moveLeft; }
			set { moveLeft = value; }
		}

    }
}
