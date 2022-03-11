#include <stdlib.h>
#include <math.h>

int comparator(const void* a, const void* b) 
{
	return *(int*)a - *(int*)b;
}

void convolution(unsigned char** image, int height, int width, int bps, double* kernel)
{
	unsigned char** new = (unsigned char**)malloc(height * sizeof(unsigned char*));
	for (int i = 0; i < height; i++)
		new[i] = (unsigned char*)malloc(width * sizeof(unsigned char));

	double res = 0;
	for (int color = 0; color < 3; color++)
		for (int row = 1; row < height - 1; row++)
			for (int col = color + bps; col < width - bps; col += bps)
			{
				for (int y = 0; y < 3; y++)
					for (int x = 0; x < 3; x ++)
					{
						res += image[row + y - 1][col + (x - 1) * bps] * kernel[y * 3 + x];
					}
				new[row][col] = (unsigned char)min(255, max(0, res));
				res = 0;
			}

	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < width; j++)
			image[i][j] = new[i][j];
		free(new[i]);
	}
	free(new);
}

void grey(unsigned char** image, int height, int width, int bps)
{
	unsigned char avg;
	for (int y = 0; y < height; y++)
		for (int x = 0; x < width; x += bps)
		{
			avg = (unsigned char)(image[y][x] * 0.299 + image[y][x + 1] * 0.587 + image[y][x + 2] * 0.114);
			image[y][x] = avg;
			image[y][x+1] = avg;
			image[y][x+2] = avg;
		}
}

void median(unsigned char** image, int height, int width, int bps)
{
	int matrix[9] = { 0 };
	int c = 0;
	for (int color = 0; color < 3; color++)
		for (int row = 1; row < height - 1; row++)
			for (int col = color + bps; col < width - bps; col += bps)
			{
				for (int y = row - 1; y < row + 2; y++)
					for (int x = col - bps; x < col + bps + 1; x += bps)
					{
						matrix[c] = image[y][x];
						c++;
					}
				c = 0;
				qsort(matrix, 9, sizeof(int), comparator);
				image[row][col] = matrix[4];
			}
}

void gauss(unsigned char** image, int height, int width, int bps)
{
	double kernel[9] = { 1.0 / 16, 2.0 / 16, 1.0 / 16, 2.0 / 16, 4.0 / 16, 2.0 / 16, 1.0 / 16, 2.0 / 16, 1.0 / 16 };
	convolution(image, height, width, bps, kernel);
}

void sobelx(unsigned char** image, int height, int width, int bps)
{
	grey(image, height, width, bps);
	double kernel[9] = { -1, -2, -1, 0, 0, 0, 1, 2, 1 };
	convolution(image, height, width, bps, kernel);
}

void sobely(unsigned char** image, int height, int width, int bps)
{
	grey(image, height, width, bps);
	double kernel[9] = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
	convolution(image, height, width, bps, kernel);
}

void sobel(unsigned char** image, int height, int width, int bps)
{
	unsigned char** sx = (unsigned char**)malloc(height * sizeof(unsigned char*));
	unsigned char** sy = (unsigned char**)malloc(height * sizeof(unsigned char*));
	for (int i = 0; i < height; i++)
	{
		sx[i] = (unsigned char*)malloc(width * sizeof(unsigned char));
		sy[i] = (unsigned char*)malloc(width * sizeof(unsigned char));
		for (int j = 0; j < width; j++)
		{
			sx[i][j] = image[i][j];
			sy[i][j] = image[i][j];
		}
	}

	sobelx(sx, height, width, bps);
	sobely(sy, height, width, bps);
	
	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < width; j++)
			image[i][j] = (unsigned char)min(255, sqrt(sx[i][j] * sx[i][j] + sy[i][j] * sy[i][j]));
		free(sx[i]);
		free(sy[i]);
	}
	free(sx);
	free(sy);
}