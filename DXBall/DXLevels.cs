using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DXBall
{
    class DXLevels
    {
        public static List<DXBox> OpenLevel(int level)
        {
            List<DXBox> ret = null;
            switch (level)
            {
                case 1: ret = Level1(); break;
                case 2: ret = Level2(); break;
                case 3: ret = Level3(); break;
                case 4: ret = Level4(); break;
            }
            if (level > 4) ret = Level1();
            return ret;
        }

        private static List<DXBox> Level1()
        {
            List<DXBox> ret = new List<DXBox>();
            BoxKind kind = BoxKind.Blue;

            int i = 0;
            while (i < 8)
            {
                int j = 0;
                while (j < 12)
                {
                    float _i = i, _j = j;

                    if (i == 0)
					{ 
						if (j % 2 == 0) 
						{ 
						kind = BoxKind.Yellow; 
						} 
						else 
						{ 
							kind = BoxKind.Cyan; 
						} 
					}
                    else if (i % 3 == 1) 
					{ 
						if (j % 2 == 0) 
						{ 
							kind = BoxKind.Green; 
						} 
						else 
						{ 
							kind = BoxKind.Purple; 
						} 
					}
                    else if (i % 3 == 2) 
					{ 
						if (j % 2 == 0) 
						{ 
							kind = BoxKind.Red; 
						} 
						else 
						{ 
							kind = BoxKind.Blue; 
						} 
					}
                    else if (i % 3 == 0) 
					{ 
						if (j % 2 == 0) 
						{ 
						kind = BoxKind.Blue; 
						} 
						else 
						{ 
							kind = BoxKind.Red; 
						} 
					}
					ret.Add(new DXBox(kind, 96f + _j * 64f, 180f + _i * 32f));
					j++;
                }
                i++;
            }

			return ret;
        }

        private static List<DXBox> Level2()
        {
            List<DXBox> ret = new List<DXBox>();
            BoxKind kind = BoxKind.White;
            int i = 0;
            while (i < 12)
            {
                int j = 0;
                while (j < 12)
                {
                    float _i = i, _j = j;

                    if ((i % 2 == 0 && j % 2 == 1) || (i % 2 == 1 && j % 2 == 0))
                    {
                        if (i + j <= 8) kind = BoxKind.Yellow;
                        else if (i + j > 8 && i + j <= 16) kind = BoxKind.Purple;
                        else if (i + j > 16) kind = BoxKind.Red;
                        ret.Add(new DXBox(kind, 96f + _j * 64f, 180f + _i * 32f));
                    }
                    j++;
                }
                i++;
            }
            return ret;
        }

        private static List<DXBox> Level3()
        {
            List<DXBox> ret = new List<DXBox>();
            BoxKind kind = BoxKind.White;
            int i = 0;
            while (i < 12)
            {
                int j = 0;
                while (j < 12)
                {
                    float _i = i, _j = j;
                    if ((i - 12) * (i - 12) + 2 * (j - 5) * (j - 6) > 36)
                    {
                        if (j < 2 || (j >= 6 && j < 8)) kind = BoxKind.Green;
                        else if ((j >= 2 && j < 4) || (j >= 8 && j < 10)) kind = BoxKind.Blue;
                        else if ((j >= 4 && j < 6) || j >= 10) kind = BoxKind.Purple;
                        ret.Add(new DXBox(kind, 96f + _j * 64f, 180f + _i * 32f));
                    }
                    j++;
                }
                i++;
            }
            return ret;
        }

        private static List<DXBox> Level4()
        {
            List<DXBox> ret = new List<DXBox>();
            BoxKind kind = BoxKind.White;
            int i = 2;
            while (i < 10)
            {
                int j = 2;
                while (j < 10)
                {
                    float _i = i, _j = j;
                    if (i == 2 || i == 9 || j == 2 || j == 9) kind = BoxKind.Yellow;
                    else if (i == 3 || i == 8 || j == 3 || j == 8) kind = BoxKind.White;
                    else if (i == 4 || i == 7 || j == 4 || j == 7) kind = BoxKind.Red;
                    else if (i == 5 || i == 6 || j == 5 || j == 6) kind = BoxKind.Blue;
                    ret.Add(new DXBox(kind, 96f + _j * 64f, 180f + _i * 32f));
                    j++;
                }
                i++;
            }
            return ret;
        }

    }
}
