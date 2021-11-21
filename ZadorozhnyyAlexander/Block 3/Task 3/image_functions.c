#pragma once
#include "structs.h"
#include "image_functions.h"

image read_image(FILE* file, int height, int width, int bits_per_pixel)
{
	image picture;
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

image copy_image(image copy_from)
{
	image picture;
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

int save_bmp_file(bitmap_file file, image picture, char* path)
{
	FILE* outfile;
	if (!(outfile = fopen(path, "wb")))
	{
		printf("Unable to open the file to record the image\n");
		return 0;
	}
	fwrite(&file, 1, sizeof(bitmap_file), outfile);

	for (int i = picture.height - 1; i >= 0; i--)
	{
		fwrite(picture.rgb[i], 1, ((file.bitsperpixel * picture.width + 31) / 32) * 4, outfile);
	}
	fclose(outfile);
	return 1;
}

void free_image(image picture)
{
	for (int i = picture.height - 1; i >= 0; i--)
	{
		free(picture.rgb[i]);
	}
	free(picture.rgb);
}