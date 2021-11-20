#define _CRT_SECURE_NO_WARNINGS
#include <stdbool.h>
#include <stdlib.h>
#include <string.h>
#include <stdio.h>
#include <math.h>


typedef unsigned int int32;
typedef unsigned short int16;
typedef unsigned char byte;


#pragma pack(push, 1)
struct BITMAPFILEHEADER
{
	int16 bfType;
	int32 bfSize;
	int16 bfReserved1;
	int16 bfReserved2;
	int32 bfOffBits;
};


struct BITMAPINFOHEADER
{
	int32 biSize;
	int32 biWidth;
	int32 biHeight;
	int16 biPlanes;
	int16 biBitCount;
	int32 biCompression;
	int32 biSizeImage;
	int32 biXPelsPerMeter;
	int32 biYPelsPerMeter;
	int32 biClrUsed;
	int32 biClrImportant;
};
#pragma pack(pop)


struct pixel
{
	byte blue;
	byte green;
	byte red;
};


struct image
{
	struct pixel** pixels;
	int32 height;
	int32 width;
};


void readImage(FILE* file, struct BITMAPFILEHEADER* fileHeader, struct BITMAPINFOHEADER* infoHeader, struct image* image)
{
	fread(fileHeader, sizeof(struct BITMAPFILEHEADER), 1, file);
	fread(infoHeader, sizeof(struct BITMAPINFOHEADER), 1, file);

	struct pixel** pixels = malloc(infoHeader->biHeight * sizeof(struct pixel*));
	for (int32 i = 0; i < infoHeader->biHeight; i++)
	{
		pixels[i] = malloc(infoHeader->biWidth * sizeof(struct pixel));
	}

	for (int32 i = 0; i < infoHeader->biHeight; i++)
	{
		for (int32 j = 0; j < infoHeader->biWidth; j++)
		{
			pixels[i][j].blue = (byte)getc(file);
			pixels[i][j].green = (byte)getc(file);
			pixels[i][j].red = (byte)getc(file);

			if (infoHeader->biBitCount == 32)
			{
				getc(file);
			}
		}
	}

	image->pixels = pixels;
	image->height = infoHeader->biHeight;
	image->width = infoHeader->biWidth;

	fclose(file);
}


void writeImage(FILE* file, struct BITMAPFILEHEADER* fileHeader, struct BITMAPINFOHEADER* infoHeader, struct image* image)
{
	fwrite(fileHeader, sizeof(struct BITMAPFILEHEADER), 1, file);
	fwrite(infoHeader, sizeof(struct BITMAPINFOHEADER), 1, file);

	for (int32 i = 0; i < image->height; i++)
	{
		for (int32 j = 0; j < image->width; j++)
		{
			fwrite(&(image->pixels[i][j].blue), 1, 1, file);
			fwrite(&(image->pixels[i][j].green), 1, 1, file);
			fwrite(&(image->pixels[i][j].red), 1, 1, file);

			if (infoHeader->biBitCount == 32)
			{
				putc(0, file);
			}
		}
	}

	for (int32 i = 0; i < image->height; i++)
	{
		free(image->pixels[i]);
	}
	free(image->pixels);

	fclose(file);
}


void filter(struct image* inImage, struct image* outImage, double* matrix, bool check)
{
	int32 height = inImage->height;
	int32 width = inImage->width;

	outImage->height = height;
	outImage->width = width;
	struct pixel** pixels = malloc(height * sizeof(struct pixel*));
	for (int32 i = 0; i < height; i++)
	{
		pixels[i] = malloc(width * sizeof(struct pixel));
	}
	outImage->pixels = pixels;

	for (int32 i = 1; i < height - 1; i++)
	{
		for (int32 j = 1; j < width - 1; j++)
		{
			double sum_blue = 0;
			double sum_green = 0;
			double sum_red = 0;
			for (int32 k_i = 0; k_i < 3; k_i++)
			{
				for (int32 k_j = 0; k_j < 3; k_j++)
				{
					sum_blue += inImage->pixels[i - 1 + k_i][j - 1 + k_j].blue * matrix[3 * k_i + k_j];
					sum_green += inImage->pixels[i - 1 + k_i][j - 1 + k_j].green * matrix[3 * k_i + k_j];
					sum_red += inImage->pixels[i - 1 + k_i][j - 1 + k_j].red * matrix[3 * k_i + k_j];
				}
			}

			int32 final_sum_blue = round(sum_blue);
			int32 final_sum_green = round(sum_green);
			int32 final_sum_red = round(sum_red);

			if (check)
			{
				sum_blue = sum_blue > 255 ? 255 : sum_blue;
				sum_blue = sum_blue < 0 ? 0 : sum_blue;
				sum_green = sum_green > 255 ? 255 : sum_green;
				sum_green = sum_green < 0 ? 0 : sum_green;
				sum_red = sum_red > 255 ? 255 : sum_red;
				sum_red = sum_red < 0 ? 0 : sum_red;
			}

			outImage->pixels[i][j].blue = (byte)sum_blue;
			outImage->pixels[i][j].green = (byte)sum_green;
			outImage->pixels[i][j].red = (byte)sum_red;
		}
	}
}


