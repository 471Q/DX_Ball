using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DXBall
{
    /// <summary>
    /// inheritance from Form
    /// </summary>
    public partial class PlayerName : Form
    {
		public static string passingName;
        /// <summary>
        /// Player Name input
        /// </summary>
        public PlayerName()
        {
            InitializeComponent();
        }

        private void PlayerName_Load(object sender, EventArgs e)
        {
            
        }
        /// <summary>
        /// Button click to enter the main menu
		/// click this button and the user will directly go to main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
			passingName = textBox1.Text;
            Form1 openForm = new Form1();
            openForm.Show();
            Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}
