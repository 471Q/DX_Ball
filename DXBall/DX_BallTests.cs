using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Runtime.InteropServices;
using System.IO;
using DXBall.Properties;
using NUnit.Framework;

namespace DXBall
{
	[TestFixture()]
	public class DX_BallTests
	{
		[Test()]
		public void CollectableCollectionCheckTest()
		{
			Collectables c = new MultiBall(0f, 0f);
			DXLine l = new DXLine(0f, 50f);

			c.AddLine(l);

			Assert.IsFalse(c.Collected);

			c.PosY = l.LineRectangle.Top;
			c.PosX = l.LineRectangle.Right;

			c.CollectionCheck(l.LineRectangle);

			Assert.IsTrue(c.Collected);
		}
	}
}
