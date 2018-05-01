using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DXBall
{
	public abstract class Collectables
	{
		protected float posX, posY;
		protected TextureBrush collectableBrush;
		protected bool collected, atStartPosition, fallen;
		protected Bitmap resPicture;
		protected RectangleF collRectangle;
		protected DXLine line;

		protected Collectables(float posX, float posY)
		{
			this.posX = posX;
			this.posY = posY;
			collected = false;
			atStartPosition = true;
			fallen = false;
		}

		public void AddLine(DXLine _line)
		{
			line = _line;
		}

		public void PutCollectable()
		{
			collectableBrush.ResetTransform();
			collectableBrush.TranslateTransform(posX, posY);
			collRectangle.X = posX;
			collRectangle.Y = posY;

			if (posY > 715f)
			{
				fallen = true;
				collected = false;
			}
		}

		public abstract void Effect();

		private bool isInside(float _posX, float _posY, float left, float right, float bottom, float top)
		{
			return _posX >= left && _posX <= right && _posY <= bottom && _posY >= top;
		}

		public bool CollectionCheck(RectangleF rect)
		{
			collected = false;

			rect = line.LineRectangle;

			if (isInside(posX, posY, rect.Left, rect.Right, rect.Bottom, rect.Top - 5f))
			{
				collected = true;
			}

			return collected;
		}

		public void Descend()
		{ 
			if(!atStartPosition && !collected)
			{
				posY += 3;
			}
		}

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

		public bool Collected
		{
			get { return collected; }
			set { collected = value; }
		}

		public bool Fallen
		{
			get { return fallen; }
			set { fallen = value; }
		}

		public bool AtStartPosition
		{
			get { return atStartPosition; }
			set { atStartPosition = value; }
		}

		public TextureBrush CollectableBrush
		{
			get { return collectableBrush; }
		}

		public RectangleF CollRectangle
		{
			get { return collRectangle; }
		}
	}
}
