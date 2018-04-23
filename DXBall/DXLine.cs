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
	public class DXLine
	{
		private float posX, posY;
		private TextureBrush lineBrush;
		private RectangleF lineRectangle;
		private bool moveRight, moveLeft;
		/// <summary>
		/// Initializes a new instance of the <see cref="T:DXBall.DXLine"/> class.
		/// </summary>
		/// <param name="_posX">Position x.</param>
		/// <param name="_posY">Position y.</param>
		public DXLine(float _posX, float _posY)
		{
			lineBrush = new TextureBrush(Properties.Resources.linenormal);
			lineBrush.TranslateTransform(posX, posY);
			lineRectangle = new RectangleF(posX, posY, 128f, 32f / 2);
			posX = _posX;
			posY = _posY;
		}

		/// <summary>
		/// Moves the line.
		/// </summary>
		public void MoveLine()
		{
			if (Math.Abs(lineRectangle.Width - 64f) < 5f)
			{
				if (posX < 65f) posX = 65f;
				else if (posX > 830f) posX = 830f;
			}
			else if (Math.Abs(lineRectangle.Width - 128f) < 5f)
			{
				if (posX < 64f) posX = 64f;
				else if (posX > 768f) posX = 768f;
			}
			else if (Math.Abs(lineRectangle.Width - 256f) < 5f)
			{
				if (posX < 60f) posX = 60f;
				else if (posX > 643f) posX = 643f;
			}
				         
         	lineBrush.ResetTransform();
			lineBrush.TranslateTransform(posX, posY);
			lineRectangle.X = posX;
			lineRectangle.Y = posY;

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

		public float PosX
		{
			get { return posX; }
			set { posX = value; }
		}

		public float PosY
		{
			get { return posY; }
			set { posY = value; }		}

		public TextureBrush LineBrush
		{
			get { return lineBrush; }
			set { lineBrush = value; }
		}

		public RectangleF LineRectangle
		{
			get { return lineRectangle; }
			set { lineRectangle = value; }
		}
	}
}