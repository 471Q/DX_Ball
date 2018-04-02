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

namespace DXBall
{
	public partial class Form1 : Form
	{
		// for play and pause
		static bool conti = true;
		static bool samePause = false;

		private Bitmap bitmapBuffer;

		private PXTitle mainTitle, playTitle, highScoresTitle, exitTitle;
		private List<DXBox> boxes;
		private List<Ball> balls;
		private List<DXWall> walls;
		private DXLine line;
		private DXAnimation[] breakingAnimations;
		private List<DXAnimation> animations;
		private int Score, Level, Life, ScoreOnBoard, ComboScore;
		private FontFamily fontFamily;
		private Font UpBoardFont, GameOverFont, MainTitleFont;
		private static PrivateFontCollection myFonts;
		private static IntPtr fontBuffer;
		private bool GameOver, MainMenu, MainMenuOpening, LevelFinished;

		public Form1()
		{
			InitializeComponent();


			Graphics graphics = this.CreateGraphics();
			graphics.SmoothingMode = SmoothingMode.HighSpeed;
			graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

			LoadFont();

			//Cursor.Hide();

			this.SetStyle(
			ControlStyles.UserPaint |
			ControlStyles.AllPaintingInWmPaint |
			ControlStyles.DoubleBuffer, true);

			MainMenuObjects();

			//StartGame();
		}

		private void MainMenuObjects()
		{
			MainMenu = false; MainMenuOpening = true; LevelFinished = false;
			mainTitle = new PXTitle(Color.FromArgb(255, 0, 0), Color.FromArgb(0, 255, 0), 100f, 0f, 800f, 100f);
			mainTitle.AlphaIsMax += mainTitle_AlphaIsMax;
			playTitle = new PXTitle(Color.FromArgb(255, 255, 0), Color.FromArgb(0, 0, 255), 100f, 0f, 800f, 100f);
			playTitle.Alpha = 120; playTitle.TitleFont = UpBoardFont; playTitle.X = 200; playTitle.Y = 200;
			highScoresTitle = new PXTitle(Color.FromArgb(255, 255, 0), Color.FromArgb(0, 0, 255), 100f, 0f, 800f, 100f);
			highScoresTitle.Alpha = 120; highScoresTitle.TitleFont = UpBoardFont; highScoresTitle.X = 300; highScoresTitle.Y = 500;
			exitTitle = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 500f, 0f, 960f, 100f);
			exitTitle.Alpha = 120; exitTitle.TitleFont = UpBoardFont; exitTitle.X = 600; exitTitle.Y = 600;
		}

		void mainTitle_AlphaIsMax(object sender, EventArgs e)
		{
			MainMenuOpening = false;
			MainMenu = true;
		}

		private void LoadFont()
		{
			if (myFonts == null)
			{
				myFonts = new PrivateFontCollection();
				byte[] font = Properties.Resources.shablagoo;
				fontBuffer = Marshal.AllocCoTaskMem(font.Length);
				Marshal.Copy(font, 0, fontBuffer, font.Length);
				myFonts.AddMemoryFont(fontBuffer, font.Length);
			}
			fontFamily = myFonts.Families[0];
			UpBoardFont = new Font(fontFamily, 28f, FontStyle.Bold);
			GameOverFont = new Font(fontFamily, 60f, FontStyle.Bold);
			MainTitleFont = new Font(fontFamily, 70f);
		}

