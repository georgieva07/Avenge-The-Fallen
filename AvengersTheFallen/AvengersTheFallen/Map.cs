using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvengersTheFallen
{
    public class Map
    {
        public int height;//visina na prozorecot
        public int width;//sirina na prozorecot
        Image backgroundImage;//slika za pozadina
        string level;//nivo
        List<Obstacle> obstacles;//lista koordinati na site prepreki
        List<Enemy> enemies;//lista koordinati na site neprijateli
		public bool Final = false;
		public int Progress { get; set; }

        public Map(int h, int w, String l)
        {
            height = h;
            width = w;
            obstacles = new List<Obstacle>();
            enemies = new List<Enemy>();
            level = l;
			Progress = 0;
        }

        public void Draw(Graphics g)
        {
			//ja iscrtuva mapata i preprekite na nea
			//g.DrawImage(backgroundImage, p);
			if (Final == false)
			{
				for (int i = 0; i < obstacles.Count; i++)
				{
					obstacles[i].Draw(g);
				}
				for (int i = 0; i < enemies.Count; i++)
				{
					enemies[i].Draw(g);
				}
			}
        }

        public void moveObstacles()
        {
            //gi pomestuva site prepreki nadolu
            int i = 0;
            for (i = 0; i < obstacles.Count; i++)
            {
                Point k = obstacles[i].Location;
                k.Y = k.Y + 4;
                if (k.Y < height)
                    obstacles[i].Location = k;
                else
                {
                    obstacles.RemoveAt(i);
                    i--;
                }
            }
        }

        public void moveEnemies()
        {
            //gi pomestuva site neprijateli nadolu
            int i = 0;
            for (i = 0; i < enemies.Count; i++)
            {
                Point k = enemies[i].Location;
                k.Y = k.Y + 4;
                if (k.Y < height)
                    enemies[i].Location = k;
                else
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }
        }

        public void moveEnemyShots()
        {
            foreach (Enemy e in enemies)
            {
                foreach (Weapon w in e.shots)
                {
                    w.MoveEnemy();
                }
            }
        }

        public void shoot()
        {
            foreach (Enemy e in enemies)
            {
                e.shoot();
            }
        }

        public void AddObstacles()
        {
            //dodava random prepreki na mapata
            int n = Form1.r.Next(1, 5);
            List<Obstacle> new_obstacles = new List<Obstacle>();
            for (int i = 0; i < n; i++)
            {
                bool t = true;
                int k = Form1.r.Next(0, width - 100);
                foreach (Obstacle j in new_obstacles)
                {
                    if (k <= j.Location.X + 100 && k >= j.Location.X - 100)
                    {
                        t = false;
                        break;
                    }
                }
                if (!t)
                {
                    i--;
                }
                else
                {
                    new_obstacles.Add(new Obstacle(new Point(k, -100), level));
                }
            }
            foreach (Obstacle j in new_obstacles)
            {
                obstacles.Add(j);
            }
        }

        public void AddEnemies()
        {
            //dodava random neprijateli na mapata
            int n = Form1.r.Next(1, 5);
            List<Enemy> new_enemies = new List<Enemy>();
            for (int i = 0; i < n; i++)
            {
                bool t = true;
                int k = Form1.r.Next(0, width - 100);
                foreach (Enemy j in new_enemies)
                {
                    if (k < j.Location.X + 100 && k > j.Location.X - 100)
                    {
                        t = false;
                        break;
                    }
                }
                if (!t)
                {
                    i--;
                }
                else
                {
                    new_enemies.Add(new Enemy(new Point(k, -100), level));
                }
            }
            foreach (Enemy j in new_enemies)
            {
                enemies.Add(j);
            }
        }

        public Boolean checkCollisionAvengerObstacle(Avenger avenger)
		{
			//vraka true ako avengerot se sudri so nekoja prepreka
			Point a = avenger.Location;
			Boolean t = false;
			int i;
			for (i = 0; i < obstacles.Count; i++)
			{
				Point p = obstacles[i].Location;
				if (a.X > p.X && a.X < p.X + obstacles[i].image.Width)
				{
					if (a.Y > p.Y && a.Y < p.Y + obstacles[i].image.Height)
					{
						t = true;
						break;
					}
					if (a.Y + avenger.Character.Height > p.Y && a.Y + avenger.Character.Height < p.Y + obstacles[i].image.Height)
					{
						t = true;
						break;
					}
				}
				if (a.X + avenger.Character.Width > p.X && a.X + avenger.Character.Width < p.X + obstacles[i].image.Width)
				{
					if (a.Y > p.Y && a.Y < p.Y + obstacles[i].image.Height)
					{
						t = true;
						break;
					}
					if (a.Y + avenger.Character.Height > p.Y && a.Y + avenger.Character.Height < p.Y + obstacles[i].image.Height)
					{
						t = true;
						break;
					}
				}


				if (p.X > a.X && p.X < a.X + avenger.Character.Width)
				{
					if (p.Y > a.Y && p.Y < a.Y + avenger.Character.Height)
					{
						t = true;
						break;
					}
					if (p.Y + obstacles[i].image.Height > a.Y && p.Y + obstacles[i].image.Height < a.Y + avenger.Character.Height)
					{
						t = true;
						break;
					}
				}
				if (p.X + obstacles[i].image.Width > a.X && p.X + obstacles[i].image.Width < a.X + avenger.Character.Width)
				{
					if (p.Y > a.Y && p.Y < a.Y + avenger.Character.Height)
					{
						t = true;
						break;
					}
					if (p.Y + obstacles[i].image.Height > a.Y && p.Y + obstacles[i].image.Height < a.Y + avenger.Character.Height)
					{
						t = true;
						break;
					}
				}
			}
			if (t)
				obstacles.RemoveAt(i);
			return t;
		}

		public Boolean checkCollisionAvengerEnemy(Avenger avenger)
		{
			//vraka true ako avengerot se sudri so nekoja prepreka
			Point a = avenger.Location;
			Boolean t = false;
			int i;
			for (i = 0; i < enemies.Count; i++)
			{
				Point p = enemies[i].Location;
				if (a.X > p.X && a.X < p.X + enemies[i].image.Width)
				{
					if (a.Y > p.Y && a.Y < p.Y + enemies[i].image.Height)
					{
						t = true;
						break;
					}
					if (a.Y + avenger.Character.Height > p.Y && a.Y + avenger.Character.Height < p.Y + enemies[i].image.Height)
					{
						t = true;
						break;
					}
				}
				if (a.X + avenger.Character.Width > p.X && a.X + avenger.Character.Width < p.X + enemies[i].image.Width)
				{
					if (a.Y > p.Y && a.Y < p.Y + enemies[i].image.Height)
					{
						t = true;
						break;
					}
					if (a.Y + avenger.Character.Height > p.Y && a.Y + avenger.Character.Height < p.Y + enemies[i].image.Height)
					{
						t = true;
						break;
					}
				}


				if (p.X > a.X && p.X < a.X + avenger.Character.Width)
				{
					if (p.Y > a.Y && p.Y < a.Y + avenger.Character.Height)
					{
						t = true;
						break;
					}
					if (p.Y + enemies[i].image.Height > a.Y && p.Y + enemies[i].image.Height < a.Y + avenger.Character.Height)
					{
						t = true;
						break;
					}
				}
				if (p.X + enemies[i].image.Width > a.X && p.X + enemies[i].image.Width < a.X + avenger.Character.Width)
				{
					if (p.Y > a.Y && p.Y < a.Y + avenger.Character.Height)
					{
						t = true;
						break;
					}
					if (p.Y + enemies[i].image.Height > a.Y && p.Y + enemies[i].image.Height < a.Y + avenger.Character.Height)
					{
						t = true;
						break;
					}
				}
			}
			if (t)
				enemies.RemoveAt(i);
			return t;
		}

		public void checkCollisionWeaponObstacle(Avenger avenger)
		//detektira dali avengerot pogodil nekoja precka
		{
			for (int i = 0; i < obstacles.Count; i++)
			{
				for (int j = 0; j < avenger.shots.Count; j++)
				{
					Point p = obstacles[i].Location;
					if (avenger.shots[j].Location.X > p.X && avenger.shots[j].Location.X < p.X + obstacles[i].image.Width)
					{
						if (avenger.shots[j].Location.Y > p.Y && avenger.shots[j].Location.Y < p.Y + obstacles[i].image.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							Progress++;
							break;
						}
						if (avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height > p.Y && avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height < p.Y + obstacles[i].image.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							Progress++;
							break;
						}
					}
					if (avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width > p.X && avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width < p.X + obstacles[i].image.Width)
					{
						if (avenger.shots[j].Location.Y > p.Y && avenger.shots[j].Location.Y < p.Y + obstacles[i].image.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							Progress++;
							break;
						}
						if (avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height > p.Y && avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height < p.Y + obstacles[i].image.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							Progress++;
							break;
						}
					}



					if (p.X > avenger.shots[j].Location.X && p.X < avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width)
					{
						if (p.Y > avenger.shots[j].Location.Y && p.Y < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							Progress++;
							break;
						}
						if (p.Y + obstacles[i].image.Height > avenger.shots[j].Location.Y && p.Y + obstacles[i].image.Height < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							Progress++;
							break;
						}
					}
					if (p.X + obstacles[i].image.Width > avenger.shots[j].Location.X && p.X + obstacles[i].image.Width < avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width)
					{
						if (p.Y > avenger.shots[j].Location.Y && p.Y < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							Progress++;
							break;
						}
						if (p.Y + obstacles[i].image.Height > avenger.shots[j].Location.Y && p.Y + obstacles[i].image.Height < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							Progress++;
							break;
						}
					}
				}

			}
		}

		public Boolean checkCollisionAvengerEnemyWeapon(Avenger avenger)
		//detektira dali avengerot e pogoden od istrel na enemy
		{
			bool t = false;
			for (int i = 0; i < enemies.Count; i++)
			{
				for (int j = 0; j < enemies[i].shots.Count; j++)
				{
					Point p = enemies[i].shots[j].Location;
					if (avenger.Location.X > p.X && avenger.Location.X < p.X + enemies[i].shots[j].WeaponImage.Width)
					{
						if (avenger.Location.Y > p.Y && avenger.Location.Y < p.Y + enemies[i].shots[j].WeaponImage.Height)
						{
							enemies[i].shots.RemoveAt(j);
							t = true;
							break;
						}
						if (avenger.Location.Y + avenger.Character.Height > p.Y && avenger.Location.Y + avenger.Character.Height < p.Y + enemies[i].shots[j].WeaponImage.Height)
						{
							enemies[i].shots.RemoveAt(j);
							t = true;
							break;
						}
					}
					if (avenger.Location.X + avenger.Character.Width > p.X && avenger.Location.X + avenger.Character.Width < p.X + enemies[i].shots[j].WeaponImage.Width)
					{
						if (avenger.Location.Y > p.Y && avenger.Location.Y < p.Y + enemies[i].shots[j].WeaponImage.Height)
						{
							enemies[i].shots.RemoveAt(j);
							t = true;
							break;
						}
						if (avenger.Location.Y + avenger.Character.Height > p.Y && avenger.Location.Y + avenger.Character.Height < p.Y + enemies[i].shots[j].WeaponImage.Height)
						{
							enemies[i].shots.RemoveAt(j);
							t = true;
							break;
						}
					}



					if (p.X > avenger.Location.X && p.X < avenger.Location.X + avenger.Character.Width)
					{
						if (p.Y > avenger.Location.Y && p.Y < avenger.Location.Y + avenger.Character.Height)
						{
							enemies[i].shots.RemoveAt(j);
							t = true;
							break;
						}
						if (p.Y + enemies[i].shots[j].WeaponImage.Height > avenger.Location.Y && p.Y + enemies[i].shots[j].WeaponImage.Height < avenger.Location.Y + avenger.Character.Height)
						{
							avenger.shots.RemoveAt(j);
							t = true;
							break;
						}
					}
					if (p.X + enemies[i].shots[j].WeaponImage.Width > avenger.Location.X && p.X + enemies[i].shots[j].WeaponImage.Width < avenger.Location.X + avenger.Character.Width)
					{
						if (p.Y > avenger.Location.Y && p.Y < avenger.Location.Y + avenger.Character.Height)
						{
							enemies[i].shots.RemoveAt(j);
							t = true;
							break;
						}
						if (p.Y + enemies[i].shots[j].WeaponImage.Height > avenger.Location.Y && p.Y + enemies[i].shots[j].WeaponImage.Height < avenger.Location.Y + avenger.Character.Height)
						{
							enemies[i].shots.RemoveAt(j);
							t = true;
							break;
						}
					}
				}

			}

			return t;
		}
		public void checkCollisionWeaponEnemy(Avenger avenger)
		{
			//detektra dali avengerot pogodil enemy
			for (int i = 0; i < enemies.Count; i++)
			{
				for (int j = 0; j < avenger.shots.Count; j++)
				{
					Point p = enemies[i].Location;
					if (avenger.shots[j].Location.X > p.X && avenger.shots[j].Location.X < p.X + enemies[i].image.Width)
					{
						if (avenger.shots[j].Location.Y > p.Y && avenger.shots[j].Location.Y < p.Y + enemies[i].image.Height)
						{
							avenger.shots.RemoveAt(j);
							enemies.RemoveAt(i);
							Progress++;
							break;
						}
						if (avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height > p.Y && avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height < p.Y + enemies[i].image.Height)
						{
							avenger.shots.RemoveAt(j);
							enemies.RemoveAt(i);
							Progress++;
							break;
						}
					}
					if (avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width > p.X && avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width < p.X + enemies[i].image.Width)
					{
						if (avenger.shots[j].Location.Y > p.Y && avenger.shots[j].Location.Y < p.Y + enemies[i].image.Height)
						{
							avenger.shots.RemoveAt(j);
							enemies.RemoveAt(i);
							Progress++;
							break;
						}
						if (avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height > p.Y && avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height < p.Y + enemies[i].image.Height)
						{
							avenger.shots.RemoveAt(j);
							enemies.RemoveAt(i);
							Progress++;
							break;
						}
					}



					if (p.X > avenger.shots[j].Location.X && p.X < avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width)
					{
						if (p.Y > avenger.shots[j].Location.Y && p.Y < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
						{
							avenger.shots.RemoveAt(j);
							enemies.RemoveAt(i);
							Progress++;
							break;
						}
						if (p.Y + enemies[i].image.Height > avenger.shots[j].Location.Y && p.Y + enemies[i].image.Height < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
						{
							avenger.shots.RemoveAt(j);
							enemies.RemoveAt(i);
							Progress++;
							break;
						}
					}
					if (p.X + enemies[i].image.Width > avenger.shots[j].Location.X && p.X + enemies[i].image.Width < avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width)
					{
						if (p.Y > avenger.shots[j].Location.Y && p.Y < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
						{
							avenger.shots.RemoveAt(j);
							enemies.RemoveAt(i);
							Progress++;
							break;
						}
						if (p.Y + enemies[i].image.Height > avenger.shots[j].Location.Y && p.Y + enemies[i].image.Height < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
						{
							avenger.shots.RemoveAt(j);
							enemies.RemoveAt(i);
							Progress++;
							break;
						}
					}
				}

			}
		}

		public Obstacle findNearObstacleHulk (Avenger avenger)
		{
            Point a = new Point(avenger.Location.X - 20, avenger.Location.Y - 20);
            Obstacle t = null;
            int i;
            for (i = 0; i < obstacles.Count; i++)
            {
                Point p = obstacles[i].Location;
                if (a.X > p.X && a.X < p.X + obstacles[i].image.Width)
                {
                    if (a.Y > p.Y && a.Y < p.Y + obstacles[i].image.Height)
                    {
                        break;
                    }
                    if (a.Y + avenger.Character.Height + 40 > p.Y && a.Y + avenger.Character.Height + 40 < p.Y + obstacles[i].image.Height)
                    {
                        break;
                    }
                }
                if (a.X + avenger.Character.Width + 40 > p.X && a.X + avenger.Character.Width + 40 < p.X + obstacles[i].image.Width)
                {
                    if (a.Y > p.Y && a.Y < p.Y + obstacles[i].image.Height)
                    {
                        break;
                    }
                    if (a.Y + avenger.Character.Height + 40 > p.Y && a.Y + avenger.Character.Height + 40 < p.Y + obstacles[i].image.Height)
                    {
                        break;
                    }
                }


                if (p.X > a.X && p.X < a.X + avenger.Character.Width + 40)
                {
                    if (p.Y > a.Y && p.Y < a.Y + avenger.Character.Height + 40)
                    {
                        break;
                    }
                    if (p.Y + obstacles[i].image.Height > a.Y && p.Y + obstacles[i].image.Height < a.Y + avenger.Character.Height + 40)
                    {
                        break;
                    }
                }
                if (p.X + obstacles[i].image.Width > a.X && p.X + obstacles[i].image.Width  < a.X + avenger.Character.Width + 40)
                {
                    if (p.Y > a.Y && p.Y < a.Y + avenger.Character.Height + 40)
                    {
                        break;
                    }
                    if (p.Y + obstacles[i].image.Height > a.Y && p.Y + obstacles[i].image.Height < a.Y + avenger.Character.Height + 40)
                    {
                        break;
                    }
                }
            }
            if (i != obstacles.Count)
            {
                t = new Obstacle(obstacles[i].Location, "Hulk");
                t.image = obstacles[i].image;
                obstacles.RemoveAt(i);
            }
            return t;
        }
    }
}