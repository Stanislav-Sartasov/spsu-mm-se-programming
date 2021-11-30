#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>

#pragma pack(push)
#pragma pack(1)

struct fileHeader
{
	unsigned short fileType;
	unsigned int fileSize;
	unsigned short reservedByte1;
	unsigned short reservedByte2;
	unsigned int offset;
};

struct bitmapHeader
{
	unsigned int headerSize;
	unsigned int width;
	unsigned int height;
	unsigned short colorPlanes;
	unsigned short bitsPerPixel;
	unsigned int compression;
	unsigned int sizeOfBitmap;
	unsigned int hresolution;
	unsigned int vresolution;
	unsigned int colors;
	unsigned int importantColors;
};
#pragma pack(pop)

struct pix
{
	unsigned char r;
	unsigned char g;
	unsigned char b;
};


void grayFilter(struct pix* pixels, int height, int width)
{
	int res;
	for (int i = 0; i < height * width; i++)
	{
		res = (pixels[i].r + pixels[i].g + pixels[i].b) / 3;
		pixels[i].r = res;
		pixels[i].g = res;
		pixels[i].b = res;
	}
}

int bubbleSort(int* a)
{
	int tmp;
	for (int i = 1; i < 9; i++)
	{
		for (int j = 1; j < 9; j++)
		{
			if (a[j] > a[j - 1])
			{
				tmp = a[j];
				a[j] = a[j - 1];
				a[j - 1] = tmp;
			}
		}
	}
	return a[4];
}

float gauss(int* a)
{
	float res = 0;
	for (int i = 0; i < 9; i++)
	{
		if ((i == 0) || (i == 2) || (i == 6) || (i == 8))
			res += a[i] / 16;
		if ((i == 1) || (i == 3) || (i == 5) || (i == 7))
			res += a[i] / 8;
		if (i == 4)
			res += a[i] / 4;
	}
	return res;
}

int sobelX(int* a)
{
	float res = 0;
	for (int i = 0; i < 9; i++)
	{
		if ((i == 0) || (i == 6))
			res -= a[i];
		if (i == 3)
			res -= a[i] * 2;
		if ((i == 2) || (i == 8))
			res += a[i];
		if (i == 5)
			res += a[i] * 2;
	}
	return (int)min(255, max(0, abs(res)));
}

int sobelY(int* a)
{
	float res = 0;
	for (int i = 0; i < 9; i++)
	{
		if ((i == 0) || (i == 2))
			res -= a[i];
		if (i == 1)
			res -= a[i] * 2;
		if ((i == 6) || (i == 8))
			res += a[i];
		if (i == 7)
			res += a[i] * 2;
	}
	return (int)min(255, max(0, abs(res)));
}

void filter(struct pix* pixels, int height, int width, char* filter)
{
	struct pix* copyPixels;
	copyPixels = (struct pix*)malloc(width * height * sizeof(struct pix));
	memcpy(copyPixels, pixels, width * height * sizeof(struct pix));
	int* blockRed;
	int* blockGreen;
	int* blockBlue;
	blockRed = (int*)malloc(9 * sizeof(int));
	blockGreen = (int*)malloc(9 * sizeof(int));
	blockBlue = (int*)malloc(9 * sizeof(int));
	for (int i = 0; i < height * width - 2 * width; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				if ((i % width != 0) && (i < (width * height - width)))
				{
					blockRed[j + 3 * k] = copyPixels[i + j + width * k].r;
					blockGreen[j + 3 * k] = copyPixels[i + j + width * k].g;
					blockBlue[j + 3 * k] = copyPixels[i + j + width * k].b;
				}
			}
		}
		if (strcmp(filter, "median") == 0)
		{
			pixels[i + 1 + width].r = bubbleSort(blockRed);
			pixels[i + 1 + width].g = bubbleSort(blockGreen);
			pixels[i + 1 + width].b = bubbleSort(blockBlue);
		}
		if (strcmp(filter, "gauss") == 0)
		{
			pixels[i + 1 + width].r = (int)gauss(blockRed);
			pixels[i + 1 + width].g = (int)gauss(blockGreen);
			pixels[i + 1 + width].b = (int)gauss(blockBlue);
		}
		if (strcmp(filter, "sobelX") == 0)
		{
			pixels[i + 1 + width].r = (int)sobelX(blockRed);
			pixels[i + 1 + width].g = (int)sobelX(blockGreen);
			pixels[i + 1 + width].b = (int)sobelX(blockBlue);
		}
		if (strcmp(filter, "sobelY") == 0)
		{
			pixels[i + 1 + width].r = (int)sobelY(blockRed);
			pixels[i + 1 + width].g = (int)sobelY(blockGreen);
			pixels[i + 1 + width].b = (int)sobelY(blockBlue);
		}
	}
	free(blockRed);
	free(blockGreen);
	free(blockBlue);
}

