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
				WeaponImage = Resources.strange;
			}
			else if (Owner == "Thor")
			{
				WeaponImage = Resources.mjolnir;
			}
			else if (Owner == "Hulk")
			{
				WeaponImage = Resources.strange;
			}
			else if (Owner == "ScarletWitch")
			{
				WeaponImage = Resources.strange;
			}
			else if (Owner == "CaptainAmerica")
			{
				WeaponImage = Resources.strange;
			}
			else if (Owner == "DrStrange")
			{
				WeaponImage = Resources.strange;
			}
			WeaponImage = new Bitmap(WeaponImage, new Size(30, 40));
		}

		public void Draw(Graphics g)
		{
			g.DrawImage(WeaponImage, Location);
		}
		
		public void Move()
		{
			if(Location.Y - 10 >= 0)
			{
				Location = new Point(Location.X, Location.Y - 10);
			}
		}

        public void MoveEnemy()
        {
            if (Location.Y - 10 <=500)
            {
                Location = new Point(Location.X, Location.Y + 6);
            }
        }
	}

}
