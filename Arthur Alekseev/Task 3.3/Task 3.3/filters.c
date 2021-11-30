#include "file_utils.h"
#include <stdlib.h>
#define byte unsigned char

// Function for monochrome filter
void monochrome_filter(struct bmp_file* target)
{
	int avg;
	for (int i = 0; i < (target->bits_per_pixel * target->width * target->height / 8); i += target->bits_per_pixel / 8)
	{
		avg = target->data[i] + target->data[i + 1] + target->data[i + 2];
		target->data[i] = avg / 3;
		target->data[i + 1] = avg / 3;
		target->data[i + 2] = avg / 3;
	}
}

// Function for median
byte med_func(byte* buffer)
{
	// Sort buffer
	byte temp;
	for (int i1 = 0; i1 < 9 - 1; i1++)
		for (int j1 = i1 + 1; j1 < 9; j1++)
		{
			if (buffer[i1] > buffer[j1])
			{
				temp = buffer[i1];
				buffer[i1] = buffer[j1];
				buffer[j1] = temp;
			}
		}
	return buffer[4];
}

// Runs for every pixel and applies some effect to it according by it's 3x3 area
byte base_filter_pixel(byte* buffer, float* matrix)
{
	int result = 0;
	for (int x = -1; x < 2; x++)
		for (int y = -1; y < 2; y++)
			result += buffer[(x + 1) + 3 * (y + 1)] * matrix[(x + 1) + 3 * (y + 1)];
	return min(255, max(0, abs(result)));
}

// Functions. They call base filter pixel with matrix arguments
byte sobel_x_func(byte* buffer)
{
	float matrix[] = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
	return base_filter_pixel(buffer, matrix);
}

byte sobel_y_func(byte* buffer)
{
	float matrix[] = { -1, -2, -1, 0, 0, 0, 1, 2, 1 };
	return base_filter_pixel(buffer, matrix);
}

byte gauss_func(byte* buffer)
{
	float matrix[] = { 1 / 16.0, 2 / 16.0, 1 / 16.0, 2 / 16.0, 4 / 16.0, 2 / 16.0, 1 / 16.0, 2 / 16.0, 1 / 16.0 };
	return base_filter_pixel(buffer, matrix);
}

// Base filter. It calls filters for pixels and gives them info about 3x3 area
void base_filter(struct bmp_file* target, byte(*func)(byte*), int clear_pixels)
{
	// Create copy of data
	byte* datacpy = (byte*)malloc(sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);
	memcpy(datacpy, target->data, sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);
	if (clear_pixels)
		memset(target->data, 0, sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);

	// Create buffer to sort and find med
	byte* buffer = (byte*)malloc(sizeof(byte) * 9);
	byte temp = 0;
	int offset = target->bits_per_pixel * target->width / 8;
	int max_size = offset * target->height;
	int byps = target->bits_per_pixel / 8;
	// Process filter
	for (int i = offset; i < max_size - offset; i += byps)
	{
		if (i % offset < 1 || max_size - i % offset < 1)
			continue;
		for (int j = 0; j < 3; j++)
		{
			// Collect data in 3x3 area (once for each channel)
			for (int x = -1; x < 2; x++)
				for (int y = -1; y < 2; y++)
					buffer[(x + 1) + 3 * (y + 1)] = datacpy[i + (y + 1) * offset + (x + 1) * byps + j];

			// Write element in the middle
			target->data[i + j] = (*func)(buffer);
		}
	}
	free(datacpy);
}
// Interfaces of the filters. These functions call base filter 3x3 with parameters
void med_filter(struct bmp_file* target)
{
	base_filter(target, med_func, 0);
}

void sobel_x(struct bmp_file* target)
{
	monochrome_filter(target);
	base_filter(target, sobel_x_func, 1);
}

void sobel_y(struct bmp_file* target)
{
	monochrome_filter(target);
	base_filter(target, sobel_y_func, 1);
}

void gauss_filter(struct bmp_file* target)
{
	base_filter(target, gauss_func, 0);
}

