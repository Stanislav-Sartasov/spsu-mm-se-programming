#include <stdio.h>
#include <malloc.h>

#ifndef BMPFILTER_BITMAP_INFO_H
#define BMPFILTER_BITMAP_INFO_H

#define HEADER_BITS_COUNT 54

struct bitmap_info
{
	int size;
	int offset_bits_count;
	int width;
	int height;
	short bit_planes_count;
	short bit_count;
	int colors_used_count;
	int colors_important_count;
};


//Обращаемся к нужным позициям в начале файла, чтобы собрать информацию о картинке
struct bitmap_info get_bitmap_info(FILE *file);

#endif //BMPFILTER_BITMAP_INFO_H
