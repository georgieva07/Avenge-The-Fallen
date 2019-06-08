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
        Map map;

		public Form1()
		{
			InitializeComponent();
			this.DoubleBuffered = true;
            this.Height = 500;
            this.Width = 1000;
			avenger = new Avenger("Thor", new Point(this.Width/2, (int)this.Height * 70 / 100));
            timerGenerateObstacles.Interval = timerMapMove.Interval * 54;
            timerMapMove.Enabled = true;
            timerGenerateObstacles.Enabled = true;
            map = new Map(this.Height, this.Width, avenger.Name);
		}

		private void Form1_Resize(object sender, EventArgs e)
		{
			//avenger.Resize(this.Width, this.Height);
			Invalidate(true);
		}

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.White);
			avenger.Draw(e.Graphics);
            map.Draw(e.Graphics);
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

        private void TimerGenerateObstacles_Tick(object sender, EventArgs e)
        {
            map.AddObstacles();
            Invalidate();
        }

        private void TimerMapMove_Tick(object sender, EventArgs e)
        {
            map.moveObstacles();
            Invalidate();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            timerGenerateObstacles.Enabled = !timerGenerateObstacles.Enabled;
            timerMapMove.Enabled = timerGenerateObstacles.Enabled;
        }
    }
}
