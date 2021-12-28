#pragma once

#include <stdio.h>
#include <stdlib.h>
#include <stdint.h>
#include <string.h>
#include <math.h>

#pragma pack(push, 1)

typedef struct bitMapFileHeader
{
	int8_t bfType[2];
	uint32_t bfSize;
	uint16_t bfReserved1;
	uint16_t bfReserved2;
	uint32_t bfOffBits;
} bitMapFileHeader;

typedef struct bitMapInfoHeader
{
	uint32_t biSize;
	uint32_t biWidth;
	uint32_t biHeight;
	uint16_t biPlanes;
	uint16_t biBitCount;
	uint32_t biCompression;
	uint32_t biSizeImage;
	uint32_t biXPelsPerMeter;
	uint32_t biYPelsPerMeter;
	uint32_t biClrUsed;
	uint32_t biClrImportant;
} bitMapInfoHeader;

#pragma pack(pop)

typedef struct colors
{
	uint8_t rgb[3];
	uint8_t alpha;
} colors;

typedef struct image
{
	colors** pixels;
	uint32_t height, width, paletteSize, padding;
	int8_t* palette;
} image;

int16_t readBmp(FILE* file, bitMapFileHeader* fHead, bitMapInfoHeader* iHead, image* img);
int16_t writeBmp(FILE* file, bitMapFileHeader fHead, bitMapInfoHeader iHead, image* img);
void filter(image* img, int8_t* nameFilter);