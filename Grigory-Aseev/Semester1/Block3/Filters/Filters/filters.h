#pragma once

#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include <string.h>

#pragma pack(push, 1)
typedef struct bmp_file_header
{
	char bf_type[2];
	unsigned int bf_size;
	unsigned short bf_reserved1;
	unsigned short bf_reserved2;
	unsigned int bf_offbits;
} bmp_file_header;

typedef struct bmp_info_header
{
	unsigned int bi_size;
	unsigned int bi_width;
	unsigned int bi_height;
	unsigned short bi_planes;
	unsigned short bi_bit_count;
	unsigned int bi_compression;
	unsigned int bi_size_image;
	unsigned int bi_x_pels_per_meter;
	unsigned int bi_y_pels_per_meter;
	unsigned int bi_clr_used;
	unsigned int bi_clr_important;
} bmp_info_header;

#pragma pack(pop)

typedef struct pixel {
	unsigned char rgb[3];
} pixel;

typedef struct image
{
	struct pixel** bits;
	unsigned int height, width;
} image;

int read_bmp(FILE* file, struct bmp_file_header* file_head, struct bmp_info_header* file_info, struct image* img, char* palette, int* palette_size, int* padding);
int write_bmp(FILE* file, struct bmp_file_header file_head, struct bmp_info_header file_info, struct image* img, char* palette, int palette_size, int padding);
void filter(struct image* img, char* str);