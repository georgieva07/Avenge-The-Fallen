using AvengersTheFallen.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvengersTheFallen
{
    class Enemy
    {
        public Point Location { get; set; }
        public Image image;
        string level;
        public List<Weapon> shots;
        public Enemy(Point Location, string level)
        {
            this.level = level;
            this.Location = Location;
            if(level == "Thor")
            {
                image = new Bitmap(Resources.ThorEnemy, new Size(90, 90));
                shots = new List<Weapon>();
            }
        }

        public void shoot()
        {
            Weapon g = new Weapon(level, new Point(Location.X + image.Width/2 - 20, Location.Y + image.Height));
            g.WeaponImage = new Bitmap(Resources.ThorEnemyWeapon, new Size(40, 40));
            shots.Add(g);
        }


        public void MoveShots()
        {
            foreach (Weapon w in shots)
            {
                w.MoveEnemy();
            }
            for (int i = 0; i < shots.Count; i++)
            {
                if (shots[i].Location.Y == 0)
                {
                    shots.RemoveAt(i);
                }
            }
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(image, Location);
            foreach (Weapon w in shots)
            {
                w.Draw(g);
            }
        }

        public void Move()
        {
            Location = new Point(Location.X, Location.Y + 4);
        }
    }
}
