#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>
#include "filters.h"
#include "tools.h"


int main(int argc, char* argv[])
{
	printf("This program applies the following filters to images:\n");
	printf("3x3 Gaussian filter, median filter, gray filter, Sobel(X) filter and Sobel(Y) filter.\n");
	if (!value_test(argc, argv[2]))
		return -1;

	fopen_s(&file_input, argv[1], "rb");
	fopen_s(&file_out, argv[3], "wb");

	if (file_input == NULL)
	{
		printf("The input file was not found.\n");
		return -1;
	}
	if (file_out == NULL)
	{
		printf("Could not open output file.\n");
		return -1;
	}

	fread(&header, sizeof(header), 1, file_input);
	fread(&bmp, sizeof(bmp), 1, file_input);

	if ((bmp.bits_pixel != 24) && (bmp.bits_pixel != 32))
	{
		printf("Your image has an incorrect format.\n");
		return -1;
	}

	int number_max = 0;
	int number = 0;
	char buffer[4];

	struct pixel* array = (struct pixel*)malloc(header.file_size - sizeof(header) - sizeof(bmp));
	if (bmp.bits_pixel == 24)
	{
		fseek(file_input, header.bias, SEEK_SET);
		fread(array, header.file_size - sizeof(header) - sizeof(bmp), 1, file_input);
	}
	else
	{
		fseek(file_input, header.bias, SEEK_SET);
		while (number * 4 < header.file_size - sizeof(header) - sizeof(bmp))
		{
			fread(&(array[number]), 4, 1, file_input);
			number++;
		}
		number_max = number + 1;
	}

	if (strcmp(argv[2], "gray") == 0)
		gray(array, bmp.height, bmp.width, argv[2]);
	if (strcmp(argv[2], "median") == 0)
		filter(array, bmp.height, bmp.width, argv[2]);
	if (strcmp(argv[2], "gauss") == 0)
		filter(array, bmp.height, bmp.width, argv[2]);
	if (strcmp(argv[2], "sobelX") == 0)
	{
		gray(array, bmp.height, bmp.width);
		filter(array, bmp.height, bmp.width, argv[2]);
	}
	if (strcmp(argv[2], "sobelY") == 0)
	{
		gray(array, bmp.height, bmp.width);
		filter(array, bmp.height, bmp.width, argv[2]);
	}

	fwrite(&header, sizeof(header), 1, file_out);
	fwrite(&bmp, sizeof(bmp), 1, file_out);

	if (bmp.bits_pixel == 24)
		fwrite(array, header.file_size - sizeof(header) - sizeof(bmp), 1, file_out);
	else
	{
		number = 0;
		while (number < number_max)
		{
			fwrite(&(array[number]), 4, 1, file_out);
			number++;
		}
	}

	free(array);

	fclose(file_input);
	fclose(file_out);

	return 0;
}