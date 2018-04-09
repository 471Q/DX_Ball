using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DXBall
{
    class DXAnimation
    {
        private float posX, posY;
        public float PosX 
        { 
            get { return posX; } 
            set { posX = value; }
        }
        public float PosY 
        { 
            get { return posY; } 
            set { posY = value; }
        }
        private DXTiledTexture tiledTexture;
        private int currentTile;
        private RectangleF animationRectangle;
        public RectangleF AnimationRectangle 
        { 
            get { return animationRectangle; } 
        }
        public bool Over 
        { 
            get; 
            set; 
        }
        public bool Visible 
        { 
            get; 
            set; 
        }

        public event EventHandler AnimationTurned;

        public DXAnimation(float _posX, float _posY, DXTiledTexture _tiledTexture)
        {
            posX = _posX; 
            posY = _posY;
            animationRectangle = new RectangleF(_posX, _posY, _tiledTexture.TileWidth, _tiledTexture.TileHeight);
            tiledTexture = _tiledTexture;
            Over = false; 
            currentTile = 0; 
            Visible = false;
        }

        public void OnAnimationTurned()
        {
            EventHandler handler = AnimationTurned;
            if (handler != null) { handler(this, EventArgs.Empty); }
        }

        public TextureBrush PutAnimation()
        {
            TextureBrush ret = tiledTexture.GetBrush(currentTile);
            ret.ResetTransform();
            ret.TranslateTransform(posX, posY);
            animationRectangle.X = posX; 
            animationRectangle.Y = posY;
            currentTile++; 
            if (currentTile == tiledTexture.TilesLength) 
            { 
                currentTile = 0;
                OnAnimationTurned();
            }
            return ret;
        }

        public DXAnimation Clone()
        {
            DXAnimation cloned = new DXAnimation(this.posX, this.posY, this.tiledTexture);
            cloned.animationRectangle = new RectangleF(posX, posY, tiledTexture.TileWidth, tiledTexture.TileHeight);
            cloned.AnimationTurned = this.AnimationTurned;
            return cloned;
        }

        ~DXAnimation() { GC.Collect(); }
    }
}
