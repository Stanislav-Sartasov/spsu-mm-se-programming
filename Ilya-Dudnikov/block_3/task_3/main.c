#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include "filters.h"

#pragma pack(push)
#pragma pack(1)
typedef struct bmp_file_header
{
	char signature[2];
	int file_size;
	int reserved;
	int data_offset;

	int header_size;
	int width;
	int height;
	short planes;
	short bits_per_pixel;
	int compression;
	int image_size;
	int pixel_per_meter_x;
	int pixel_per_meter_y;
	int colors_used;
	int important_colours;
} bmp_header;
#pragma pack(pop)

int apply_filter(unsigned char **image, char *filter_name, int width, int height, int bytes_per_pixel);

int main(int argc, char *argv[])
{
	if (argc != 4)
	{
		printf("Expected 3 arguments. Usage: <executable> <input file name> <filter name> <output file name>\n");
		printf("Available filter: greyscale\n");
		return 1;
	}

	FILE *input = fopen(argv[1], "rb");
	if (input == NULL)
	{
		fprintf(stderr, "Could not open the input file\n");
		return 1;
	}

	bmp_header bmph;
	fread(&bmph, 1, sizeof(bmp_header), input);
	if (bmph.signature[0] != 'B' || bmph.signature[1] != 'M')
	{
		fprintf(stderr, "File corrupted\n");
	}

	if (bmph.width % 4 && bmph.bits_per_pixel == 24)
		bmph.width += 4 - bmph.width % 4;

	int real_width = bmph.width * (bmph.bits_per_pixel / 8);

	unsigned char **image = (unsigned char**)malloc(bmph.height * sizeof(unsigned char*));
	for (int i = 0; i < bmph.height; i++)
	{
		image[i] = (unsigned char*)malloc(real_width * sizeof(unsigned char));
		fread(image[i], sizeof(unsigned char), real_width, input);
	}
	fclose(input);
	FILE *output = fopen(argv[3], "wb");
	if (output == NULL)
	{
		fprintf(stderr, "Could not open the output file\n");
		return 1;
	}

	if (!apply_filter(image, argv[2], real_width, bmph.height, bmph.bits_per_pixel / 8))
		return 1;

	fwrite(&bmph, 1, sizeof(bmp_header), output);
	for (int i = 0; i < bmph.height; i++)
	{
		fwrite(image[i], sizeof(char), real_width, output);
		free(image[i]);
	}
	printf("Filter applied successfully!");
	free(image);
	fclose(output);
	return 0;
}

int apply_filter(unsigned char **image, char *filter_name, int width, int height, int bytes_per_pixel)
{
	if (!strcmp(filter_name, "greyscale"))
		grey_filter(image, width, height, bytes_per_pixel);
	else if (!strcmp(filter_name, "average"))
		avg_filter(image, width, height, bytes_per_pixel);
	else if (!strcmp(filter_name, "gaussian"))
		gaussian_filter(image, width, height, bytes_per_pixel);
	else if (!strcmp(filter_name, "sobel_y"))
		sobel_filter(image, width, height, bytes_per_pixel, 0);
	else if (!strcmp(filter_name, "sobel_x"))
		sobel_filter(image, width, height, bytes_per_pixel, 1);
	else
	{
		printf("Expected 3 arguments. Usage: <executable> <input file name> <filter name> <output file name>\n");
		printf("Available filter: greyscale, average, gaussian, sobel_x, sobel_y\n");
		return 0;
	}
	return 1;
}