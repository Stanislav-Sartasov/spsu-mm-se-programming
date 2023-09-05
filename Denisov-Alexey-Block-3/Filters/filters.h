#include "bmpHeader.h"

#ifndef FILTERS
#define FILTERS

void median(BYTE**** image, bmpHeader header, int myWidth, int channels);

void gauss(BYTE**** image, bmpHeader header, int myWidth, int channels);

void sobelX(BYTE**** image, bmpHeader header, int myWidth, int channels);

void sobelY(BYTE**** image, bmpHeader header, int myWidth, int channels);

void greyscale(BYTE**** image, bmpHeader header, int myWidth, int channels);

void convolution(BYTE**** image, bmpHeader header, int myWidth, int channels, double kernel[9]);

int cmp(const void* p1, const void* p2);

#endif // FILTERS
