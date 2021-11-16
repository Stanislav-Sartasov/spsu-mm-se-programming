#pragma once
#include "structs.h"

void grey_filter(Image picture);

Image sobel_filter(Image picture, int direction);

void middle_filter(Image picture);

void gausses_filter_3x3(Image picture);



