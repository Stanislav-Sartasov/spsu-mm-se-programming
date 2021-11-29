#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <math.h>
#include "bmp_files.h"
#include "filters_and_kernels.h"

int main(int argc, char* argv[])
{
	printf("Filters\n");

	// check arguments

	if (argc != 4)
	{
		printf("You enter %d arguments, but you have to enter %d", argc - 1, 3);
		return -1;
	}

	// opening file

	FILE* bmp_file;

	if ((bmp_file = fopen(argv[1], "rb")) == NULL)
	{
		printf("Can't open file\n");
		return -1;
	}

	// getting file statistic

	struct BIT_header bit_header;
	struct DIB_header dib_header;

	fread(&bit_header.file_type, 2, 1, bmp_file);
	fread(&bit_header.file_size, sizeof(int), 3, bmp_file);
	fread(&dib_header, sizeof(struct DIB_header), 1, bmp_file);

	// show stat

	show_stat(&bit_header, &dib_header);

	// checking characteristic

	if (bit_header.file_type[0] != 'B' || bit_header.file_type[1] != 'M')
	{
		printf("it's not a bmp file\n");
		fclose(bmp_file);
		return -1;
	}

	if (dib_header.compressison != 0)
	{
		printf("there is compression in the file\n");
		fclose(bmp_file);
		return -1;
	}

	if ((dib_header.bit_per_pixel != 24 + dib_header.bit_per_pixel != 32) != 1)
	{
		printf("there is not 24 or 32 bit per pixel\n");
		fclose(bmp_file);
		return -1;
	}

	fseek(bmp_file, bit_header.image_offset, SEEK_SET);

	// creating array of picture
	
	struct bmp_image picture;
	picture.height = dib_header.height;
	picture.width = dib_header.width;
	picture.bytes = dib_header.width * dib_header.bit_per_pixel / 8; // bytes in a row
	picture.bytes_per_pixel = dib_header.bit_per_pixel / 8;
	read_image(bmp_file, &picture);
	fclose(bmp_file);

	// using filter

	if (strcmp(argv[2], "Median") == 0)
	{
		convolution(&picture, median_filter, null_core, 1, 3);
	}
	else if (strcmp(argv[2], "Gauss") == 0)
	{
		convolution(&picture, gauss_filter, gauss_core, 1, 3);
	}
	else if (strcmp(argv[2], "SobelX") == 0)
	{
		convolution(&picture, sobel_x_filter, sobel_x_core, 1, 3);
	}
	else if (strcmp(argv[2], "SobelY") == 0)
	{
		convolution(&picture, sobel_y_filter, sobel_y_core, 1, 3);
	}
	else if (strcmp(argv[2], "Sobel") == 0)
	{
		convolution(&picture, sobel_filter, sobel_core, 2, 3);
	}
	else if (strcmp(argv[2], "Grey") == 0)
	{
		convolution(&picture, grey_filter, null_core, 0, 0);
	}
	else if (strcmp(argv[2], "SharrX") == 0)
	{
		convolution(&picture, sharr_x_filter, sharr_x_core, 1, 3);
	}
	else if (strcmp(argv[2], "SharrY") == 0)
	{
		convolution(&picture, sharr_y_filter, sharr_y_core, 1, 3);
	}
	else if (strcmp(argv[2], "Sharr") == 0)
	{
		convolution(&picture, sharr_filter, sharr_core, 2, 3);
	}
	else if (strcmp(argv[2], "Average") == 0)
	{
		convolution(&picture, averaging_filter, averaging_core, 1, 3);
	}
	else if (strcmp(argv[2], "Negative") == 0)
	{
		convolution(&picture, negative_filter, null_core, 0, 0);
	}

	FILE* bmp_out;

	if ((bmp_out = fopen(argv[3], "w")) == NULL)
	{
		printf("Can't create file for writing\n");
		free_image(&picture);
		fclose(bmp_file);
		return -1;
	}

	// writing the image back

	fwrite(&bit_header.file_type, sizeof(char), 2, bmp_out);
	fwrite(&bit_header.file_size, sizeof(int), 3, bmp_out);
	fwrite(&dib_header, sizeof(struct DIB_header), 1, bmp_out);
	write_image(bmp_out, &picture);
	fclose(bmp_out);

	free_image(&picture);
	printf("Work has done\n");

	return 0;
}