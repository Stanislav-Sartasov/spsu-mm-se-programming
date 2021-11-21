#pragma once
#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#pragma pack(push)
#pragma pack(2)

typedef struct bitmap_file
{
	char name[2]; // it must be BM
	int size;
	int garbage; // not required
	int image_offset; // offset from where image starts in file.

	int header_size;
	int width;
	int height;
	unsigned short colorplanes;
	unsigned short bitsperpixel;
	int compression;
	int image_size;

	int horizontal_resolution;
	int vertical_resolution;
	int number_of_colors;
	int number_of_important_colors;
} bitmap_file;

#pragma pack(pop)

typedef struct rgb_colors
{
	unsigned char blue;
	unsigned char green;
	unsigned char red;
} rgb_colors;

typedef struct image
{
	int height;
	int width;
	int bits_per_pixel;
	rgb_colors** rgb;
} image;