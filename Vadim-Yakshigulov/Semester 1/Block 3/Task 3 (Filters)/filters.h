#pragma once
#include "bmp.h"
#include <stdlib.h>


void apply_filter(bmp_image_t *image, int n, rgb_t (*filter)(uint8_t**));
rgb_t greyscale_filter(uint8_t **matrix);
rgb_t median_filter(uint8_t **matrix);
rgb_t gaussian_filter_3x3(uint8_t **matrix);
rgb_t gaussian_filter_5x5(uint8_t **matrix);
rgb_t sobelX_filter(uint8_t **matrix);
rgb_t sobelY_filter(uint8_t **matrix);