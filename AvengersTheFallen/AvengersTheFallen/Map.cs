using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvengersTheFallen
{
    class Map
    {
        int height;//visina na prozorecot
        int width;//sirina na prozorecot
        Image backgroundImage;//slika za pozadina
        String level;//nivo
        List<Point> obstacles;//lista koordinati na site prepreki
        public Map(int h, int w, String l)
        {
            height = h;
            width = w;
            obstacles = new List<Point>();
            level = l;
        }

        public void Draw(Graphics g)
        {
            //ja iscrtuva mapata i preprekite na nea
            Point p = new Point(0, 0);
            //g.DrawImage(backgroundImage, p);
            Brush h = new SolidBrush(Color.Red);
            for (int i = 0; i < obstacles.Count; i++)
            {
                g.FillRectangle(h, obstacles[i].X, obstacles[i].Y, 50, 100);
            }
            h.Dispose();
        }

        public void moveObstacles()
        {
            //gi pomestuva site prepreki nadolu
            int i = 0;
            for(i = 0; i < obstacles.Count; i++)
            {
                Point k = obstacles[i];
                k.Y = k.Y + 4;
                if (k.Y < height)
                    obstacles[i] = k;
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
            Random r = new Random();
            int a = -1, b = -1, c = -1;
            a = r.Next(0, width - 10);
            obstacles.Add(new Point(a, -100));
            b = r.Next(0, width - 10);
            while (b >= a - 100 && b <= a + 100)
            {
                b = r.Next(0, width - 10);
            }
            obstacles.Add(new Point(b, -100));
            c = r.Next(0, width - 10);
            while ((c >= a - 100 && c <= a + 100) || (c >= b - 100 && c <= b + 100))
            {
                c = r.Next(0, width - 10);
            }
            obstacles.Add(new Point(c, -100));
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
                Point p = obstacles[i];
                if(a.X > p.X && a.X < p.X + 50)
                {
                    if(a.Y > p.Y && a.Y < p.Y + 100)
                    {
                        t = true;
                        break;
                    }
                    if (a.Y + avenger.Character.Height > p.Y && a.Y + avenger.Character.Height < p.Y + 100)
                    {
                        t = true;
                        break;
                    }
                }
                if (a.X + avenger.Character.Width > p.X && a.X + avenger.Character.Width < p.X + 50)
                {
                    if (a.Y > p.Y && a.Y < p.Y + 100)
                    {
                        t = true;
                        break;
                    }
                    if (a.Y + avenger.Character.Height > p.Y && a.Y + avenger.Character.Height < p.Y + 100)
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
