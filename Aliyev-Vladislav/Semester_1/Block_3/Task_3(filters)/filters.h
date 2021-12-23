#ifndef MY_FILTERS
#define MY_FILTERS

void gray(struct pixel* pixels, int height, int width);

float gauss(int* a);

int sobelX(int* a);

int sobelY(int* a);

void filter(struct pixel* pixels, int height, int width, char* filter);

#endif