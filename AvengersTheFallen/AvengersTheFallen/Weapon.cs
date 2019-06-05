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
		}


		
	}

}
