using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DXBall
{
	public class MultiBall : Collectables
	{
		private List<Ball> multiBalls;

		public MultiBall(float posX, float posY) : base(posX, posY) 
		{
			multiBalls = new List<Ball>();
			Ball b1 = new Ball(0f, 0f);
			Ball b2 = new Ball(0f, 0f);
			Ball b3 = new Ball(0f, 0f);
			multiBalls.Add(b1);
			multiBalls.Add(b2);
			multiBalls.Add(b3);
			foreach (Ball b in multiBalls)
			{
				b.AtStartPosition = false;
			}
			resPicture = Properties.Resources.multiball;
			collRectangle = new RectangleF(posX, posY, 22f, 24f);
			collectableBrush = new TextureBrush(resPicture);
			PutCollectable();
		}

		public override void Effect() { }
	}
}
