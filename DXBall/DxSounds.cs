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
using System.Media;

namespace DXBall
{
	public class DXSounds
	{
		public DXSounds()
		{
			/// <summary>
			/// Default Constructor s
			/// </summary>
		}
		/// <summary>
		/// Fetches and Play the Sound
		/// </summary>
		public void SoundPlay()
		{
			System.Media.SoundPlayer player = new System.Media.SoundPlayer("Form1.Resources.Hit.wav");
			player.Load();
			player.Play();
		}
	}
}
