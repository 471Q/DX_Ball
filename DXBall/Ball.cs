using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DXBall
{

    class Ball
    {
        private float posX, posY, velX, velY, vel, detVel, maxVel;
        private double angle;
        public float PosX { get { return posX; } }
        public float PosY { get { return posY; } }
        public float MaxVelocity { get { return maxVel; } set { maxVel = value; } }
        public double Angle { get { return angle; } }
        public bool FalledDown { get; set; }
        public bool AtStartPosition { get; set; }
        private TextureBrush ballBrush;
        private RectangleF ballRectangle;
        private List<DXBox> _boxes;
        private DXLine _line;
        public TextureBrush BallBrush { get { return ballBrush; } }
        public RectangleF BallRectangle { get { return ballRectangle; } }
        public List<RectangleF> OtherRectangles { get; set; }
        public List<DXBox> Boxes { get { return _boxes; } set { _boxes = value; } }

        public event EventHandler BrokeBox;
        public event EventHandler TouchLine;

        private Bitmap resPicture;
        private bool touchedLine;

        public Ball(float _posX, float _posY)
        {
            resPicture = Properties.Resources.ballnormal;
            ballBrush = new TextureBrush(resPicture);
            ballBrush.TranslateTransform(_posX, _posY);
            ballRectangle = new RectangleF(_posX, _posY, 16f, 16f); posX = _posX; posY = _posY;
            OtherRectangles = new List<RectangleF>(); touchedLine = false; FalledDown = false; AtStartPosition = true;
        }

        public void OnBrokeBox(DXBox box)
        {
            EventHandler handler = BrokeBox;
            if (handler != null) { handler(box, EventArgs.Empty); }
        }

        public void OnTouchedLine()
        {
            EventHandler handler = TouchLine;
            if (handler != null) { handler(this, EventArgs.Empty); }
        }

        public void SetDeterminateVelocity(float _vel, float _detVel, double _angle)
        {
            angle = _angle; vel = _vel; detVel = _detVel;
            velX = _detVel * (float)Math.Cos(angle);
            velY = _detVel * (float)Math.Sin(angle);
        }

        public void DeterminateMove()
        {
            if (!touchedLine && BounceRectangleF(_line.LineRectangle)) 
            {
                OnTouchedLine(); touchedLine = true;
                float lineTouchPositionX = posX - _line.PosX - 56f; // Top Çubuğa hangi noktasından değiyor
                if (lineTouchPositionX < -64f) { lineTouchPositionX = -64f; }
                else if (lineTouchPositionX > 64f) { lineTouchPositionX = 64f; }
                ChangeAngle(angle + (lineTouchPositionX / 64f) * (Math.PI / 9));
                IncreaseSpeed();
            }

            foreach (RectangleF _rect in OtherRectangles)
            {
                BounceRectangleF(_rect);
            }
            foreach (DXBox _box in _boxes)
            {
                if (_box.Touched) continue;
                _box.Broken = BounceRectangleF(_box.BoxRectangle);
                if (_box.Broken) { OnBrokeBox(_box); _box.PosX = -1000f; _box.Touched = true; }
            }
            GetAwayFromRightAngles();
            posX += velX; posY += velY;
        }

        private void GetAwayFromRightAngles()
        {
            double _angle = angle % (2 * Math.PI);
            if (_angle < 0) _angle += 2 * Math.PI;

            if (betweenTwoAngles(_angle, 0, Math.PI / 18) || 
                betweenTwoAngles(_angle, Math.PI / 2, 10 * Math.PI / 18) ||
                betweenTwoAngles(_angle, Math.PI, 19 * Math.PI / 18) ||
                betweenTwoAngles(_angle, 3 * Math.PI / 2, 28 * Math.PI / 18)) ChangeAngle(angle + 0.02);
            else if (betweenTwoAngles(_angle, 35 * Math.PI / 18, 2 * Math.PI) || 
                betweenTwoAngles(_angle, 8 * Math.PI / 18, Math.PI / 2) ||
                betweenTwoAngles(_angle, 17 * Math.PI / 18, Math.PI) ||
                betweenTwoAngles(_angle, 26 * Math.PI / 18, 3 * Math.PI / 2)) ChangeAngle(angle - 0.02);
        }

        private bool betweenTwoAngles(double value, double lowerValue, double upperValue)
        { return value >= lowerValue && value <= upperValue; }

        private void IncreaseSpeed()
        {
            float _newVel = vel + 0.125f;
            if (_newVel > maxVel) _newVel = maxVel;
            SetDeterminateVelocity(_newVel, detVel, angle);
        }

        private bool BounceRectangleF(RectangleF _rect)
        {
            bool ret = false;
            double alfaNormal;
            float left = _rect.Left, right = _rect.Right, top = _rect.Top, bottom = _rect.Bottom,
                cenX = posX + 8f, cenY = posY + 8f;
            bool betweenTopAndBottom = betweenTwoValues(cenY, top, bottom),
                betweenLeftAndRight = betweenTwoValues(cenX, left, right);
            if (betweenTwoValues(posX, left, right) && betweenTopAndBottom)
            { ChangeAngle(Math.PI - angle); posX += velX; posY += velY; ret = true; }
            if (betweenTwoValues(posX + 16, left, right) && betweenTopAndBottom)
            { ChangeAngle(Math.PI - angle); posX += velX; posY += velY; ret = true; }
            if (betweenTwoValues(posY, top, bottom) && betweenLeftAndRight)
            { ChangeAngle(-angle); posX += velX; posY += velY; ret = true; }
            if (betweenTwoValues(posY + 16, top, bottom) && betweenLeftAndRight)
            { ChangeAngle(-angle); posX += velX; posY += velY; ret = true; }

            float vertDistTop = (top - cenY), vertDistBottom = (bottom - cenY),
                horzDistLeft = (left - cenX), horzDistRight = (right - cenX);
            bool onTopLeftCorner = (vertDistTop * vertDistTop + horzDistLeft * horzDistLeft) <= 64f,
                onTopRightCorner = (vertDistTop * vertDistTop + horzDistRight * horzDistRight) <= 64f,
                onBottomLeftCorner = (vertDistBottom * vertDistBottom + horzDistLeft * horzDistLeft) <= 64f,
                onBottomRightCorner = (vertDistBottom * vertDistBottom + horzDistRight * horzDistRight) <= 64f;
            if (onTopLeftCorner)
            {
                alfaNormal = Math.Atan(horzDistLeft / vertDistTop); ret = true;
                ChangeAngle(Math.PI - angle - 2 * alfaNormal); posX += velX; posY += velY;
            }
            if (onTopRightCorner)
            {
                alfaNormal = Math.Atan(horzDistRight / vertDistTop); ret = true;
                ChangeAngle(Math.PI - angle - 2 * alfaNormal); posX += velX; posY += velY;
            }
            if (onBottomLeftCorner)
            {
                alfaNormal = Math.Atan(horzDistLeft / vertDistBottom); ret = true;
                ChangeAngle(Math.PI - angle - 2 * alfaNormal); posX += velX; posY += velY;
            }
            if (onBottomRightCorner)
            {
                alfaNormal = Math.Atan(horzDistRight / vertDistBottom); ret = true;
                ChangeAngle(Math.PI - angle - 2 * alfaNormal); posX += velX; posY += velY;
            }
            return ret;
        }

        public void ChangeAngle(double _angle)
        {
            angle = _angle;
            velX = detVel * (float)Math.Cos(angle);
            velY = detVel * (float)Math.Sin(angle);
        }

        private bool betweenTwoValues(float val, float bt1, float bt2) { return val >= bt1 && val <= bt2; }

        public void PutBall()
        {
            float move_start = 0f;
            while (move_start <= vel) { DeterminateMove(); move_start += detVel; }
            angle = angle % (2 * Math.PI);
            if (angle < 0) angle += 2 * Math.PI;
            if (posY > _line.PosY + 32f) FalledDown = true;
            touchedLine = false;
            if (AtStartPosition) { ToStartPosition(); }
            ballBrush.ResetTransform();
            ballBrush.TranslateTransform(posX, posY);
            ballRectangle.X = posX; ballRectangle.Y = posY;
        }

        public void ToStartPosition() { posX = _line.PosX + 56f; posY = _line.PosY - 20f; }

        //public void MoveBall()
        //{
        //    float leftPoint = posX, rightPoint = posX + 16f, topPoint = posY, bottomPoint = posY + 16f,
        //        bounceX = 0f, bounceY = 0f;

        //    foreach (RectangleF _rect in OtherRectangles)
        //    {
        //        float deltaRight = _rect.Left - rightPoint, deltaLeft = _rect.Right - leftPoint,
        //            deltaTop = _rect.Bottom - topPoint, deltaBottom = _rect.Top - bottomPoint;
        //        bool betweenTopAndBottom = topPoint + 8 >= _rect.Top + velY && topPoint + 8 <= _rect.Bottom + velY,
        //            betweenLeftAndRight = leftPoint + 8 >= _rect.Left + velX && leftPoint + 8 <= _rect.Right + velY;
        //        if (deltaRight >= 0 && deltaRight < velX && betweenTopAndBottom)
        //        { bounceX += 2 * deltaRight - velX; velX = -velX; }
        //        if (deltaLeft <= 0 && deltaLeft > velX && betweenTopAndBottom)
        //        { bounceX += 2 * deltaLeft - velX; velX = -velX; }
        //        if (deltaBottom >= 0 && deltaBottom < velY && betweenLeftAndRight)
        //        { bounceY += 2 * deltaBottom - velY; velY = -velY; }
        //        if (deltaTop <= 0 && deltaTop > velY && betweenLeftAndRight)
        //        { bounceY += 2 * deltaTop - velY; velY = -velY; }
        //    }
        //    posX += bounceX; posY += bounceY;
        //    posX += velX; posY += velY;
            
        //    ballBrush.ResetTransform();
        //    ballBrush.TranslateTransform(posX, posY);
        //    ballRectangle.X = posX; ballRectangle.Y = posY;
        //}

        //private void CalculateBounceTarget()
        //{
        //    float centerX, centerY;
        //    float dv, dvx, dvy;
        //    double ang;
        //    ang = Math.Atan((float)(velX / velY));
        //    dv = 0.1f; dvx = dv * ((float)Math.Cos(ang)); dvy = dv * ((float)Math.Sin(ang));
        //    centerX = (ballRectangle.Right + ballRectangle.Left) / 2;
        //    centerY = (ballRectangle.Bottom + ballRectangle.Top) / 2;
        //    while (true)
        //    {
        //        centerX += dvx; centerY += dvy;
        //        foreach (RectangleF _rect in AllRectangles)
        //        {
        //            float delXLeft = Math.Abs(centerX - _rect.Left), delXRight = centerX - _rect.Left,
        //                delYTop = centerY - _rect.Top, delYBottom = centerY - _rect.Bottom;
        //            if (delXLeft < 16f)
        //            {
        //                break;
        //            }
        //        }
        //    }
        //}

        //public void IntersectRectangle(RectangleF destRect)
        //{
        //    PointF[] _points = new PointF[4];
        //    _points[0] = new PointF((ballRectangle.Right + ballRectangle.Left) / 2, ballRectangle.Top);
        //    _points[1] = new PointF(ballRectangle.Right, (ballRectangle.Bottom + ballRectangle.Top) / 2);
        //    _points[2] = new PointF((ballRectangle.Right + ballRectangle.Left) / 2, ballRectangle.Bottom);
        //    _points[3] = new PointF(ballRectangle.Left, (ballRectangle.Bottom + ballRectangle.Top) / 2);
        //    if (IntersectPointToRectangle(_points[0], destRect) || IntersectPointToRectangle(_points[2], destRect))
        //    { velY = -velY; MoveBall(); }
        //    if (IntersectPointToRectangle(_points[1], destRect) || IntersectPointToRectangle(_points[3], destRect))
        //    { velX = -velX; MoveBall(); }
        //}

        //private bool IntersectPointToRectangle(PointF _point, RectangleF _rect)
        //{
        //    return _point.Y > _rect.Top && _point.Y < _rect.Bottom && _point.X > _rect.Left && _point.X < _rect.Right;
        //}

        public void AddRectangleOfLine(DXLine __line) { _line = __line; }

        public void AddRectanglesOfWalls(List<DXWall> _walls)
        {
            foreach (DXWall _wall in _walls)
            {
                OtherRectangles.Add(_wall.WallRectangle);
            }
        }

        ~Ball() { GC.Collect(); }
    }
}
