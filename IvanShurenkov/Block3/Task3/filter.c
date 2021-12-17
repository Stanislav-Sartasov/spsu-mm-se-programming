#include <stdlib.h>
#include "filter.h"

#define SQ(x) (x) * (x)

void gray(UINT8 **image, UINT32 height, UINT32 row, UINT16 bit_cnt)
{
	for (int i = 0; i < height; i++)
	{
		for (int j = 0; j + 2 < row; j += 3 + (bit_cnt == 32))
		{
			INT8 gray = image[i][j] * 0.2126 + image[i][j + 1] * 0.7152 + image[i][j + 2] * 0.0722;
			image[i][j] = gray;
			image[i][j + 1] = gray;
			image[i][j + 2] = gray;
		}
	}
}

int cmp(const int *a, const int *b)
{
	return ((*a) > (*b));
}

void median_filter(UINT8 **image, UINT32 height, UINT32 row, UINT16 bit_cnt)
{
	int step = 3 + (bit_cnt == 32);
	for (int clr_num = 0; clr_num < 3; clr_num++)
	{
		for (int i = 1; i + 1 < height; i++)
		{
			for (int j = step + clr_num; j + step < row; j += step)
			{
				int index = 0;
				int *arr = (int *) malloc(9 * sizeof(int));
				for (int i = 0; i < 9; i++)
					arr[i] = 256;
				for (int q = i - 1; q <= i + 1; q++)
				{
					for (int w = j - step; w <= j + step; w += step)
					{
						if (0 <= w && w < row && 0 <= q && q < height)
						{
							arr[index] = image[q][w];
							index++;
						}
					}
				}
				qsort(arr, 9, sizeof(int), cmp);
				image[i][j] = arr[index / 2];
			}
		}
	}
}

void gauss_filter(UINT8 **image, UINT32 height, UINT32 row, UINT16 bit_cnt)
{
	int step = 3 + (bit_cnt == 32);
	double gauss_matrix[3] = {0.147761, 0.118318, 0.0947416};
	for (int clr_num = 0; clr_num < 3; clr_num++)
	{
		for (int i = 1; i + 1 < height; i++)
		{
			for (int j = step + clr_num; j + step < row; j += step)
			{
				double clr = 0;
				for (int q = i - 1; q <= i + 1; q++)
				{
					for (int w = j - step; w <= j + step; w += step)
					{
						int gauss_id = (q != i) + (w != j);
						clr += image[q][w] * gauss_matrix[gauss_id];
					}
				}
				image[i][j] = clr;
			}
		}
	}
}

void sobel_filter(UINT8 **image, UINT32 height, UINT32 row, UINT16 bit_cnt, int matrix[3][3])
{
	gray(image, height, row, bit_cnt);

	int step = 3 + (bit_cnt == 32);

	/*int gx_matrix[3][3] = {{-1, 0, 1},
						   {-2, 0, 2},
						   {-1, 0, 1}};
	int gy_matrix[3][3] = {{-1, -2, -1},
						   {0,  0,  0},
						   {1,  2,  1}};*/

	UINT8 **ans_image = (UINT8 **) malloc(height * sizeof(UINT8 *));

	for (int i = 0; i < height; i++)
	{
		ans_image[i] = (UINT8 *) malloc(row * sizeof(UINT8));
	}

	for (int i = 1; i + 1 < height; i++)
	{
		for (int j = step; j + step < row; j += step)
		{
			int g = 0;
			int gx = 0;
			int gy = 0;
			for (int q = i - 1; q <= i + 1; q++)
			{
				for (int w = j - step; w <= j + step; w += step)
				{
					g += image[q][w] * matrix[q - i + 1][(w - j + step) / step];
					/*gx += image[q][w] * gx_matrix[q - i + 1][(w - j + step) / step];
					gy += image[q][w] * gy_matrix[q - i + 1][(w - j + step) / step];*/
				}
			}
			int clr = 0;
			if (SQ(g) > SQ(128))
				clr = 255;
			/*if (SQ(gx) + SQ(gy) > SQ(60))
				clr = 255;*/
			ans_image[i][j] = clr;
		}
	}
	for (int i = 1; i + 1 < height; i++)
	{
		for (int j = step; j + step < row; j += step)
		{
			for (int q = 0; q < step; q++)
			{
				image[i][j + q] = ans_image[i][j];
			}
		}
	}
	for (int i = 0; i < height; i++)
	{
		free(ans_image[i]);
	}
	free(ans_image);
}
