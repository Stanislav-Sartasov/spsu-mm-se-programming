#pragma once
#pragma pack(push, 1)

#include <stdint.h>

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

typedef struct
{
	unsigned char blue;
	unsigned char green;
	unsigned char red;
} rgb_t;

typedef struct
{
	rgb_t* pixels;
	int height;
	int width;
} bmp_image_t;



void bmp_open(const char *filename, bmp_header_t* header, bmp_image_t* image);
void bmp_create(const char *filename, bmp_header_t *header, bmp_image_t *image);
void bmp_image_free(bmp_image_t *image);