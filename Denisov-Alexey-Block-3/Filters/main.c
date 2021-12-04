#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "bmpHeader.h"
#include "filters.h"

int main(int argc, char* argv[])
{

	printf("This program applies to input 24 or 32 bit BMP image one of median, gauss, sobelX, sobelY, grayscale filters.");

	if (strcmp(argv[2], "median") != 0 && strcmp(argv[2], "gauss") != 0 && strcmp(argv[2], "sobelX") != 0 && strcmp(argv[2], "sobelY") != 0 && strcmp(argv[2], "grayscale") != 0)
	{
		printf("There is only median, gauss, sobelX, sobelY and grayscale filters");
		return -1;
	}
	
	FILE* inputFile = fopen(argv[1], "rb");
	if (inputFile == NULL)
	{
		printf("Input error.");
		return -1;
	}
	bmpHeader header = bmpLoad(inputFile);

	if (header.biBitCount != 32 && header.biBitCount != 24)
	{
		printf("Unsupported bitness of image.");
	}

	FILE* outputFile = fopen(argv[3], "wb");
	if (outputFile == NULL)
	{
		printf("Unable to create output file.");
		return -1;
	}
	BYTE headerFull[HEADER_SIZE];
	fseek(inputFile, 0, SEEK_SET);
	fread(headerFull, sizeof(BYTE), HEADER_SIZE * sizeof(BYTE), inputFile);
	fwrite(headerFull, sizeof(BYTE), HEADER_SIZE, outputFile);


	BYTE*** image = (BYTE***)malloc(header.biHeight * sizeof(BYTE**));
	int myWidth, channels;
	if (header.biBitCount == 24)
	{
		myWidth = header.biWidth + header.biWidth % 4;
		channels = 3;
	}
	else
	{
		myWidth = header.biWidth;
		channels = 4;
	}

	fseek(inputFile, HEADER_SIZE, SEEK_SET);
	for (int i = 0; i < header.biHeight; i++)
	{
		image[i] = (BYTE**)malloc(myWidth * sizeof(BYTE*));
		for (int j = 0; j < myWidth; j++)
		{
			image[i][j] = (BYTE*)malloc(channels * sizeof(BYTE));
			fread(image[i][j], sizeof(BYTE), channels * sizeof(BYTE), inputFile);
		}
	}

	if (strcmp(argv[2], "median") == 0)
	{
		median(&image, header, myWidth, channels);
	}
	else if (strcmp(argv[2], "gauss") == 0)
	{
		gauss(&image, header, myWidth, channels);
	}
	else if (strcmp(argv[2], "sobelX") == 0)
	{
		sobelX(&image, header, myWidth, channels);
	}
	else if (strcmp(argv[2], "sobelY") == 0)
	{
		sobelY(&image, header, myWidth, channels);
	}
	else if (strcmp(argv[2], "greyscale") == 0)
	{
		greyscale(&image, header, myWidth, channels);
	}

	for (int i = 0; i < header.biHeight; i++) 
	{
		for (int j = 0; j < myWidth; j++)
		{
			fwrite(image[i][j], sizeof(BYTE), channels * sizeof(BYTE), outputFile);
		}
	}

	for (int i = 0; i < header.biHeight; i++)
	{
		for (int j = 0; j < myWidth; j++)
		{
			free(image[i][j]);
		}
		free(image[i]);
	}
	free(image);

	fclose(inputFile);
	fclose(outputFile);

	return 0;
}