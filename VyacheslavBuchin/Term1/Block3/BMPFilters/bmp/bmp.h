//
// Created by Вячеслав Бучин on 23.11.2021.
//

#ifndef BMPFILTERS_BMP_H
#define BMPFILTERS_BMP_H

#include <inttypes.h>
#include <stdio.h>

#define BMP_FILE_OPENING_ERROR -1
#define BMP_MALLOC_ERROR -2
#define BMP_UNSUPPORTED_FILE -3
#define BMP_DAMAGED_FILE -4

#pragma pack(push, 1)

typedef struct bmp_header
{
	char signature[2];
	int32_t file_size;
	int32_t reserved;
	int32_t data_offset;

	int32_t size;
	int32_t width;
	int32_t height;
	int16_t planes;
	int16_t bits_per_pixel;
	int32_t compression;
	int32_t image_size;
	int32_t x_pixels_per_m;
	int32_t y_pixels_per_m;
	int32_t colors_used;
	int32_t colors_important;
} bmp_header_t;

#pragma pack(pop)

typedef struct bmp
{
	bmp_header_t header;
	unsigned char** content;
} bmp_t;

int bmp_load(const char* filename, bmp_t** destination);

void bmp_free(bmp_t* bmp);

int bmp_write(const char* filename, bmp_t* bmp);

#endif //BMPFILTERS_BMP_H
