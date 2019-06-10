﻿using AvengersTheFallen.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvengersTheFallen
{
	public class Avenger
	{
		public Point Location { get; set; }
		public string Name { get; set; }
		public Image Character { get; set; }
        public int width, height;
		public List<Weapon> shots { get; set; }

		public Avenger(string name, Point position)
		{
			Name = name;
			Location = position;
            width = 1000;
            height = 500;
			shots = new List<Weapon>();
			if (Name == "IronMan")
			{
				Character = Resources.ironman;
			}
			else if (Name == "Thor")
			{
				Character = Resources.thor;
			}
			else if (Name == "Hulk")
			{
				Character = Resources.hulk;
			}
			else if (Name == "ScarletWitch")
			{
				Character = Resources.scarletwitch;
			}
			else if (Name == "CaptainAmerica")
			{
				Character = Resources.captainamerica;
			}
			else if (Name == "DrStrange")
			{
				Character = Resources.drstrange;
			}
            Character = new Bitmap(Character, new Size(40, 90));
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

		public void Move(string direction)
		{
			if (direction == "Left")
			{
				if (Location.X - 10 >= 0)
				{
                    Location = new Point(Location.X - 10, Location.Y);
				}
				
			}
			if (direction == "Right")
			{
				if (Location.X + 4 <= width - Character.Width)
				{
                    Location = new Point(Location.X + 10, Location.Y);
				}
			}
		}

		public void MoveShots()
		{
			foreach (Weapon w in shots)
			{
				w.Move();
			}
			for(int i = 0; i<shots.Count; i++)
			{
				if(shots[i].Location.Y == 0)
				{
					shots.RemoveAt(i);
				}
			}
		}

		public void AddShot()
		{
	        shots.Add(new Weapon(Name, new Point(Location.X, Location.Y - 20)));
		}

        public void AddShotHulk(Obstacle o)
        {
            if(o!=null)
                shots.Add(new Weapon(Name, new Point(Location.X, Location.Y - 20), o));
        }
    }
}
