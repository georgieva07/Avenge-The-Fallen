using AvengersTheFallen.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvengersTheFallen
{
    public class Weapon
    {
        public Image WeaponImage { get; set; }
        public string Owner { get; set; }
        public Point Location { get; set; }

        public Weapon(string owner, Point location)
        {
            Owner = owner;
            Location = location;

			if (Owner == "IronMan")
			{
				WeaponImage = Resources.iman;
				WeaponImage = new Bitmap(WeaponImage, new Size(15, 50));
			}
			else if (Owner == "Thor")
			{
				WeaponImage = Resources.mjolnir;
				WeaponImage = new Bitmap(WeaponImage, new Size(20, 30));
			}
			else if (Owner == "Hulk")
			{
				WeaponImage = Resources.fist;
				WeaponImage = new Bitmap(WeaponImage, new Size(20, 30));
			}
			else if (Owner == "ScarletWitch")
			{
				WeaponImage = Resources.red;
				WeaponImage = new Bitmap(WeaponImage, new Size(20, 30));
			}
			else if (Owner == "CaptainAmerica")
			{
				WeaponImage = Resources.shield;
				WeaponImage = new Bitmap(WeaponImage, new Size(20, 30));
			}
			else if (Owner == "DrStrange")
			{
				WeaponImage = Resources.strange;
				WeaponImage = new Bitmap(WeaponImage, new Size(20, 30));
			}
			else if (Owner == "Thanos")
			{
				WeaponImage = Resources.strange;
				WeaponImage = new Bitmap(WeaponImage, new Size(20, 30));
			}
			
		
        }

        public Weapon(string owner, Point location, Obstacle o)
        {
            WeaponImage = o.image;
            WeaponImage.RotateFlip(RotateFlipType.Rotate90FlipNone);
            Owner = owner;
            Location = location;
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(WeaponImage, Location);
        }

        public void Move()
        {
            if (Location.Y >= 0)
            {
                Location = new Point(Location.X, Location.Y - 10);
            }
        }

        public void MoveEnemy()
        {
            if (Location.Y <= 500)
            {
                Location = new Point(Location.X, Location.Y + 10);
            }
        }

        public void MoveBoss()
        {
            if (Location.Y + 10 <= 500)
            {
                Location = new Point(Location.X, Location.Y + 10);
            }
        }
    }

}
