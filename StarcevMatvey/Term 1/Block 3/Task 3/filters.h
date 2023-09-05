#pragma once

int grey(unsigned char** image, int row, int y, int t);

int med_middle(unsigned char** image, int row, int y, int t);

int five_x_five_gauss(unsigned char** image, int row, int y, int t);

int multi_sobel(unsigned char** image, int x, int y, int row, char flag, int t);