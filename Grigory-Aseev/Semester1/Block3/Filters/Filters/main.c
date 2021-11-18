#include <stdlib.h>
#include <stdio.h>
#include "filters.h"

int main(int argc, char* argv[])
{
	printf("This program can apply filters such as averaging, Gaussian 5 by 5, Sobel by X, Y, grayscale to bmp (32 bits or 24 bits)\n");
	printf("the command line should contain the path to the source image, the name of the filter(gray, median, gauss, sobelX, sobelY, sobel), the path for the output image\n");
	FILE* file_input, * file_output;
	if (argc != 4)
	{
		printf("Arguments are not enough");
		return 1;
	}

	fopen_s(&file_input, argv[1], "rb");
	fopen_s(&file_output, argv[3], "wb");
	if (file_input == NULL || file_output == NULL)
	{
		printf("Failed to open files");
		return 1;
	}

	struct bmp_file_header file_head;
	struct bmp_info_header file_info;
	struct image img;
	char* palette = NULL;
	int palette_size, padding;

	if (read_bmp(file_input, &file_head, &file_info, &img, palette, &palette_size, &padding))
	{
		printf("Image data could not be read");
		return 1;
	}
	filter(&img, argv[2]);
	if (write_bmp(file_output, file_head, file_info, &img, palette, palette_size, padding))
	{
		printf("Failed to record image data");
		return 1;
	}
	fclose(file_input);
	fclose(file_output);
	printf("Success!");
	return 0;
}