void averagingFilter(struct image* inImage, struct image* outImage)
{
	double matrix[9] = { 0 };
	for (int i = 0; i < 9; i++)
	{
		matrix[i] = 1.0 / 9;
	}

	filter(inImage, outImage, matrix, false);
}


void gaussianFilter(struct image* inImage, struct image* outImage)
{
	double matrix[9] = { 0 };
	matrix[0] = 1.0 / 16;
	matrix[1] = 1.0 / 8;
	matrix[2] = 1.0 / 16;
	matrix[3] = 1.0 / 8;
	matrix[4] = 1.0 / 4;
	matrix[5] = 1.0 / 8;
	matrix[6] = 1.0 / 16;
	matrix[7] = 1.0 / 8;
	matrix[8] = 1.0 / 16;

	filter(inImage, outImage, matrix, false);
}


void sobelXFilter(struct image* inImage, struct image* outImage)
{
	double matrix[9] = { 0 };
	matrix[0] = -1.0;
	matrix[1] = -2.0;
	matrix[2] = -1.0;
	matrix[3] = 0.0;
	matrix[4] = 0.0;
	matrix[5] = 0.0;
	matrix[6] = 1.0;
	matrix[7] = 2.0;
	matrix[8] = 1.0;

	filter(inImage, outImage, matrix, true);
}


void sobelYFilter(struct image* inImage, struct image* outImage)
{
	double matrix[9] = { 0 };
	matrix[0] = -1.0;
	matrix[1] = 0.0;
	matrix[2] = 1.0;
	matrix[3] = -2.0;
	matrix[4] = 0.0;
	matrix[5] = 2.0;
	matrix[6] = -1.0;
	matrix[7] = 0.0;
	matrix[8] = 1.0;

	filter(inImage, outImage, matrix, true);
}


void sobelFilter(struct image* inImage, struct image* outImage)
{
	struct image x;
	struct image y;
	sobelXFilter(inImage, &x);
	sobelYFilter(inImage, &y);

	int32 height = inImage->height;
	int32 width = inImage->width;

	outImage->height = height;
	outImage->width = width;
	struct pixel** pixels = malloc(height * sizeof(struct pixel*));
	for (int32 i = 0; i < height; i++)
	{
		pixels[i] = malloc(width * sizeof(struct pixel));
	}
	outImage->pixels = pixels;

	for (int32 i = 1; i < height - 1; i++)
	{
		for (int32 j = 1; j < width - 1; j++)
		{
			outImage->pixels[i][j].blue = round(sqrt(pow(x.pixels[i][j].blue, 2) + pow(y.pixels[i][j].blue, 2)));
			outImage->pixels[i][j].green = round(sqrt(pow(x.pixels[i][j].green, 2) + pow(y.pixels[i][j].green, 2)));
			outImage->pixels[i][j].red = round(sqrt(pow(x.pixels[i][j].red, 2) + pow(y.pixels[i][j].red, 2)));
		}
	}

	for (int32 i = 0; i < x.height; i++)
	{
		free(x.pixels[i]);
	}
	free(x.pixels);

	for (int32 i = 0; i < y.height; i++)
	{
		free(y.pixels[i]);
	}
	free(y.pixels);
}


