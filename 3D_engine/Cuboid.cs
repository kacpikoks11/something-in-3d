using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
using System.Windows.Media;
namespace _3D_engine
{
    internal class Cuboid : Block
    {
        private double[,] _Tops = new double[4,2];
        private int _Height;
        public double[,] Tops { get => _Tops; set => _Tops = value; }

        public Cuboid(int x1, int y1, int x2, int y2, int x3, int y3, int x4, int y4, int Heigth)
        {
            Tops[0, 0] = x1;
            Tops[0, 1] = y1;
            Tops[1, 0] = x2;
            Tops[1, 1] = y2;
            Tops[2, 0] = x3;
            Tops[2, 1] = y3;
            Tops[3, 0] = x4;
            Tops[3, 1] = y4;
            _Height = Heigth;
        }

        public Cuboid(int x, int y, int Height, int len)
        {
            _Height = Height;
            Tops[0, 0] = x;
            Tops[0, 1] = y;
            Tops[1, 0] = x + len;
            Tops[1, 1] = y;
            Tops[2, 0] = x + len;
            Tops[2, 1] = y + len;
            Tops[3, 0] = x;
            Tops[3, 1] = y + len;
        }

        public void GetLine(double x1, double y1, double x2, double y2, double angle, double z1, double z2, ref List<Line> List)
        {
            double[] pair1;
            double[] pair2;

            pair1 = Rotate(x1 - 400, y1 - 290, angle);// translated x1 and y1
            pair2 = Rotate(x2 - 400, y2 - 290, angle); //Translated x2 and y2

            if (pair1[1] < 0 || pair2[1] < 0)
            {
                List.Add(new Line());
                return;
            }

            pair1[0] = pair1[0] * Engine.EyeScreen_dist / pair1[1] + 400;
            pair1[1] = (-z1 + Engine.Height) * Engine.EyeScreen_dist / pair1[1] + 290;
            pair2[0] = pair2[0] * Engine.EyeScreen_dist / pair2[1] + 400;
            pair2[1] = (-z2 + Engine.Height) * Engine.EyeScreen_dist / pair2[1] + 290;

  
            List.Add(new Line() { X1 = pair1[0], Y1 = pair1[1], X2 = pair2[0], Y2 = pair2[1], Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255))});
        }



        //TODO Return Lines in 2D
        public List<Line> ReturnLines(double angle)
        {
            List<Line> ReturnList = new();

            //bottom
            GetLine(Tops[0, 0], Tops[0, 1], Tops[1, 0], Tops[1, 1], angle,0, 0, ref ReturnList);
            GetLine(Tops[1, 0], Tops[1, 1], Tops[2, 0], Tops[2, 1], angle,0, 0, ref ReturnList); 
            GetLine(Tops[2, 0], Tops[2, 1], Tops[3, 0], Tops[3, 1], angle,0, 0, ref ReturnList);
            GetLine(Tops[3, 0], Tops[3, 1], Tops[0, 0], Tops[0, 1], angle,0, 0, ref ReturnList); 
            for(double i = Tops[0, 0]; i < Tops[2, 0]; ++i)
            {
                GetLine(i, Tops[0, 1], Tops[3, 0] + i - Tops[0, 0], Tops[3, 1], angle, 0, 0, ref ReturnList);
            } 


            //top
            GetLine(Tops[0, 0], Tops[0, 1], Tops[1, 0], Tops[1, 1], angle, _Height, _Height, ref ReturnList);
            GetLine(Tops[1, 0], Tops[1, 1], Tops[2, 0], Tops[2, 1], angle, _Height, _Height, ref ReturnList);
            GetLine(Tops[2, 0], Tops[2, 1], Tops[3, 0], Tops[3, 1], angle, _Height, _Height, ref ReturnList);
            GetLine(Tops[3, 0], Tops[3, 1], Tops[0, 0], Tops[0, 1], angle, _Height, _Height, ref ReturnList);
            for (double i = Tops[0, 0]; i < Tops[2, 0]; ++i)
            {
                GetLine(i, Tops[0, 1], Tops[3, 0] + i - Tops[0, 0], Tops[3, 1], angle, _Height, _Height, ref ReturnList);
            }

            //sides
            for (int i = 0; i < 4; ++i)
                GetLine(Tops[i, 0], Tops[i, 1], Tops[i, 0], Tops[i, 1], angle, _Height, 0, ref ReturnList);

            for (double j = Tops[0, 0]; j < Tops[1, 0]; ++j)
                GetLine(j, Tops[0, 1], j, Tops[0, 1], angle, _Height, 0, ref ReturnList);
            for (double j = Tops[1, 1]; j < Tops[2, 1]; ++j)
                GetLine(Tops[1, 0], j, Tops[1, 0], j, angle, _Height, 0, ref ReturnList);
            for (double j = Tops[2, 0]; j > Tops[3, 0]; --j)
                GetLine(j, Tops[2, 1], j, Tops[2, 1], angle, _Height, 0, ref ReturnList);
            for (double j = Tops[3, 1]; j > Tops[0, 1]; --j)
                GetLine(Tops[3, 0], j, Tops[3, 0], j, angle, _Height, 0, ref ReturnList);






            /*
            pair1 = Rotate(Tops[0, 0] - 400, Tops[0, 1] - 290, angle);
            pair2 = Rotate(Tops[1, 0] - 400, Tops[1, 1] - 290, angle);
            ReturnList.Add(new Line() { X1 = pair1[0] + 400, Y1 = pair1[1] + 290, X2 = pair2[0] + 400, Y2 = pair2[1] + 290, Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255))});

            pair1 = Rotate(Tops[1, 0] - 400, Tops[1, 1] - 290, angle);
            pair2 = Rotate(Tops[2, 0] - 400, Tops[2, 1] - 290, angle);
            ReturnList.Add(new Line() { X1 = pair1[0] + 400, Y1 = pair1[1] + 290, X2 = pair2[0] + 400, Y2 = pair2[1] + 290, Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255))});

            pair1 = Rotate(Tops[2, 0] - 400, Tops[2, 1] - 290, angle);
            pair2 = Rotate(Tops[3, 0] - 400, Tops[3, 1] - 290, angle);
            ReturnList.Add(new Line() { X1 = pair1[0] + 400, Y1 = pair1[1] + 290, X2 = pair2[0] + 400, Y2 = pair2[1] + 290, Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255))});

            pair1 = Rotate(Tops[3, 0] - 400, Tops[3, 1] - 290, angle);
            pair2 = Rotate(Tops[0, 0] - 400, Tops[0, 1] - 290, angle);
            ReturnList.Add(new Line() { X1 = pair1[0] + 400, Y1 = pair1[1] + 290, X2 = pair2[0] + 400, Y2 = pair2[1] + 290, Stroke = new SolidColorBrush(Color.FromRgb(255, 255, 255))});
            */
            return ReturnList;
        }

        public void Move(double x, double y)
        {
            for(int i = 0; i < 4; ++i)
            {
                Tops[i, 0] += x;
                Tops[i, 1] += y;
            }
        }
        
        public double[] Rotate(double x, double y, double angle)
        {
            double[] ret = new double[2];
            ret[0] = x * Math.Cos(angle) - y * Math.Sin(angle);
            ret[1] = x * Math.Sin(angle) + y * Math.Cos(angle);
            return ret;
        }
        
    }
}
