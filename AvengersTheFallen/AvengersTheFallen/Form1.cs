using AvengersTheFallen.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AvengersTheFallen
{
    public partial class Form1 : Form
	{
		private Avenger avenger;
        private Map map;
        public static Random r;
		//private int tickCount;

		public Form1()
		{
            InitializeComponent();
            this.DoubleBuffered = true;
            r = new Random();
			//tickCount = 0;
            timerGenerateObstacles.Interval = timerMapMove.Interval * 54;
            timerMapMove.Enabled = true;
            timerGenerateObstacles.Enabled = true;
            this.Height = 500;
            this.Width = 1000;
            Form1_Resize(null, null);
            avenger = new Avenger("Thor", new Point(1000 / 2, 500 - 90));
            map = new Map(500, 1000, avenger.Name);
            TimerGenerateObstacles_Tick(null, null);
        }

		private void Form1_Resize(object sender, EventArgs e)
		{
            int x = 0, y = 0;
            if(this.Width> (this.Height-40) * 2)
            {
                panel1.Height = this.Height - 40 ;
                panel1.Width = panel1.Height * 2;
                y = 0;
                x = (this.Width - panel1.Width) / 2;
            }
            else
            {
                panel1.Width = this.Width;
                panel1.Height = this.Width / 2 - 20;
                x = 0;
                y = (this.Height - 40 - panel1.Height) / 2;
            }
            panel1.Location = new Point(x, y);
			panel1.Invalidate(true);
		}

		private void Form1_KeyDown(object sender, KeyEventArgs e)
		{
			if(e.KeyData == Keys.Left)
			{
				avenger.Move("Left");
			}
			if (e.KeyData == Keys.Right) 
			{
				avenger.Move("Right");
			}
			if(e.KeyData == Keys.Space)
			{
				avenger.AddShot();	
			}
            panel1.Invalidate(true);
        }


        private void TimerGenerateObstacles_Tick(object sender, EventArgs e)
        {
            map.AddObstacles();
            Invalidate(true);
        }

        private void TimerMapMove_Tick(object sender, EventArgs e)
        {
            map.moveObstacles();
			avenger.MoveShots();
			map.checkCollisionWeapon(avenger);
            Boolean t = map.checkCollisionObstacle(avenger);
            if (t) { }
            //avenger takes damage
            panel1.Invalidate(true);
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            e.Graphics.ScaleTransform((float)(panel1.Width / 1000.0F), ((float)(panel1.Height) / 500.0F));
            avenger.Draw(e.Graphics);
            map.Draw(e.Graphics);
        }


		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.White);
		}
	}
}
