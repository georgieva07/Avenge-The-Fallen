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
        int height;
        int width;
        Image backgroundImage;
        String level;
        List<Point> obstacles;
        public Map(int h, int w, String l)
        {
            height = h;
            width = w;
            obstacles = new List<Point>();
            level = l;
        }

        public void Draw(Graphics g)
        {
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
            Random r = new Random();
            int a = -1, b = -1, c = -1;
            a = r.Next(0, width - 10);
            obstacles.Add(new Point(a, 0));
            b = r.Next(0, width - 10);
            while (b >= a - 100 && b <= a + 100)
            {
                b = r.Next(0, width - 10);
            }
            obstacles.Add(new Point(b, 0));
            c = r.Next(0, width - 10);
            while ((c >= a - 100 && c <= a + 100) || (c >= b - 100 && c <= b + 100))
            {
                c = r.Next(0, width - 10);
            }
            obstacles.Add(new Point(c, 0));
        }
    }
}
