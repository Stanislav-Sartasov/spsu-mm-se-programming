#ifndef FILTERS_H
#define FILTERS_H

#include "../bmp/bmp.h"

void median(bmp_image_t* image);

void gauss(bmp_image_t* image);

void sobel_x(bmp_image_t* image);

void sobel_y(bmp_image_t* image);

void grayscale(bmp_image_t* image);

#endif // BMP_H