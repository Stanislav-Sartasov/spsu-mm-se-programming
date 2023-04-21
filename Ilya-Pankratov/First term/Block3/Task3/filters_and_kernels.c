#include <math.h>
#include <stdio.h>
#include <stdlib.h>
#include "bmp_files.h"
#include "filters_and_kernels.h"

void free_core(void*** core, int number_cores, int core_size);

void*** null_core();

// useful tools for calculating 

double gauss_function(int x, int y, float sigma)
{
	double temp = 2 * sigma * sigma;
	return (1 / (temp * 3.14)) * pow(2.7, (-(x * x) - (y * y)) / temp);
}

int compare(const void* x1, const void* x2)
{
	return (*(int*)x1 - *(int*)x2);
}

int** create_array(int height, int width, int condition)
{
	int** temp = NULL;

	if (condition)
	{
		temp = (int**)malloc(height * sizeof(int*));
		for (int i = 0; i < height; i++)
			temp[i] = (int*)malloc(width * sizeof(int));
	}

	return temp;
}

void free_array(int** array, int height)
{
	for (int i = 0; i < height; i++)
		free(array[i]);
	free(array);
}

// function for image processing

void recovery(struct bmp_image* picture, int start_line)
{
	for (int i = 0; i < start_line; i++)
	{
		for (int j = 0; j < picture->bytes; j += picture->bytes_per_pixel)
			for (int rgb = 0; rgb < 3; rgb++)
			{
				if (i == j / picture->bytes_per_pixel)
				{
					picture->image[i][j + rgb] = picture->image[2 * start_line - i - 1][2 * start_line - j + rgb - 1];
					picture->image[picture->height - 1 - i][j + rgb] = picture->image[picture->height - 2 * start_line + i][2 * start_line - j + rgb - 1];
				}
				else
				{
					picture->image[i][j + rgb] = picture->image[2 * start_line - i - 1][j + rgb];
					picture->image[picture->height - 1 - i][j - rgb] = picture->image[picture->height - 2 * start_line + i][j - rgb];
				}
			}
		for (int j = start_line; j < picture->height - start_line; j++)
			for (int rgb = 0; rgb < 3; rgb++)
			{
				picture->image[j][i * picture->bytes_per_pixel + rgb] = picture->image[j][(2 * start_line - 1 - i) * picture->bytes_per_pixel + rgb];
				picture->image[j][(picture->bytes - 1) - i * picture->bytes_per_pixel - rgb] = picture->image[j][picture->bytes + (i - 2 * start_line) * picture->bytes_per_pixel - rgb];
			}
	}
	return;
}

void convolution(struct bmp_image* picture, unsigned char (*filter)(struct bmp_image*, int, int, int, void*** kernel, int, int), void*** (*core)(int number_cores, int core_size), int number_cores, int core_size)
{
	int start = core_size / 2;
	void*** kernel;

	if (core_size == 0 || number_cores == 0)
		kernel = NULL;
	else
		kernel = core(number_cores, core_size);

	int flag = (kernel != NULL) && (number_cores == 2 || (((int***)kernel)[0][0][0] == 1 && ((int***)kernel)[0][1][1] == 0) || ((int***)kernel)[0][0][0] == 3);

	if (flag)
		convolution(picture, grey_filter, null_core, 0, 0);

	int** temp = create_array(picture->height, picture->width, flag);

	for (int i = start; i < picture->height - (start + 1); i++)
	{
		for (int j = start * picture->bytes_per_pixel; j < picture->bytes - start * picture->bytes_per_pixel; j += picture->bytes_per_pixel)
		{
			if (kernel == NULL && temp != NULL)
			{
				picture->image[i][j] = picture->image[i][j + 1] = picture->image[i][j + 2] = filter(picture, i - start, j - start * picture->bytes_per_pixel, 0, kernel, core_size, number_cores);
			}
			else if (flag)
			{
				temp[i][j / picture->bytes_per_pixel] = filter(picture, i - start, j - start * picture->bytes_per_pixel, 0, kernel, core_size, number_cores);
			}
			else
			{
				for (int rgb = 0; rgb < 3; rgb++)
					picture->image[i][j + rgb] = filter(picture, i - start, j - start * picture->bytes_per_pixel, rgb, kernel, core_size, number_cores);
			}
		}
	}

	if (flag)
	{
		for (int i = start; i < picture->height - (start + 1); i++)
			for (int j = start * picture->bytes_per_pixel; j < picture->bytes - start * picture->bytes_per_pixel; j += picture->bytes_per_pixel)
				for (int rgb = 0; rgb < 3; rgb++)
					picture->image[i][j + rgb] = temp[i][j / picture->bytes_per_pixel];

		free_array(temp, picture->height);
	}

	if (kernel == NULL)
		return;

	free_core(kernel, number_cores, core_size);
	recovery(picture, start);
	return;
}

