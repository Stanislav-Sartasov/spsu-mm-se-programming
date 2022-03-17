using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    interface IFilter
    {
        void PixelConverse(ref RGB[,] pixels, int height, int width);
    }
}
