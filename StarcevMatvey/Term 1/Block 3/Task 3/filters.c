#include <math.h>
#include "input_and_tools.h"
#include "filters.h"

int grey(unsigned char** image, int row, int y, int t)
{
	for (int i = 0; i < y; i++)
	{
		for (int j = 0; j < row ; j += t)
		{
			unsigned char z = (unsigned char)(image[i][j] * 0.3 + image[i][j + 1] * 0.59 + image[i][j + 2] * 0.11);
			image[i][j] = z;
			image[i][j + 1] = z;
			image[i][j + 2] = z;
		}
	}

	return 0;
}

int med_middle(unsigned char** image, int row, int y, int t)
{
	for (int a = 0; a < 3; a++)
	{
		for (int i = 1; i < y - 1; i++)
		{
			for (int j = a + t; j < row - t; j += t)
			{
				int c = 0;
				int arr[9] = { 0 };
				for (int k = i - 1; k < i + 2; k++)
				{
					for (int h = j - t; h < j + t + 1; h += t)
					{
						arr[c] = image[k][h];
						c++;
					}
				}
				sort_of_array(arr, 9);
				image[i][j] = arr[4];
			}
		}
	}
}

int five_x_five_gauss(unsigned char** image, int row, int y, int t)
{
	double gauss[5] = { 0.028087, 0.23431, 0.475207, 0.23431, 0.028087 };
	for (int i = 1; i < y - 1; i++)
	{
		for (int j = t; j < row - t; j += t)
		{
			double r = 0;
			double g = 0;
			double b = 0;
			for (int k = i - 2; k < i + 3; k++)
			{
				for (int h = j - 2 * t; h < j + 2 * t + 1; h += t)
				{
					if (k >= 0 && k < y && h >= 0 && h < row)
					{
						r += image[k][h] * gauss[k - i + 2] * gauss[(h - j + 2 * t) / t];
						g += image[k][h + 1] * gauss[k - i + 2] * gauss[(h - j + 2 * t) / t];
						b += image[k][h + 2] * gauss[k - i + 2] * gauss[(h - j + 2 * t) / t];
					}
				}
			}
			image[i][j] = (unsigned char)r;
			image[i][j + 1] = (unsigned char)g;
			image[i][j + 2] = (unsigned char)b;
		}
	}

	return 0;
}

int multi_sobel(unsigned char** image, int x, int y, int row, char flag, int t)
{
	grey(image, row, y, t);

	// limit for edge
	const int limit = 74;

	int matrix_x[3][3] = { {-1, 0, 1}, {-2, 0, 2}, {-1, 0, 1} };
	int matrix_y[3][3] = { {-1, -2, -1}, {0, 0, 0}, {1, 2, 1} };

	unsigned char** pr = (unsigned char**)malloc(y * sizeof(unsigned char*));
	for (int i = 0; i < y; i++)
	{
		pr[i] = (unsigned char*)malloc(x * sizeof(unsigned char));
	}

	for (int i = 1; i < y - 1; i++)
	{
		for (int j = t; j < row - t; j += t)
		{
			int g = 0;
			int gy = 0;
			int gx = 0;
			for (int k = i - 1; k < i + 2; k++)
			{
				for (int h = j - t; h < j + t + 1; h += t)
				{
					gx += image[k][h] * matrix_x[k - (i - 1)][(h - (j - t)) / t];
					gy += image[k][h] * matrix_y[k - (i - 1)][(h - (j - t)) / t];
				}
			}

			// normal Sobel
			if (flag == '0')
			{
				g = (int)(sqrt(gy * gy + gx * gx));
				if (g < limit)
				{
					pr[i][j / t] = 0;
				}
				else
				{
					pr[i][j / t] = 255;
				}
			}
			// X Sobel
			else if (flag == 'X')
			{
				if (abs(gx) < limit)
				{
					pr[i][j / t] = 0;
				}
				else
				{
					pr[i][j / t] = 255;
				}
			}
			// Y Sobel
			else
			{
				if (abs(gy) < limit)
				{
					pr[i][j / t] = 0;
				}
				else
				{
					pr[i][j / t] = 255;
				}
			}
		}
	}

	for (int i = 1; i < y - 1; i++)
	{
		for (int j = t; j < row - t; j += t)
		{
			for (int k = 0; k < t; k++)
			{
				image[i][j + k] = pr[i][j / t];
			}
		}
	}

	for (int i = 0; i < y; i++)
	{
		free(pr[i]);
	}
	free(pr);

	return 0;
}