using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    struct BitmapHeader
    {
        public char[] Name;
        public uint Size;
        public int Garbage;
        public uint ImageOffset;
    }
}
