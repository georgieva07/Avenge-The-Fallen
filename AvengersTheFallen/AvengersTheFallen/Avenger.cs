using AvengersTheFallen.Properties;
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
		public Point Position { get; set; }
		public string Name { get; set; }
		public Image Character { get; set; }

		public Avenger(string name, Point position)
		{
			Name = name;
			Position = position;
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
		}

		public void Resize(int width, int height)
		{
			Position = new Point(width / 2, (int)height-Character.Height);
		}
		public void Draw(Graphics g)
		{
			g.DrawImage(Character, new Point(Position.X - Character.Width/2, Position.Y - Character.Height/2));
		}

		public void Move(int width, int height, string direction)
		{
			if (direction == "Left")
			{
				if (Position.X - 4 >= 0)
				{
					Position = new Point(Position.X - 4, Position.Y);
				}
				
			}
			if (direction == "Right")
			{
				if (Position.X + 4 <= width)
				{
					Position = new Point(Position.X + 4, Position.Y);
				}
			}
		}

	}
}
