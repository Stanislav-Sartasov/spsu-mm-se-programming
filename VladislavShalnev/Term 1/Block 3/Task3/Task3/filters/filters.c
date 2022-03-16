#include <stdlib.h>
#include <string.h>

#include "filters.h"
#include "../bmp/bmp.h"

void filter(bmp_image_t* image, int (*filter_function)(int*))
{
	int windows[3][9];

	int size = image->width * image->height;

	pixel* buffer = (pixel*)malloc(size * sizeof(pixel));
	memcpy(buffer, image->data, size * sizeof(pixel));

	for (int index = 0; index < size; index++)
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				int column = index % image->width;
				int row = index / image->width;

				// checking if we've gone outside the picture
				int extra_index = index;
				if (column + (j - 1) < image->width && column + (j - 1) >= 0)
					extra_index += (j - 1);
				if (row + (i - 1) < image->height && row + (i - 1) >= 0)
					extra_index += (i - 1) * image->width;
				//
				
				windows[0][i * 3 + j] = buffer[extra_index].red;
				windows[1][i * 3 + j] = buffer[extra_index].green;
				windows[2][i * 3 + j] = buffer[extra_index].blue;
			}
		}

		image->data[index].red = filter_function(windows[0]);
		image->data[index].green = filter_function(windows[1]);
		image->data[index].blue = filter_function(windows[2]);
	}

	free(buffer);
}

int compare(const int* arg1, const int* arg2)
{
	return *arg2 - *arg1;
}

int median_function(int* window)
{
	qsort(window, 9, sizeof(int), compare);

	return window[4];
}

int apply_matrix(int* window, int* matrix)
{
	float result = 0;

	for (int i = 0; i < 9; i++)
		result += window[i] * matrix[i];

	return (int)result;
}

int gauss_function(int* window)
{
	int matrix[9] = {
		1, 2, 1,
		2, 4, 2,
		1, 2, 1
	};

	// duplicating code due to division by 16
	float result = 0;

	for (int i = 0; i < 9; i++)
		result += window[i] * matrix[i] / 16;

	return (int)result;
}

int sobel_x_function(int* window)
{
	int matrix[9] = {
		-1, 0, 1,
		-2, 0, 2,
		-1, 0, 1
	};

	return min(abs(apply_matrix(window, matrix)), 255);
}

int sobel_y_function(int* window)
{
	int matrix[9] = {
		-1, -2, -1,
		0, 0, 0,
		1, 2, 1
	};

	return min(abs(apply_matrix(window, matrix)), 255);
}

void grayscale(bmp_image_t* image)
{
	int average;
	for (int i = 0; i < image->width * image->height; i++)
	{
		average = (image->data[i].red + image->data[i].green + image->data[i].blue) / 3;
		image->data[i].red = average;
		image->data[i].green = average;
		image->data[i].blue = average;
	}
}

void sobel_x(bmp_image_t* image)
{
	grayscale(image);
	filter(image, sobel_x_function);
}

void sobel_y(bmp_image_t* image)
{
	grayscale(image);
	filter(image, sobel_y_function);
}

void median(bmp_image_t* image)
{
	filter(image, median_function);
}

void gauss(bmp_image_t* image)
{
	filter(image, gauss_function);
}