// cores

void*** averaging_core(int number_cores, int core_size)
{
	void*** core = (void***)malloc(number_cores * sizeof(int**));

	for (int i = 0; i < number_cores; i++)
	{
		core[i] = (int**)malloc(core_size * sizeof(int*));
		for (int j = 0; j < core_size; j++)
		{
			core[i][j] = (int*)malloc(core_size * sizeof(int));
			for (int k = 0; k < core_size; k++)
				((int***)core)[i][j][k] = 1;
		}
	}

	return core;
}

void*** gauss_core(int number_cores, int core_size)
{
	void*** core = (void***)malloc(number_cores * sizeof(double**));
	double sum = 0;
	int offset = core_size / 2 - core_size + 1;

	for (int i = 0; i < number_cores; i++)
	{
		core[i] = (double**)malloc(core_size * sizeof(double*));
		for (int j = 0; j < core_size; j++)
		{
			core[i][j] = (double*)malloc(core_size * sizeof(double));
			for (int k = 0; k < core_size; k++)
			{
				double result = gauss_function(i + offset, j + offset, 1);
				((double***)core)[i][j][k] = result;
				sum += result;
			}
		}

		for (int x = 0; x < core_size; x++)
			for (int y = 0; y < core_size; y++)
				((double***)core)[i][x][y] /= sum;
	}

	return core;
}

void*** sobel_x_core(int number_cores, int core_size)
{
	void*** core = (void***)malloc(number_cores * sizeof(int**));

	for (int i = 0; i < number_cores; i++)
	{
		core[i] = (int**)malloc(core_size * sizeof(int*));
		for (int j = 0; j < core_size; j++)
			core[i][j] = (int*)malloc(core_size * sizeof(int));
	}

	((int***)core)[0][0][0] = 1;
	((int***)core)[0][0][1] = 0;
	((int***)core)[0][0][2] = -1;
	((int***)core)[0][1][0] = 2;
	((int***)core)[0][1][1] = 0;
	((int***)core)[0][1][2] = -2;
	((int***)core)[0][2][0] = 1;
	((int***)core)[0][2][1] = 0;
	((int***)core)[0][2][2] = -1;

	return core;
}
void*** sobel_y_core(int number_cores, int core_size)
{
	void*** core = (void***)malloc(number_cores * sizeof(int**));

	for (int i = 0; i < number_cores; i++)
	{
		core[i] = (int**)malloc(core_size * sizeof(int*));
		for (int j = 0; j < core_size; j++)
			core[i][j] = (int*)malloc(core_size * sizeof(int));
	}

	((int***)core)[0][0][0] = 1;
	((int***)core)[0][0][1] = 2;
	((int***)core)[0][0][2] = 1;
	((int***)core)[0][1][0] = 0;
	((int***)core)[0][1][1] = 0;
	((int***)core)[0][1][2] = 0;
	((int***)core)[0][2][0] = -1;
	((int***)core)[0][2][1] = -2;
	((int***)core)[0][2][2] = -1;

	return core;
}

void*** sobel_core(int number_cores, int core_size)
{
	void*** core = (void***)malloc(number_cores * sizeof(int**));

	for (int i = 0; i < number_cores; i++)
	{
		if (i == 0)
		{
			core[i] = *(sobel_x_core(number_cores - 1, core_size));
		}
		else
		{
			core[i] = *(sobel_x_core(number_cores - 1, core_size));
		}
	}

	return core;
}

void*** sharr_x_core(int number_cores, int core_size)
{
	void*** core = (void***)malloc(number_cores * sizeof(int**));

	for (int i = 0; i < number_cores; i++)
	{
		core[i] = (int**)malloc(core_size * sizeof(int*));
		for (int j = 0; j < core_size; j++)
			core[i][j] = (int*)malloc(core_size * sizeof(int));
	}

	((int***)core)[0][0][0] = 3;
	((int***)core)[0][0][1] = 0;
	((int***)core)[0][0][2] = -3;
	((int***)core)[0][1][0] = 10;
	((int***)core)[0][1][1] = 0;
	((int***)core)[0][1][2] = -10;
	((int***)core)[0][2][0] = 3;
	((int***)core)[0][2][1] = 0;
	((int***)core)[0][2][2] = -3;

	return core;
}

