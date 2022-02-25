using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1._1
{
    public class Pixel
    {
        public int R;
        public int G;
        public int B;
        
        public Pixel(int r, int g, int b)
        {
            R = r; 
            G = g; 
            B = b;
        }

        public static Pixel operator +(Pixel first, Pixel second)
        {
            return new Pixel(first.R + second.R, first.G + second.G, first.B + second.B);
        }

        public static Pixel operator /(Pixel first, int second)
        {
            return new Pixel(first.R / second, first.G / second, first.B / second);
        }

        public static bool operator ==(Pixel first, Pixel second)
        {
            return first.R == second.R && first.G == second.G && first.B == second.B;
        }

        public static bool operator !=(Pixel first, Pixel second)
        {
            return !(first == second);
        }

        public static Pixel operator *(Pixel first, int second)
        {
            return new Pixel(first.R * second, first.G * second, first.B * second);
        }

        public Pixel Absolute()
        {
            return new Pixel(Math.Abs(R), Math.Abs(G), Math.Abs(B));
        }
    }
}
