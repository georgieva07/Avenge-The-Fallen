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
		private Boss boss;
        public static Random r;

		public Form1()
		{
            InitializeComponent();
            this.DoubleBuffered = true;
            r = new Random();
            timerGenerateObstacles.Interval = timerMapMove.Interval * 108;
            timerEnemyShoot.Interval = timerMapMove.Interval * 54;
            timerMapMove.Enabled = true;
            timerEnemyShoot.Enabled = true;
            timerGenerateObstacles.Enabled = true;
            this.Height = 500;
            this.Width = 1000;
            Form1_Resize(null, null);
            avenger = new Avenger("Thor", new Point(1000 / 2, 500 - 90));
			boss = new Boss(new Point(1000/2, 0), r);
            map = new Map(500, 1000, avenger.Name);
            TimerGenerateObstacles_Tick(null, null);
        }

		private void Form1_Resize(object sender, EventArgs e)
		{
            int x = 0, y = 0;
            if(this.Width - 16 >= (this.Height - 40) * 2)
            {
                panel1.Height = this.Height - 40;
                panel1.Width = panel1.Height * 2;
                x = (this.Width - 16 - panel1.Width) / 2;
                y = 0;
            }
            else
            {
                panel1.Width = this.Width - 16;
                panel1.Height = panel1.Width / 2;
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
                //if (avenger.Name == "Hulk")
                    //avenger.AddShotHulk(map.findNearObstacleHulk(avenger));
                //else
				    avenger.AddShot();	
			}
            panel1.Invalidate(true);
        }


        private void TimerGenerateObstacles_Tick(object sender, EventArgs e)
        {
			if (map.Final == false)
			{
				map.AddObstacles();
				map.AddEnemies();
				panel1.Invalidate();
			}
        }

        private void TimerMapMove_Tick(object sender, EventArgs e)
        {
			if (map.Final == false)
			{
				map.moveObstacles();
				map.moveEnemies();
				map.moveEnemyShots();
				avenger.MoveShots();
				map.checkCollisionWeaponObstacle(avenger);
				map.checkCollisionWeaponEnemy(avenger);

				if (map.checkCollisionAvengerObstacle(avenger) || map.checkCollisionAvengerEnemy(avenger) || map.checkCollisionAvengerEnemyWeapon(avenger))
				{
					avenger.TakeDamage();
				}

				if (avenger.Damage == 3)
				{
					timerMapMove.Stop();
					GameOver form = new GameOver();
					if (form.ShowDialog() == DialogResult.OK)
					{
						Reset();
					}
					else
					{
						this.Close();
					}
				}

				if(map.Progress == 20)
				{
					map.Final = true;
					boss.Final = true;
				}
			}
			else
			{
				avenger.MoveShots();

			}
            panel1.Invalidate(true);
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
			e.Graphics.Clear(Color.White);
			e.Graphics.ScaleTransform((float)(panel1.Width / 1000.0F), ((float)(panel1.Height) / 500.0F));
			avenger.Draw(e.Graphics);

			if(map.Final==false)
				map.Draw(e.Graphics);

			if(boss.Final)
				boss.Draw(e.Graphics);
        }

		private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.Black);
		}

        private void TimerEnemyShoot_Tick(object sender, EventArgs e)
        {
            map.shoot();
        }

		public void Reset()
		{
			avenger = new Avenger("Hulk", new Point(1000 / 2, 500 - 90));
			map = new Map(500, 1000, avenger.Name);
			TimerGenerateObstacles_Tick(null, null);
			timerMapMove.Start();
		}

		private void timerBossMove_Tick(object sender, EventArgs e)
		{
			if(boss.Final == true)
			{
				boss.Move();
			}
		}
	}
}
