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

namespace DXBall
{
	/// <summary>
	///
	/// </summary>
	public partial class Form1 : Form
	{
		// Declared for play and pause game 
		static bool conti = true;
		static bool samePause = false;
		static bool check = false;
		static bool startMenuHighScore = false;
		static bool comeBack = false;
		static bool gameStarted = false;
		static bool optionClicked = false;
		static bool mute = false;
		static bool keyControls = false;
		static bool resume = false;
		private int countdown;
		public int scoreYPosition = 140;
		public List<string> scores;

		private Bitmap bitmapBuffer;

		private PXTitle mainTitle, playTitle, highScoresTitle, exitTitle, highScoresTitle1, optionTitle, muteTitle, mutestatusTitle, controlTitle, controlStatusTitle, backgroundTitle, backgroundStatus, backTitle;
		private List<DXBox> boxes;
		private List<Collectables> colls;
		private List<Ball> balls, multiBalls;
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
		private BackgroundPage backGround;

		/// <summary>
		/// Initializes a new instance of the <see cref="T:DXBall.Form1"/> class.
		/// </summary>
		public Form1()
		{
			InitializeComponent();

			Graphics graphics = this.CreateGraphics();
			graphics.SmoothingMode = SmoothingMode.HighSpeed;
			graphics.PixelOffsetMode = PixelOffsetMode.HighSpeed;

			Bitmap b = new Bitmap(Resources.cursor);

			this.Cursor = CustomCursor.CreateCursor(b, b.Height / 2, b.Width / 2);

			LoadFont();

			//Cursor.Hide();

			this.SetStyle(
			ControlStyles.UserPaint |
			ControlStyles.AllPaintingInWmPaint |
			ControlStyles.DoubleBuffer, true);

			MainMenuObjects();

			//StartGame();
		}

