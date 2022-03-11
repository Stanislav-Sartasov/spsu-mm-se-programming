#include "filters.h"
#include <string.h>
#include <stdlib.h>
#include <stdio.h>

void filter(uint8_t* pixels, bitmap_info_header* info_header, uint8_t* output, int filter_type)
{

	/*
	 * filter_type:
	 * 0 - median filter
	 * 1 - gaussian filter
	 * 2 - sobel x filter
	 * 3 - sobel y filter
	 */

	int width = info_header->width;
	int height = info_header->height;
	int pixel_size = info_header->color_depth / 8;
	int useful_row_length = width * pixel_size;
	int row_length = width * pixel_size + (4 - (useful_row_length % 4)) % 4;
	int offsets_x[9] = {-1, 0, 1, -1, 0 , 1, -1, 0 , 1};
	int offsets_y[9] = {-1, -1, -1, 0 , 0, 0, 1, 1, 1};

	rgb window[9];
	double gaussian_kernel[9];
	int sobel_kernel[9];
	if (filter_type == 1)
	{
		double gaussian_kernel_values[9] = {1.0 / 16, 1.0 / 8, 1.0 / 16, 1.0 / 8, 1.0 / 4, 1.0 / 8, 1.0 / 16, 1.0 / 8, 1.0 / 16};
		memcpy(gaussian_kernel, gaussian_kernel_values, sizeof(gaussian_kernel_values));
	}
	else if (filter_type == 2)
	{
		int sobel_x_kernel[9] = {-1, -2, -1, 0, 0, 0, 1, 2, 1};
		memcpy(sobel_kernel, sobel_x_kernel, sizeof(sobel_x_kernel));
	}
	else if (filter_type == 3)
	{
		int sobel_y_kernel[9]= {-1, 0, 1, -2, 0 , 2, -1, 0, 1};
		memcpy(sobel_kernel, sobel_y_kernel, sizeof(sobel_y_kernel));
	}
	else if (filter_type != 0)
	{
		printf("Filter mode %d not supported! Exiting...\n", filter_type);
		exit(-1);
	}

	for (int y = 0; y < height; y++)
	{
		for (int x = 0; x < width; x++)
		{
			int pixel_offset = y * row_length + x * pixel_size;
			int pixel_grayscale = 0;
			uint8_t output_grayscale, red = 0, green = 0, blue = 0;
			rgb output_pixel;
			for (int j = 0; j < 9; j++)
			{
				rgb pixel;
				int offset_x = offsets_x[j];
				int offset_y = offsets_y[j];
				offset_x = (x + offset_x > 0) && (x + offset_x < width) ? offset_x : -offset_x;
				offset_y = (y + offset_y > 0) && (y + offset_y < height) ? offset_y : -offset_y;
				uint8_t* pixel_pointer = pixels + pixel_offset + offset_y * row_length + offset_x * pixel_size;
				pixel = get_rgb(pixel_pointer, info_header);
				if (filter_type == 0)
				{
					window[j] = pixel;
				}
				else if (filter_type == 1)
				{
					red += pixel.red * gaussian_kernel[j];
					green += pixel.green * gaussian_kernel[j];
					blue += pixel.blue * gaussian_kernel[j];
				}
				else if ((filter_type == 2) || (filter_type == 3))
				{
					int relative_luminance = (int)(0.2126 * (pixel.red) + 0.7152 * (pixel.green) + 0.0722 * (pixel.blue));
					pixel_grayscale += (relative_luminance * sobel_kernel[j]);
				}
			}
			if (filter_type == 0)
			{
				qsort(window, 9, sizeof(rgb), compare_pixels);
				output_pixel = window[4];
			}
			else if (filter_type == 1)
			{
				output_pixel.red = red;
				output_pixel.green = green;
				output_pixel.blue = blue;
			}
			else if ((filter_type == 2) || (filter_type == 3))
			{
				output_grayscale = abs(pixel_grayscale);
				output_pixel.red = output_grayscale;
				output_pixel.green = output_grayscale;
				output_pixel.blue = output_grayscale;
			}
			put_rgb(output + pixel_offset, info_header, output_pixel);
		}
	}
}

void grayscale(uint8_t* pixels, bitmap_info_header* info_header, uint8_t* output)
{
	int width = info_header->width;
	int height = info_header->height;
	int pixel_size = info_header->color_depth / 8;
	int useful_row_length = width * pixel_size;
	int row_length = width * pixel_size + (4 - (useful_row_length % 4)) % 4;
	for (int y = 0; y < height; y++)
	{
		for (int x = 0; x < width; x++)
		{
			int pixel_offset = y * row_length + x * pixel_size;
			rgb pixel = get_rgb(pixels + pixel_offset, info_header);
			int relative_luminance = (int)(0.2126 * (pixel.red) + 0.7152 * (pixel.green) + 0.0722 * (pixel.blue));
			rgb output_pixel = { .red = relative_luminance / 3, .green = relative_luminance / 3, .blue = relative_luminance / 3};
			put_rgb(output + pixel_offset, info_header, output_pixel);
		}
	}
}
