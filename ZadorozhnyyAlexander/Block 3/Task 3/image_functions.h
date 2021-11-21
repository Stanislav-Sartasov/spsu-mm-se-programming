#pragma once
#include "structs.h"

image read_image(FILE* file, int height, int width, int bits_per_pixel);

image copy_image(image copy_from);

int save_bmp_file(bitmap_file file, image picture, char*);

void free_image(image picture);
