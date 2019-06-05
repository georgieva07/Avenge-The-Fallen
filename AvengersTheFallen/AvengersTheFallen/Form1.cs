using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvengersTheFallen
{
	public partial class Form1 : Form
	{
		private Avenger avenger;

		public Form1()
		{
			InitializeComponent();
			this.DoubleBuffered = true;
			avenger = new Avenger("Thor", new Point(this.Width/2, (int)this.Height * 70 / 100));
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			avenger.Resize(this.Width, this.Height);
			Invalidate(true);
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.White);
			avenger.Draw(e.Graphics);
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyData == Keys.A)
			{
				avenger.Move(this.Width, this.Height, "Left");
			}
			if (e.KeyData == Keys.D)
			{
				avenger.Move(this.Width, this.Height, "Right");
			}

			Invalidate(true);
		}
	}
}
