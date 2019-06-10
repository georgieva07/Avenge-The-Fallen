using AvengersTheFallen.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AvengersTheFallen
{
    class Obstacle
    {
        public Point Location { get; set; }
        public Image image;
        string level;
        enum Thor
        {
            car,
            bus,
            motorcycle
        }
        public Obstacle(Point p, string level)
        {
            Location = p;
            this.level = level;
            if(level == "Thor")
            {
                int k = Form1.r.Next(0, Enum.GetNames(typeof(Thor)).Length);
                Thor e = (Thor)k;
                if (Thor.car == e)
                    image = new Bitmap(Resources.car,new Size(50,100));//prezemeno od [www.kisspng.com]
                if (Thor.bus == e)
                    image = new Bitmap(Resources.bus, new Size(50, 150));//prezemeno od [www.pngkit.com]
                if (Thor.motorcycle == e)
                    image = new Bitmap(Resources.motorcycle, new Size(30, 60));//prezemeno od [ya-webdesign.com]
            }
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(image, Location.X, Location.Y, image.Width, image.Height);
        }
    }
}
