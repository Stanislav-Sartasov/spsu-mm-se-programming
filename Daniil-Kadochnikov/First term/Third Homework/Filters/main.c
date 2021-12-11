#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include"filters.h"

#pragma pack(push)
#pragma pack(1) 



struct bitMapFileHeader
{
	unsigned char char1, char2;
	unsigned long bfSize;
	unsigned short bfReserved1;
	unsigned short bfReserved2;
	unsigned long bfOffBits;
};

struct bitMapInfo
{
	unsigned long biSize;
	unsigned long biWidth;
	unsigned long biHeight;
	unsigned short biPlanes;
	unsigned short biBitCount;
	unsigned long biCompression;
	unsigned long biSizeImage;
	unsigned long biXPelsPerMeter;
	unsigned long biYPelsPerMeter;
	unsigned long biClrUsed;
	unsigned long biClrImportant;
};

#pragma pack(pop)



void writingError()
{
	printf("ERROR: failed to write pixels in file.\n");
	printf("Completing the programm.\n");
	exit(-1);
}

int checkArguement(char* filter)
{
	if (!(strcmp(filter, "median"))) return 0;
	if (!(strcmp(filter, "gauss3"))) return 0;
	if (!(strcmp(filter, "sobelX"))) return 0;
	if (!(strcmp(filter, "sobelY"))) return 0;
	if (!(strcmp(filter, "grey"))) return 0;
	if (!(strcmp(filter, "gauss5"))) return 0;
	else
	{
		printf("ERROR: you have written the worg filter name. Filters' names:\n-Median filter 3x3 - median;\n-Gaussian filter 3x3 - gauss3;\n-Gaussian filter 5x5 - gauss5;\n-Sobel filter on X - sobelX;\n-Sobel filter on Y - sobelY;\n-grey filter - grey.\n\n");
		return 1;
	}
}

int main(int argc, char* argv[])
{
	
	printf("\nThe program gets a 24 or 32 bit BMP-file and applies one of the following filters:\n-Median filter 3x3 - median;\n-Gaussian filter 3x3 - gauss;\n-Sobel filter on X - sobelX;\n-Sobel filter on Y - sobelY;\n-grey filter - grey.\n\n");
	if (argc != 4)
	{
		printf("Inappropriate amount of arguments.\nAppropriate from: <program> <input file name> <filter name> <output file name>\n");
		return -1;
	}
	if (checkArguement(argv[2])) return -1;

	FILE
		* inputFile,
		* outputFile;

	if ((inputFile = fopen(argv[1], "rb")) == NULL)
	{
		printf("ERROR: unable to open file \"%s\".\n", argv[1]);
		printf("Completing the programm.\n");
		return -1;
	}
	if ((outputFile = fopen(argv[3], "wb")) == NULL)
	{
		printf("ERROR: unable to create and open file \"%s\".\n", argv[3]);
		printf("Completing the programm.\n");
		return -1;
	}

	struct bitMapFileHeader newHeader;
	struct bitMapInfo newInfo;

	fread(&newHeader, sizeof(newHeader), 1, inputFile);
	fread(&newInfo, sizeof(newInfo), 1, inputFile);

	struct image image;
	image.height = newInfo.biHeight;
	image.width = newInfo.biWidth;

	if ((newHeader.char1 != 'B' || newHeader.char2 != 'M'))
	{
		printf("ERROR: wrong file type of \"%s\".\n", argv[1]);
		printf("Completing the programm.\n");
		return -1;
	}
	if (newInfo.biBitCount != 24 && newInfo.biBitCount != 32)
	{
		printf("ERROR: wrong file format of \"%s\".\n", argv[1]);
		printf("Completing the programm.\n");
		return -1;
	}

	if ((image.pixels = (struct rgb**)malloc(image.height * sizeof(struct rgb*))) == NULL)
	{
		allocationError();
	}
	int coefficient, added, coefficient2;
	for (coefficient = 0; coefficient < newInfo.biHeight; coefficient++)
	{
		if ((image.pixels[coefficient] = (struct rgb*)malloc(image.width * sizeof(struct rgb))) == NULL)
		{
			allocationError();
		}
	}

	fseek(inputFile, newHeader.bfOffBits, SEEK_SET);

	if (newInfo.biBitCount == 24)
	{
		added = (4 - ((newInfo.biWidth * 3) % 4)) % 4;
		for (coefficient = 0; coefficient < newInfo.biHeight; coefficient++)
		{
			fread(&image.pixels[coefficient][0], 3 * newInfo.biWidth, 1, inputFile);
			fseek(inputFile, added, SEEK_CUR);
		}
	}
	else
	{
		for (coefficient = 0; coefficient < image.height; coefficient++)
		{
			for (coefficient2 = 0; coefficient2 < image.width; coefficient2++)
			{
				fread(&image.pixels[coefficient][coefficient2], 3, 1, inputFile);
				fseek(inputFile, 1, SEEK_CUR);
			}
		}
	}

	if (strcmp(argv[2], "median") == 0)
	{
		filter(&image, medianFunction, 3);
	}
	if (strcmp(argv[2], "gauss3") == 0)
	{
		filter(&image, gauss3Function, 3);
	}
	if (strcmp(argv[2], "gauss5") == 0)
	{
		filter(&image, gauss5Function, 5);
	}
	if (strcmp(argv[2], "sobelX") == 0)
	{
		greyFilter(&image);
		filter(&image, sobelXFunction, 3);
	}
	if (strcmp(argv[2], "sobelY") == 0)
	{
		greyFilter(&image);
		filter(&image, sobelYFunction, 3);
	}
	if (strcmp(argv[2], "grey") == 0)
	{
		greyFilter(&image);
	}

	if ((fwrite(&newHeader, sizeof(newHeader), 1, outputFile)) == NULL)
	{
		printf("ERROR: failed to write header in file.\n");
		printf("Completing the programm.\n");
		return -1;
	}
	if ((fwrite(&newInfo, sizeof(newInfo), 1, outputFile)) == NULL)
	{
		printf("ERROR: failed to write info in file.\n");
		printf("Completing the programm.\n");
		return -1;
	}

	char gap[1] = "\0";
	if (newInfo.biBitCount == 24)
	{
		for (coefficient = 0; coefficient < newInfo.biHeight; coefficient++)
		{
			if ((fwrite(&image.pixels[coefficient][0], 3 * newInfo.biWidth, 1, outputFile)) == NULL)
			{
				writingError();
			}
			if ((fwrite(&gap, sizeof(char), added, outputFile)) == NULL && added != 0)
			{
				writingError();
			}
			free(image.pixels[coefficient]);
		}
	}
	else
	{
		for (coefficient = 0; coefficient < image.height; coefficient++)
		{
			for (coefficient2 = 0; coefficient2 < image.width; coefficient2++)
			{
				if ((fwrite(&image.pixels[coefficient][coefficient2], 3, 1, outputFile)) == NULL)
				{
					writingError();
				}
				if ((fwrite(&gap, sizeof(char), 1, outputFile)) == NULL)
				{
					writingError();
				}
			}
			free(image.pixels[coefficient]);
		}
	}

	printf("\nSUCCESS: the filter was been applied.\n");

	free(image.pixels);
	fclose(inputFile);
	fclose(outputFile);
	return 0;
}