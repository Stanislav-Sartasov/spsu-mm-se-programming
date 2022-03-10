#ifndef MY_FILTERS
#define MY_FILTERS

void med_filter(struct bmp_file* target);
void sobel_x(struct bmp_file* target);
void sobel_y(struct bmp_file* target);
void gauss_filter(struct bmp_file* target);
void monochrome_filter(struct bmp_file* target);

#endif