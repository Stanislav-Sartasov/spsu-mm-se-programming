//
// Created by Вячеслав Бучин on 26.11.2021.
//

#include "bmp_filter.h"
#include "filter/filter.h"

#include <stdio.h>
#include <string.h>

void print_help()
{
	printf("This program applies the following filters to the given image:\n");
	printf("	Filter name:		<name to use>\n");
	printf("	Median filter 		<avg>\n");
	printf("	Gaussian filter 	<gaussian>\n");
	printf("	Grayscale conversion 	<grayscl>\n");
	printf("	X-axis sobel filter 	<sobelX>\n");
	printf("	Y-axis sobel filter 	<sobelY>\n");
	printf("	Maximum filter		<max>\n");
	printf("\n");
	printf("Usage: <input> <filter name> <output>\n");
}

void print_error(const char* message)
{
	printf("%s\n", message);
	printf("Use --help flag to see more information\n");
}

int validate_args(int argc, char** argv)
{
	if (argc == 2 && strcmp(argv[1], "--help") == 0)
	{
		print_help();
		return 1;
	}

	if (argc == 4)
		return 0;

	print_error("Incorrect arguments");
	return 1;
}

void process_bmp_error(int code, const char* filename)
{
	switch (code)
	{
		case BMP_FILE_OPENING_ERROR:
			fprintf(stderr, "Cannot open %s\n", filename);
			break;
		case BMP_MALLOC_ERROR:
			fprintf(stderr, "Segmentation fault\n");
			break;
		case BMP_UNSUPPORTED_FILE:
			fprintf(stderr, "%s is unsupported file\n", filename);
			break;
		case BMP_DAMAGED_FILE:
			fprintf(stderr, "%s is damaged file\n", filename);
			break;
		default:
			break;
	}
}

int process_bmp_filter(bmp_t* image, const char* filter)
{
	if (strcmp(filter, "avg") == 0)
		filter_apply_avg(image);
	else if (strcmp(filter, "gaussian") == 0)
		filter_apply_gaussian(image);
	else if (strcmp(filter, "grayscl") == 0)
		filter_apply_grayscale(image);
	else if (strcmp(filter, "sobelX") == 0)
		filter_apply_sobelX(image);
	else if (strcmp(filter, "sobelY") == 0)
		filter_apply_sobelY(image);
	else if (strcmp(filter, "max") == 0)
		filter_apply_max(image);
	else
		return NO_SUCH_FILTER;
	return 0;
}