		/// <summary>
		/// Mains the menu objects.
		/// </summary>
		private void MainMenuObjects()
		{
			MainMenu = false;
			MainMenuOpening = true;
			LevelFinished = false;

			mainTitle = new PXTitle(Color.FromArgb(255, 0, 0), Color.FromArgb(255, 104, 0), 100f, 0f, 800f, 100f);
			mainTitle.AlphaIsMax += mainTitle_AlphaIsMax;
			// Play Game
			playTitle = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 100f, 0f, 800f, 100f);
			playTitle.Alpha = 180;
			playTitle.TitleFont = UpBoardFont;
			playTitle.X = 400;
			playTitle.Y = 410;
			//Highscore(Resume Page)
			highScoresTitle = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 100f, 0f, 800f, 100f);
			highScoresTitle.Alpha = 180;
			highScoresTitle.TitleFont = UpBoardFont;
			highScoresTitle.X = 380;
			highScoresTitle.Y = 480;
			//Exit Game
			exitTitle = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 500f, 0f, 960f, 100f);
			exitTitle.Alpha = 180;
			exitTitle.TitleFont = UpBoardFont;
			exitTitle.X = 400;
			exitTitle.Y = 550;
			//Highscore (Home Page)
			highScoresTitle1 = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 100f, 0f, 800f, 100f);
			highScoresTitle1.Alpha = 180;
			highScoresTitle.TitleFont = UpBoardFont;
			highScoresTitle.X = 380;
			highScoresTitle.Y = 480;
			//option
			optionTitle = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 100f, 0f, 800f, 100f);
            optionTitle.Alpha = 180;
            optionTitle.TitleFont = UpBoardFont;
            optionTitle.X = 400;
            optionTitle.Y = 500;
 
 			// mute title
            muteTitle = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 100f, 0f, 800f, 100f);
            muteTitle.Alpha = 180;
            muteTitle.TitleFont = UpBoardFont;
            muteTitle.X = 200;
            muteTitle.Y = 200;
 			//Mute On and Off Button
            mutestatusTitle = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 100f, 0f, 800f, 100f);
            mutestatusTitle.Alpha = 180;
            mutestatusTitle.TitleFont = UpBoardFont;
            mutestatusTitle.X = 400;
            mutestatusTitle.Y = 200;
			// Contro Title
			controlTitle = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 100f, 0f, 800f, 100f);
			controlTitle.Alpha = 180;
            controlTitle.TitleFont = UpBoardFont;
            controlTitle.X = 200;
            controlTitle.Y = 280;
			// Control Option to Change
			controlStatusTitle = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 100f, 0f, 800f, 100f);
			controlStatusTitle.Alpha = 180;
			controlStatusTitle.TitleFont = UpBoardFont;
			controlStatusTitle.X = 400;
			controlStatusTitle.Y = 260;
			// Background Title
			backgroundTitle = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 100f, 0f, 800f, 100f);
            backgroundTitle.Alpha = 180;
            backgroundTitle.TitleFont = UpBoardFont;
            backgroundTitle.X = 200;
            backgroundTitle.Y = 360;
			// Background Number
			backgroundStatus = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 100f, 0f, 800f, 100f);
			backgroundStatus.Alpha = 180;
			backgroundStatus.TitleFont = UpBoardFont;
			backgroundStatus.X = 530;
			backgroundStatus.Y = 360;
			// Go back in Resume Option Page
            backTitle = new PXTitle(Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 255), 100f, 0f, 800f, 100f);
            backTitle.Alpha = 180;
            backTitle.TitleFont = UpBoardFont;
            backTitle.X = 200;
            backTitle.Y = 500;
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

		/// <summary>
		/// Starts the game.
		/// </summary>
		private void StartGame()
		{
			countdown = 105;
			GameOver = false;
			MainMenu = false;
			MainMenuOpening = false;
			LevelFinished = false;
			Score = 0;
			Life = 3;
			Level = 1;
			ScoreOnBoard = 0;
			line = new DXLine(400f, 707f);

			walls = new List<DXWall>();
			walls.Add(new DXWall(WallType.Vertical, 48f, 120f));
			walls.Add(new DXWall(WallType.Vertical, 896f, 120f));
			walls.Add(new DXWall(WallType.Horizontal, 48f, 104f));

			balls = new List<Ball>();
			Ball b = new Ball(0f, 0f);

			b.BrokeBox += b_BrokeBox;
			b.TouchLine += b_TouchLine;
			b.SetDeterminateVelocity(15f, 0.25f, -Math.PI * 0.75);
			b.MaxVelocity = 30f;
			balls.Add(b);

			backGround = new BackgroundPage();

			boxes = DXLevels.OpenLevel(Level);
			colls = DXCollectables.OpenCollectables(Level);

			foreach (Collectables c in colls)
			{
				c.AddLine(line);
			}

			LoadAnimations();

			balls[0].AddRectangleOfLine(line);
			balls[0].AddRectanglesOfWalls(walls);
			balls[0].Boxes = boxes;
			balls[0].Collectables = colls;
		}

		/// <summary>
		/// Nexts the level.
		/// </summary>
		private void NextLevel()
		{
			countdown = 105;
			LevelFinished = false;
			GameOver = false;
			MainMenu = false;
			MainMenuOpening = false;
			Level++;
			balls[0].AtStartPosition = true; 
			balls[0].ToStartPosition();
			balls[0].SetDeterminateVelocity(15f + (0.5f * Level), 0.25f, -Math.PI * 0.75);
			balls[0].MaxVelocity = 30f + (0.5f * Level);
			boxes = DXLevels.OpenLevel(Level);
			colls = DXCollectables.OpenCollectables(Level);
			balls[0].Boxes = boxes;
			balls[0].Collectables = colls;

			foreach (Collectables c in colls)
			{
				c.AddLine(line);
			}
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

			anm.PosX = box.PosX - 32f;
			anm.PosY = box.PosY - 8f;
			anm.Visible = true;
			animations.Add(anm);
		}

		/// <summary>
		/// Loads the animations.
		/// </summary>
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
			while (i < 7)
			{
				breakingAnimations[i].AnimationTurned += breakingAnimation_AnimationTurned; 
				i++;
			}

			animations = new List<DXAnimation>();
		}

		void breakingAnimation_AnimationTurned(object sender, EventArgs e)
		{
			DXAnimation anm = (DXAnimation)sender;
			anm.Visible = false;
		}

		private void PutBoxes()
		{
			foreach (DXBox _box in boxes)
			{
				_box.PutBox();
			}
		}

		private void PutCollectables()
		{
			foreach (Collectables c in colls)
			{
				c.PutCollectable();
			}
		}

		/// <summary>
		/// When the mouse is moved.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void Mouse_Move(object sender, MouseEventArgs e)
		{
			if (MainMenu || !conti)
			{
				playTitle.Focused = playTitle.PositionInHere(e.X, e.Y);
				highScoresTitle.Focused = highScoresTitle.PositionInHere(e.X, e.Y);
				exitTitle.Focused = exitTitle.PositionInHere(e.X, e.Y);
				optionTitle.Focused = optionTitle.PositionInHere(e.X, e.Y);
            }
			 if (optionClicked)
            {
                muteTitle.Focused = true;
                mutestatusTitle.Focused = mutestatusTitle.PositionInHere(e.X, e.Y);
				controlTitle.Focused = true;
				controlStatusTitle.Focused = controlStatusTitle.PositionInHere(e.X, e.Y);
				backgroundTitle.Focused = true;
				backTitle.Focused = true;
			}
			if (line == null) return;

			if(!keyControls) line.PosX = e.X;
		}


		// ------------------------------ Start Menu logic ------------------------------ //
		private void GameTimerTick(object sender, EventArgs e)
		{
			//checking high scores
			if (startMenuHighScore)
			{
				ShowHighScore();
				if (check)
				{
					conti = true;
					comeBack = true;
					startMenuHighScore = false;
					if (gameStarted)
					{
						conti = false;
					}
					if (!gameStarted)
					{
						conti = true;
					}
				}
			}
			else if (comeBack)
			{
				comeBack = false;
			}
			else if (optionClicked)
            {
                optionScreen();
            }

			// if statement to pause screen
			else if (conti)
			{
				if (!resume)
				{
					if (!GameOver && !MainMenuOpening && !MainMenu && !LevelFinished && !samePause)
					{
						Moves();
						PutCollectables();
						PutBoxes();
						DeleteGoneObjects();
						IsBallsFalled();
						IsBoxesBroken();
						DescendColls();
						CollectCheck();
						backGround.ChangeBackground();
					}
				}
				//else if (GameOver && !MainMenuOpening && !MainMenu && !LevelFinished && !samePause)
				//{
				//	WriteHighScore();
				//	WriteName();
				//}
				else if (GameOver)
				{
					gameStarted = false;
				}
				Draw();
			}
			else
			{
				resumeScreen();
			}
			check = false; 
		}

		private void optionScreen()
		{
			if (bitmapBuffer != null)
			{
				Graphics graphics = Graphics.FromImage(bitmapBuffer);
				graphics.Clear(Color.Black);

				//SizeF titleTextSize = graphics.MeasureString("DX Ball", MainTitleFont);

				//mainTitle.IncreaseAlpha(4, 251);
				//graphics.DrawString("DX Ball",
				//    MainTitleFont, mainTitle.TitleBrush,
				//    new PointF((960f - titleTextSize.Width) / 2, (720f - titleTextSize.Height) / 2));

				if (muteTitle.Focused) muteTitle.IncreaseAlpha(5, 250);
				else muteTitle.DecreaseAlpha(5,170);
				if (mutestatusTitle.Focused) mutestatusTitle.IncreaseAlpha(5, 250);
				else mutestatusTitle.DecreaseAlpha(5,170);
				if (backTitle.Focused) backTitle.IncreaseAlpha(5, 250);
				else backTitle.DecreaseAlpha(5,170);
				if (controlTitle.Focused) controlTitle.IncreaseAlpha(5, 250);
				else controlTitle.DecreaseAlpha(5,170);
				if (controlStatusTitle.Focused) controlStatusTitle.IncreaseAlpha(5, 250);
				else controlStatusTitle.DecreaseAlpha(5,170);
				if (backgroundTitle.Focused) backgroundTitle.IncreaseAlpha(5, 250);
				else backgroundTitle.DecreaseAlpha(5,170);
				if (backgroundStatus.Focused) backgroundStatus.IncreaseAlpha(5, 250);
				else backgroundStatus.DecreaseAlpha(5,170);
					
				SizeF muteTextSize = graphics.MeasureString("Mute", UpBoardFont);
				graphics.DrawString("Mute", UpBoardFont, muteTitle.TitleBrush, new Point(muteTitle.X, muteTitle.Y));
				muteTitle.Width = (int)muteTextSize.Width;
				muteTitle.Height = (int)muteTextSize.Height;

				SizeF muteStatusTextSize = graphics.MeasureString(mute ? "On" : "Off", UpBoardFont);
				graphics.DrawString(mute ? "On" : "Off", UpBoardFont, mutestatusTitle.TitleBrush, new Point(mutestatusTitle.X, mutestatusTitle.Y));
				mutestatusTitle.Width = (int)muteStatusTextSize.Width;
				mutestatusTitle.Height = (int)muteStatusTextSize.Height;

				SizeF controlTextSize = graphics.MeasureString("Controls", UpBoardFont);
				graphics.DrawString("Controls", UpBoardFont, controlTitle.TitleBrush, new Point(controlTitle.X, controlTitle.Y));
				controlTitle.Width = (int)controlTextSize.Width;
				controlTitle.Height = (int)controlTextSize.Height;

				SizeF controlStatusTextSize = graphics.MeasureString(keyControls ? "Mouse Moves The Paddle\nLeft Click Releases Ball" : "Directional Keys Move The Paddle\nSpacebar Releases Ball", UpBoardFont);
				graphics.DrawString(keyControls ? "Mouse Moves The Paddle\nLeft Click Releases Ball" : "Directional Keys Move The Paddle\nSpacebar Releases Ball", UpBoardFont, controlStatusTitle.TitleBrush, new Point(controlStatusTitle.X, controlStatusTitle.Y));
				controlStatusTitle.Width = (int)controlTextSize.Width;
				controlStatusTitle.Height = (int)controlTextSize.Height;

				if (backGround.Selection == 1)
				{
					SizeF backgroundTextSize = graphics.MeasureString("1", UpBoardFont);
					graphics.DrawString("1", UpBoardFont, controlTitle.TitleBrush, new Point(backgroundStatus.X, backgroundStatus.Y));
					backgroundStatus.Width = (int)backgroundTextSize.Width;
					backgroundStatus.Height = (int)backgroundTextSize.Height;
				}
				else if (backGround.Selection == 2)
				{
					SizeF backgroundTextSize = graphics.MeasureString("2", UpBoardFont);
					graphics.DrawString("2", UpBoardFont, controlTitle.TitleBrush, new Point(backgroundStatus.X, backgroundStatus.Y));
					backgroundStatus.Width = (int)backgroundTextSize.Width;
					backgroundStatus.Height = (int)backgroundTextSize.Height;
				}
				else if (backGround.Selection == 3) 
				{ 
					SizeF backgroundTextSize = graphics.MeasureString("3", UpBoardFont);
					graphics.DrawString("3", UpBoardFont, controlTitle.TitleBrush, new Point(backgroundStatus.X, backgroundStatus.Y));
					backgroundStatus.Width = (int)backgroundTextSize.Width;
					backgroundStatus.Height = (int)backgroundTextSize.Height;
				}

				SizeF backgroundTitleSize = graphics.MeasureString("Background Image", UpBoardFont);
				graphics.DrawString("Background Image", UpBoardFont, backgroundTitle.TitleBrush, new Point(backgroundTitle.X, backgroundTitle.Y));
				backgroundTitle.Width = (int)backgroundTitleSize.Width;
				backgroundTitle.Height = (int)backgroundTitleSize.Height;

				SizeF backTextSize = graphics.MeasureString("Back", UpBoardFont);
				graphics.DrawString("Back", UpBoardFont, backTitle.TitleBrush, new Point(backTitle.X, backTitle.Y));
				backTitle.Width = (int)backTextSize.Width;
				backTitle.Height = (int)backTextSize.Height;

				Invalidate();
				MainMenu = false;
				return;
			}
		}

		/// <summary>
		/// Resumes the screen.
		/// </summary>
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
				else playTitle.DecreaseAlpha(5,170);
				if (highScoresTitle.Focused) highScoresTitle.IncreaseAlpha(5, 250);
				else highScoresTitle.DecreaseAlpha(5,170);
				if (exitTitle.Focused) exitTitle.IncreaseAlpha(5, 250);
				else exitTitle.DecreaseAlpha(5,170);
				if (optionTitle.Focused) optionTitle.IncreaseAlpha(5, 250);
                else optionTitle.DecreaseAlpha(5,170);

				SizeF playTextSize = graphics.MeasureString("Resume Game", UpBoardFont);
				graphics.DrawString("Resume Game", UpBoardFont, playTitle.TitleBrush, new Point(380, 400));
				playTitle.Width = (int)playTextSize.Width; 
                playTitle.Height = (int)playTextSize.Height;

				SizeF highScoresTextSize = graphics.MeasureString("High Scores", UpBoardFont);
				graphics.DrawString("High Scores", UpBoardFont, highScoresTitle.TitleBrush, new PointF(highScoresTitle.X, 450));
				highScoresTitle.Width = (int)highScoresTextSize.Width;
                highScoresTitle.Height = (int)highScoresTextSize.Height;

				SizeF exitTextSize = graphics.MeasureString("Exit Game", UpBoardFont);
				graphics.DrawString("Exit Game", UpBoardFont, exitTitle.TitleBrush, new PointF(exitTitle.X, exitTitle.Y));
				exitTitle.Width = (int)exitTextSize.Width; 
                exitTitle.Height = (int)exitTextSize.Height;

				SizeF optionTextSize = graphics.MeasureString("Options", UpBoardFont);
				graphics.DrawString("Options", UpBoardFont, optionTitle.TitleBrush, new PointF(optionTitle.X, optionTitle.Y));
                optionTitle.Width = (int)optionTextSize.Width;
                optionTitle.Height = (int)optionTextSize.Height;


				Invalidate();
				MainMenu = false;
				return;
			}
		}

		/// <summary>
		/// Shows the High score
		/// </summary>
		private void initialCheck()
		{
			string score;
			string path = Environment.CurrentDirectory + "/" + "highScore.txt";

			if (!File.Exists(path))
			{
				MessageBox.Show("Finish a level First");
			}
			else
			{
				using (StreamReader sr = new StreamReader(path))
				{
					score = sr.ReadLine();
				}

				if (score == null)
				{
					MessageBox.Show("Finish a level First");
				}
			}
		}

		/// <summary>
		/// Prints the high score to a text file.
		/// </summary>
		private void WriteHighScore()
		{
			string path = Environment.CurrentDirectory + "/" + "highScore.txt";

			if (!File.Exists(path))
			{
				File.CreateText(path);
			}

			using (StreamWriter sw = File.AppendText(path))
			{
				sw.WriteLine(PlayerName.passingName + "\t" + ScoreOnBoard);
				sw.Close();
			}
		}

		/// <summary>
		/// Shows the High score
		/// </summary>
		private void ShowHighScore()
		{
			string path = Environment.CurrentDirectory + "/" + "highScore.txt";

			if (!File.Exists(path))
			{
				return;
			}

			if (bitmapBuffer != null)
			{
				Graphics graphics = Graphics.FromImage(bitmapBuffer);
				graphics.Clear(Color.Black);

				SizeF Player = graphics.MeasureString("Score Board", UpBoardFont);
				graphics.DrawString("Score Board", UpBoardFont, highScoresTitle.TitleBrush, new Point(highScoresTitle1.X, highScoresTitle1.Y));
				highScoresTitle1.X = 360;
				highScoresTitle1.Y = 90;
				highScoresTitle1.Width = (int)Player.Width; 
				highScoresTitle1.Height = (int)Player.Height;

				var score = File.ReadAllLines(path);
				scores = new List<string>(score);

				foreach (string s in scores)
				{
					graphics.DrawString(s, UpBoardFont, highScoresTitle.TitleBrush, new Point(highScoresTitle1.X, scoreYPosition));
					scoreYPosition += 30;
				}

				Invalidate();
				MainMenu = true;
				return;
			}
		}

		/// <summary>
		/// Form1 Keys to enter pause menu
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		private void Form1_KeyUp(object sender, KeyEventArgs e)
		{
			switch (e.KeyCode)
			{
				case Keys.Escape:
					{
						check = true;
						if (conti)
						{
							conti = !conti; // make counti value false
						}
					}
					break;
				case Keys.P:
					samePause = !samePause;
					break;
				case Keys.Right:
					line.MoveRight = false;
					break;
				case Keys.Left:
					line.MoveLeft = false;
					break;
			}
		}


		private void Key_Down(object sender, KeyEventArgs e)
		{
			if (keyControls)
			{
				switch (e.KeyCode)
				{
					case Keys.Right:
						line.MoveRight = true;
						break;
					case Keys.Left:
						line.MoveLeft = true;
						break;
					case Keys.Space:
						{
							foreach (Ball ball in balls)
							{
								ball.AtStartPosition = false;
							}
						}
						break;
				}
			}
		}

		// ------------------------------ end menu logic ------------------------------ //

		private void CreateBackBuffer(object sender, EventArgs e) //Form1_Load ve Form1_Resize
		{
			if (bitmapBuffer != null) bitmapBuffer.Dispose();
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

		private void DescendColls()
		{
			foreach (Collectables c in colls)
			{
				if (c.AtStartPosition == false)
				{
					c.Descend();
				}
			}
		}

		private void CollectCheck()
		{
			foreach (Ball ball in balls)
			{
				foreach (Collectables c in ball.Collectables)
				{
					c.CollectionCheck(line.LineRectangle);

					if (c.Collected)
					{
						if (c is OneUp)
						{
							Life++;
						}

						if (c is LengthenLine || c is ShortenLine)
						{
							c.Effect();
						}
						if (c is Speed)
						{
							Life++;
						}

						if (c is MultiBall)
						{
							multiBalls = new List<Ball>();

							Ball b1 = new Ball(0f, 0f);
							Ball b2 = new Ball(0f, 0f);
							Ball b3 = new Ball(0f, 0f);

							b1.PosX = b2.PosX = b3.PosX = ball.PosX;
							b1.PosY = b2.PosY = b3.PosY = ball.PosY;

							b1.AtStartPosition = b2.AtStartPosition = b3.AtStartPosition = ball.AtStartPosition;

							b1.BrokeBox += b_BrokeBox;
							b2.BrokeBox += b_BrokeBox;
							b3.BrokeBox += b_BrokeBox;

							b1.TouchLine += b_TouchLine;
							b2.TouchLine += b_TouchLine;
							b3.TouchLine += b_TouchLine;

							b1.SetDeterminateVelocity(15f, 0.25f, -Math.PI* 0.75);
							b2.SetDeterminateVelocity(15f, 0.25f, -Math.PI* 0.75);
							b3.SetDeterminateVelocity(15f, 0.25f, -Math.PI* 0.75);

							b1.MaxVelocity = 30f;
							b2.MaxVelocity = 30f;
							b3.MaxVelocity = 30f;

							multiBalls.Add(b1);
							multiBalls.Add(b2);
							multiBalls.Add(b3);

							foreach (Ball b in multiBalls)
							{
								b.AddRectangleOfLine(line);
								b.AddRectanglesOfWalls(walls);
								b.Boxes = boxes;
								b.Collectables = colls;
							}

							balls = multiBalls;
						}

						if (c is CatchBallCollection)
						{
							balls[0].AtStartPosition = true;
							balls[0].ToStartPosition();
						}
						if (c is Speed)
						{
							foreach (Ball b in balls)
							{
								b.MaxVelocity = 60f;
							}
						}
					}
				}
			}
		}

		private void DeleteGoneObjects()
		{
			int i = 0, c = boxes.Count;
			while (i < c) 
			{
				if (boxes[i].Broken) 
				{
					boxes.RemoveAt(i);
					c--;
				} 
				i++;
			}

			i = 0; 
			c = animations.Count;
			while (i < c) 
			{ 
				if (!animations[i].Visible) 
				{ 
					animations.RemoveAt(i); 
					c--; 
				} 
				i++; 
			}

			i = 0; 
			c = balls.Count;

			while (i < c) 
			{ 
				if (balls[i].FalledDown && c != 1) 
				{ 
					balls.RemoveAt(i); 
					c--; 
				} 
				i++; 
			}

			i = 0;
			c = colls.Count;

			while (i < c)
			{
				if (colls[i].Fallen || colls[i].Collected)
				{
					colls.RemoveAt(i);
					c--;
				}
				i++;
			}
		}

		private void IsBallsFalled()
		{
			bool allFalled = true;
			foreach (Ball ball in balls) 
			{ 
				allFalled = allFalled && ball.FalledDown; 
			}
			if (allFalled)
			{
				if (Life == 0) 
				{ 
					GameOver = true; 
					WriteHighScore();
				}
				else 
				{
					balls[0].FalledDown = false;
					balls[0].AtStartPosition = true;
					balls[0].ToStartPosition();
					Life--;
				}
			}
		}

		private void IsBoxesBroken()
		{
			if (boxes.Count == 0) 
			{ 
				LevelFinished = true; 
			}
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
					else playTitle.DecreaseAlpha(5,170);
					if (highScoresTitle.Focused) highScoresTitle.IncreaseAlpha(5, 250);
					else highScoresTitle.DecreaseAlpha(5,170);
					if (exitTitle.Focused) exitTitle.IncreaseAlpha(5, 250);
					else exitTitle.DecreaseAlpha(5,170);
					if (optionTitle.Focused) optionTitle.IncreaseAlpha(5, 250);
                    else optionTitle.DecreaseAlpha(5,170);

					SizeF playTextSize = graphics.MeasureString("Play Game", UpBoardFont);
					graphics.DrawString("Play Game", UpBoardFont, playTitle.TitleBrush, new Point(playTitle.X, playTitle.Y));
					playTitle.Width = (int)playTextSize.Width; 
                    playTitle.Height = (int)playTextSize.Height;

					SizeF highScoresTextSize = graphics.MeasureString("High Scores", UpBoardFont);
					graphics.DrawString("High Scores", UpBoardFont, highScoresTitle.TitleBrush, new PointF(highScoresTitle.X, highScoresTitle.Y));
					highScoresTitle.Width = (int)highScoresTextSize.Width; 
                    highScoresTitle.Height = (int)highScoresTextSize.Height;

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

				graphics.DrawImage(Properties.Resources.backButton, new PointF(850f, 32f));

				graphics.FillRectangle(line.LineBrush, line.LineRectangle);

				graphics.FillRectangle(backGround.BackGroundBrush, backGround.BackGroundPg);

				foreach (DXWall wall in walls)
				{
					graphics.FillRectangle(wall.WallBrush, wall.WallRectangle);
				}

				foreach (Ball ball in balls)
				{
					graphics.FillRectangle(ball.BallBrush, ball.BallRectangle);
				}

				foreach (DXAnimation animation in animations)
				{
					if (animation.Visible)
						graphics.FillRectangle(animation.PutAnimation(), animation.AnimationRectangle);
				}

				foreach (DXBox box in boxes)
				{
					graphics.FillRectangle(box.BoxBrush, box.BoxRectangle);
				}

				foreach (Collectables c in colls)
				{
					if (!c.AtStartPosition)
					{
						graphics.FillRectangle(c.CollectableBrush, c.CollRectangle);
					}
				}

				foreach (DXBox box in boxes)
				{
					graphics.FillRectangle(box.BoxBrush, box.BoxRectangle);
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

				if (resume)
                {
                    float x = ClientSize.Width;
					float y = ClientSize.Height;
 
                    if (countdown > 70)
                    {
                        graphics.DrawString("3", MainTitleFont, Brushes.Red, new PointF((x / 2) - 35, (y / 2) + 110));
                    }
 
                    else if (countdown > 35)
                    {
                        graphics.DrawString("2", MainTitleFont, Brushes.Yellow, new PointF((x / 2) - 35, (y / 2) + 110));
                    }
 
                    else
                    {
                        graphics.DrawString("1", MainTitleFont, Brushes.Green, new PointF((x / 2) - 35, (y / 2) + 110));
                    }
 
 
                    countdown--;
 
                    if (countdown == 0)
                    {
                        resume = !resume;
                        countdown = 105;
                    }
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
			if (e.X >= 845 && e.Y <= 910 && e.Y >= 28 && e.Y <= 65)
			{
				check = true;
				if (conti)
				{
					conti = !conti; // make counti value false   
				}
			}
			if (LevelFinished) 
			{ 
				NextLevel(); 
				return; 
			}

			if (GameOver)
			{
				MainMenuOpening = true; 
				MainMenu = false; 
				GameOver = false; 
				return;
			}

			if (MainMenu)
			{
				if (playTitle.PositionInHere(e.X, e.Y)) 
				{
                    StartGame();
					gameStarted = true;
				}
				else if (exitTitle.PositionInHere(e.X, e.Y)) 
				{ 
					Environment.Exit(0); 
				}
				else if (highScoresTitle.PositionInHere(e.X, e.Y))
				{
					startMenuHighScore = true;
					initialCheck();
				}
				else if (optionTitle.PositionInHere(e.X, e.Y))
                {
                    optionClicked = true;
                }
                else if (backTitle.PositionInHere(e.X, e.Y))
                {
                    optionClicked = false;
                }
				return;
			}

			// ------------------------------ start menu logic ------------------------------ //
			else if(optionClicked)
            {
                if (mutestatusTitle.PositionInHere(e.X, e.Y))
                {
                    mute = !mute;
                }

				if (controlStatusTitle.PositionInHere(e.X, e.Y))
				{
					keyControls = !keyControls;
				}

				if (backgroundStatus.PositionInHere(e.X, e.Y))
				{
					if (backGround.Selection < 3)
					{
						backGround.Selection++;
					}
					else
					{
						backGround.Selection = 1;
					}
				}
 
                else if (backTitle.PositionInHere(e.X, e.Y))
                {
                    optionClicked = false;
                }
                return;
            }
			else if (!conti)
			{
				if (playTitle.PositionInHere(e.X, e.Y)) 
				{
					resume = true;
					conti = !conti; 
				}
				else if (exitTitle.PositionInHere(e.X, e.Y)) 
				{ 
					Environment.Exit(0); 
				}
				else if (highScoresTitle.PositionInHere(e.X, e.Y))
				{
					startMenuHighScore = true;
				}
				else if (optionTitle.PositionInHere(e.X, e.Y))
                {
                    optionClicked = true;
                }
                else if (backTitle.PositionInHere(e.X, e.Y))
                {
                    optionClicked = false;
                }
				return;
			}
			// ------------------------------ end meny logic ------------------------------ //

			if (balls == null) return;

			if (!keyControls)
			{
				foreach (Ball ball in balls)
				{
					ball.AtStartPosition = false;
				}
			}
		}
	}
}