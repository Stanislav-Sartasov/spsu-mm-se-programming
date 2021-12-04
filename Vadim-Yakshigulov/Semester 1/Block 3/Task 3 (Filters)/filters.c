#include "filters.h"
#include <memory.h>


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


void apply_3x3_filter(bmp_image_t *image, uint8_t (*filter)(uint8_t *))
{
	rgb_t *buffer = (rgb_t *) malloc(image->height * image->width * sizeof(rgb_t));
	memcpy(buffer, image->pixels, image->height * image->width * sizeof(rgb_t));

	int offset = 1;
	for (int x = offset; x < image->height - offset; ++x)
	{
		for (int y = offset; y < image->width - offset; ++y)
		{
			int pos = x * image->width + y;
			uint8_t red_matrix[9];
			uint8_t green_matrix[9];
			uint8_t blue_matrix[9];

			int current_matrix_position = 0;
			for (int a = -1; a < 2; ++a)
			{
				for (int b = -1; b < 2; ++b)
				{
					int matrix_position_offset = image->width * a + b;
					red_matrix[current_matrix_position] = buffer[pos + matrix_position_offset].red;
					green_matrix[current_matrix_position] = buffer[pos + matrix_position_offset].green;
					blue_matrix[current_matrix_position] = buffer[pos + matrix_position_offset].blue;

					current_matrix_position++;
				}
			}
			image->pixels[pos].red = filter(red_matrix);
			image->pixels[pos].green = filter(green_matrix);
			image->pixels[pos].blue = filter(blue_matrix);
		}
	}
	free(buffer);
}

void apply_greyscale_filter(bmp_image_t *image)
{
	int average;
	for (int i = 0; i < image->height * image->width; i++)
	{
		average = (image->pixels[i].blue + image->pixels[i].green + image->pixels[i].red) / 3;
		image->pixels[i].blue = average;
		image->pixels[i].green = average;
		image->pixels[i].red = average;
	}
}

uint8_t median_filter(uint8_t *matrix)
{
	qsort(matrix, 9, sizeof(uint8_t), comparator);
	return normalize(matrix[4]);
}

uint8_t gaussian_filter(uint8_t *matrix)
{
	int proportions[9] = {1, 2, 1, 2, 4, 2, 1, 2, 1};
	double result = 0;
	for (int i = 0; i < 9; i++)
		result += matrix[i] * proportions[i] / 16.0;

	return normalize(result);
}

uint8_t sobelX_filter(uint8_t *matrix)
{
	int proportions[9] = {-1, 0, 1, -2, 0, 2, -1, 0, 1};
	double result = 0;
	for (int i = 0; i < 9; i++)
	{
		result += matrix[i] * proportions[i];
	}
	return normalize(result);
}

uint8_t sobelY_filter(uint8_t *matrix)
{
	int proportions[9] = {-1, -2, -1, 0, 0, 0, 1, 2, 1};
	double result = 0;
	for (int i = 0; i < 9; i++)
	{
		result += matrix[i] * proportions[i];
	}
	return normalize(result);
}

