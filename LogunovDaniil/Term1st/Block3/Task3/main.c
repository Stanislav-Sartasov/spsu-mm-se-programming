#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <string.h>
#include <stdarg.h>
#include <stdlib.h>
#include "filters.h"

typedef int (*filterPointer)(byte**, int, int, int);

#define FILTER_NAMES { "MEDIAN", "GAUSS", "GRAYSCALE", "SOBELY", "SOBELX" }
#define FILTERS      { median,   gauss,   grayscale,   sobelY,   sobelX   }
#define SIZE(A) (sizeof(A) / sizeof(A[0]))

int identifyFilter(const char* name)
{
	_strupr(name);
	char* filters[] = FILTER_NAMES;
	for (int i = 0; i < SIZE(filters); i++)
	{
		if (!strcmp(name, filters[i]))
			return i;
	}
	return -1;
}

void filtersAvailableMessage()
{
	printf("The program supports only the following filters:\n");
	char* filters[] = FILTER_NAMES;
	for (int i = 0; i < SIZE(filters); i++)
	{
		printf("   %s", filters[i]);
		if (i == SIZE(filters))
			return;
		printf(";\n");
	}
}

void greetingsMessage()
{
	printf("This program applies filter to the given image.\n\n");
}

void filterApplyingMessage()
{
	printf("The filter process has started...\n\n");
}

void farewellMessage()
{
	printf("The filter has been successfully applied!\n");
}

int errorMessage(int errorCode, int strNum, ...)
{
	printf("Error: ");

	va_list args;
	va_start(args, strNum);
	for (int i = 0; i < strNum; i++)
	{
		printf(" %s", va_arg(args, char*));
	}
	va_end(args);

	printf("\n");

	return errorCode;
}

// little-endian byte array to int value
int bytesToInt(byte* arr, int size)
{
	int res = 0;
	for (int i = size - 1; i > -1; i--)
	{
		res = res * 256 + arr[i];
	}
	return res;
}

int main(int argc, char* argv[])
{
	greetingsMessage();
	
	if (argc != 4)
		return errorMessage(-1, 3, "argument mismatch\nplease use the following format: ",
			argv[0], "<input file> <filter name> <output file>\n");

	FILE* fin;
	fopen_s(&fin, argv[1], "rb");
	if (fin == NULL)
		return errorMessage(-2, 2, "could not open", argv[1]);

	FILE* fout;
	fopen_s(&fout, argv[3], "wb");
	if (fout == NULL)
		return errorMessage(-2, 2, "could not open", argv[3]);

	byte bmpHeader[54];
	fread(bmpHeader, sizeof(byte), 54, fin);
	if (bmpHeader[0] != 'B' || bmpHeader[1] != 'M')
		return errorMessage(1, 1, "Image is corrupted");

	// decoding necessary data
	int offbits = bytesToInt(bmpHeader + 10, 4);
	int bmpBiSize = bytesToInt(bmpHeader + 14, 4);
	int width = bytesToInt(bmpHeader + 18, 4);
	int height = bytesToInt(bmpHeader + 22, 4);
	int bitCount = bytesToInt(bmpHeader + 28, 2);
	int compression = bytesToInt(bmpHeader + 30, 4);
	int clrUsed = bytesToInt(bmpHeader + 46, 4);

	// checking most of the compatibility issues
	if (bmpBiSize != 40 || compression != 0 || !(bitCount == 24 || bitCount == 32)
		|| offbits != (54 + clrUsed * 4))
		return errorMessage(1, 1, "Image is either corrupted or not supported");

	// skipping palette colors
	byte* palette = malloc(sizeof(byte) * 4 * clrUsed);
	if (palette == NULL && clrUsed > 0)
		return errorMessage(-1, 1, "could not allocate memory");
	fread(palette, sizeof(byte), clrUsed * 4, fin);

	// allocating and reading image data
	int lineWidth = ((bitCount * width + 31) / 32) * 4;
	byte** img = malloc(sizeof(byte*) * height);
	if (img == NULL)
		return errorMessage(-1, 1, "could not allocate memory");
	int success = 1;
	for (int i = 0; i < height; i++)
	{
		img[i] = malloc(sizeof(byte) * lineWidth);
		success &= img[i] != NULL;
	}
	if (!success)
		return errorMessage(-1, 1, "could not allocate memory");

	for (int i = 0; i < height; i++)
		fread(img[i], sizeof(byte), lineWidth, fin);

	// filters, finally!
	filterPointer filters[] = FILTERS;
	int ind = identifyFilter(argv[2]);
	if (ind == -1)
	{
		errorMessage(2, 1, "unknown filter");
		filtersAvailableMessage();
		return 2;
	}
	if (ind >= SIZE(filters))
		return errorMessage(3, 2, "the filter is yet to be implemented:", argv[2]);

	filterApplyingMessage();

	if (filters[ind](img, height, lineWidth, bitCount / 8))
		return errorMessage(1, 1, "could not allocate memory for filters");

	fwrite(bmpHeader, sizeof(byte), 54, fout);
	fwrite(palette, sizeof(byte), clrUsed * 4, fout);

	for (int i = 0; i < height; i++)
	{
		fwrite(img[i], sizeof(byte), lineWidth, fout);
		free(img[i]);
	}
	free(img);
	free(palette);
	fclose(fin);
	fclose(fout);

	farewellMessage();

	return 0;
}