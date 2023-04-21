//
// Created by Вячеслав Бучин on 26.11.2021.
//

#include <math.h>
#include <stdlib.h>
#include <memory.h>

#include "kernel.h"

typedef unsigned char byte;

byte normalize(double value)
{
	if (value < 0)
		return 0;
	if (value > 255)
		return 255;
	return (int) round(value);
}

void apply_kernel(bmp_t* image, double kernel[3][3], double (*modify)(double, double))
{
	int height = image->header.height;
	int width = image->header.width;
	int width_bytes = width * image->header.bits_per_pixel / 8;

	byte** old_content = (byte**) malloc(height * sizeof(byte*));

	width_bytes = width_bytes + (4 - width_bytes % 4) % 4;
	for (int i = 0; i < height; i++)
	{
		old_content[i] = (byte*) malloc(width_bytes * sizeof(byte));
		memcpy(old_content[i], image->content[i], width_bytes);
	}


	int bytes_per_pixel = image->header.bits_per_pixel / 8;
	for (int i = 0; i < image->header.height - 2; i++)
	{
		for (int j = 0; j < (image->header.width - 2) * bytes_per_pixel; j += bytes_per_pixel)
		{
			for (int color = 0; color < 3; color++)
			{
				double new_value = old_content[i][j + color] * kernel[0][0];
				for (int row = 0; row < 3; row++)
					for (int col = 0; col < 3; col++)
						if (row == 0 && col == 0)
							continue;
						else
							new_value = modify(new_value, old_content[i + row][j + col * bytes_per_pixel + color] * kernel[row][col]);
				image->content[i + 1][j + bytes_per_pixel + color] = normalize(new_value);
			}
		}
	}

	for (int i = 0; i < height; i++)
		free(old_content[i]);
	free(old_content);
}