#pragma once

// function for image processing

void convolution(struct bmp_image* picture, unsigned char (*filter)(struct bmp_image*, int, int, int, void*** kernel, int, int), void*** (*core)(int number_cores, int core_size), int number_cores, int core_size);

// cores

void*** averaging_core(int number_cores, int core_size);

void*** gauss_core(int number_cores, int core_size);

void*** sobel_x_core(int number_cores, int core_size);

void*** sobel_y_core(int number_cores, int core_size);

void*** sobel_core(int number_cores, int core_size);

void*** sharr_x_core(int number_cores, int core_size);

void*** sharr_y_core(int number_cores, int core_size);

void*** sharr_core(int number_cores, int core_size);

void*** null_core();

// filters

unsigned char median_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores);

unsigned char negative_filter(struct bmp_image* picture, int x, int y, int rgb); 

unsigned char averaging_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores);  

unsigned char gauss_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores);

unsigned char grey_filter(struct bmp_image* picture, int x, int y);

unsigned char sobel_x_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores);

unsigned char sobel_y_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores);

unsigned char sobel_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores);

unsigned char sharr_x_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores);

unsigned char sharr_y_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores);

unsigned char sharr_filter(struct bmp_image* picture, int x, int y, int rgb, void*** core, int core_size, int number_cores);