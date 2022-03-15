using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    internal class Gauss : MatrixFilter
    {
        internal Gauss()
        {
            matrix = new double[]
            {
                0.000789, 0.006581, 0.013347, 0.006581, 0.000789,
                0.006581, 0.054901, 0.111345, 0.054901, 0.006581,
                0.013347, 0.111345, 0.225821, 0.111345, 0.013347,
                0.006581, 0.054901, 0.111345, 0.054901, 0.006581,
                0.000789, 0.006581, 0.013347, 0.006581, 0.000789
            };
        }
    }
}
