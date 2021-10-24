#pragma once

int grey(unsigned char** image, int row, int y);

int med_middle(unsigned char** image, int row, int y);

int five_x_five_gauss(unsigned char** image, int row, int y);

int sobel_x(unsigned char** image, int x, int y, int row);

int sobel_y(unsigned char** image, int x, int y, int row);

int sobel(unsigned char** image, int x, int y, int row);