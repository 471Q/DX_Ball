using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DXBall
{
	public class OneUp : Collectables
	{
		public OneUp(float posX, float posY) : base(posX, posY)
		{
			resPicture = Properties.Resources.oneup;
			collRectangle = new RectangleF(posX, posY, 39f, 31f);
			collectableBrush = new TextureBrush(resPicture);
			PutCollectable();
		}

		public override void Effect(){ }

	}
}
