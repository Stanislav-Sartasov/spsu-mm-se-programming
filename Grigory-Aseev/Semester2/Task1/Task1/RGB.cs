using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class RGB
    {
        public byte[] ArrayRGB = new byte[3];

        public RGB(byte[] data)
        {
            ArrayRGB[0] = data[0];
            ArrayRGB[1] = data[1];
            ArrayRGB[2] = data[2];
        }
    }
}
