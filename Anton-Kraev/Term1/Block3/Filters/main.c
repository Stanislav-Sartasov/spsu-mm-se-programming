#define _CRT_SECURE_NO_WARNINGS
#include "filters.h"
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#pragma pack(push, 1)
typedef struct bitMapHeader
{
	char bfType[2];
	unsigned int bfSize;
	unsigned short bfReserved1;
	unsigned short bfReserved2;
	unsigned int bfOffBits;

	unsigned int biSize;
	int biWidth;
	int biHeight;
	unsigned short biPlanes;
	unsigned short biBitCount;
	int biCompression;
	int biSizeImage;
	int biXPelsPerMeter;
	int biYPelsPerMeter;
	unsigned int biClrUsed;
	unsigned int biClrImportant;
} bmpHeader;
#pragma pack(pop)

int applyFilter(unsigned char** image, int height, int width, int bps, char* filter);

int main(int argc, char* argv[])
{
	printf("This program applies one of the filters to the image.\n");
	printf("List of acceptable filters: grey, median, gauss, sobelx, sobely, sobel\n");

	if (argc != 4)
	{
		printf("Incorrect input! Use: <main.exe> <input.bmp> <filtername> <output.bmp>");
		return 1;
	}

	FILE* input = fopen(argv[1], "rb");
	if (input == NULL)
	{
		printf("Could not open the file for reading!");
		return 1;
	}

	bmpHeader header;
	fread(&header, 1, sizeof(bmpHeader), input);
	
	if (header.bfType[0] != 'B' || header.bfType[1] != 'M')
	{
		printf("File is corrupted!");
		return 1;
	}
	 
	int realWidth = (((header.biWidth * header.biBitCount + 31) >> 5) << 2)
		+ (4 - ((header.biWidth * header.biBitCount) >> 3) & 3);
	
	unsigned char** image = (unsigned char**)malloc(header.biHeight * sizeof(unsigned char*));
	for (int i = 0; i < header.biHeight; i++)
	{
		image[i] = (unsigned char*)malloc(realWidth * sizeof(unsigned char));
		fread(image[i], 1, realWidth, input);
	}

	fclose(input);

	if (!applyFilter(image, header.biHeight, realWidth, header.biBitCount / 8, argv[2]))
		return 1;
	printf("Filter applied successfully!\n");

	FILE* output = fopen(argv[3], "wb");
	if (output == NULL)
	{
		printf("Could not open the file for writing!");
		return 1;
	}

	fwrite(&header, 1, sizeof(bmpHeader), output);
	for (int i = 0; i < header.biHeight; i++)
	{
		fwrite(image[i], 1, realWidth, output);
		free(image[i]);
	}

	free(image);
	fclose(output);

	return 0;
}

int applyFilter(unsigned char** image, int height, int width, int bps, char* filter)
{
	if (!strcmp(filter, "grey"))
		grey(image, height, width, bps);
	else if (!strcmp(filter, "median"))
		median(image, height, width, bps);
	else if (!strcmp(filter, "gauss"))
		gauss(image, height, width, bps);
	else if (!strcmp(filter, "sobel"))
		sobel(image, height, width, bps);
	else if (!strcmp(filter, "sobelx"))
		sobelx(image, height, width, bps);
	else if (!strcmp(filter, "sobely"))
		sobely(image, height, width, bps);
	else
	{
		printf("Incorrect filter name");
		return 0;
	}
	return 1;
}