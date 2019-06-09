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
        public Point position { get; set; }
        Image obstacle;
        public Obstacle(Point p)
        {
            position = p;
        }
    }
}
