#include "tools.h"

struct filter_data
{
	rgb window[9];
	double gaussian_kernel[9];
};

typedef struct filter_data filter_data;

void filter(uint8_t* pixels, bitmap_info_header* info_header, uint8_t* output, int filter_type);

void grayscale(uint8_t* pixels, bitmap_info_header* info_header, uint8_t* output);
