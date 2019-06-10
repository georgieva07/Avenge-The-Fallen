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

        public Map(int h, int w, String l)
        {
            height = h;
            width = w;
            obstacles = new List<Obstacle>();
            enemies = new List<Enemy>();
            level = l;
        }

        public void Draw(Graphics g)
        {
            //ja iscrtuva mapata i preprekite na nea
            //g.DrawImage(backgroundImage, p);
            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].Draw(g);
            }
            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].Draw(g);
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
            //dodava tri random prepreki na mapata
            int a = -1, b = -1, c = -1;
            a = Form1.r.Next(0, width - 100);
            obstacles.Add(new Obstacle(new Point(a, -100), level));
            b = Form1.r.Next(0, width - 10);
            while (b >= a - 100 && b <= a + 100)
            {
                b = Form1.r.Next(0, width - 10);
            }
            obstacles.Add(new Obstacle(new Point(b, -100), level));
            c = Form1.r.Next(0, width - 10);
            while ((c >= a - 100 && c <= a + 100) || (c >= b - 100 && c <= b + 100))
            {
                c = Form1.r.Next(0, width - 10);
            }
            obstacles.Add(new Obstacle(new Point(c, -100), level));
        }


        public void AddEnemies()
        {
            //dodava tri random neprijateli na mapata
            int a = -1, b = -1, c = -1;
            a = Form1.r.Next(0, width - 100);
            enemies.Add(new Enemy(new Point(a, -100), level));
            b = Form1.r.Next(0, width - 10);
            while (b >= a - 100 && b <= a + 100)
            {
                b = Form1.r.Next(0, width - 100);
            }
            enemies.Add(new Enemy(new Point(b, -100), level));
            c = Form1.r.Next(0, width - 10);
            while ((c >= a - 100 && c <= a + 100) || (c >= b - 100 && c <= b + 100))
            {
                c = Form1.r.Next(0, width - 100);
            }
            enemies.Add(new Enemy(new Point(c, -100), level));
        }

        public Boolean checkCollisionObstacle(Avenger avenger)
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

		public void checkCollisionWeapon(Avenger avenger)
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
							break;
						}
						if (avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height > p.Y && avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height < p.Y + obstacles[i].image.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							break;
						}
					}
					if (avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width > p.X && avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width < p.X + obstacles[i].image.Width)
					{
						if (avenger.shots[j].Location.Y > p.Y && avenger.shots[j].Location.Y < p.Y + obstacles[i].image.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							break;
						}
						if (avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height > p.Y && avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height < p.Y + obstacles[i].image.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							break;
						}
					}



					if (p.X > avenger.shots[j].Location.X && p.X < avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width)
					{
						if (p.Y > avenger.shots[j].Location.Y && p.Y < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							break;
						}
						if (p.Y + obstacles[i].image.Height > avenger.shots[j].Location.Y && p.Y + obstacles[i].image.Height < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							break;
						}
					}
					if (p.X + obstacles[i].image.Width > avenger.shots[j].Location.X && p.X + obstacles[i].image.Width < avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width)
					{
						if (p.Y > avenger.shots[j].Location.Y && p.Y < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
							break;
						}
						if (p.Y + obstacles[i].image.Height > avenger.shots[j].Location.Y && p.Y + obstacles[i].image.Height < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
						{
							avenger.shots.RemoveAt(j);
							obstacles.RemoveAt(i);
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