namespace DXBall
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.gameTimer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// gameTimer;
			// 
			this.gameTimer.Enabled = true;
			this.gameTimer.Interval = 30;
			this.gameTimer.Tick += new System.EventHandler(this.GameTimerTick);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Black;
			this.ClientSize = new System.Drawing.Size(1280, 886);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.CreateBackBuffer);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.ScreenPaint);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
			this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Mouse_Click);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Mouse_Move);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Key_Down);
			this.Resize += new System.EventHandler(this.CreateBackBuffer);
			this.ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Timer gameTimer;
	}
}