void readGrayscaleAndWriteImage(FILE* fileIn, FILE* fileOut)
{
	struct BITMAPFILEHEADER fileHeader;
	struct BITMAPINFOHEADER infoHeader;
	fread(&fileHeader, sizeof(struct BITMAPFILEHEADER), 1, fileIn);
	fread(&infoHeader, sizeof(struct BITMAPINFOHEADER), 1, fileIn);

	struct pixel** pixels = malloc(infoHeader.biHeight * sizeof(struct pixel*));
	for (int32 i = 0; i < infoHeader.biHeight; i++)
	{
		pixels[i] = malloc(infoHeader.biWidth * sizeof(struct pixel));
	}

	for (int32 i = 0; i < infoHeader.biHeight; i++)
	{
		for (int32 j = 0; j < infoHeader.biWidth; j++)
		{
			pixels[i][j].blue = (byte)getc(fileIn);
			pixels[i][j].green = (byte)getc(fileIn);
			pixels[i][j].red = (byte)getc(fileIn);

			if (infoHeader.biBitCount == 32)
			{
				getc(fileIn);
			}
		}
	}

	fclose(fileIn);

	fileHeader.bfSize = 1078 + infoHeader.biHeight * infoHeader.biWidth;
	fileHeader.bfOffBits = 1078;
	infoHeader.biBitCount = 8;
	infoHeader.biSizeImage = fileHeader.bfSize;
	infoHeader.biClrUsed = 256;
	infoHeader.biClrImportant = 0;

	fwrite(&fileHeader, sizeof(struct BITMAPFILEHEADER), 1, fileOut);
	fwrite(&infoHeader, sizeof(struct BITMAPINFOHEADER), 1, fileOut);

	for (int32 i = 0; i < 256; i++)
	{
		putc(i, fileOut);
		putc(i, fileOut);
		putc(i, fileOut);
		putc(0, fileOut);
	}

	for (int32 i = 0; i < infoHeader.biHeight; i++)
	{
		for (int32 j = 0; j < infoHeader.biWidth; j++)
		{
			putc(round(0.299 * pixels[i][j].red + 0.587 * pixels[i][j].green + 0.114 * pixels[i][j].blue), fileOut);
		}
	}

	fclose(fileOut);

	for (int32 i = 0; i < infoHeader.biHeight; i++)
	{
		free(pixels[i]);
	}
	free(pixels);
}


int main(int argc, char* argv[])
{
	printf("This program filters the image. Available filters:\n");
	printf("- averaging <averaging>,\n");
	printf("- gaussian <gaussian>,\n");
	printf("- sobel filter on the X axis <sobelx>,\n");
	printf("- sobel filter on the Y axis <sobely>,\n");
	printf("- sobel filter (all axes) <sobel>\n");
	printf("- converting to grayscale <grayscale>\n\n");

	if (argc != 4)
	{
		printf("Incorrect arguments. The program has finished its work.\n");
		return -1;
	}
	else
	{
		FILE* inFile = fopen(argv[1], "rb");
		if (inFile == NULL)
		{
			printf("Input file is not found. The program has finished its work.\n");
			return -1;
		}

		if (strcmp(argv[2], "averaging") && strcmp(argv[2], "gaussian") && strcmp(argv[2], "sobelx") &&
			strcmp(argv[2], "sobely") && strcmp(argv[2], "sobel") && strcmp(argv[2], "grayscale"))
		{
			printf("There is no such a filter. The program has finished its work.\n");
			return -1;
		}

		FILE* outFile = fopen(argv[3], "wb");

		if (!strcmp(argv[2], "grayscale"))
		{
			readGrayscaleAndWriteImage(inFile, outFile);
			printf("The image has been converted to grayscale.\n");
			return 0;
		}
		else
		{
			struct BITMAPFILEHEADER fileHeader;
			struct BITMAPINFOHEADER infoHeader;
			struct image inImage;
			struct image outImage;

			readImage(inFile, &fileHeader, &infoHeader, &inImage);

			if (!strcmp(argv[2], "averaging"))
			{
				averagingFilter(&inImage, &outImage);
			}
			else if (!strcmp(argv[2], "gaussian"))
			{
				gaussianFilter(&inImage, &outImage);
			}
			else if (!strcmp(argv[2], "sobelx"))
			{
				sobelXFilter(&inImage, &outImage);
			}
			else if (!strcmp(argv[2], "sobely"))
			{
				sobelYFilter(&inImage, &outImage);
			}
			else
			{
				sobelFilter(&inImage, &outImage);
			}

			writeImage(outFile, &fileHeader, &infoHeader, &outImage);

			printf("Filtration has been successful.\n");
			return 0;
		}
	}
}