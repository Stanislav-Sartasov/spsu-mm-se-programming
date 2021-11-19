#pragma once
#include "structs.h"
#include "image_functions.h"

Image read_image(FILE* file, int height, int width, int bits_per_pixel)
{
	Image picture;
	picture.height = height;
	picture.width = width;
	picture.bits_per_pixel = bits_per_pixel;
	picture.rgb = (rgb_colors**)malloc(height * sizeof(void*));
	int bytestoread = ((bits_per_pixel * width + 31) / 32) * 4;
	int num_of_rgb = bytestoread / sizeof(rgb_colors) + 1;

	for (int i = height - 1; i >= 0; i--)
	{
		picture.rgb[i] = (rgb_colors*)malloc(num_of_rgb * sizeof(rgb_colors));
		fread(picture.rgb[i], 1, bytestoread, file);
	}

	return picture;
}

Image copy_image(Image copy_from)
{
	Image picture;
	picture.height = copy_from.height;
	picture.width = copy_from.width;
	picture.bits_per_pixel = copy_from.bits_per_pixel;
	picture.rgb = (rgb_colors**)malloc(picture.height * sizeof(void*));
	int bytestoread = ((picture.bits_per_pixel * picture.width + 31) / 32) * 4;
	int num_of_rgb = bytestoread / sizeof(rgb_colors) + 1;

	for (int i = picture.height - 1; i >= 0; i--)
	{
		picture.rgb[i] = (rgb_colors*)malloc(num_of_rgb * sizeof(rgb_colors));
		for (int j = 0; j < picture.width; j++)
		{
			picture.rgb[i][j] = copy_from.rgb[i][j];
		}
	}
	return picture;
}

int save_bmp_file(Bitmap_file file, Image picture, char* path)
{
	FILE* outfile;
	if (!(outfile = fopen(path, "wb")))
	{
		printf("Unable to open the file to record the image\n");
		return 0;
	}
	fwrite(&file, 1, sizeof(Bitmap_file), outfile);

	for (int i = picture.height - 1; i >= 0; i--)
	{
		fwrite(picture.rgb[i], 1, ((file.bitsperpixel * picture.width + 31) / 32) * 4, outfile);
	}
	fclose(outfile);
	return 1;
}

void free_image(Image picture)
{
	for (int i = picture.height - 1; i >= 0; i--)
	{
		free(picture.rgb[i]);
	}
	free(picture.rgb);
}