//
// Created by ilyah on 02.11.2021.
//

#ifndef TASK_3_FILTERS_H
#define TASK_3_FILTERS_H

void grey_filter(unsigned char **image, int width, int height, int Bps);

void avg_filter(unsigned char **image, int width, int height, int Bps);

void gaussian_filter(unsigned char **image, int width, int height, int Bps);

void sobel_filter(unsigned char **image, int width, int height, int Bps, int direction);

#endif //TASK_3_FILTERS_H
