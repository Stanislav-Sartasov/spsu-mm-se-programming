#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "bmp.h"
#include "filters.h"

void invalid_input_error()
{
	printf("Invalid input. Must be: <program> <input file name> <filter name> <output file name>\n");
	printf("Allowed filter names:\n"
		   "median, gaussian_3x3, gaussian_5x5, sobelX, sobelY, greyscale\n");
	exit(-1);
}

typedef rgb_t (*filter)(uint8_t**);

filter compare_filter_by_name(char* filter_name)
{
	if (!strcmp("median", filter_name))
		return median_filter;
	if (!strcmp("greyscale", filter_name))
		return greyscale_filter;
	if (!strcmp("gaussian_3x3", filter_name))
		return gaussian_filter_3x3;
	if (!strcmp("gaussian_5x5", filter_name))
		return gaussian_filter_5x5;
	if (!strcmp("sobelX", filter_name))
		return sobelX_filter;
	if (!strcmp("sobelY", filter_name))
		return sobelY_filter;
	return NULL;
}

int compare_area_by_name(char* filter_name)
{
	if (!strcmp("median", filter_name))
		return 3;
	if (!strcmp("greyscale", filter_name))
		return 1;
	if (!strcmp("gaussian_3x3", filter_name))
		return 3;
	if (!strcmp("gaussian_5x5", filter_name))
		return 5;
	if (!strcmp("sobelX", filter_name))
		return 3;
	if (!strcmp("sobelY", filter_name))
		return 3;
	return 1;

}


int main(int argc, char *argv[])
{
	printf("The program gets a 24 or 32 bit BMP-file and applies one of the following filters:\n"
		   "median, gaussian_3x3, gaussian_5x5, sobelX, sobelY, greyscale.\n");
	printf("Input format: <program> <input file name> <filter name> <output file name>\n\n");
	if (argc != 4)
		invalid_input_error();

	bmp_header_t header;
	bmp_image_t image;

	bmp_open(argv[1], &header, &image);

	filter used_filter = compare_filter_by_name(argv[2]);
	if (used_filter == NULL)
		invalid_input_error();

	apply_filter(&image, compare_area_by_name(argv[2]), used_filter);

	bmp_create(argv[3], &header, &image);
	bmp_image_free(&image);
	printf("Filter has been successfully applied!\n");
}