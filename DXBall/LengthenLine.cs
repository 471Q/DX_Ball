using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DXBall
{
	public class LengthenLine : Collectables
	{
		public LengthenLine(float posX, float posY) : base(posX, posY)
		{
			resPicture = Properties.Resources.lengthenline;
			collRectangle = new RectangleF(posX, posY, 32f, 11f);
			collectableBrush = new TextureBrush(resPicture);
			PutCollectable();
		}

		public override void Effect()
		{
			if (Math.Abs(line.LineRectangle.Width - 64f) < 5f)
			{
				line.LineRectangle = new RectangleF(line.PosX, line.PosY, 128f, 32f / 2);
				line.LineBrush = new TextureBrush(Properties.Resources.linenormal);
			}
			else if (Math.Abs(line.LineRectangle.Width - 128f) < 5f)
			{
				line.LineRectangle = new RectangleF(line.PosX, line.PosY, 256f, 32f / 2);
				line.LineBrush = new TextureBrush(Properties.Resources.linelengthened);
			}
		}
	}
}
