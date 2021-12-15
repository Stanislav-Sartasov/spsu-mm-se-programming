#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "filters_and_file_functions.h"

int main(int argc, char** argv)
{
	struct bitmap_header header;
	struct dib_header dibheader;
	struct image image;
	if (argc != 4)
	{
		printf("Not enough arguments or more than enough arguments\n");
		return 1;
	}
	FILE* fp = fopen(argv[1], "rb");
	if (fp == NULL)
	{
		printf("Error reading file\n");
		return 1;
	}
	fread(header.name, 2, 1, fp);
	fread(&header.size, 3 * sizeof(int), 1, fp);
	fread(&dibheader, sizeof(struct dib_header), 1, fp);
	fseek(fp, header.image_offset, SEEK_SET);
	image = read_image(fp, dibheader.height, dibheader.width, dibheader.bits_per_pixel);
	if (apply_filters(image, argv[2], argv[3], dibheader, header))
	{
		printf("Filter was applied and you can see filtered image in <");
		for (int i = 0; i < strlen(argv[2]); i++)
		{
			printf("%c", argv[2][i]);
		}
		printf(">\n");
	}
	fclose(fp);
	return 0;
}