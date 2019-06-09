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

        public Map(int h, int w, String l)
        {
            height = h;
            width = w;
            obstacles = new List<Obstacle>();
            level = l;
        }

        public void Draw(Graphics g)
        {
            //ja iscrtuva mapata i preprekite na nea
            //g.DrawImage(backgroundImage, p);
            for (int i = 0; i < obstacles.Count; i++)
            {
                obstacles[i].Draw(g);
            }
        }

        public void moveObstacles()
        {
            //gi pomestuva site prepreki nadolu
            int i = 0;
            for (i = 0; i < obstacles.Count; i++)
            {
                Point k = obstacles[i].position;
                k.Y = k.Y + 4;
                if (k.Y < height)
                    obstacles[i].position = k;
                else
                {
                    obstacles.RemoveAt(i);
                    i--;
                }
            }
        }

		
        public void AddObstacles()
        {
            //dodava tri random prepreki na mapata
            int a = -1, b = -1, c = -1;
            a = Form1.r.Next(0, width - 100);
            obstacles.Add(new Obstacle(new Point(a, -100), level));
            b = Form1.r.Next(0, width - 10);
            while (b >= a - 100 && b <= a + 100)
            {
                b = Form1.r.Next(0, width - 10);
            }
            obstacles.Add(new Obstacle(new Point(b, -100), level));
            c = Form1.r.Next(0, width - 10);
            while ((c >= a - 100 && c <= a + 100) || (c >= b - 100 && c <= b + 100))
            {
                c = Form1.r.Next(0, width - 10);
            }
            obstacles.Add(new Obstacle(new Point(c, -100), level));
        }

        public Boolean checkCollisionObstacle(Avenger avenger)
        {
            //vraka true ako avengerot se sudri so nekoja prepreka
            //raboti samo ako avenger e pomal od preprekata
            Point a = avenger.Position;
            Boolean t = false;
            int i;
            for (i = 0; i < obstacles.Count; i++)
            {
                Point p = obstacles[i].position;
                if (a.X > p.X && a.X < p.X + obstacles[i].image.Width)
                {
                    if (a.Y > p.Y && a.Y < p.Y + obstacles[i].image.Height)
                    {
                        t = true;
                        break;
                    }
                    if (a.Y + avenger.Character.Height > p.Y && a.Y + avenger.Character.Height < p.Y + obstacles[i].image.Height)
                    {
                        t = true;
                        break;
                    }
                }
                if (a.X + avenger.Character.Width > p.X && a.X + avenger.Character.Width < p.X + obstacles[i].image.Width)
                {
                    if (a.Y > p.Y && a.Y < p.Y + obstacles[i].image.Height)
                    {
                        t = true;
                        break;
                    }
                    if (a.Y + avenger.Character.Height > p.Y && a.Y + avenger.Character.Height < p.Y + obstacles[i].image.Height)
                    {
                        t = true;
                        break;
                    }
                }


                if (p.X > a.X && p.X < a.X + avenger.Character.Width)
                {
                    if (p.Y > a.Y && p.Y < a.Y + avenger.Character.Height)
                    {
                        t = true;
                        break;
                    }
                    if (p.Y + obstacles[i].image.Height > a.Y && p.Y + obstacles[i].image.Height < a.Y + avenger.Character.Height)
                    {
                        t = true;
                        break;
                    }
                }
                if (p.X + obstacles[i].image.Width > a.X && p.X + obstacles[i].image.Width < a.X + avenger.Character.Width)
                {
                    if (p.Y > a.Y && p.Y < a.Y + avenger.Character.Height)
                    {
                        t = true;
                        break;
                    }
                    if (p.Y + obstacles[i].image.Height > a.Y && p.Y + obstacles[i].image.Height < a.Y + avenger.Character.Height)
                    {
                        t = true;
                        break;
                    }
                }
            }
            if (t)
                obstacles.RemoveAt(i);
            return t;
        }
    }
}