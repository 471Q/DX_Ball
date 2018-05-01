using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DXBall
{
	public class CatchBallCollection : Collectables
	{
		public CatchBallCollection(float posX, float posY) : base(posX, posY)
		{
			resPicture = Properties.Resources.CatchBall;
			collRectangle = new RectangleF(posX, posY, 39f, 32f);
			collectableBrush = new TextureBrush(resPicture);
			PutCollectable();
		}

		public override void Effect()
		{
		}
	}
}
