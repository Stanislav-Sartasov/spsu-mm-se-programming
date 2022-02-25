using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1._1
{
    public class SobelYFilter : KernelFilter
    {
        public SobelYFilter()
        {
            kernel = new int[]
            {
                -1, -2, -1,
                0, 0, 0,
                1, 2, 1
            };

            divisor = 1;
        }

        public new void ProcessBitmap(Bitmap bmp)
        {
            new GrayScale().ProcessBitmap(bmp);
            
            base.ProcessBitmap(bmp);
        }
    }
}
