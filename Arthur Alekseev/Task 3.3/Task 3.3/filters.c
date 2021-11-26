#include "file_utils.h"
#include <stdlib.h>
#define byte unsigned char

// Mono
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

// SobelX Filter
void sobel_x(struct bmp_file* target)
{
	// Make image monochromatic
	monochrome_filter(target);
	// Create copy of data
	byte* datacpy = (byte*)malloc(sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);
	memcpy(datacpy, target->data, sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);

	int offset = target->bits_per_pixel * target->width / 8;
	int max_size = offset * target->height;
	int byps = target->bits_per_pixel / 8;
	// Process filter
	for (int i = offset; i < max_size - offset; i += byps)
	{
		if (i % offset < 1 || max_size - i < 1)
			continue;
		for (int j = 0; j < 3; j++)
			target->data[i + j] = abs(max(datacpy[i - byps + j] - datacpy[i + j], datacpy[i + byps + j] - datacpy[i + j]));
	}
	free(datacpy);
}

// SobelY
void sobel_y(struct bmp_file* target)
{
	// Make image monochromatic
	monochrome_filter(target);
	// Copy data
	byte* datacpy = (byte*)malloc(sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);
	memcpy(datacpy, target->data, sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);

	int offset = target->bits_per_pixel * target->width / 8;
	int max_size = offset * target->height;
	int byps = target->bits_per_pixel / 8;
	// Process image
	for (int i = offset; i < max_size - offset; i += byps)
	{
		if (i % offset < 1 || max_size - i < 1)
			continue;
		for (int j = 0; j < 3; j++)
			target->data[i + j] = abs(max(datacpy[i + j - offset] - datacpy[i + j], datacpy[i + offset + j] - datacpy[i + j]));
	}
	free(datacpy);
}

// MedFilter
void med_filter(struct bmp_file* target)
{
	// Create copy of data
	byte* datacpy = (byte*)malloc(sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);
	memcpy(datacpy, target->data, sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);
	// Create buffer to sort and find med
	byte* buffer = (byte*)malloc(sizeof(byte) * 9);

	byte temp = 0;
	int offset = target->bits_per_pixel * target->width / 8;
	int max_size = offset * target->height;
	int byps = target->bits_per_pixel / 8;
	// Process filter
	for (int i = offset; i < max_size - offset; i += byps)
	{
		if (i % offset < 1 || max_size - i < 1)
			continue;
		for (int j = 0; j < 3; j++)
		{
			// Collect data in 3x3 area
			buffer[0] = datacpy[i - offset - byps + j];
			buffer[1] = datacpy[i - offset + j];
			buffer[2] = datacpy[i - offset + byps + j];
			buffer[3] = datacpy[i - byps + j];
			buffer[4] = datacpy[i + j];
			buffer[5] = datacpy[i + byps + j];
			buffer[6] = datacpy[i + offset - byps + j];
			buffer[7] = datacpy[i + offset + j];
			buffer[8] = datacpy[i + offset + byps + j];

			// Sort buffer
			for (int i1 = 0; i1 < 9 - 1; i1++) 
			{
				for (int j1 = i1 + 1; j1 < 9; j1++) 
				{
					if (buffer[i1] > buffer[j1]) {
						temp = buffer[i1];
						buffer[i1] = buffer[j1];
						buffer[j1] = temp;
					}
				}
			}

			// Write element in the middle
			target->data[i + j] = buffer[4];
		}
	}

	free(buffer);
	free(datacpy);
}

// Gauss3
void gauss3_filter(struct bmp_file* target)
{
	// Copy data
	byte* datacpy = (byte*)malloc(sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);
	memcpy(datacpy, target->data, sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);

	// 3x3 gauss kernel
	float matrix[] = { 1 / 16.0, 1 / 8.0, 1 / 16.0, 1 / 8.0, 1 / 4.0, 1 / 8.0, 1 / 16.0, 1 / 8.0, 1 / 16.0 };

	int offset = target->bits_per_pixel * target->width / 8;
	int max_size = offset * target->height;
	int byps = target->bits_per_pixel / 8;
	// Filter processing
	for (int i = offset; i < max_size - offset; i += byps)
	{
		if (i % offset < 1 || max_size - i < 1)
			continue;
		for (int j = 0; j < 3; j++)
			target->data[i + j] = datacpy[i - offset - byps + j] * matrix[0] + datacpy[i - offset + j] * matrix[1] + datacpy[i - offset + byps + j] * matrix[2] +
			datacpy[i - byps + j] * matrix[3] + datacpy[i + j] * matrix[4] + datacpy[i + byps + j] * matrix[5] +
			datacpy[i + offset - byps + j] * matrix[6] + datacpy[i + offset + j] * matrix[7] + datacpy[i + offset + byps + j] * matrix[8];
	}
	free(datacpy);
}

// Gauss5
void gauss5_filter(struct bmp_file* target)
{
	// Create a copy of data
	byte* datacpy = (byte*)malloc(sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);
	memcpy(datacpy, target->data, sizeof(byte) * (int)(target->bits_per_pixel / 8) * target->width * target->height);

	// Guassin kernel
	float matrix[] = { 1, 4, 7, 4, 1, 4, 16, 26, 16, 4, 7, 26, 41, 26, 7, 4, 16, 26, 16, 4, 1, 4, 7, 4, 1 };

	int offset = target->bits_per_pixel * target->width / 8;
	int max_size = offset * target->height;
	int byps = target->bits_per_pixel / 8;
	int current = 0;

	// Process filter
	for (int i = offset * 2; i < max_size - offset * 2; i += byps)
	{
		if (i % offset < 2 || max_size - i < 2)
			continue;
		for (int j = 0; j < 3; j++)
		{
			current = 0;
			for (int x = 0; x < 5; x++)
				for (int y = 0; y < 5; y++)
					current += matrix[5 * x + y] * datacpy[i - offset * (y - 2) - 3 * (x - 2)];
			target->data[i + j] = current / 273;
		}
	}
	free(datacpy);
}

