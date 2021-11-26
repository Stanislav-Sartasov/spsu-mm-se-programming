#ifndef MY_FILTERS
#define MY_FILTERS

void gauss5_filter(struct bmp_file* target);
void med_filter(struct bmp_file* target);
void sobel_x(struct bmp_file* target);
void sobel_y(struct bmp_file* target);
void gauss3_filter(struct bmp_file* target);
void monochrome_filter(struct bmp_file* target);

#endif