int arguementsCheck(int argcount, char* filter)
{
	if (argcount != 4)
	{
		printf("You have to submit three arguments to the input.\n");
		return 0;
	}
	if ((strcmp(filter, "gauss") != 0) && (strcmp(filter, "median") != 0) && (strcmp(filter, "sobelX") != 0)
		&& (strcmp(filter, "sobelY") != 0) && (strcmp(filter, "gray") != 0))
	{
		printf("You have entered the wrong filter name. Available filters: gauss, median, sobelX, sobelY, gray.\n");
		return 0;
	}
	return 1;
}

int main(int argc, char* argv[])
{
	printf("This program applies the following filters to images:\n");
	printf("3x3 Gaussian filter, median filter, gray filter, SobelX filter and SobelY filter\n");
	if (!arguementsCheck(argc, argv[2]))
		return -1;

	FILE* fileInp;
	FILE* fileOut;

	struct fileHeader header;
	struct bitmapHeader bmpheader;

	fopen_s(&fileInp, argv[1], "rb");
	fopen_s(&fileOut, argv[3], "wb");

	if (fileInp == NULL)
	{
		printf("Problems with the input file.\n");
		return -1;
	}
	if (fileOut == NULL)
	{
		printf("Problems with the output file.\n");
		return -1;
	}

	fread(&header, sizeof(header), 1, fileInp);
	fread(&bmpheader, sizeof(bmpheader), 1, fileInp);

	if ((bmpheader.bitsPerPixel != 24) && (bmpheader.bitsPerPixel != 32))
	{
		printf("Your image has an incorrect format.\n");
		return -1;
	}

	int maxIndex = 0;
	int index = 0;
	unsigned char buffer[4];

	struct pix* array = (struct pix*)malloc(header.fileSize - sizeof(header) - sizeof(bmpheader));
	if (bmpheader.bitsPerPixel == 24)
	{
		fseek(fileInp, header.offset, SEEK_SET);
		fread(array, header.fileSize - sizeof(header) - sizeof(bmpheader), 1, fileInp);
	}
	else
	{
		fseek(fileInp, header.offset, SEEK_SET);
		while (index * 4 < header.fileSize - sizeof(header) - sizeof(bmpheader))
		{
			fread(&(array[index]), 4, 1, fileInp);
			index++;
		}
		maxIndex = index + 1;
	}

	if (strcmp(argv[2], "gray") == 0)
		grayFilter(array, bmpheader.height, bmpheader.width, argv[2]);
	if (strcmp(argv[2], "median") == 0)
		filter(array, bmpheader.height, bmpheader.width, argv[2]);
	if (strcmp(argv[2], "gauss") == 0)
		filter(array, bmpheader.height, bmpheader.width, argv[2]);
	if (strcmp(argv[2], "sobelX") == 0)
	{
		grayFilter(array, bmpheader.height, bmpheader.width);
		filter(array, bmpheader.height, bmpheader.width, argv[2]);
	}
	if (strcmp(argv[2], "sobelY") == 0)
	{
		grayFilter(array, bmpheader.height, bmpheader.width);
		filter(array, bmpheader.height, bmpheader.width, argv[2]);
	}

	fwrite(&header, sizeof(header), 1, fileOut);
	fwrite(&bmpheader, sizeof(bmpheader), 1, fileOut);

	if (bmpheader.bitsPerPixel == 24)
		fwrite(array, header.fileSize - sizeof(header) - sizeof(bmpheader), 1, fileOut);
	else
	{
		index = 0;
		while (index < maxIndex)
		{
			fwrite(&(array[index]), 4, 1, fileOut);
			index++;
		}
	}

	free(array);

	fclose(fileInp);
	fclose(fileOut);

	printf("The filter has been applied");
	return 0;
}