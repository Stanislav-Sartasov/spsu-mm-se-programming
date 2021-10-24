#include <math.h>
#include "input_and_tools.h"
#include "filters.h"

int grey(unsigned char** image, int row, int y)
{
	for (int i = 0; i < y; i++)
	{
		for (int j = 0; j < row ; j += 3)
		{
			unsigned char z = (unsigned char)(image[i][j] * 0.3 + image[i][j + 1] * 0.59 + image[i][j + 2] * 0.11);
			image[i][j] = z;
			image[i][j + 1] = z;
			image[i][j + 2] = z;
		}
	}

	return 0;
}

int med_middle(unsigned char** image, int row, int y)
{
	for (int a = 0; a < 3; a++)
	{
		//int a = 0;
		for (int i = 1; i < y - 1; i++)
		{
			for (int j = a + 3; j < row - 3; j += 3)
			{
				int c = 0;
				int arr[9] = { 0 };
				for (int k = i - 1; k < i + 2; k++)
				{
					for (int h = j - 3; h < j + 4; h += 3)
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

int five_x_five_gauss(unsigned char** image, int row, int y)
{
	double gauss[5] = { 0.028087, 0.23431, 0.475207, 0.23431, 0.028087 };
	for (int i = 1; i < y - 1; i++)
	{
		for (int j = 3; j < row - 3; j += 3)
		{
			double r = 0;
			double g = 0;
			double b = 0;
			for (int k = i - 2; k < i + 3; k++)
			{
				for (int h = j - 6; h < j + 7; h += 3)
				{
					if (k < 0 || k >= y || h < 0 || h >= row)
					{
						r += 0;
						g += 0;
						b += 0;
					}
					else
					{
						r += image[k][h] * gauss[k - i + 2] * gauss[(h - j + 6) / 3];
						g += image[k][h + 1] * gauss[k - i + 2] * gauss[(h - j + 6) / 3];
						b += image[k][h + 2] * gauss[k - i + 2] * gauss[(h - j + 6) / 3];
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

int sobel_x(unsigned char** image, int x, int y, int row)
{
	grey(image, row, y);
	med_middle(image, row, y);

	int** pr = (int**)malloc(y * sizeof(int*));
	for (int i = 0; i < y; i++)
	{
		pr[i] = (int*)malloc(x * sizeof(int));
	}

	for (int i = 1; i < y - 1; i++)
	{
		for (int j = 3; j < row - 3; j += 3)
		{
			int g = image[i - 1][j + 3] + 2 * image[i][j + 3] + image[i + 1][j + 3] - (image[i - 1][j - 3] + 2 * image[i][j - 3] + image[i + 1][j - 3]);
			if (g < 74)
			{
				g = 255;
			}
			else
			{
				g = 0;
			}
			pr[i][j / 3] = g;
		}
	}

	for (int i = 1; i < y - 1; i++)
	{
		for (int j = 3; j < row - 3; j += 3)
		{
			if (pr[i][j / 3] == 255)
			{
				image[i][j] = 0;
				image[i][j + 1] = 0;
				image[i][j + 2] = 0;
			}
			else
			{
				image[i][j] = 255;
				image[i][j + 1] = 255;
				image[i][j + 2] = 255;
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

int sobel_y(unsigned char** image, int x, int y, int row)
{
	grey(image, row, y);
	med_middle(image, row, y);

	int** pr = (int**)malloc(y * sizeof(int*));
	for (int i = 0; i < y; i++)
	{
		pr[i] = (int*)malloc(x * sizeof(int));
	}

	for (int i = 1; i < y - 1; i++)
	{
		for (int j = 3; j < row - 3; j += 3)
		{
			int g = image[i - 1][j - 3] + 2 * image[i - 1][j] + image[i - 1][j + 3] - (image[i + 1][j - 3] + 2 * image[i + 1][j] + image[i + 1][j + 3]);
			if (g < 74)
			{
				g = 255;
			}
			else
			{
				g = 0;
			}
			pr[i][j / 3] = g;
		}
	}

	for (int i = 1; i < y - 1; i++)
	{
		for (int j = 3; j < row - 3; j += 3)
		{
			if (pr[i][j / 3] == 255)
			{
				image[i][j] = 0;
				image[i][j + 1] = 0;
				image[i][j + 2] = 0;
			}
			else
			{
				image[i][j] = 255;
				image[i][j + 1] = 255;
				image[i][j + 2] = 255;
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

int sobel(unsigned char** image, int x, int y, int row)
{
	grey(image, row, y);
	med_middle(image, row, y);

	int** pr = (int**)malloc(y * sizeof(int*));
	for (int i = 0; i < y; i++)
	{
		pr[i] = (int*)malloc(x * sizeof(int));
	}

	for (int i = 1; i < y - 1; i++)
	{
		for (int j = 3; j < row - 3; j += 3)
		{
			int gx = image[i - 1][j + 3] + 2 * image[i][j + 3] + image[i + 1][j + 3] - (image[i - 1][j - 3] + 2 * image[i][j - 3] + image[i + 1][j - 3]);
			int gy = image[i + 1][j - 3] + 2 * image[i + 1][j] + image[i + 1][j + 3] - (image[i - 1][j - 3] + 2 * image[i - 1][j] + image[i - 1][j + 3]);
			int g = (int)(sqrt(gy * gy + gx * gx));
			if (g < 72)
			{
				g = 255;
			}
			else
			{
				g = 0;
			}
			pr[i][j / 3] = g;
		}
	}

	for (int i = 1; i < y - 1; i++)
	{
		for (int j = 3; j < row - 3; j += 3)
		{
			if (pr[i][j / 3] == 255)
			{
				image[i][j] = 0;
				image[i][j + 1] = 0;
				image[i][j + 2] = 0;
			}
			else
			{
				image[i][j] = 255;
				image[i][j + 1] = 255;
				image[i][j + 2] = 255;
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