using AvengersTheFallen.Properties;
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
        public Image enemyImage;
        public int Progress { get; set; }

        public Map(int h, int w, String l)
        {
            height = h;
            width = w;
            obstacles = new List<Obstacle>();
            enemies = new List<Enemy>();
            level = l;
            Progress = 0;
            if (level == "Thor")
            {
                enemyImage = new Bitmap(Resources.ThorEnemy, new Size(50, 90));
                backgroundImage = new Bitmap(Resources.ThorBackground, new Size(1000, 500));
            }
            else if (level == "Hulk")
            {
                enemyImage = new Bitmap(Resources.HulkEnemy, new Size(46, 74));
                backgroundImage = new Bitmap(Resources.HulkBackground, new Size(1000, 500));
            }
            else if (level == "IronMan")
            {
                enemyImage = new Bitmap(Resources.IronManEnemy, new Size(46, 74));
                backgroundImage = new Bitmap(Resources.IronManBackground, new Size(1000, 500));
            }
			else if (level == "ScarletWitch")
			{
				enemyImage = new Bitmap(Resources.IronManEnemy, new Size(46, 74));
				backgroundImage = new Bitmap(Resources.IronManBackground, new Size(1000, 500));
			}
			else if (level == "CaptainAmerica")
			{
				enemyImage = new Bitmap(Resources.HulkEnemy, new Size(46, 74));
				backgroundImage = new Bitmap(Resources.CaptainAmericaBackground, new Size(1000, 500));
			}
			else if (level == "DrStrange")
			{
				enemyImage = new Bitmap(Resources.IronManEnemy, new Size(46, 74));
				backgroundImage = new Bitmap(Resources.IronManBackground, new Size(1000, 500));
			}
		}

        public void Draw(Graphics g)
        {
            //ja iscrtuva mapata i preprekite na nea
            g.DrawImage(backgroundImage, new Point(0,0));
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
            int n = Form1.r.Next(1, 6);
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
            int n = Form1.r.Next(1, 4);
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
                    new_enemies.Add(new Enemy(new Point(k, -100), level, enemyImage));
                }
            }
            foreach (Enemy j in new_enemies)
            {
                enemies.Add(j);
            }
        }

        public bool checkCollisionAvengerObstacle(Avenger avenger)
        {
            //vraka true ako avengerot se sudri so nekoja prepreka
            Rectangle a = new Rectangle(avenger.Location.X, avenger.Location.Y, avenger.Character.Width, avenger.Character.Height);
            bool t = false;
            int i;
            for (i = 0; i < obstacles.Count; i++)
            {
                Rectangle b = new Rectangle(obstacles[i].Location.X, obstacles[i].Location.Y, obstacles[i].image.Width, obstacles[i].image.Height);
                if (a.IntersectsWith(b))
                {
                    t = true;
                    break;
                }
            }
            if (t)
                obstacles.RemoveAt(i);
            return t;
        }

        public bool checkCollisionAvengerEnemy(Avenger avenger)
        {
            //vraka true ako avengerot se sudri so nekoj neprijatel
            Rectangle a = new Rectangle(avenger.Location.X, avenger.Location.Y, avenger.Character.Width, avenger.Character.Height);
            bool t = false;
            int i;
            for (i = 0; i < enemies.Count; i++)
            {
                Rectangle b = new Rectangle(enemies[i].Location.X, enemies[i].Location.Y, enemies[i].image.Width, enemies[i].image.Height);
                if (a.IntersectsWith(b))
                {
                    t = true;
                    enemies.RemoveAt(i);
                    break;
                }
            }
            return t;
        }

        public void checkCollisionWeaponObstacle(Avenger avenger)
        //detektira dali avengerot pogodil nekoja precka
        {
            for (int i = obstacles.Count - 1; i > -1; i--)
            {
                Rectangle a = new Rectangle(obstacles[i].Location.X, obstacles[i].Location.Y, obstacles[i].image.Width, obstacles[i].image.Height);
                for (int j = avenger.shots.Count - 1; j > -1; j--)
                {
                    Rectangle b = new Rectangle(avenger.shots[j].Location.X, avenger.shots[j].Location.Y, avenger.shots[j].WeaponImage.Width, avenger.shots[j].WeaponImage.Height);
                    if (a.IntersectsWith(b))
                    {
                        avenger.shots.RemoveAt(j);
                        obstacles.RemoveAt(i);
                        Progress++;
                        break;
                    }
                }
            }
        }

        public bool checkCollisionAvengerEnemyWeapon(Avenger avenger)
        //detektira dali avengerot e pogoden od istrel na enemy
        {
            bool t = false;
            Rectangle a = new Rectangle(avenger.Location.X, avenger.Location.Y, avenger.Character.Width, avenger.Character.Height);
            for (int i = enemies.Count - 1; i > -1; i--)
            {
                for (int j = enemies[i].shots.Count - 1; j > -1; j--)
                {
                    Rectangle b = new Rectangle(enemies[i].shots[j].Location.X, enemies[i].shots[j].Location.Y, enemies[i].shots[j].WeaponImage.Width, enemies[i].shots[j].WeaponImage.Height);
                    if (a.IntersectsWith(b))
                    {
                        enemies[i].shots.RemoveAt(j);
                        t = true;
                        break;
                    }
                }
            }
            return t;
        }
        public void checkCollisionWeaponEnemy(Avenger avenger)
        {
            //detektra dali avengerot pogodil enemy
            for (int i = enemies.Count - 1; i > -1; i--)
            {
                Rectangle a = new Rectangle(enemies[i].Location.X, enemies[i].Location.Y, enemies[i].image.Width, enemies[i].image.Height);
                for (int j = avenger.shots.Count - 1; j > -1; j--)
                {
                    Rectangle b = new Rectangle(avenger.shots[j].Location.X, avenger.shots[j].Location.Y, avenger.shots[j].WeaponImage.Width, avenger.shots[j].WeaponImage.Height);
                    if (a.IntersectsWith(b))
                    {
                        avenger.shots.RemoveAt(j);
                        enemies.RemoveAt(i);
                        Progress++;
                        break;
                    }
                }
            }
        }

        public Obstacle findNearObstacleHulk(Avenger avenger)
        {
            Rectangle a = new Rectangle(new Point(avenger.Location.X - 75, avenger.Location.Y - 75), new Size(avenger.Character.Width + 150, avenger.Character.Height + 150));
            Obstacle t = null;
            int i;
            for (i = 0; i < obstacles.Count; i++)
            {
                Rectangle b = new Rectangle(obstacles[i].Location, new Size(obstacles[i].image.Width, obstacles[i].image.Height));
                if (a.IntersectsWith(b))
                    break;
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