#include "filters.h"
#include <stdlib.h>

int compare(const int *left, const int *right);

void apply_kernel(unsigned char **image, int width, int height, int bps, const int *kernel, int avg_filter_flag)
{
	int dx[] = {-1, 0, 1, -1, 0, 1, -1, 0, 1};
	int dy[] = {1, 1, 1, 0, 0, 0, -1, -1, -1};

	unsigned char **new_image = (unsigned char**)malloc(height * sizeof(unsigned char*));
	for (int i = 0; i < height; i++)
		new_image[i] = (unsigned char*)malloc(width * sizeof(unsigned char));

	for (int row = 0; row < height; row++)
	{
		for (int col = 0; col < width; col += bps)
		{
			int avg_r[9] = {0};
			int avg_g[9] = {0};
			int avg_b[9] = {0};

			for (int i = 0; i < 9; i++)
			{
				int new_col = col + bps * dx[i];
				int new_row = row + dy[i];

				if (0 <= new_col && new_col < width && 0 <= new_row && new_row < height)
				{
					if (avg_filter_flag)
					{
						avg_r[i] = image[new_row][new_col];
						avg_g[i] = image[new_row][new_col + 1];
						avg_b[i] = image[new_row][new_col + 2];
					}
					else
					{
						avg_r[0] += kernel[i] * image[new_row][new_col];
						avg_g[0] += kernel[i] * image[new_row][new_col + 1];
						avg_b[0] += kernel[i] * image[new_row][new_col + 2];
					}
				}
			}
			if (avg_filter_flag)
			{
				qsort(avg_r, 9, sizeof(int), (int(*) (const void *, const void *))compare);
				qsort(avg_g, 9, sizeof(int), (int(*) (const void *, const void *))compare);
				qsort(avg_b, 9, sizeof(int), (int(*) (const void *, const void *))compare);
				new_image[row][col] = avg_r[4];
				new_image[row][col + 1] = avg_g[4];
				new_image[row][col + 2] = avg_b[4];
			}
			else
			{
				new_image[row][col] = avg_r[0] / 16;
				new_image[row][col + 1] = avg_g[0] / 16;
				new_image[row][col + 2] = avg_b[0] / 16;
			}
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

void grey_filter(unsigned char **image, int width, int height, int bps)
{
	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j < width; j += bps)
		{
			int avg = (image[i][j] + image[i][j + 1] + image[i][j + 2]) / 3;
			image[i][j] = avg;
			image[i][j + 1] = avg;
			image[i][j + 2] = avg;
		}
	}
}

void avg_filter(unsigned char **image, int width, int height, int bps)
{
	int empty_kernel[] = {};
	apply_kernel(image, width, height, bps, empty_kernel, 1);
}

void gaussian_filter(unsigned char **image, int width, int height, int bps)
{
	int gaussian_kernel[9] = {1, 2, 1, 2, 4, 2, 1, 2, 1};

	apply_kernel(image, width, height, bps, gaussian_kernel, 0);
}

void sobel_filter(unsigned char **image, int width, int height, int bps, int direction)
{
	grey_filter(image, width, height, bps);
	avg_filter(image, width, height, bps);
	int kernel[9] = {-1, -2, -1, 0, 0, 0, 1, 2, 1};
	if (direction == 1)
	{
		kernel[0] = kernel[6] = -(kernel[2] = kernel[8] = 1);
		kernel[3] = -(kernel[5] = 2);
		kernel[1] = kernel[4] = kernel[7] = 0;
	}

	apply_kernel(image, width, height, bps, kernel, 0);
}

int compare(const int *left, const int *right)
{
	return *left - *right;
}