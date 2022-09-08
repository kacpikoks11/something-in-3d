using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Shapes;
namespace _3D_engine
{
    internal class Engine
    {
        private int[] _Pos = new int[2];
        public const int Height = 30;
        private double _Angle;
        public const int EyeScreen_dist = 400;

        private List<Block> _Objects = new();

        public Engine()
        {
            Pos[0] = 0;
            Pos[1] = 0;
            Add_Object(new Cuboid(30, 30, 50, 30, 50, 50, 30, 50, 30));

            //Add_Object(new Cuboid(300, 300, 500, 300, 500, 500, 300, 500, 75));
            Add_Object(new Cuboid(80,30,40,30));

            Add_Object(new Cuboid(130, 70, 20, 20));
            _Angle = 0;
        }

        public int[] Pos 
        { 
            get => _Pos; 
            set => _Pos = value;
        }

        public double Angle 
        { 
            get => _Angle; 
            set => _Angle = value; 
        }

        public List<Block> GetListOfObjects()
        {
            return _Objects;
        }
        public void Add_Object(Block Obj)
        {
            _Objects.Add(Obj);
        }

        public void Move(int dist)
        {
            foreach(Block x in _Objects)
            {
                x.Move(dist * Math.Sin(Angle), dist * Math.Cos(Angle));
            }
        }
        public void Rotate(double angle)
        {
            Angle += angle;
        }
       
    }
}