		private void StartGame()
		{
			Cursor.Hide();
			GameOver = false; MainMenu = false; MainMenuOpening = false; LevelFinished = false;
			Score = 0; Life = 3; Level = 1; ScoreOnBoard = 0;
			line = new DXLine(400f, 640f);

			walls = new List<DXWall>();
			walls.Add(new DXWall(WallType.Vertical, 48f, 120f));
			walls.Add(new DXWall(WallType.Vertical, 896f, 120f));
			walls.Add(new DXWall(WallType.Horizontal, 48f, 104f));

			balls = new List<Ball>();
			Ball b = new Ball(0f, 0f);
			b.BrokeBox += b_BrokeBox; b.TouchLine += b_TouchLine;
			b.SetDeterminateVelocity(15f, 0.25f, -Math.PI * 0.75); b.MaxVelocity = 30f;
			balls.Add(b);

			boxes = DXLevels.OpenLevel(Level);

			LoadAnimations();

			balls[0].AddRectangleOfLine(line);
			balls[0].AddRectanglesOfWalls(walls);
			balls[0].Boxes = boxes;
		}

		private void NextLevel()
		{
			LevelFinished = false; GameOver = false; MainMenu = false; MainMenuOpening = false;
			Level++;
			balls[0].AtStartPosition = true; balls[0].ToStartPosition();
			balls[0].SetDeterminateVelocity(15f + (0.5f * Level), 0.25f, -Math.PI * 0.75);
			balls[0].MaxVelocity = 30f + (0.5f * Level);
			boxes = DXLevels.OpenLevel(Level);
			balls[0].Boxes = boxes;
		}

		void b_TouchLine(object sender, EventArgs e)
		{
			ComboScore = 0;
		}

		void b_BrokeBox(object sender, EventArgs e)
		{
			DXBox box = (DXBox)sender;
			Score += 10 + ComboScore;
			ComboScore += 5;
			DXAnimation anm = null;
			switch (box.Kind)
			{
				case BoxKind.Red: anm = breakingAnimations[0].Clone(); break;
				case BoxKind.Blue: anm = breakingAnimations[1].Clone(); break;
				case BoxKind.Green: anm = breakingAnimations[2].Clone(); break;
				case BoxKind.Yellow: anm = breakingAnimations[3].Clone(); break;
				case BoxKind.Purple: anm = breakingAnimations[4].Clone(); break;
				case BoxKind.Cyan: anm = breakingAnimations[5].Clone(); break;
				case BoxKind.White: anm = breakingAnimations[6].Clone(); break;
			}
			if (anm == null) return;
			anm.PosX = box.PosX - 32f; anm.PosY = box.PosY - 8f;
			anm.Visible = true;
			animations.Add(anm);
		}

		private void LoadAnimations()
		{
			DXTiledTexture tiledTextureRedBroke = new DXTiledTexture(Properties.Resources.redbroke, 5, 4);
			DXTiledTexture tiledTextureBlueBroke = new DXTiledTexture(Properties.Resources.bluebroke, 5, 4);
			DXTiledTexture tiledTextureGreenBroke = new DXTiledTexture(Properties.Resources.greenbroke, 5, 4);
			DXTiledTexture tiledTextureYellowBroke = new DXTiledTexture(Properties.Resources.yellowbroke, 5, 4);
			DXTiledTexture tiledTexturePurpleBroke = new DXTiledTexture(Properties.Resources.purplebroke, 5, 4);
			DXTiledTexture tiledTextureCyanBroke = new DXTiledTexture(Properties.Resources.cyanbroke, 5, 4);
			DXTiledTexture tiledTextureWhiteBroke = new DXTiledTexture(Properties.Resources.whitebroke, 5, 4);

			breakingAnimations = new DXAnimation[7];
			breakingAnimations[0] = new DXAnimation(0f, 0f, tiledTextureRedBroke);
			breakingAnimations[1] = new DXAnimation(0f, 0f, tiledTextureBlueBroke);
			breakingAnimations[2] = new DXAnimation(0f, 0f, tiledTextureGreenBroke);
			breakingAnimations[3] = new DXAnimation(0f, 0f, tiledTextureYellowBroke);
			breakingAnimations[4] = new DXAnimation(0f, 0f, tiledTexturePurpleBroke);
			breakingAnimations[5] = new DXAnimation(0f, 0f, tiledTextureCyanBroke);
			breakingAnimations[6] = new DXAnimation(0f, 0f, tiledTextureWhiteBroke);

			int i = 0;
			while (i < 7) { breakingAnimations[i].AnimationTurned += breakingAnimation_AnimationTurned; i++; }
			animations = new List<DXAnimation>();
		}

