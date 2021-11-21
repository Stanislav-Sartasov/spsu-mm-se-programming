#pragma once
#include "structs.h"

void grey_filter(image picture);

image sobel_filter(image picture, int direction);

void middle_filter(image picture);

void gausses_filter_3x3(image picture);



