using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace DXBall
{
	public class BackgroundPage
	{
		protected TextureBrush backgroundBrush;
		protected Bitmap resPicture;
		protected RectangleF backgroundPg;
		protected int selection = 1;

		public BackgroundPage()
		{
			///848 x 600
			backgroundBrush = new TextureBrush(Properties.Resources.bg_Titles);
			backgroundPg = new RectangleF(48f, 104f, 896f - 48f, 600f);
		}

		public void ChangeBackground()
		{
			if (selection == 1)
			{
				backgroundBrush = new TextureBrush(Properties.Resources.bg_Titles);
				backgroundPg = new RectangleF(48f, 104f, 896f - 48f, 600f);
			}
			else if (selection == 2)
			{
				backgroundBrush = new TextureBrush(Properties.Resources.bg_RetroCityBig);
				backgroundPg = new RectangleF(48f, 104f, 896f - 48f, 600f);
			}
			else if (selection == 3)
			{
				backgroundBrush = new TextureBrush(Properties.Resources.bg_Halloween);
				backgroundPg = new RectangleF(48f, 104f, 896f - 48f, 600f);
			}
		}

		public TextureBrush BackGroundBrush
		{
			get { return backgroundBrush; }
			set { backgroundBrush = value; }
		}

		public RectangleF BackGroundPg
		{
			get { return backgroundPg; }
			set { backgroundPg = value; }
		}

		public int Selection 
		{ 
			get { return selection; } 
			set { selection = value; } 
		}
	}
}
