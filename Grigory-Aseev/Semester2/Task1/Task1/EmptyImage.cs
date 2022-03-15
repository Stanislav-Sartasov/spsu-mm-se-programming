using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class EmptyImage : Image
    {
        private EmptyImage(BmpHeader bmpHeader, RGB[,] pixels) : base(bmpHeader, pixels)
        {
            filterSuccess = false;
        }

        public static EmptyImage CreateEmptyImage(int width = 960, int height = 1024)
        {
            BmpHeader bmpHeader = new BmpHeader();

            bmpHeader.BfType[0] = 66;
            bmpHeader.BfType[1] = 77;
            bmpHeader.BfSize = 54 + 3 * width * height;
            bmpHeader.BiHeight = height;
            bmpHeader.BiWidth = width;
            bmpHeader.Reserved = 0;
            bmpHeader.BfOffbits = 54;
            bmpHeader.BiSize = 40;
            bmpHeader.BiPlanes = 1;
            bmpHeader.BiBitCount = 24;
            bmpHeader.BiCompression = 0;
            bmpHeader.BiSizeImage = 0;
            bmpHeader.BiXPelsPerMeter = 0;
            bmpHeader.BiYPelsPerMeter = 0;
            bmpHeader.BiClrUsed = 0;
            bmpHeader.BiClrImportant = 0;

            RGB[,] pixels = new RGB[height, width];

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    pixels[i, j] = new RGB(new byte[] { 0, 0, 0 });
                }
            }

            return new EmptyImage(bmpHeader, pixels);
        }

        public override void ApplyFilter(string? nameFilter)
        {
            if (nameFilter is null)
            {
                Console.WriteLine("The name of the filter was not entered.");
            }

            Console.WriteLine("This is an empty image that does not support changing with filters.");
        }
    }
}
