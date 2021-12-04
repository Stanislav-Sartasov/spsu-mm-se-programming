#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "bmp.h"
#include "filters.h"

void invalid_input_error()
{
	printf("Invalid input. Must be: <program> <input file name> <filter name> <output file name>\n");
	printf("Allowed filter names:\n"
		   "median, gaussian, sobelX, sobelY, greyscale.\n");
	exit(-1);
}

typedef uint8_t (*filter)(uint8_t *matrix);;

filter compare(char* filter_name)
{
	if (!strcmp("median", filter_name))
		return median_filter;
	if (!strcmp("gaussian", filter_name))
		return gaussian_filter;
	if (!strcmp("sobelX", filter_name))
		return sobelX_filter;
	if (!strcmp("sobelY", filter_name))
		return sobelY_filter;
	return NULL;

}


int main(int argc, char *argv[])
{
	printf("The program gets a 24 or 32 bit BMP-file and applies one of the following filters:\n"
		   "median, gaussian, sobelX, sobelY, greyscale.\n");
	printf("Input format: <program> <input file name> <filter name> <output file name>\n\n");
	if (argc != 4)
		invalid_input_error();

	bmp_header_t header;
	bmp_image_t image;

	bmp_open(argv[1], &header, &image);

	if (strcmp("greyscale", argv[2]) == 0)
		apply_greyscale_filter(&image);
	else
	{
		filter used_filter = compare(argv[2]);
		if (used_filter == NULL)
			invalid_input_error();
		apply_3x3_filter(&image, used_filter);
	}

	bmp_create(argv[3], &header, &image);
	bmp_image_free(&image);
	printf("Filter has been successfully applied!\n");
}