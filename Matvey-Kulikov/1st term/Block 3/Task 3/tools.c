#include "tools.h"
#include <stdlib.h>
#include <stdio.h>

int get_mask_offset(uint32_t mask)
{
	if (mask < 256)
	{
		return 0;
	}
	else if (mask < 65536)
	{
		return 8;
	}
	else if (mask < 16777216)
	{
		return 16;
	}
	return 24;
}

rgb get_rgb(const uint8_t* pixel, bitmap_info_header* info_header)
{
	rgb pixel_rgb;
	if (info_header->color_depth == 24)
	{
		pixel_rgb.red = pixel[2];
		pixel_rgb.green = pixel[1];
		pixel_rgb.blue = pixel[0];
	}
	else if (info_header->color_depth == 32)
	{
		if (info_header->red_mask && info_header->green_mask && info_header->blue_mask)
		{
			uint32_t pixel_value = *(uint32_t*)pixel;
			pixel_rgb.red = (int8_t)((pixel_value & info_header->red_mask) >> get_mask_offset(info_header->red_mask));
			pixel_rgb.green = (int8_t)((pixel_value & info_header->green_mask) >> get_mask_offset(info_header->green_mask));
			pixel_rgb.blue = (int8_t)((pixel_value & info_header->blue_mask) >> get_mask_offset(info_header->blue_mask));
		}
		else
		{
			pixel_rgb.red = pixel[1];
			pixel_rgb.green = pixel[2];
			pixel_rgb.blue = pixel[3];
		}
	}
	else
	{
		printf("Not 24/32 bpp images are not supported! Exiting...\n");
		exit(-1);
	}
	return pixel_rgb;
}

void put_rgb(uint8_t* pixel, bitmap_info_header* info_header, rgb pixel_rgb)
{
	if (info_header->color_depth == 24)
	{
		pixel[2] = pixel_rgb.red;
		pixel[1] = pixel_rgb.green;
		pixel[0] = pixel_rgb.blue;
	}
	else if (info_header->color_depth == 32)
	{
		if (info_header->red_mask && info_header->green_mask && info_header->blue_mask)
		{
			pixel[get_mask_offset(info_header->red_mask) / 8] = pixel_rgb.red;
			pixel[get_mask_offset(info_header->green_mask) / 8] = pixel_rgb.green;
			pixel[get_mask_offset(info_header->blue_mask) / 8] = pixel_rgb.blue;
		}
		else
		{
			pixel[2] = pixel_rgb.red;
			pixel[1] = pixel_rgb.green;
			pixel[0] = pixel_rgb.blue;
		}
	}
	else
	{
		printf("Not 24/32 bpp images are not supported! Exiting...\n");
		exit(-1);
	}
}

int compare_pixels(const void* pixel_1_pointer, const void* pixel_2_pointer)
{
	rgb* pixel_1 = (rgb*)pixel_1_pointer;
	rgb* pixel_2 = (rgb*)pixel_2_pointer;
	int relative_luminance_1 = (int)(0.2126 * (pixel_1->red) + 0.7152 * (pixel_1->green) + 0.0722 * (pixel_1->blue));
	int relative_luminance_2 = (int)(0.2126 * (pixel_2->red) + 0.7152 * (pixel_2->green) + 0.0722 * (pixel_2->blue));
	return relative_luminance_1 - relative_luminance_2;
}
