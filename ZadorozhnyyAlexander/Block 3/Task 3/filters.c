#pragma once
#include "structs.h"
#include "image_functions.h"
#include "filters.h"
#include <math.h>

int compare(const void* a, const void* b) {
	return *(int*)a - *(int*)b;
}

unsigned char greyscale(rgb_colors rgb)
{
	return ((0.3 * rgb.blue) + (0.6 * rgb.green) + (0.1 * rgb.red));
}

void grey_filter(image picture)
{
	for (int i = 0; i < picture.height; i++)
	{
		for (int j = 0; j < picture.width; j++)
		{
			picture.rgb[i][j].red = picture.rgb[i][j].green = picture.rgb[i][j].blue = greyscale(picture.rgb[i][j]);
		}
	}
}

image sobel_filter(image picture, int direction)
{
	grey_filter(picture);
	image new_pic = copy_image(picture);

	int edge = 80;
	//int g_x[3][3] = { {1, 0, -1}, {2, 0, -2}, {1, 0, -1} };
	//int g_y[3][3] = { {1, 2, 1}, {0, 0, 0}, {-1, -2, -1} };
	for (int i = 1; i < picture.height - 1; i++)
	{
		for (int j = 1; j < picture.width - 1; j++)
		{
			/*int sum_x = 0, sum_y = 0;
			for (int k = -1; k <= 1; k++)
			{
				for (int p = -1; p <= 1; p++)
				{
					sum_x += picture.rgb[i + k][j + p].red * g_x[k + 1][p + 1];
					sum_y += picture.rgb[i + k][j + p].red * g_y[k + 1][p + 1];
				}
			}*/
			int sum_x = (picture.rgb[i + 1][j - 1].red + 2 * picture.rgb[i + 1][j].red + picture.rgb[i + 1][j + 1].red) -
				(picture.rgb[i - 1][j - 1].red + 2 * picture.rgb[i - 1][j].red + picture.rgb[i - 1][j + 1].red);
			int sum_y = (picture.rgb[i - 1][j + 1].red + 2 * picture.rgb[i][j + 1].red + picture.rgb[i + 1][j + 1].red) -
				(picture.rgb[i - 1][j - 1].red + 2 * picture.rgb[i][j - 1].red + picture.rgb[i + 1][j - 1].red);
			switch (direction)
			{
			case -1:
				new_pic.rgb[i][j].red = new_pic.rgb[i][j].green = new_pic.rgb[i][j].blue = abs(sum_x) <= edge ? 0 : 255;
				break;
			case 0:
				new_pic.rgb[i][j].red = new_pic.rgb[i][j].green = new_pic.rgb[i][j].blue = sqrt(pow(sum_x, 2) + pow(sum_y, 2)) <= edge ? 0 : 255;
				break;
			case 1:
				new_pic.rgb[i][j].red = new_pic.rgb[i][j].green = new_pic.rgb[i][j].blue = abs(sum_y) <= edge ? 0 : 255;
				break;
			}
		}
	}
	free_image(picture);
	return new_pic;
}

void middle_filter(image picture)
{
	for (int i = 1; i < picture.height - 1; i++)
	{
		for (int j = 1; j < picture.width - 1; j++)
		{
			int mid_arr[3][9];
			int index = 0;
			for (int k = -1; k <= 1; k++)
			{
				for (int p = -1; p <= 1; p++)
				{
					mid_arr[0][index] = picture.rgb[i + k][j + p].red;
					mid_arr[1][index] = picture.rgb[i + k][j + p].green;
					mid_arr[2][index] = picture.rgb[i + k][j + p].blue;
					index++;
				}
			}
			qsort(mid_arr[0], 9, sizeof(int), compare);
			qsort(mid_arr[1], 9, sizeof(int), compare);
			qsort(mid_arr[2], 9, sizeof(int), compare);
			picture.rgb[i][j].red = mid_arr[0][4];
			picture.rgb[i][j].green = mid_arr[1][4];
			picture.rgb[i][j].blue = mid_arr[2][4];
		}
	}
}

void gausses_filter_3x3(image picture)
{
	int g[3][3] = { {1, 2, 1}, {2, 4, 2}, {1, 2, 1} };
	for (int i = 1; i < picture.height - 1; i++)
	{
		for (int j = 1; j < picture.width - 1; j++)
		{
			int r_k = 0, g_k = 0, b_k = 0;
			for (int k = -1; k <= 1; k++)
			{
				for (int p = -1; p <= 1; p++)
				{
					r_k += picture.rgb[i + k][j + p].red * g[k + 1][p + 1];
					g_k += picture.rgb[i + k][j + p].green * g[k + 1][p + 1];
					b_k += picture.rgb[i + k][j + p].blue * g[k + 1][p + 1];
				}
			}
			picture.rgb[i][j].red = r_k / 16;
			picture.rgb[i][j].green = g_k / 16;
			picture.rgb[i][j].blue = b_k / 16;
		}
	}
}