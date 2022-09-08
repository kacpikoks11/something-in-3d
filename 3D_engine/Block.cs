using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;

namespace _3D_engine
{
    internal interface Block
    {
        public List<Line> ReturnLines(double angle);
        public void Move(double x, double y);
        public double[] Rotate(double x, double y, double angle);
    }
}
