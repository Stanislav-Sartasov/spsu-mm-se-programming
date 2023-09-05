//
// Created by Вячеслав Бучин on 26.11.2021.
//

#ifndef BMPFILTERS_FILTER_H
#define BMPFILTERS_FILTER_H

#include "../bmp/bmp.h"

void filter_apply_grayscale(bmp_t* image);

void filter_apply_avg(bmp_t* image);

void filter_apply_gaussian(bmp_t* image);

void filter_apply_sobelX(bmp_t* image);

void filter_apply_sobelY(bmp_t* image);

void filter_apply_max(bmp_t* image);

#endif //BMPFILTERS_FILTER_H
