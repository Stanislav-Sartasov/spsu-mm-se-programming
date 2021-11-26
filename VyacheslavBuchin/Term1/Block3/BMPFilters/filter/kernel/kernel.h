//
// Created by Вячеслав Бучин on 26.11.2021.
//

#ifndef BMPFILTERS_KERNEL_H
#define BMPFILTERS_KERNEL_H

#include "../../bmp/bmp.h"

void apply_kernel(bmp_t* image, double kernel[3][3], double (*modify)(double value, double modification));

#endif //BMPFILTERS_KERNEL_H
