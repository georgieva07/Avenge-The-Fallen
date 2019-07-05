using AvengersTheFallen.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvengersTheFallen
{
    public class Enemy
    {
        public Point Location { get; set; }
        public Image image;
        string level;
        public List<Weapon> shots;
        public Image WeaponImage;
        public Enemy(Point Location, string level, Image image)
        {
            this.level = level;
            this.Location = Location;
            this.image = image;
            shots = new List<Weapon>();
            if (level == "Thor")
            {
                WeaponImage = new Bitmap(Resources.ThorEnemyWeapon, new Size(30, 50));
            }
            if (level == "Hulk")
            {
                WeaponImage = new Bitmap(Resources.HulkEnemyWeapon, new Size(30, 41));
            }
            if (level == "IronMan")
            {
                WeaponImage = new Bitmap(Resources.IronManEnemyWeapon, new Size(30, 41));
            }
        }

        public void shoot()
        {
            Weapon g = new Weapon(level, new Point(Location.X + image.Width / 2 - 20, Location.Y + image.Height));
            g.WeaponImage = WeaponImage;
            shots.Add(g);
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(image, Location);
            foreach (Weapon w in shots)
            {
                w.Draw(g);
            }
        }
    }
}
