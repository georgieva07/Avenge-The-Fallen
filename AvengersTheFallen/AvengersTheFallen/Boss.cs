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
		private int Step = 5;

		public Boss(Point position)
		{
			Location = position;
			width = 1000;
			height = 500;
			Damage = 0;
			shots = new List<Weapon>();
			Character = Resources.thanos;
			Character = new Bitmap(Character, new Size(90, 150));
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

		public void Move(Avenger avenger)
		{
			if(avenger.Location.X < Location.X)
			{
				Step = -2;
			}
			else if (avenger.Location.X > Location.X)
			{
				Step = 2;
			}
			else
			{
				Step = 0;
			}
			
			if (Location.X + Step >= 0 && Location.X + Step <= width - Character.Width)
			{
				Location = new Point(Location.X + Step, Location.Y);
			}


		}

		public void MoveShots()
		{
			foreach (Weapon w in shots)
			{
				w.MoveBoss();
			}
			for (int i = 0; i < shots.Count; i++)
			{
				if (shots[i].Location.Y == height)
				{
					shots.RemoveAt(i);
				}
			}
		}

		public void AddShot()
		{
			shots.Add(new Weapon(Name, new Point(Location.X, Location.Y + Character.Height)));
		}

		public void TakeDamage()
		{
			Damage++;
		}

		public Boolean checkCollisionAvengerBossWeapon(Avenger avenger)
		//detektira dali avengerot e pogoden od istrel na boss
		{
			bool t = false;
			for (int j = 0; j < shots.Count; j++)
			{
				Point p = shots[j].Location;
				if (avenger.Location.X > p.X && avenger.Location.X < p.X + shots[j].WeaponImage.Width)
				{
					if (avenger.Location.Y > p.Y && avenger.Location.Y < p.Y + shots[j].WeaponImage.Height)
					{
						shots.RemoveAt(j);
						t = true;
						break;
					}
					if (avenger.Location.Y + avenger.Character.Height > p.Y && avenger.Location.Y + avenger.Character.Height < p.Y + shots[j].WeaponImage.Height)
					{
						shots.RemoveAt(j);
						t = true;
						break;
					}
				}
				if (avenger.Location.X + avenger.Character.Width > p.X && avenger.Location.X + avenger.Character.Width < p.X + shots[j].WeaponImage.Width)
				{
					if (avenger.Location.Y > p.Y && avenger.Location.Y < p.Y + shots[j].WeaponImage.Height)
					{
						shots.RemoveAt(j);
						t = true;
						break;
					}
					if (avenger.Location.Y + avenger.Character.Height > p.Y && avenger.Location.Y + avenger.Character.Height < p.Y + shots[j].WeaponImage.Height)
					{
						shots.RemoveAt(j);
						t = true;
						break;
					}
				}
				if (p.X > avenger.Location.X && p.X < avenger.Location.X + avenger.Character.Width)
				{
					if (p.Y > avenger.Location.Y && p.Y < avenger.Location.Y + avenger.Character.Height)
					{
						shots.RemoveAt(j);
						t = true;
						break;
					}
					if (p.Y + shots[j].WeaponImage.Height > avenger.Location.Y && p.Y + shots[j].WeaponImage.Height < avenger.Location.Y + avenger.Character.Height)
					{
						shots.RemoveAt(j);
						t = true;
						break;
					}
				}
				if (p.X + shots[j].WeaponImage.Width > avenger.Location.X && p.X + shots[j].WeaponImage.Width < avenger.Location.X + avenger.Character.Width)
				{
					if (p.Y > avenger.Location.Y && p.Y < avenger.Location.Y + avenger.Character.Height)
					{
						shots.RemoveAt(j);
						t = true;
						break;
					}
					if (p.Y + shots[j].WeaponImage.Height > avenger.Location.Y && p.Y + shots[j].WeaponImage.Height < avenger.Location.Y + avenger.Character.Height)
					{
						shots.RemoveAt(j);
						t = true;
						break;
					}
				}
			}

			return t;
		}


		public void checkCollisionWeaponBoss(Avenger avenger)
		{
			//detektra dali avengerot go pogodil boss

			for (int j = 0; j < avenger.shots.Count; j++)
			{
				Point p = this.Location;
				if (avenger.shots[j].Location.X > p.X && avenger.shots[j].Location.X < p.X + Character.Width)
				{
					if (avenger.shots[j].Location.Y > p.Y && avenger.shots[j].Location.Y < p.Y + Character.Height)
					{
						avenger.shots.RemoveAt(j);
						Damage++;
						break;
					}
					if (avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height > p.Y && avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height < p.Y + Character.Height)
					{
						avenger.shots.RemoveAt(j);
						Damage++;
						break;
					}
				}
				if (avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width > p.X && avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width < p.X + Character.Width)
				{
					if (avenger.shots[j].Location.Y > p.Y && avenger.shots[j].Location.Y < p.Y + Character.Height)
					{
						avenger.shots.RemoveAt(j);
						Damage++;
						break;
					}
					if (avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height > p.Y && avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height < p.Y + Character.Height)
					{
						avenger.shots.RemoveAt(j);
						Damage++;
						break;
					}
				}
				

				if (p.X > avenger.shots[j].Location.X && p.X < avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width)
				{
					if (p.Y > avenger.shots[j].Location.Y && p.Y < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
					{
						avenger.shots.RemoveAt(j);
						Damage++;
						break;
					}
					if (p.Y + Character.Height > avenger.shots[j].Location.Y && p.Y + Character.Height < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
					{
						avenger.shots.RemoveAt(j);
						Damage++;
						break;
					}
				}
				if (p.X + Character.Width > avenger.shots[j].Location.X && p.X + Character.Width < avenger.shots[j].Location.X + avenger.shots[j].WeaponImage.Width)
				{
					if (p.Y > avenger.shots[j].Location.Y && p.Y < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
					{
						avenger.shots.RemoveAt(j);
						Damage++;
						break;
					}
					if (p.Y + Character.Height > avenger.shots[j].Location.Y && p.Y + Character.Height < avenger.shots[j].Location.Y + avenger.shots[j].WeaponImage.Height)
					{
						avenger.shots.RemoveAt(j);
						Damage++;
						break;
					}
				}
				

			}
		}
	}
}
