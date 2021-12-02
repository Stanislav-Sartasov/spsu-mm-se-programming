
#include <stdio.h>

#include "bmp/bmp.h"
#include "filters/filters.h"
#include "lib/error.h"
#include <stdlib.h>

void choose_filter(bmp_image_t* image, char* filter_type)
{
	if (!strcmp(filter_type, "Median"))
	{
		printf("Running median filter...\n\n");
		median(image);
	}
	else if (!strcmp(filter_type, "Gauss"))
	{
		printf("Running Gauss filter...\n\n");
		gauss(image);
	}
	else if (!strcmp(filter_type, "SobelX"))
	{
		printf("Running SobelX filter...\n\n");
		sobel_x(image);
	}
	else if (!strcmp(filter_type, "SobelY"))
	{
		printf("Running SobelY filter...\n\n");
		sobel_y(image);
	}
	else if (!strcmp(filter_type, "Grayscale"))
	{
		printf("Running grayscale filter...\n\n");
		grayscale(image);
	}
	else
	{
		error("No such filter. Available filters: Median, Gauss, SobelX, SobelY, Grayscale.\n");
		exit(-1);
	}	
}

int main(int argc, char* argv[])
{
	printf("This program applies the selected filter to the given bmp image.\n\n");

	if (argc != 4)
		return error("Invalid command line parameters.\n");

	char* input_filename = argv[1];
	char* output_filename = argv[3];
	char* filter_type = argv[2];

	bmp_image_t* image = read_image(input_filename);
	if (image == NULL)
		return error("Something went wrong in reading image.\n");

	choose_filter(image, filter_type);

	if (!write_image(output_filename, image))
		return error("Something went wrong in writing image.\n");

	close_image(image);

	printf("Done! Check the output image.\n");

	return 0;
}