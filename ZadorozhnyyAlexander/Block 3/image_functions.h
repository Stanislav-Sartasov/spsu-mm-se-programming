#pragma once
#include "structs.h"

Image read_image(FILE* file, int height, int width, int bits_per_pixel);

Image copy_image(Image copy_from);

void save_bmp_file(Bitmap_file file, Image picture);

void free_image(Image picture);
