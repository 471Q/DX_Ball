﻿using System;
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
		public float PosX 
        { 
            get { return posX; }
        }
		public float PosY
        { 
            get { return posY; } 
        }
		public float MaxVelocity
        { 
            get { return maxVel; } 
            set { maxVel = value; }
        }
		public double Angle 
        { 
            get { return angle; } 
        }
		public bool FalledDown 
        { 
            get;
            set;
        }
		public bool AtStartPosition
        { 
            get;
            set; 
        }
		private TextureBrush ballBrush;
		private RectangleF ballRectangle;
		private List<DXBox> _boxes;
		private DXLine _line;
		public TextureBrush BallBrush 
        { 
            get { return ballBrush; } 
        }
		public RectangleF BallRectangle 
        { 
            get { return ballRectangle; }
        }
		public List<RectangleF> OtherRectangles 
        { 
            get; 
            set; 
        }
		public List<DXBox> Boxes 
        { 
            get { return _boxes; }
            set { _boxes = value; }
        }

		public event EventHandler BrokeBox;
		public event EventHandler TouchLine;

		private Bitmap resPicture;
		private bool touchedLine;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DXBall.Ball"/> class.
		/// </summary>
		/// <param name="_posX">Position x.</param>
		/// <param name="_posY">Position y.</param>
		public Ball(float _posX, float _posY)
		{
			resPicture = Properties.Resources.ballnormal;
			ballBrush = new TextureBrush(resPicture);
			ballBrush.TranslateTransform(_posX, _posY);
			ballRectangle = new RectangleF(_posX, _posY, 16f, 16f);
			posX = _posX;
			posY = _posY;
			OtherRectangles = new List<RectangleF>();
			touchedLine = false;
			FalledDown = false;
			AtStartPosition = true;
		}

		/// <summary>
		/// Event handler for when boxes break
		/// </summary>
		/// <param name="box">Box.</param>
		public void OnBrokeBox(DXBox box)
		{
			EventHandler handler = BrokeBox;
			if (handler != null) { handler(box, EventArgs.Empty); }
		}

		/// <summary>
		/// When the ball touches the paddle
		/// </summary>
		public void OnTouchedLine()
		{
			EventHandler handler = TouchLine;
			if (handler != null) { handler(this, EventArgs.Empty); }
		}

		/// <summary>
		/// Sets the determinate velocity.
		/// </summary>
		/// <param name="_vel">Vel.</param>
		/// <param name="_detVel">Det vel.</param>
		/// <param name="_angle">Angle.</param>
		public void SetDeterminateVelocity(float _vel, float _detVel, double _angle)
		{
			angle = _angle;
			vel = _vel;
			detVel = _detVel;
			velX = _detVel * (float)Math.Cos(angle);
			velY = _detVel * (float)Math.Sin(angle);
		}

		/// <summary>
		/// Determinates the move.
		/// </summary>
		public void DeterminateMove()
		{
			//How the ball is reflected on the paddle
			if (!touchedLine && BounceRectangleF(_line.LineRectangle))
			{
				OnTouchedLine();
				touchedLine = true;
				float lineTouchPositionX = posX - _line.PosX - 56f; //From which point is the ball to the ball
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
				if (_box.Broken)
				{
					OnBrokeBox(_box);
					_box.PosX = -1000f;
					_box.Touched = true;
				}
			}
			//GetAwayFromRightAngles();
			posX += velX;
			posY += velY;
		}

		/// <summary>
		/// Resolves angle calculation when the ball hits a corner
		/// </summary>
		private void GetAwayFromRightAngles()
		{
			double _angle = angle % (2 * Math.PI);
			if (_angle < 0) _angle += 2 * Math.PI;

			//between 0 and 10, between 90 and 100, between 180 and 190, 270 and 280 degrees change angle to angle plus 1.15radian
			if (betweenTwoAngles(_angle, 0, Math.PI / 18) ||
				betweenTwoAngles(_angle, Math.PI / 2, 10 * Math.PI / 18) ||
				betweenTwoAngles(_angle, Math.PI, 19 * Math.PI / 18) ||
				betweenTwoAngles(_angle, 3 * Math.PI / 2, 28 * Math.PI / 18)) ChangeAngle(angle + 0.02);

			//between 350 and 360, between 80 and 90, between 170 and 180, between 260 and 270  angle minus 1.15radian
			else if (betweenTwoAngles(_angle, 35 * Math.PI / 18, 2 * Math.PI) ||
				betweenTwoAngles(_angle, 8 * Math.PI / 18, Math.PI / 2) ||
				betweenTwoAngles(_angle, 17 * Math.PI / 18, Math.PI) ||
				betweenTwoAngles(_angle, 26 * Math.PI / 18, 3 * Math.PI / 2)) ChangeAngle(angle - 0.02);
		}

		private bool betweenTwoAngles(double value, double lowerValue, double upperValue)
		{ return value >= lowerValue && value <= upperValue; }

		/// <summary>
		/// Increases the speed.
		/// </summary>
		private void IncreaseSpeed()
		{
			float _newVel = vel + 0.125f;
			if (_newVel > maxVel) _newVel = maxVel;
			SetDeterminateVelocity(_newVel, detVel, angle);
		}

		/// <summary>
		/// Ball behavior when it bounces off of surfaces
		/// </summary>
		/// <returns><c>true</c>, if rectangle f was bounced, <c>false</c> otherwise.</returns>
		/// <param name="_rect">Rect.</param>
		private bool BounceRectangleF(RectangleF _rect)
		{
			bool ret = false;

			float 	left = _rect.Left,
					right = _rect.Right,
					top = _rect.Top,
					bottom = _rect.Bottom;

			if (betweenTwoValues(posX, left, right) && betweenTwoValues(posY + 8f, top, bottom))
			{
				ChangeAngle(Math.PI - angle);
				posX += velX;
				posY += velY;
				ret = true;
			}

			if (betweenTwoValues(posX + 16, left, right) && betweenTwoValues(posY + 8f, top, bottom))
			{
				ChangeAngle(Math.PI - angle);
				posX += velX;
				posY += velY;
				ret = true;
			}

			if (betweenTwoValues(posY, top, bottom) && betweenTwoValues(PosX + 8f, left, right))
			{
				ChangeAngle(-angle);
				posX += velX;
				posY += velY;
				ret = true;
			}

			if (betweenTwoValues(posY + 16, top, bottom) && betweenTwoValues(PosX + 8f, left, right))
			{
				ChangeAngle(-angle);
				posX += velX;
				posY += velY;
				ret = true;
			}

			float 	vertDistTop = (top - (posY + 8f)),
					vertDistBottom = (bottom - (posY + 8f)),
					horzDistLeft = (left - (posX + 8f)),
					horzDistRight = (right - (posX + 8f));

			return ret;
		}

		/// <summary>
		/// Changes the angle after impact.
		/// </summary>
		/// <param name="_angle">Angle.</param>
		public void ChangeAngle(double _angle)
		{
			angle = _angle;
			velX = detVel * (float)Math.Cos(angle);
			velY = detVel * (float)Math.Sin(angle);
		}

		private bool betweenTwoValues(float val, float bt1, float bt2)
		{
			return val >= bt1 && val <= bt2;
		}

		/// <summary>
		/// Puts the ball.
		/// </summary>
		public void PutBall()
		{
			float move_start = 0f;
			while (move_start <= vel)
			{
				DeterminateMove();
				move_start += detVel;
			}
			angle = angle % (2 * Math.PI);
			if (angle < 0) angle += 2 * Math.PI;
			if (posY > _line.PosY + 32f) FalledDown = true;
			touchedLine = false;
			if (AtStartPosition) 
            { 
                ToStartPosition(); 
            }
			ballBrush.ResetTransform();
			ballBrush.TranslateTransform(posX, posY);
			ballRectangle.X = posX; ballRectangle.Y = posY;
		}

		/// <summary>
		/// Starts the ball in the start position.
		/// </summary>
		public void ToStartPosition()
		{
			posX = _line.PosX + 56f;
			posY = _line.PosY - 20f;
		}

		public void AddRectangleOfLine(DXLine __line) 
		{ 
			_line = __line; 
		}

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
