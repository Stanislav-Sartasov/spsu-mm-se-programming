using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Filter
    {
        double[,] Gray = { { 0.2126, 0.7152, 0.0722 } };
        double[,] Median = { { 0 } };
        double[,] SobelX = { { -1, 0, 1 }, { -2, 0, 2 }, { -1, 0, 1 } };
        double[,] SobelY = { { -1, -2, -1 }, { 0, 0, 0 }, { 1, 2, 1 } };
        double[,] ScharrX = { { 3, 0, -3 }, { 10, 0, -10 }, { 3, 0, -3 } };
        double[,] ScharrY = { { 3, 10, 3 }, { 0, 0, 0 }, { -3, -10, -3 } };
        double[,] Gauss;
        public  double[,] this[string filterName]
        {
            get
            {
                if (filterName.StartsWith("gauss"))
                {
                    string[] words = filterName.Split(new char[] { 's' });
                    Int32.TryParse(words[2], out int kernelSize);
                    if (kernelSize == 0)
                    {
                        return null;
                    }
                    Gauss = CreateGaussMatrix(kernelSize);
                    return Gauss;
                }
                switch (filterName)
                {
                    case "sobelX": return SobelX;
                    case "sobelY": return SobelY;
                    case "scharrX": return ScharrX;
                    case "scharrY": return ScharrY;
                    case "median": return Median;
                    case "gray": return Gray;
                    case "sobelXY": return Concatenate(SobelX, SobelY);
                    case "scharrXY": return Concatenate(ScharrX, ScharrY);
                    default: return null;
                }
            }
        }
        public double GaussFunc(int x, int y, float sigma)
        {
            double tmp = 2 * sigma * sigma;
            return (1 / (tmp * 3.14)) * Math.Pow(2.7, -((x * x + y * y) / tmp));
        }

        public double[,] CreateGaussMatrix(int coreSize)
        {
            double[,] matrix = new double[coreSize, coreSize];

            int imp = (int)(coreSize / 2 + 1) - coreSize;

            double sum = 0;

            for (int p = 0; p < coreSize; p++)
                for (int q = 0; q < coreSize; q++)
                {
                    double gau = GaussFunc(imp + p, imp + q, 1);
                    matrix[p, q] = gau;
                    sum += gau;
                }

            for (int p = 0; p < coreSize; p++)
                for (int q = 0; q < coreSize; q++)
                    matrix[p, q] = matrix[p, q] / sum;

            return matrix;
        }

        public double[,] Concatenate(double[,] firstMatrix, double[,] secondMatrix)
        {
            int firstMatrixX = firstMatrix.GetLength(1);
            int firstMatrixY = firstMatrix.GetLength(0);
            int secondMatrixX = secondMatrix.GetLength(1);
            int secondMatrixY = secondMatrix.GetLength(0);
            double[,] resultMatrix = new double[firstMatrixY + secondMatrixY, firstMatrixX];
            int i;
            for (i = 0; i < firstMatrixY; i++)
            {
                for (int j = 0; j < firstMatrixX; j++)
                {
                    resultMatrix[i, j] = firstMatrix[i, j];
                }
            }
            for (i = firstMatrixY; i < firstMatrixY + secondMatrixY; i++)
            {
                for (int j = 0; j < secondMatrixX; j++)
                {
                    resultMatrix[i, j] = secondMatrix[i - firstMatrixY, j];
                }
            }

            return resultMatrix;
        }
    }
}
