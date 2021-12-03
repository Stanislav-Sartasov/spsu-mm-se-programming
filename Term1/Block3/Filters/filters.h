#pragma once

void grey(unsigned char** image, int height, int width, int bps);

void median(unsigned char** image, int height, int width, int bps);

void gauss(unsigned char** image, int height, int width, int bps);

void sobel(unsigned char** image, int height, int width, int bps, char flag);