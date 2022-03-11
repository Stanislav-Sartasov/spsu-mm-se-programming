//
// Created by Вячеслав Бучин on 23.11.2021.
//

#include <stdlib.h>

#include "bmp.h"

typedef unsigned char byte;

int validate_header(bmp_header_t* header)
{
	if (header->signature[0] != 'B' || header->signature[1] != 'M' ||
		(header->bits_per_pixel != 24 && header->bits_per_pixel != 32))
		return BMP_UNSUPPORTED_FILE;

	return 0;
}

void read_header(FILE* file, bmp_header_t* bmp)
{
	fread(bmp, sizeof(bmp_header_t), 1, file);
}

int read_content(FILE* file, bmp_t* bmp)
{
	int height = bmp->header.height;
	int width = bmp->header.width;

	bmp->content = (byte**) malloc(height * sizeof(byte*));
	if (bmp->content == NULL)
		return BMP_MALLOC_ERROR;

	int width_bytes = width * bmp->header.bits_per_pixel / 8;
	width_bytes = width_bytes + (4 - width_bytes % 4) % 4;
	for (int i = height - 1; i >= 0; i--)
	{
		bmp->content[i] = (byte*) malloc(width_bytes * sizeof(byte));
		if (bmp->content[i] == NULL)
		{
			for (int j = height - 1; j > i; j--)
				free(bmp->content[j]);
			free(bmp->content);
			return BMP_MALLOC_ERROR;
		}

		int read_bytes = (int) fread(bmp->content[i], sizeof(byte), width_bytes, file);
		if (read_bytes != width_bytes)
			return BMP_DAMAGED_FILE;
	}

	if (fgetc(file) != EOF)
		return BMP_DAMAGED_FILE;

	return 0;
}

int bmp_load(const char* filename, bmp_t** destination)
{
	*destination = (bmp_t*) malloc(sizeof(bmp_t));
	if (*destination == NULL)
		return BMP_MALLOC_ERROR;

	FILE* source = fopen(filename, "rb");
	if (source == NULL)
		return BMP_FILE_OPENING_ERROR;

	read_header(source, &(*destination)->header);

	int validation_code = validate_header(&(*destination)->header);
	if (validation_code != 0)
		return validation_code;

	int content_reading_code = read_content(source, *destination);
	if (content_reading_code != 0)
		return content_reading_code;

	fclose(source);
	return 0;
}

void bmp_free(bmp_t* bmp)
{
	int height = bmp->header.height;

	for (int i = 0; i < height; i++)
		free(bmp->content[i]);
	free(bmp->content);
	free(bmp);
}

int bmp_write(const char* filename, bmp_t* bmp)
{
	FILE* destination = fopen(filename, "wb");
	if (destination == NULL)
		return BMP_FILE_OPENING_ERROR;

	fwrite(&bmp->header, sizeof(bmp_header_t), 1, destination);

	int height = bmp->header.height;
	int width = bmp->header.width;
	int width_bytes = width * bmp->header.bits_per_pixel / 8;
	width_bytes = width_bytes + (4 - width_bytes % 4) % 4;

	for (int i = height - 1; i >= 0; i--)
		fwrite(bmp->content[i], sizeof(byte) * width_bytes, 1, destination);

	fclose(destination);
	return 0;
}
