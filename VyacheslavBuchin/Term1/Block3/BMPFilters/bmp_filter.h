//
// Created by Вячеслав Бучин on 26.11.2021.
//

#ifndef BMPFILTERS_BMP_FILTER_H
#define BMPFILTERS_BMP_FILTER_H

#include "bmp/bmp.h"

#define NO_SUCH_FILTER -1

int validate_args(int argc, char** argv);

void process_bmp_error(int code, const char* filename);

int process_bmp_filter(bmp_t* image, const char* filter);

void print_error(const char* message);

#endif //BMPFILTERS_BMP_FILTER_H
