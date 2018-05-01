using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXBall
{
	class DXCollectables
	{
		public static List<Collectables> OpenCollectables(int level)
		{
			List<Collectables> coll = null;
			switch (level)
			{
				case 1: coll = Level1(); break;
				case 2: coll = Level2(); break;
				case 3: coll = Level3(); break;
				case 4: coll = Level4(); break;
			}
			if (level > 4) coll = Level1();
			return coll;
		}

		private static List<Collectables> Level1()
		{
			List<Collectables> coll = new List<Collectables>();

			int i = 0;
			while (i < 8)
			{
				int j = 0;
				while (j < 12)
				{
					float _i = i, _j = j;

					if (i == 3 && j == 5)
					{
						coll.Add(new OneUp(108f + _j * 64f, 180f + _i * 32f));
					}
					else if (i == 7 && j == 10)
					{
						coll.Add(new LengthenLine(112f + _j * 64f, 190f + _i * 32f));
					}
					else if (i == 5 && j == 3)
					{
						coll.Add(new ShortenLine(112f + _j * 64f, 190f + _i * 32f));
					}
					else if (i == 7 && j == 3)
					{
						coll.Add(new MultiBall(112f + _j * 65f, 190f + _i * 31f));
					}
					else if (i == 7 && j == 2)
					{
						coll.Add(new Speed(112f + _j * 64f, 190f + _i * 32f));
					}
					else if (i == 4 && j == 8)
					{
						coll.Add(new CatchBallCollection(112f + _j* 64f, 188f + _i* 30f));
					}
					j++;
				}
				i++;
			}

			return coll;
		}

		private static List<Collectables> Level2()
		{
			List<Collectables> coll = new List<Collectables>();

			int i = 0;
			while (i < 12)
			{
				int j = 0;
				while (j < 12)
				{
					float _i = i, _j = j;

					if (i == 7 && j == 8)
					{
						coll.Add(new OneUp(108f + _j * 64f, 180f + _i * 32f));
					}
					else if (i == 11 && j == 10)
					{
						coll.Add(new LengthenLine(112f + _j * 64f, 190f + _i * 32f));
					}
					else if (i == 1 && j == 4)
					{
						coll.Add(new ShortenLine(112f + _j * 64f, 190f + _i * 32f));
					}
					else if (i == 9 && j == 0)
					{
						coll.Add(new MultiBall(112f + _j* 65f, 190f + _i* 31f));
					}
					else if (i == 5 && j == 6)
					{
						coll.Add(new CatchBallCollection(112f + _j* 64f, 189f + _i* 30f));
					}
					else if (i == 5 && j == 6)
					{
						coll.Add(new CatchBallCollection(112f + _j* 64f, 189f + _i* 30f));
					}
					j++;
				}
				i++;
			}
			return coll;
		}

		private static List<Collectables> Level3()
		{
			List<Collectables> coll = new List<Collectables>();

			int i = 0;
			while (i < 12)
			{
				int j = 0;
				while (j < 12)
				{
					float _i = i, _j = j;

					if (i == 5 && j == 4)
					{
						coll.Add(new OneUp(108f + _j * 64f, 180f + _i * 32f));
					}
					else if (i == 5 && j == 7)
					{
						coll.Add(new LengthenLine(112f + _j * 64f, 190f + _i * 32f));
					}
					else if (i == 2 && j == 6)
					{
						coll.Add(new ShortenLine(112f + _j * 64f, 190f + _i * 32f));
					}
					else if (i == 11 && j == 1)
					{
						coll.Add(new MultiBall(112f + _j* 65f, 190f + _i* 31f));
					}
					else if (i == 5 && j == 10)
					{
						coll.Add(new CatchBallCollection(112f + _j* 64f, 189f + _i* 30f));
					}
					j++;
				}
				i++;
			}
			return coll;
		}

		private static List<Collectables> Level4()
		{
			List<Collectables> coll = new List<Collectables>();

			int i = 0;
			while (i < 12)
			{
				int j = 0;
				while (j < 12)
				{
					float _i = i, _j = j;

					if (i == 2 && j == 2)
					{
						coll.Add(new OneUp(108f + _j * 64f, 180f + _i * 32f));
					}
					else if (i == 5 && j == 4)
					{
						coll.Add(new LengthenLine(112f + _j * 64f, 190f + _i * 32f));
					}
					else if (i == 3 && j == 3)
					{
						coll.Add(new ShortenLine(112f + _j * 64f, 190f + _i * 32f));
					}
					else if (i == 9 && j == 2)
					{
						coll.Add(new MultiBall(112f + _j* 65f, 190f + _i* 31f));
					}
					else if (i == 7 && j == 2)
					{
						coll.Add(new Speed(112f + _j * 64f, 190f + _i * 32f));
					}
					else if (i == 7 && j == 5)
					{
						coll.Add(new CatchBallCollection(112f + _j* 64f, 187f + _i* 31f));
					}
					j++;
				}
				i++;
			}
			return coll;
		}
	}
}
