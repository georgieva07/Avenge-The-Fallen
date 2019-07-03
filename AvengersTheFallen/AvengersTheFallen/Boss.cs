using AvengersTheFallen.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvengersTheFallen
{
	public class Boss
	{
		public Point Location { get; set; }
		public string Name = "Thanos";
		public Image Character { get; set; }
		public int width, height;
		public List<Weapon> shots { get; set; }
		public int Damage { get; set; }
		public bool Final = false;
		private Random random;
		private int Movements = 1;

		public Boss(Point position, Random r)
		{
			Location = position;
			width = 1000;
			height = 500;
			Damage = 0;
			random = r;
			shots = new List<Weapon>();
			Character = Resources.thanos;
			Character = new Bitmap(Character, new Size(90, 200));
		}

		public void Draw(Graphics g)
		{
			g.DrawImage(Character, new Point(Location.X, Location.Y));
			g.DrawRectangle(new Pen(Color.Red), Location.X, Location.Y, Character.Width, Character.Height);

			foreach (Weapon w in shots)
			{
				w.Draw(g);
			}
		}

		public void Move()
		{
			int step = 1;
			
			if (Location.Y + step >= 0)
			{
				Location = new Point(Location.X, Location.Y-10);
				if (Location.Y == 0)
				{
					step *= -1;
				}

			}

			else if (Location.Y + step <= step*20 + Character.Height)
			{
				Location = new Point(Location.X, Location.Y + 10);
				if(Location.Y == Character.Height + 20 * step)
				{
					step *= -1;
				}
	
			}

		}

		public void MoveShots()
		{
			foreach (Weapon w in shots)
			{
				w.Move();
			}
			for (int i = 0; i < shots.Count; i++)
			{
				if (shots[i].Location.Y == 0)
				{
					shots.RemoveAt(i);
				}
			}
		}

		public void AddShot()
		{
			shots.Add(new Weapon(Name, new Point(Location.X, Location.Y - 20)));
		}

		public void TakeDamage()
		{
			Damage++;
		}
	}
}
