#include "bitmap_info.h"
#include <stdlib.h>

#ifndef BMPFILTER_IMAGE_FILTERS_H
#define BMPFILTER_IMAGE_FILTERS_H

#define GRAY_FILTER_RED_COEFFICIENT 0.3
#define GRAY_FILTER_GREEN_COEFFICIENT 0.59
#define GRAY_FILTER_BLUE_COEFFICIENT 0.11

int number_comparator(const void *p1, const void *p2);

int my_abs(int x);

void apply_gray(struct bitmap_info bmp_info, unsigned char ***picture, int row_size, int channels);

void apply_median_3x3(struct bitmap_info bmp_info, unsigned char ***picture, int row_size, int channels);

void apply_gauss_3x3(struct bitmap_info bmp_info, unsigned char ***picture, int row_size, int channels);

// type == 0 -> по x, иначе по y
void apply_sobel_3x3(struct bitmap_info bmp_info, unsigned char ***picture, int row_size, int channels, int type);

#endif
