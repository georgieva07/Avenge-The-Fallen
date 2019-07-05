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
		public string Prev;
        public Image backgroundImage;
        public bool[] won;
        public enum PanelView
        {
            menu,
            level_select,
            game,
			game_over
        };
        public PanelView panelView;
		public Form1()
		{
            InitializeComponent();
            won = new bool[7];                ;
            WindowState = FormWindowState.Maximized;
            this.DoubleBuffered = true;
            r = new Random();
            panelView = PanelView.menu;
            timerGenerateObstacles.Interval = timerMapMove.Interval * 108;
            timerEnemyShoot.Interval = timerMapMove.Interval * 54;
            this.Height = 500;
            this.Width = 1000;
            Form1_Resize(null, null);
			KeyPreview = true;
            backgroundImage = new Bitmap(Resources.menuBackground, new Size(1000, 500));
        }

		private void Form1_Resize(object sender, EventArgs e)
		{
            panel1.Controls.Clear();
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
            if (avenger == null)
                return;
			if(e.KeyData == Keys.Left)
			{
				avenger.Move("Left");
			}
			if (e.KeyData == Keys.Right) 
			{
				avenger.Move("Right");
			}
            panel1.Invalidate(true);
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                avenger.AddShot();
                if (avenger.Name == "Hulk")
                    avenger.AddShotHulk(map.findNearObstacleHulk(avenger));

            }
            panel1.Invalidate(true);
        }

        private void TimerGenerateObstacles_Tick(object sender, EventArgs e)
        {
            if (map.Final == false)
            {
				map.Generate();
                
                panel1.Invalidate();
            }
        }

        private void TimerGenerateEnemies_Tick(object sender, EventArgs e)
        {
			/*if (map.Final == false)
            {
                map.AddEnemies();
                panel1.Invalidate();
            }*/
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
	
				if (avenger.Damage == 5)
                {
                    timerMapMove.Enabled = false;
                    timerEnemyShoot.Enabled = false;
                    timerGenerateObstacles.Enabled = false;
                    panelView = PanelView.game_over;
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
				boss.Move(avenger);
				if(r.Next(20) == 1)
				{
					boss.AddShot();
				}
				boss.MoveShots();
				boss.checkCollisionWeaponBoss(avenger);
				if (boss.checkCollisionAvengerBossWeapon(avenger))
				{
					avenger.TakeBossDamage();
				}
				if (avenger.BossDamage == 5)
				{
					panelView = PanelView.game_over;
				}

				if (boss.Damage == 10)
				{
					panelView = PanelView.level_select;
                    if(avenger.Name == "Hulk")
                    {
                        won[1] = true;
                    }
                    if (avenger.Name == "Thor")
                    {
                        won[2] = true;
                    }
                    if (avenger.Name == "DrStrange")
                    {
                        won[3] = true;
                    }
                    if (avenger.Name == "ScarletWitch")
                    {
                        won[4] = true;
                    }
                    if (avenger.Name == "CaptainAmerica")
                    {
                        won[5] = true;
                    }
                    if (avenger.Name == "IronMan")
                    {
                        won[6] = true;
                    }
                }
			}
            panel1.Invalidate(true);
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
			e.Graphics.Clear(Color.White);
			e.Graphics.ScaleTransform((float)(panel1.Width / 1000.0F), ((float)(panel1.Height) / 500.0F));
            if (panelView == PanelView.game)
            {
                if (map.Final == false)
                    map.Draw(e.Graphics);
                if (boss.Final)
                    boss.Draw(e.Graphics);
                avenger.Draw(e.Graphics);
            }
            else if(panelView == PanelView.menu)
            {
                Button play, quit;
                e.Graphics.DrawImage(backgroundImage, new Point(0, 0));
                play = new Button();
                play.FlatStyle = FlatStyle.Popup;
                play.BackColor = Color.FromArgb(100, Color.White);
                play.Width = panel1.Width / 10;
                play.Height = panel1.Height / 10;
                play.Font = new Font(play.Font.FontFamily, play.Height / 3);
                play.Text = "PLAY";
                play.Location = new Point((panel1.Width/2 - play.Width / 2), panel1.Height / 2 - play.Height);
                play.Click += new System.EventHandler(this.panel1PlayButton);
                panel1.Controls.Add(play);

                quit = new Button();
                quit.FlatStyle = FlatStyle.Popup;
                quit.BackColor = Color.FromArgb(100, Color.White);
                quit.Width = panel1.Width / 10;
                quit.Height = panel1.Height / 10;
                quit.Font = new Font(quit.Font.FontFamily, quit.Height / 3);
                quit.Text = "QUIT";
                quit.Location = new Point((panel1.Width/2 - quit.Width / 2), panel1.Height / 2 + quit.Height);
                quit.Click += new System.EventHandler(this.panel1QuitButton);
                panel1.Controls.Add(quit);
            }
            else if (panelView == PanelView.level_select)
            {
                Button hulk, thor, strange, scarlet_witch, captain_america, iron_man, back;
                e.Graphics.DrawImage(backgroundImage, new Point(0, 0));
                hulk = new Button();
                hulk.FlatStyle = FlatStyle.Popup;
                hulk.BackColor = Color.FromArgb(100, Color.White);
                if(won[1])
                    hulk.BackColor = Color.FromArgb(100, Color.Green);
                hulk.Width = panel1.Width / 10;
                hulk.Height = panel1.Height / 10;
                hulk.Font = new Font(hulk.Font.FontFamily, hulk.Height / 3);
                hulk.Text = "Hulk";
                hulk.Location = new Point(((panel1.Width - (hulk.Width * 6 + panel1.Width / 100)) / 2 + 0 * (panel1.Width / 10 + panel1.Width / 100)), panel1.Height / 2 - hulk.Height);
                hulk.Click += new System.EventHandler(this.panel1HulkButton);
                panel1.Controls.Add(hulk);

                thor = new Button();
                thor.FlatStyle = FlatStyle.Popup;
                thor.BackColor = Color.FromArgb(100, Color.White);
                if (won[2])
                    thor.BackColor = Color.FromArgb(100, Color.Green);
                thor.Width = panel1.Width / 10;
                thor.Height = panel1.Height / 10;
                thor.Font = new Font(thor.Font.FontFamily, thor.Height / 3);
                thor.Text = "Thor";
                thor.Location = new Point(((panel1.Width - (hulk.Width * 6 + panel1.Width / 100))/2 + 1 * (panel1.Width / 10 + panel1.Width / 100)), panel1.Height / 2 - thor.Height);
                thor.Click += new System.EventHandler(this.panel1ThorButton);
                panel1.Controls.Add(thor);

                strange = new Button();
                strange.FlatStyle = FlatStyle.Popup;
                strange.BackColor = Color.FromArgb(100, Color.White);
                if (won[3])
                    strange.BackColor = Color.FromArgb(100, Color.Green);
                strange.Width = panel1.Width / 10;
                strange.Height = panel1.Height / 10;
                strange.Font = new Font(strange.Font.FontFamily, strange.Height / 3);
                strange.Text = "Strange";
                strange.Location = new Point(((panel1.Width - (hulk.Width * 6 + panel1.Width / 100)) / 2 + 2 * (panel1.Width / 10 + panel1.Width / 100)), panel1.Height / 2 - strange.Height);
                strange.Click += new System.EventHandler(this.panel1StrangeButton);
                panel1.Controls.Add(strange);

                scarlet_witch = new Button();
                scarlet_witch.FlatStyle = FlatStyle.Popup;
                scarlet_witch.BackColor = Color.FromArgb(100, Color.White);
                if (won[4])
                    scarlet_witch.BackColor = Color.FromArgb(100, Color.Green);
                scarlet_witch.Width = panel1.Width / 10;
                scarlet_witch.Height = panel1.Height / 10;
                scarlet_witch.Font = new Font(scarlet_witch.Font.FontFamily, scarlet_witch.Height / 3);
                scarlet_witch.Text = "Scarlet";
                scarlet_witch.Location = new Point(((panel1.Width - (hulk.Width * 6 + panel1.Width / 100)) / 2 + 3 * (panel1.Width / 10 + panel1.Width / 100)), panel1.Height / 2 - scarlet_witch.Height);
                scarlet_witch.Click += new System.EventHandler(this.panel1ScarletButton);
                panel1.Controls.Add(scarlet_witch);

                captain_america = new Button();
                captain_america.FlatStyle = FlatStyle.Popup;
                captain_america.BackColor = Color.FromArgb(100, Color.White);
                if (won[5])
                    captain_america.BackColor = Color.FromArgb(100, Color.Green);
                captain_america.Width = panel1.Width / 10;
                captain_america.Height = panel1.Height / 10;
                captain_america.Font = new Font(captain_america.Font.FontFamily, captain_america.Height / 4.5F);
                captain_america.Text = "Captain America";
                captain_america.Location = new Point(((panel1.Width - (hulk.Width * 6 + panel1.Width / 100)) / 2 + 4 * (panel1.Width / 10 + panel1.Width / 100)), panel1.Height / 2 - captain_america.Height);
                captain_america.Click += new System.EventHandler(this.panel1CaptainAmericaButton);
                panel1.Controls.Add(captain_america);

                iron_man = new Button();
                iron_man.FlatStyle = FlatStyle.Popup;
                iron_man.BackColor = Color.FromArgb(100, Color.White);
                if (won[6])
                    iron_man.BackColor = Color.FromArgb(100, Color.Green);
                iron_man.Width = panel1.Width / 10;
                iron_man.Height = panel1.Height / 10;
                iron_man.Font = new Font(iron_man.Font.FontFamily, iron_man.Height / 4.5F);
                iron_man.Text = "Iron man";
                iron_man.Location = new Point(((panel1.Width - (hulk.Width * 6 + panel1.Width / 100)) / 2 + 5 * (panel1.Width / 10 + panel1.Width / 100)), panel1.Height / 2 - captain_america.Height);
                iron_man.Click += new System.EventHandler(this.panel1IronManButton);
                panel1.Controls.Add(iron_man);

                back = new Button();
                back.FlatStyle = FlatStyle.Popup;
                back.BackColor = Color.FromArgb(100, Color.White);
                back.Width = panel1.Width / 10;
                back.Height = panel1.Height / 10;
                back.Font = new Font(back.Font.FontFamily, back.Height / 3);
                back.Text = "Back";
                back.Location = new Point((panel1.Width / 2 - back.Width / 2), panel1.Height / 2 + back.Height);
                back.Click += new System.EventHandler(this.panel1BackButton);
                panel1.Controls.Add(back);
            }
			else if (panelView == PanelView.game_over)
            {
                e.Graphics.DrawImage(backgroundImage, new Point(0, 0));
                e.Graphics.DrawImage(new Bitmap(Resources.GameOver, new Size(362,124)), new Point(319, 188));
                Button start_over, quit;

				start_over = new Button();
                start_over.FlatStyle = FlatStyle.Popup;
                start_over.BackColor = Color.FromArgb(100, Color.White);
                start_over.Width = panel1.Width / 10;
				start_over.Height = panel1.Height / 10;
				start_over.Font = new Font(start_over.Font.FontFamily, start_over.Height / 4.5F);
				start_over.Text = "Start Over";
				start_over.Location = new Point(panel1.Width*20/100, panel1.Height - panel1.Height*20/100);
				start_over.Click += new System.EventHandler(this.panel1StartOverButton);
				panel1.Controls.Add(start_over);

				quit = new Button();
                quit.FlatStyle = FlatStyle.Popup;
                quit.BackColor = Color.FromArgb(100, Color.White);
                quit.Width = panel1.Width / 10;
				quit.Height = panel1.Height / 10;
				quit.Font = new Font(quit.Font.FontFamily, quit.Height / 4.5F);
				quit.Text = "Quit";
				quit.Location = new Point(panel1.Width - panel1.Width * 20 / 100, panel1.Height - panel1.Height * 20 / 100);
				quit.Click += new System.EventHandler(this.panel1QuitButton);
				panel1.Controls.Add(quit);

			}
		}

        private void panel1IronManButton(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panelView = PanelView.game;
            timerMapMove.Enabled = true;
            timerEnemyShoot.Enabled = true;
            timerGenerateObstacles.Enabled = true;
            avenger = new Avenger("IronMan", new Point(1000 / 2, 500 - 120));
			Prev = "IronMan";
            boss = new Boss(new Point(1000 / 2, 0));
            map = new Map(500, 1000, avenger.Name, r);
        }

        private void panel1CaptainAmericaButton(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panelView = PanelView.game;
            timerMapMove.Enabled = true;
            timerEnemyShoot.Enabled = true;
            timerGenerateObstacles.Enabled = true;
			avenger = new Avenger("CaptainAmerica", new Point(1000 / 2, 500 - 120));
			Prev = "CaptainAmerica";
			boss = new Boss(new Point(1000 / 2, 0));
            map = new Map(500, 1000, avenger.Name, r);
        }

        private void panel1ScarletButton(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panelView = PanelView.game;
            timerMapMove.Enabled = true;
            timerEnemyShoot.Enabled = true;
            timerGenerateObstacles.Enabled = true;
            avenger = new Avenger("ScarletWitch", new Point(1000 / 2, 500 - 120));
			Prev = "ScarletWitch";
			boss = new Boss(new Point(1000 / 2, 0));
            map = new Map(500, 1000, avenger.Name, r);
        }

        private void panel1StrangeButton(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panelView = PanelView.game;
            timerMapMove.Enabled = true; 
            timerEnemyShoot.Enabled = true;
            timerGenerateObstacles.Enabled = true;
            avenger = new Avenger("DrStrange", new Point(1000 / 2, 500 - 120));
			Prev = "DrStrange";
			boss = new Boss(new Point(1000 / 2, 0));
            map = new Map(500, 1000, avenger.Name, r);
        }

        private void panel1ThorButton(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panelView = PanelView.game;
            timerMapMove.Enabled = true;
            timerEnemyShoot.Enabled = true;
            timerGenerateObstacles.Enabled = true;
            avenger = new Avenger("Thor", new Point(1000 / 2, 500 - 120));
			Prev = "Thor";
			boss = new Boss(new Point(1000 / 2, 0));
            map = new Map(500, 1000, avenger.Name, r);
        }

        private void panel1HulkButton(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panelView = PanelView.game;
            timerMapMove.Enabled = true;
            timerEnemyShoot.Enabled = true;
            timerGenerateObstacles.Enabled = true;
            avenger = new Avenger("Hulk", new Point(1000 / 2, 500 - 120));
			Prev = "Hulk";
			boss = new Boss(new Point(1000 / 2, 0));
            map = new Map(500, 1000, avenger.Name, r);
            //map.Final = true;
            //boss.Final = true;
        }

        private void panel1BackButton(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            panelView = PanelView.menu;
        }

        private void panel1QuitButton(object sender, System.EventArgs e)
        {
            panel1.Controls.Clear();
            Close();
        }

        private void panel1PlayButton(object sender, System.EventArgs e)
        {
            panel1.Controls.Clear();
            panelView = PanelView.level_select;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
		{
			e.Graphics.Clear(Color.Black);
		}

        private void TimerEnemyShoot_Tick(object sender, EventArgs e)
        {
            map.shoot();
        }

		private void panel1StartOverButton(object sender, EventArgs e)
		{
			panel1.Controls.Clear();
			panelView = PanelView.game;
            timerGenerateObstacles.Interval = timerMapMove.Interval * 108;
            timerMapMove.Enabled = true;
			timerEnemyShoot.Enabled = true;
			timerGenerateObstacles.Enabled = true;
			avenger = new Avenger(Prev, new Point(1000 / 2, 500 - 120));
			boss = new Boss(new Point(1000 / 2, 0));
			map = new Map(500, 1000, avenger.Name, r);
		}
    }
}