void*** sharr_y_core(int number_cores, int core_size)
{
	void*** core = (void***)malloc(number_cores * sizeof(int**));

	for (int i = 0; i < number_cores; i++)
	{
		core[i] = (int**)malloc(core_size * sizeof(int*));
		for (int j = 0; j < core_size; j++)
			core[i][j] = (int*)malloc(core_size * sizeof(int));
	}

	((int***)core)[0][0][0] = 3;
	((int***)core)[0][0][1] = 10;
	((int***)core)[0][0][2] = 3;
	((int***)core)[0][1][0] = 0;
	((int***)core)[0][1][1] = 0;
	((int***)core)[0][1][2] = 0;
	((int***)core)[0][2][0] = -3;
	((int***)core)[0][2][1] = -10;
	((int***)core)[0][2][2] = -3;

	return core;
}

void*** sharr_core(int number_cores, int core_size)
{
	void*** core = (void***)malloc(number_cores * sizeof(int**));

	for (int i = 0; i < number_cores; i++)
	{
		if (i == 0)
			core[i] = *(sharr_x_core(number_cores - 1, core_size));
		if (i == 1)
			core[i] = *(sharr_x_core(number_cores - 1, core_size));
	}

	return core;
}

void*** null_core()
{
	return NULL;
}

void free_core(void*** core, int number_cores, int core_size)
{
	for (int i = 0; i < number_cores; i++)
	{
		for (int j = 0; j < core_size; j++)
			free(core[i][j]);
		free(core[i]);
	}

	free(core);
	return;
}

// filters

unsigned char median_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores)
{
	int kernel[9];
	int index = 0;

	for (int i = 0; i < core_size; i++)
		for (int j = 0; j < core_size * picture->bytes_per_pixel; j += picture->bytes_per_pixel)
		{
			kernel[index] = (int)picture->image[x + i][y + j + rgb];
			index++;
		}

	qsort(&kernel, index, sizeof(int), compare);
	return (unsigned char)kernel[core_size * core_size / 2];
}

unsigned char negative_filter(struct bmp_image* picture, int x, int y, int rgb)
{
	return (unsigned char)(255 - picture->image[x][y + rgb]);
}

unsigned char averaging_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores)
{
	int sum = 0;
	for (int i = 0; i < number_cores; i++)
		for (int j = 0; j < core_size; j++)
			for (int k = 0; k < core_size * picture->bytes_per_pixel; k += picture->bytes_per_pixel)
				sum += ((int***)core)[i][j][k / picture->bytes_per_pixel] * (int)picture->image[x + j][y + k + rgb];

	return (unsigned char)(sum / (core_size * core_size));
}

unsigned char gauss_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores)
{
	double sum = 0;
	for (int i = 0; i < number_cores; i++)
		for (int j = 0; j < core_size; j++)
			for (int k = 0; k < core_size * picture->bytes_per_pixel; k += picture->bytes_per_pixel)
				sum += ((double***)core)[i][j][k / picture->bytes_per_pixel] * (int)picture->image[x + j][y + k + rgb];

	return (unsigned char)((int)sum);
}

unsigned char grey_filter(struct bmp_image* picture, int x, int y)
{
	return 0.2126 * picture->image[x][y] + 0.7152 * picture->image[x][y + 1] + 0.0722 * picture->image[x][y + 2];
}

unsigned char sobel_x_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores)
{
	int sum = 0;
	int sensivity = (((int***)core)[0][0][0] * 2 - 1) * 60; // 60 for sobel filter and 300 for sharr filter
	for (int i = 0; i < core_size; i++)
		for (int j = 0; j < core_size * picture->bytes_per_pixel; j += picture->bytes_per_pixel)
			sum += ((int***)core)[0][i][j / picture->bytes_per_pixel] * (int)picture->image[x + i][y + j];

	return (abs(sum) < sensivity ? 0 : 255);
}

unsigned char sobel_y_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores)
{
	return sobel_x_filter(picture, x, y, rgb, core, core_size, number_cores);
}

unsigned char sobel_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores)
{
	int sum[2] = { 0 };
	int sensivity = (((int***)core)[0][0][0] * 2 - 1) * 60; // 60 for sobel filter and 300 for sharr filter
	for (int i = 0; i < number_cores; i++)
		for (int j = 0; j < core_size; j++)
			for (int k = 0; k < core_size * picture->bytes_per_pixel; k += picture->bytes_per_pixel)
				sum[i] += ((int***)core)[i][j][k / picture->bytes_per_pixel] * (int)picture->image[x + j][y + k + rgb];

	return (unsigned char)(sqrt(sum[0] * sum[0] + sum[1] * sum[1]) < sensivity ? 0 : 255);
}

unsigned char sharr_x_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores)
{
	return sobel_x_filter(picture, x, y, rgb, core, core_size, number_cores);
}

unsigned char sharr_y_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores)
{
	return sobel_x_filter(picture, x, y, rgb, core, core_size, number_cores);
}

unsigned char sharr_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores)
{
	return sobel_filter(picture, x, y, rgb, core, core_size, number_cores);
}