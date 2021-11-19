#include "filters.h"
#include <stdlib.h>

int compare(const char *left, const char *right);

void grey_filter(unsigned char **image, int width, int height, int Bps)
{
	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < width; j += Bps)
		{
			int avg = (image[i][j] + image[i][j + 1] + image[i][j + 2]) / 3;
			image[i][j] = avg;
			image[i][j + 1] = avg;
			image[i][j + 2] = avg;
		}
	}
}

void avg_filter(unsigned char **image, int width, int height, int Bps)
{
	char **new_image = (char**)malloc(height * sizeof(char*));
	for (int i = 0; i < height; i++)
		new_image[i] = (char*)malloc(width * sizeof(char));
	int dx[] = {-1, 0, 1, -1, 0, 1, -1, 0, 1};
	int dy[] = {1, 1, 1, 0, 0, 0, -1, -1, -1};

	for (int row = 0; row < height; row++)
	{
		for (int col = 0; col < width; col += Bps)
		{
			char avg_r[9] = {0}, avg_g[9] = {0}, avg_b[9] = {0};
			for (int i = 0; i < 9; i++)
			{
				int new_col = col + Bps * dx[i];
				int new_row = row + dy[i];

				if (0 <= new_col && new_col < width && 0 <= new_row && new_row < height)
				{
					avg_r[i] = image[new_row][new_col];
					avg_g[i] = image[new_row][new_col + 1];
					avg_b[i] = image[new_row][new_col + 2];
				}
			}
			qsort(avg_r, 9, sizeof(char), (int(*) (const void *, const void *))compare);
			qsort(avg_g, 9, sizeof(char), (int(*) (const void *, const void *))compare);
			qsort(avg_b, 9, sizeof(char), (int(*) (const void *, const void *))compare);
			new_image[row][col] = avg_r[4];
			new_image[row][col + 1] = avg_g[4];
			new_image[row][col + 2] = avg_b[4];
		}
	}

	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < width; j++)
		{
			image[i][j] = new_image[i][j];
		}
		free(new_image[i]);
	}
	free(new_image);
}

void gaussian_filter(unsigned char **image, int width, int height, int Bps)
{
	int gaussian_kernel[9] = {1, 2, 1, 2, 4, 2, 1, 2, 1};
	char **new_image = (char**)malloc(height * sizeof(char*));
	for (int i = 0; i < height; i++)
		new_image[i] = (char*)malloc(width * sizeof(char));

	int dx[] = {-1, 0, 1, -1, 0, 1, -1, 0, 1};
	int dy[] = {1, 1, 1, 0, 0, 0, -1, -1, -1};

	for (int row = 0; row < height; row++)
	{
		for (int col = 0; col < width; col += Bps)
		{
			int avg_r = 0;
			int avg_g = 0;
			int avg_b = 0;

			for (int i = 0; i < 9; i++)
			{
				int new_col = col + Bps * dx[i];
				int new_row = row + dy[i];

				if (0 <= new_col && new_col < width && 0 <= new_row && new_row < height)
				{
					avg_r += gaussian_kernel[i] * image[new_row][new_col];
					avg_g += gaussian_kernel[i] * image[new_row][new_col + 1];
					avg_b += gaussian_kernel[i] * image[new_row][new_col + 2];
				}
			}
			new_image[row][col] = avg_r / 16;
			new_image[row][col + 1] = avg_g / 16;
			new_image[row][col + 2] = avg_b / 16;
		}
	}

	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < width; j++)
		{
			image[i][j] = new_image[i][j];
		}
		free(new_image[i]);
	}
	free(new_image);
}

void sobel_filter(unsigned char **image, int width, int height, int Bps, int direction)
{
	grey_filter(image, width, height, Bps);
	avg_filter(image, width, height, Bps);
	int kernel[9] = {-1, -2, -1, 0, 0, 0, 1, 2, 1};
	if (direction == 1)
	{
		kernel[0] = kernel[6] = -(kernel[2] = kernel[8] = 1);
		kernel[3] = -(kernel[5] = 2);
		kernel[1] = kernel[4] = kernel[7] = 0;
	}

	char **new_image = (char**)malloc(height * sizeof(char*));
	for (int i = 0; i < height; i++)
		new_image[i] = (char*)malloc(width * sizeof(char));

	int dx[] = {-1, 0, 1, -1, 0, 1, -1, 0, 1};
	int dy[] = {1, 1, 1, 0, 0, 0, -1, -1, -1};

	for (int row = 0; row < height; row++)
	{
		for (int col = 0; col < width; col += Bps)
		{
			int avg_r = 0;
			int avg_g = 0;
			int avg_b = 0;

			for (int i = 0; i < 9; i++)
			{
				int new_col = col + Bps * dx[i];
				int new_row = row + dy[i];

				if (0 <= new_col && new_col < width && 0 <= new_row && new_row < height)
				{
					avg_r += kernel[i] * image[new_row][new_col];
					avg_g += kernel[i] * image[new_row][new_col + 1];
					avg_b += kernel[i] * image[new_row][new_col + 2];
				}
			}
			new_image[row][col] = avg_r / 16;
			new_image[row][col + 1] = avg_g / 16;
			new_image[row][col + 2] = avg_b / 16;
		}
	}

	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < width; j++)
		{
			image[i][j] = new_image[i][j];
		}
		free(new_image[i]);
	}
	free(new_image);
}

int compare(const char *left, const char *right)
{
	return *left - *right;
}