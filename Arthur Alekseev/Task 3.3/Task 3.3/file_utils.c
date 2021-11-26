#include "file_utils.h"
#include <stdio.h>
#include <stdlib.h>

int parse_int(byte* begin) 
{
	// Converts four bytes in a integer 
	return 16777216 * begin[3] + 65536 * begin[2] + 256 * begin[1] + begin[0];
}

struct bmp_file* parse(FILE* bmp_image) 
{
	struct bmp_file* result = (struct bmp_file*)malloc(sizeof(struct bmp_file));
	byte next;
	// Memory allocation
	result->header_info = (byte*)malloc(sizeof(byte) * 14);
	result->additional_info = (byte*)malloc(sizeof(byte) * 32);

	// Header
	for (int i = 0; i < 14; i++)
		fread(&(result->header_info[i]), 1, 1, bmp_image);

	// Additional info
	for (int i = 0; i < 16; i++)
		fread(&(result->additional_info[i]), 1, 1, bmp_image);
	if (parse_int(&(result->additional_info[0])) != 12)
		for (int i = 0; i < parse_int(&(result->additional_info[0])) - 16; i++)
			fread(&(result->additional_info[i + 16]), 1, 1, bmp_image);

	if (parse_int(&(result->additional_info[0])) != 12)
	{
		result->height = parse_int(&(result->additional_info[8]));
		result->width = parse_int(&(result->additional_info[4]));
		result->is_reverse = 0;
		if (result < 0)
			result->is_reverse = 1;
		
		result->bits_per_pixel = 256 * result->additional_info[15] + result->additional_info[14];
		result->color_palette_length = (unsigned int)parse_int(&(result->additional_info[32]));
		result->color_palette = (byte*)malloc(sizeof(byte) * result->color_palette_length);
		for (int i = 0; i < result->color_palette_length * result->bits_per_pixel; i++)
			fread(&(result->color_palette[i]), 1, 1, bmp_image);
	}
	else
		return NULL;

	if (result->bits_per_pixel != 32 && result->bits_per_pixel != 24)
		return NULL;
	
	// Pixels begining
	result->size = (unsigned int)parse_int(&(result->header_info[2]));
	fseek(bmp_image, parse_int(&(result->header_info[10])), SEEK_SET);
	// Pixel data reading
	result->data = (byte*)malloc(result->size - 54);
	int i = 0;
	while (!feof(bmp_image)) 
	{
		fread(&(result->data[i]), 1, 1, bmp_image);
		i++;
	}

	return result;
}

void free_bmp_file(struct bmp_file* to_free)
{
	free(to_free->header_info);
	free(to_free->data);
	free(to_free->color_palette);
	free(to_free);
}
