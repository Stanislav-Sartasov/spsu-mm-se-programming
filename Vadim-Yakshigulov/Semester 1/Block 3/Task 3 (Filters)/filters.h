#pragma once
#include "bmp.h"
#include <stdlib.h>

void apply_greyscale_filter(bmp_image_t *image);
void apply_3x3_filter(bmp_image_t *image, uint8_t (*filter)(uint8_t *));
uint8_t median_filter(uint8_t *matrix);
uint8_t gaussian_filter(uint8_t *matrix);
uint8_t sobelX_filter(uint8_t *matrix);
uint8_t sobelY_filter(uint8_t *matrix);