		void breakingAnimation_AnimationTurned(object sender, EventArgs e)
		{ DXAnimation anm = (DXAnimation)sender; anm.Visible = false; }

		private void PutBoxes()
		{ foreach (DXBox _box in boxes) { _box.PutBox(); } }

		private void Mouse_Move(object sender, MouseEventArgs e)
		{
			if (MainMenu || !conti)
			{
				playTitle.Focused = playTitle.PositionInHere(e.X, e.Y);
				highScoresTitle.Focused = highScoresTitle.PositionInHere(e.X, e.Y);
				exitTitle.Focused = exitTitle.PositionInHere(e.X, e.Y);
			}
			if (line == null) return;
			line.PosX = e.X - 64;
		}


		// ------------------------------ start mangu logic ------------------------------ //
		private void GameTimerTick(object sender, EventArgs e)
		{
			if (conti)
			{
				if (!GameOver && !MainMenuOpening && !MainMenu && !LevelFinished && !samePause)
				{
					Moves();
					PutBoxes();
					DeleteGoneObjects();
					IsBallsFalled();
					IsBoxesBroken();
				}

				Draw();
			}

			else
			{
				resumeScreen();
			}
		}

		private void resumeScreen()
		{
			if (bitmapBuffer != null)
			{
				Graphics graphics = Graphics.FromImage(bitmapBuffer);
				graphics.Clear(Color.Black);

				SizeF titleTextSize = graphics.MeasureString("DX Ball", MainTitleFont);

				mainTitle.IncreaseAlpha(4, 251);
				graphics.DrawString("DX Ball",
					MainTitleFont, mainTitle.TitleBrush,
					new PointF((960f - titleTextSize.Width) / 2, (720f - titleTextSize.Height) / 2));

				if (playTitle.Focused) playTitle.IncreaseAlpha(5, 250);
				else playTitle.DecreaseAlpha(120);
				if (highScoresTitle.Focused) highScoresTitle.IncreaseAlpha(5, 250);
				else highScoresTitle.DecreaseAlpha(120);
				if (exitTitle.Focused) exitTitle.IncreaseAlpha(5, 250);
				else exitTitle.DecreaseAlpha(120);

				SizeF playTextSize = graphics.MeasureString("Resume Game", UpBoardFont);
				graphics.DrawString("Resume Game", UpBoardFont, playTitle.TitleBrush, new Point(playTitle.X, playTitle.Y));
				playTitle.Width = (int)playTextSize.Width; playTitle.Height = (int)playTextSize.Height;

				SizeF highScoresTextSize = graphics.MeasureString("High Scores", UpBoardFont);
				graphics.DrawString("High Scores", UpBoardFont, highScoresTitle.TitleBrush, new PointF(highScoresTitle.X, highScoresTitle.Y));
				highScoresTitle.Width = (int)highScoresTextSize.Width; highScoresTitle.Height = (int)highScoresTextSize.Height;

				SizeF exitTextSize = graphics.MeasureString("Exit Game", UpBoardFont);
				graphics.DrawString("Exit Game", UpBoardFont, exitTitle.TitleBrush, new PointF(exitTitle.X, exitTitle.Y));
				exitTitle.Width = (int)exitTextSize.Width; exitTitle.Height = (int)exitTextSize.Height;

				Invalidate();
				MainMenu = false;
				return;
			}
		}


		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				// if count is true
				if (conti)
				{
					conti = !conti; // make counti false
					Cursor.Show();
				}
			}

			if (e.KeyCode == Keys.P)
			{
				samePause = !samePause;
			}
		}
		// ------------------------------ end mangu logic ------------------------------ //

		private void CreateBackBuffer(object sender, EventArgs e) //Form1_Load ve Form1_Resize
		{
			if (bitmapBuffer != null)
				bitmapBuffer.Dispose();
			bitmapBuffer = new Bitmap(ClientSize.Width, ClientSize.Height);
		}

		private void Moves()
		{
			line.MoveLine();
			foreach (Ball ball in balls)
			{
				ball.PutBall();
			}
		}

		private void DeleteGoneObjects()
		{
			int i = 0, c = boxes.Count;
			while (i < c) { if (boxes[i].Broken) { boxes.RemoveAt(i); c--; } i++; }
			i = 0; c = animations.Count;
			while (i < c) { if (!animations[i].Visible) { animations.RemoveAt(i); c--; } i++; }
			i = 0; c = balls.Count;
			while (i < c) { if (balls[i].FalledDown && c != 1) { balls.RemoveAt(i); c--; } i++; }
		}

		private void IsBallsFalled()
		{
			bool allFalled = true;
			foreach (Ball ball in balls) { allFalled = allFalled && ball.FalledDown; }
			if (allFalled)
			{
				if (Life == 0) { GameOver = true; Cursor.Show(); }
				else { balls[0].FalledDown = false; balls[0].AtStartPosition = true; balls[0].ToStartPosition(); Life--; }
			}
		}

		private void IsBoxesBroken()
		{
			if (boxes.Count == 0) { LevelFinished = true; }
		}

		private void Draw()
		{
			if (bitmapBuffer != null)
			{
				Graphics graphics = Graphics.FromImage(bitmapBuffer);
				graphics.Clear(Color.Black);

				if (MainMenuOpening)
				{
					SizeF titleTextSize = graphics.MeasureString("DX Ball", MainTitleFont);

					mainTitle.IncreaseAlpha(4, 251);
					graphics.DrawString("DX Ball",
						MainTitleFont, mainTitle.TitleBrush,
						new PointF((960f - titleTextSize.Width) / 2, (720f - titleTextSize.Height) / 2));

					Invalidate();
					return;
				}
				if (MainMenu)
				{
					SizeF titleTextSize = graphics.MeasureString("DX Ball", MainTitleFont);
					graphics.DrawString("DX Ball",
						MainTitleFont, mainTitle.TitleBrush,
						new PointF((960f - titleTextSize.Width) / 2, (720f - titleTextSize.Height) / 2));

					if (playTitle.Focused) playTitle.IncreaseAlpha(5, 250);
					else playTitle.DecreaseAlpha(120);
					if (highScoresTitle.Focused) highScoresTitle.IncreaseAlpha(5, 250);
					else highScoresTitle.DecreaseAlpha(120);
					if (exitTitle.Focused) exitTitle.IncreaseAlpha(5, 250);
					else exitTitle.DecreaseAlpha(120);

					SizeF playTextSize = graphics.MeasureString("Play Game", UpBoardFont);
					graphics.DrawString("Play Game", UpBoardFont, playTitle.TitleBrush, new Point(playTitle.X, playTitle.Y));
					playTitle.Width = (int)playTextSize.Width; playTitle.Height = (int)playTextSize.Height;

					SizeF highScoresTextSize = graphics.MeasureString("High Scores", UpBoardFont);
					graphics.DrawString("High Scores", UpBoardFont, highScoresTitle.TitleBrush, new PointF(highScoresTitle.X, highScoresTitle.Y));
					highScoresTitle.Width = (int)highScoresTextSize.Width; highScoresTitle.Height = (int)highScoresTextSize.Height;

					SizeF exitTextSize = graphics.MeasureString("Exit Game", UpBoardFont);
					graphics.DrawString("Exit Game", UpBoardFont, exitTitle.TitleBrush, new PointF(exitTitle.X, exitTitle.Y));
					exitTitle.Width = (int)exitTextSize.Width; exitTitle.Height = (int)exitTextSize.Height;

					Invalidate();
					return;
				}

				if (ScoreOnBoard < Score - 10000) ScoreOnBoard += 1000;
				else if (ScoreOnBoard < Score - 1000) ScoreOnBoard += 100;
				else if (ScoreOnBoard < Score - 100) ScoreOnBoard += 10;
				else if (ScoreOnBoard < Score) ScoreOnBoard++;
				else if (ScoreOnBoard > Score) ScoreOnBoard = Score;
				graphics.DrawString(String.Format("Score: {0}", ScoreOnBoard),
					UpBoardFont, Brushes.GreenYellow, new PointF(64f, 32f));

				graphics.DrawString(String.Format("Level: {0}", Level),
					UpBoardFont, Brushes.Gold, new PointF(320f, 32f));

				graphics.DrawImageUnscaled(Properties.Resources.ballnormal, 640, 40);
				graphics.DrawString(" x " + Life.ToString(),
					UpBoardFont, Brushes.Azure, new PointF(672f, 32f));

				graphics.DrawString(String.Format("Angle: {0:0.0000}", balls[0].Angle),
					UpBoardFont, Brushes.AliceBlue, new PointF(64f, 64f));

				graphics.DrawString(String.Format("Memory Usage: {0}", System.Diagnostics.Process.GetCurrentProcess().PrivateMemorySize64),
					UpBoardFont, Brushes.AliceBlue, new PointF(320f, 64f));

				graphics.FillRectangle(line.LineBrush, line.LineRectangle);

				foreach (DXWall wall in walls)
				{
					graphics.FillRectangle(wall.WallBrush, wall.WallRectangle);
				}

				foreach (Ball ball in balls)
				{
					graphics.FillRectangle(ball.BallBrush, ball.BallRectangle);
				}
				foreach (DXBox box in boxes)
				{
					graphics.FillRectangle(box.BoxBrush, box.BoxRectangle);
				}
				foreach (DXAnimation animation in animations)
				{
					if (animation.Visible)
						graphics.FillRectangle(animation.PutAnimation(), animation.AnimationRectangle);
				}
				if (GameOver)
				{
					SizeF gameOverTextSize = graphics.MeasureString("Game Over", GameOverFont);
					graphics.DrawString("Game Over",
						GameOverFont, Brushes.LightPink,
						new PointF((960f - gameOverTextSize.Width) / 2, (720f - gameOverTextSize.Height) / 2));
				}
				if (LevelFinished)
				{
					SizeF levelFinishedTextSize = graphics.MeasureString("Level Finished", GameOverFont);
					graphics.DrawString("Level Finished",
						GameOverFont, Brushes.Indigo,
						new PointF((960f - levelFinishedTextSize.Width) / 2, (720f - levelFinishedTextSize.Height) / 2));
				}

				Invalidate();
			}
		}

		private void ScreenPaint(object sender, PaintEventArgs e) //Form1_Paint
		{
			if (bitmapBuffer != null)
			{
				e.Graphics.DrawImageUnscaled(bitmapBuffer, Point.Empty);
			}
		}

		private void Mouse_Click(object sender, MouseEventArgs e)
		{
			if (LevelFinished) { NextLevel(); return; }

			if (GameOver)
			{
				MainMenuOpening = true; MainMenu = false; GameOver = false; return;
			}

			if (MainMenu)
			{
				if (playTitle.PositionInHere(e.X, e.Y)) { StartGame(); }
				else if (exitTitle.PositionInHere(e.X, e.Y)) { Environment.Exit(0); }
				return;
			}

			// ------------------------------ start mangu logic ------------------------------ //
			if (!conti)
			{
				if (playTitle.PositionInHere(e.X, e.Y)) { conti = !conti; Cursor.Hide(); }
				else if (exitTitle.PositionInHere(e.X, e.Y)) { Environment.Exit(0); }
				return;
			}
			// ------------------------------ end mangu logic ------------------------------ //

			if (balls == null) return;
			foreach (Ball ball in balls) { ball.AtStartPosition = false; }
		}
	}
}