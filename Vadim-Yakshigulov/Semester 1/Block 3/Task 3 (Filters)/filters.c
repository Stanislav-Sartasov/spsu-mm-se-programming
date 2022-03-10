#include "filters.h"
#include <memory.h>
#include <stdio.h>


int comparator(const void *x1, const void *x2)
{
	return *(uint8_t *) x1 - *(uint8_t *) x2;
}

uint8_t normalize(double value)
{
	if (value < 0)
		return 0;
	if (value > 255)
		return 255;
	return (int) value;
}

double absolute(double value)
{
	if (value > 0) return value;
	else return -value;
}

rgb_t get_normalized_pixel(double red, double green, double blue)
{
	rgb_t pixel;
	pixel.red = normalize(red);
	pixel.green = normalize(green);
	pixel.blue = normalize(blue);
	return pixel;
}


void apply_filter(bmp_image_t *image, int n, rgb_t (*filter)(uint8_t **))
{
	rgb_t *buffer = (rgb_t *) malloc(image->height * image->width * sizeof(rgb_t));
	memcpy(buffer, image->pixels, image->height * image->width * sizeof(rgb_t));

	int offset = (n - 1) / 2;
	for (int x = offset; x < image->height - offset; ++x)
	{
		for (int y = offset; y < image->width - offset; ++y)
		{
			int pos = x * image->width + y;
			uint8_t **rgb_matrix = malloc(3 * sizeof(uint8_t *));
			for (int i = 0; i < 3; i++)
				rgb_matrix[i] = malloc(n * n * sizeof(uint8_t));

			int current_matrix_position = 0;
			for (int a = -offset; a <= offset; ++a)
			{
				for (int b = -offset; b <= offset; ++b)
				{
					int matrix_position_offset = image->width * a + b;
					rgb_matrix[0][current_matrix_position] = buffer[pos + matrix_position_offset].red;
					rgb_matrix[1][current_matrix_position] = buffer[pos + matrix_position_offset].green;
					rgb_matrix[2][current_matrix_position] = buffer[pos + matrix_position_offset].blue;

					current_matrix_position++;
				}
			}
			image->pixels[pos] = filter(rgb_matrix);
			for (int i = 0; i < 3; i++)
				free(rgb_matrix[i]);
			free(rgb_matrix);
		}
	}
	free(buffer);
}

rgb_t greyscale_filter(uint8_t **matrix)
{

	double average = (matrix[0][0] + matrix[1][0] + matrix[2][0]) / 3.0;
	return get_normalized_pixel(average, average, average);
}

rgb_t median_filter(uint8_t **matrix)
{
	for (int color = 0; color < 3; color++)
		qsort(matrix[color], 9, sizeof(uint8_t), comparator);
	return get_normalized_pixel(matrix[0][4], matrix[1][4], matrix[2][4]);
}

rgb_t gaussian_filter_3x3(uint8_t **matrix)
{
	double result[3] = {0};
	int proportions[9] = {1, 2, 1, 2, 4, 2, 1, 2, 1};

	for (int color = 0; color < 3; color++)
	{
		for (int i = 0; i < 9; i++)
			result[color] += matrix[color][i] * proportions[i];
		result[color] = result[color] / 16.0;
	}
	return get_normalized_pixel(result[0], result[1], result[2]);
}

rgb_t gaussian_filter_5x5(uint8_t **matrix)
{
	int proportions[25] = {1, 4, 7, 4, 1, 4, 16, 26, 16, 4, 7, 26, 41, 26, 7, 4, 16, 26, 16, 4, 1, 4, 7, 4, 1};
	double result[3] = {0};
	for (int color = 0; color < 3; color++)
	{
		for (int i = 0; i < 25; i++)
			result[color] += matrix[color][i] * proportions[i];
		result[color] = result[color] / 273.0;
	}
	return get_normalized_pixel(result[0], result[1], result[2]);
}

rgb_t sobelX_filter(uint8_t **matrix)
{
	int proportions[9] = {-1, 0, 1, -2, 0, 2, -1, 0, 1};
	double result[3] = {0};
	for (int color = 0; color < 3; color++)
	{
		for (int i = 0; i < 9; i++)
			result[color] += matrix[color][i] * proportions[i];
		result[color] = absolute(result[color]);
	}
	return get_normalized_pixel(result[0], result[1], result[2]);
}

rgb_t sobelY_filter(uint8_t **matrix)
{
	int proportions[9] = {-1, -2, -1, 0, 0, 0, 1, 2, 1};
	double result[3] = {0};
	for (int color = 0; color < 3; color++)
	{
		for (int i = 0; i < 9; i++)
			result[color] += matrix[color][i] * proportions[i];
		result[color] = absolute(result[color]);
	}
	return get_normalized_pixel(result[0], result[1], result[2]);
}

