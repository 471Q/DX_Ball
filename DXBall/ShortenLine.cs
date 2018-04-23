using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DXBall
{
	public class ShortenLine : Collectables
	{
		public ShortenLine(float posX, float posY) : base(posX, posY)
		{
			resPicture = Properties.Resources.shortenline;
			collRectangle = new RectangleF(posX, posY, 32f, 11f);
			collectableBrush = new TextureBrush(resPicture);
			PutCollectable();
		}

		public override void Effect() 
		{ 
			if (Math.Abs(line.LineRectangle.Width - 256f) < 5f)
			{
				line.LineRectangle = new RectangleF(line.PosX, line.PosY, 128f, 32f / 2);
				line.LineBrush = new TextureBrush(Properties.Resources.linenormal);
			}
			else if (Math.Abs(line.LineRectangle.Width - 128) < 5f)
			{
				line.LineRectangle = new RectangleF(line.PosX, line.PosY, 64f, 32f / 2);
				line.LineBrush = new TextureBrush(Properties.Resources.lineshortened);
			}
		}

	}
}
