#define _CRT_SECURE_NO_WARNINGS
#include <string.h>
#include "bmpEditor.h"

void readHeaders(FILE* fp, struct bmpHeader* header, struct bmpInfoHeader* headerInfo);

int main(int argc, char** argv)
{
	printf("This program converts the specified input bmp"
		" file according to the specified filter into the specified output file\n");
	printf("Please enter in the format shown:"
		" <program name> <input file> <filter type> <output file>\n");
	if (argc != 4)
	{
		printf("Error! Not enough arguments for the program\n");
		printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
		exit(-1);
	}
	FILE* fp = fopen(argv[1], "rb");
	if (fp == NULL)
	{
		printf("Error! An error occured while opening the input file\n");
		printf("Re-enter as <input file> <filter type> <output file>\n");
		exit(-1);
	}
	struct bmpHeader header;
	struct bmpInfoHeader headerInfo;
	readHeaders(fp, &header, &headerInfo);
	if (headerInfo.numberBitsPerPixel == 24)
	{
		struct img picture = readImage(fp, headerInfo.height, headerInfo.width);
		if (strcmp(argv[2], "median") == 0)
		{
			medianFilter(&picture);
		}
		else if (strcmp(argv[2], "gauss") == 0)
		{
			gaussFilter(&picture);
		}
		else if (strcmp(argv[2], "sobelX") == 0)
		{
			sobelXFilter(&picture);
			grayFilter(&picture);
		}
		else if (strcmp(argv[2], "sobelY") == 0)
		{
			sobelYFilter(&picture);
			grayFilter(&picture);
		}
		else if (strcmp(argv[2], "gray") == 0)
		{
			grayFilter(&picture);
		}
		else
		{
			printf("Incorrect filter format entered!\n");
			printf("Types of input filters:\n"
				"3x3 averaging filter <=> median\n"
				"3x3 Gaussian averaging filter <=> gauss\n"
				"Sobel filter on X <=> sobelX\n"
				"Sobel filter by Y <=> sobelY\n"
				"Converting an image from color to grayscale <=> gray\n");
			printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
			exit(-1);
		}
		writeImage(header, headerInfo, picture, argv);
	}
	else
	{
		struct img32 picture = readImage32(fp, headerInfo.height, headerInfo.width);
		if (strcmp(argv[2], "median") == 0)
		{
			medianFilter32(&picture);
		}
		else if (strcmp(argv[2], "gauss") == 0)
		{
			gaussFilter32(&picture);
		}
		else if (strcmp(argv[2], "sobelX") == 0)
		{
			sobelXFilter32(&picture);
			grayFilter32(&picture);
		}
		else if (strcmp(argv[2], "sobelY") == 0)
		{
			sobelYFilter32(&picture);
			grayFilter32(&picture);
		}
		else if (strcmp(argv[2], "gray") == 0)
		{
			grayFilter32(&picture);
		}
		else
		{
			printf("Incorrect filter format entered!\n");
			printf("Types of input filters:\n"
				"3x3 averaging filter <=> median\n"
				"3x3 Gaussian averaging filter <=> gauss\n"
				"Sobel filter on X <=> sobelX\n"
				"Sobel filter by Y <=> sobelY\n"
				"Converting an image from color to grayscale <=> gray\n");
			printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
			exit(-1);
		}
		writeImage32(header, headerInfo, picture, argv);
	}
	printf("Successfully! Filter %s applied to bmp image %s, result saved in %s\n", argv[2], argv[1], argv[3]);
	fclose(fp);
	return 0;
}

void readHeaders(FILE* fp, struct bmpHeader* header, struct bmpInfoHeader* headerInfo)
{
	fread(header->identifier, 2, 1, fp);
	fread(&header->size, 12, 1, fp);
	fread(headerInfo, 40, 1, fp);
	if (headerInfo->headerSize != 40)
	{
		printf("Error! Source header size "
			"bmp file is not 40, "
			"use the BITMAPINFOHEADER header standard!\n");
		printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
		exit(-1);
	}
	if (headerInfo->numberBitsPerPixel != 24 &&
		headerInfo->numberBitsPerPixel != 32)
	{
		printf("Error! The pixel format is incorrect! "
			"The file must be in 24bpp pixel format\n");
		printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
		exit(-1);
	}
	if (headerInfo->compressionMethod != 0)
	{
		printf("Error! The input bmp file uses compression. "
			"The program requires an uncompressed input file\n");
		printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
		exit(-1);
	}
}
