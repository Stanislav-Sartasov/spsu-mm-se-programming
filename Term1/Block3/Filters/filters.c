#include <stdlib.h>
#include <math.h>

int comparator(const void* a, const void* b) 
{
	return *(int*)a - *(int*)b;
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
	int matrix[3][3] = { { 1, 2, 1 }, { 2, 4, 2 }, { 1, 2, 1 } };
	int res = 0;
	for (int color = 0; color < 3; color++)
		for (int row = 1; row < height - 1; row++)
			for (int col = color + bps; col < width - bps; col += bps)
			{
				for (int y = row - 1; y < row + 2; y++)
					for (int x = col - bps; x < col + bps + 1; x += bps)
						res += image[y][x] * matrix[y - row + 1][(x - col) / bps + 1];
				image[row][col] = (unsigned char)(res / 16);
				res = 0;
			}
}

void sobel(unsigned char** image, int height, int width, int bps, char flag)
{
	grey(image, height, width, bps);

	int mx[3][3] = { {-1, -2, -1}, {0, 0, 0}, {1, 2, 1} };
	int my[3][3] = { {-1, 0, 1}, {-2, 0, 2}, {-1, 0, 1} };
	
	unsigned char** new = (unsigned char**)malloc(height * sizeof(unsigned char*));
	for (int i = 0; i < height; i++)
		new[i] = (unsigned char*)malloc(width * sizeof(unsigned char));

	int rx = 0, ry = 0;
	for (int color = 0; color < 3; color++)
		for (int row = 1; row < height - 1; row++)
			for (int col = color + bps; col < width - bps; col += bps)
			{
				for (int y = row - 1; y < row + 2; y++)
					for (int x = col - bps; x < col + bps + 1; x += bps)
					{
						rx += image[y][x] * mx[y - row + 1][(x - col) / bps + 1];
						ry += image[y][x] * my[y - row + 1][(x - col) / bps + 1];
					}
				if (flag == 'Y')
					new[row][col] = (unsigned char)ry;
				else if (flag == 'X')
					new[row][col] = (unsigned char)rx;
				else
					new[row][col] = (unsigned char)sqrt(rx * rx + ry * ry);
				rx = ry = 0;
			}

	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < width; j++)
			image[i][j] = new[i][j];
		free(new[i]);
	}
	free(new);
}