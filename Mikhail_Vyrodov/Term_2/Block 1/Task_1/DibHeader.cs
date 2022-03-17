using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1
{
    struct DibHeader
    {
        public uint HeaderSize;
        public uint Width;
        public uint Height;
        public ushort ColorPlanes;
        public ushort BitsPerPixel;
        public uint Compression;
        public uint ImageSize;
        public int[] Palette;
    }
}
