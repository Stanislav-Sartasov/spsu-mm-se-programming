#ifndef MY_FILE_UTILS
#define MY_FILE_UTILS
#include <stdio.h>
#include <stdlib.h>
#define byte unsigned char

int parse_int(byte* begin);

struct bmp_file 
{
	byte* header_info;
	byte* additional_info;
	byte* data;
	byte* color_palette;
	int size;
	int width;
	int height;
	int is_reverse;
	int bits_per_pixel;
	unsigned int color_palette_length;
};

struct bmp_file* parse(FILE* bmp_image);

void free_bmp_file(struct bmp_file* to_free);

#endif