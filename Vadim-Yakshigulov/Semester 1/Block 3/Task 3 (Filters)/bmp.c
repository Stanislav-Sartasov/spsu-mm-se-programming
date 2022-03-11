#include "bmp.h"
#include <stdio.h>
#include <stdlib.h>
#include <stdbool.h>

void invalid_file_error(const char *filename)
{
	printf("Error was occurred while opening/creating file %s", filename);
	exit(-1);
}

void is_not_a_bmp_file_error(const char *filename)
{
	printf("File format is not valid %s\n", filename);
	printf("Try *.bmp file");
	exit(-1);
}

bool valid_header(bmp_header_t *header)
{
	return (header->bits_per_pixel == 24 || header->bits_per_pixel == 32)
		   && header->signature[0] == 'B' && header->signature[1] == 'M';
}

void read_header(FILE *file, bmp_header_t *header)
{
	fread(header, sizeof(*header), 1, file);
}

void write_header(FILE *file, bmp_header_t *header)
{
	fwrite(header, sizeof(*header), 1, file);
}

void read_pixels(FILE *file, bmp_header_t *header, bmp_image_t *image)
{
	image->height = header->height;
	image->width = header->width;
	image->pixels = (rgb_t *) malloc(image->height * image->width * sizeof(rgb_t));

	fseek(file, header->data_offset, SEEK_SET);
	int i, added;
	if (header->bits_per_pixel == 24)
	{
		added = (int) (4 - ((image->width * sizeof(rgb_t)) % 4)) % 4;
		for (i = 0; i < (image->height); i++)
		{
			fread(&image->pixels[image->width * i], sizeof(rgb_t) * image->width, 1, file);
			fseek(file, added, SEEK_CUR);
		}
	}
	else
	{
		for (i = 0; i < image->height * image->width; i++)
		{
			fread(&(image->pixels[i]), sizeof(rgb_t), 1, file);
			fseek(file, 1, SEEK_CUR);
		}
	}
}

void write_pixels(FILE *file, bmp_header_t *header, bmp_image_t *image)
{
	char gap[1] = "\0";
	if (header->bits_per_pixel == 24)
	{
		int added = (int) (4 - ((header->width * 3) % 4)) % 4;
		for (int i = 0; i < header->height; i++)
		{
			fwrite(&image->pixels[header->width * i], sizeof(rgb_t) * header->width, 1, file);
			fwrite(&gap, 1, added, file);
		}
	}
	else
	{
		for (int i = 0; i < image->height * image->width; i++)
		{
			fwrite(&image->pixels[i], 3, 1, file);
			fwrite(&gap, sizeof(char), 1, file);
		}
	}

}

void bmp_open(const char *filename, bmp_header_t *header, bmp_image_t *image)
{
	FILE *file = fopen(filename, "rb");
	if (file == NULL)
		invalid_file_error(filename);

	read_header(file, header);

	if (!valid_header(header))
		is_not_a_bmp_file_error(filename);

	read_pixels(file, header, image);
	fclose(file);
}

void bmp_create(const char *filename, bmp_header_t *header, bmp_image_t *image)
{
	FILE *file = fopen(filename, "wb");
	if (file == NULL)
		invalid_file_error(filename);

	write_header(file, header);
	write_pixels(file, header, image);
	fclose(file);
}

void bmp_image_free(bmp_image_t *image)
{
	free(image->pixels);
}
