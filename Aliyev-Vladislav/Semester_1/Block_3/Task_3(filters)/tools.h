#ifndef MY_FILE_UTILS
#define MY_FILE_UTILS
#include <stdio.h>
#include <stdlib.h>

#pragma pack(push, 1)

struct file_header
{
	short type;
	int file_size;
	int bias;
	short stock_byte1;
	short stock_byte2;
};

struct bitmap
{
	int width;
	int height;
	int compression;
	int size_bitmap;
	int hpermit;
	int vpermit;
	int colors;
	int vital_colors;
	int header_size;
	short planes_color;
	short bits_pixel;
};
#pragma pack(pop)

struct pixel
{
	char r;
	char g;
	char b;
};

FILE* file_input;
FILE* file_out;

struct file_header header;
struct bitmap bmp;

int sort(int* a);

int value_test(int value, char* filter);

#endif