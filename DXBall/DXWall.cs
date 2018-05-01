using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DXBall
{
	public enum WallType { Vertical, Horizontal }

	public class DXWall
	{
		private float posX, posY;
		private TextureBrush wallBrush;
		private RectangleF wallRectangle;
		private Bitmap resPicture;

		public DXWall(WallType _type, float _posX, float _posY)
		{
			if (_type == WallType.Vertical)
			{
				resPicture = Properties.Resources.wallvertical;
				wallBrush = new TextureBrush(resPicture);
				wallBrush.TranslateTransform(_posX, _posY);
				wallRectangle = new RectangleF(_posX, _posY, 16f, 600f);
			}
			else if (_type == WallType.Horizontal)
			{
				resPicture = Properties.Resources.wallhorizontal;
				wallBrush = new TextureBrush(resPicture);
				wallBrush.TranslateTransform(_posX, _posY);
				wallRectangle = new RectangleF(_posX, _posY, 864f, 16f);
			}
			posX = _posX;
			posY = _posY;
		}

		public TextureBrush WallBrush
		{
			get { return wallBrush; }
		}

		public RectangleF WallRectangle
		{
			get { return wallRectangle; }
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
	}
}