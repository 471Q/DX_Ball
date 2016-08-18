using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DXBall
{
    class DXTiledTexture
    {
        private float tileW, tileH;
        public float TileWidth { get { return tileW; } }
        public float TileHeight { get { return tileH; } }
        private Bitmap[] tiles;
        private TextureBrush[] brushes;
        public int TilesLength { get { return tiles.Length; } }

        public DXTiledTexture(Bitmap resPicture, int tileX, int tileY)
        {
            tiles = new Bitmap[tileX * tileY];
            brushes = new TextureBrush[tileX * tileY];
            int i = 0, widthPicture = resPicture.Width, heightPicture = resPicture.Height;
            int widthTile = widthPicture / tileX, heightTile = heightPicture / tileY;
            tileW = widthTile; tileH = heightTile;
            while (i < tileY)
            {
                int j = 0;
                while (j < tileX)
                {
                    tiles[i * tileX + j] = resPicture.Clone(new Rectangle(j * widthTile, i * heightTile, widthTile, heightTile), resPicture.PixelFormat);
                    brushes[i * tileX + j] = new TextureBrush(tiles[i * tileX + j]);
                    j++;
                }
                i++;
            }
        }

        public TextureBrush GetBrush(int currentTile)
        {
            if (currentTile >= brushes.Length) { currentTile = 0; }
            TextureBrush ret = brushes[currentTile];
            return ret;
        }

        ~DXTiledTexture() { GC.Collect(); }
    }
}
