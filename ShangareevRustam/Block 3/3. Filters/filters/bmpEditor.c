#define _CRT_SECURE_NO_WARNINGS

#include "bmpEditor.h"

FILE* fileOpen(const char* fileName, const char* mode)
{
	FILE* result = fopen(fileName, mode);
	if (result == NULL)
	{
		if (strcmp(mode, "rb") == 0) printf("Error opening the input file!\n");
		else printf("Error opening the output file! Maybe the specified path does not exist for the output file\n");
		printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
		exit(-1);
	}
	return result;
}

void fileWrite(const void* buf, size_t elemSize, size_t elemCount, FILE* stream)
{
	if (fwrite(buf, elemSize, elemCount, stream) != elemCount && elemSize != 0)
	{
		printf("Error writing to file!\n");
		printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
		exit(-1);
	}
}

void fileRead(void* buf, size_t elemSize, size_t elemCount, FILE* stream)
{
	if (fread(buf, elemSize, elemCount, stream) != elemCount && elemSize != 0)
	{
		printf("Error while reading from file!\n");
		printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
		exit(-1);
	}
}

int cmp(unsigned char* x, unsigned char* y)
{
	return (int)*x - *y;
}

void applyMedianFilter(struct img picture, struct argb** cur)
{
	for (int i = 1; i < picture.height - 1; i++)
	{
		for (int j = 1; j < picture.width - 1; j++)
		{
			unsigned char reds[] = { picture.argb[i - 1][j - 1].red, picture.argb[i - 1][j].red,
				picture.argb[i][j - 1].red, picture.argb[i][j + 1].red, picture.argb[i + 1][j - 1].red,
				picture.argb[i + 1][j].red, picture.argb[i + 1][j + 1].red, picture.argb[i - 1][j + 1].red, picture.argb[i][j].red };
			unsigned char blues[] = { picture.argb[i - 1][j - 1].blue, picture.argb[i - 1][j].blue,
				picture.argb[i][j - 1].blue, picture.argb[i][j + 1].blue, picture.argb[i + 1][j - 1].blue,
				picture.argb[i + 1][j].blue, picture.argb[i + 1][j + 1].blue, picture.argb[i - 1][j + 1].blue, picture.argb[i][j].blue };
			unsigned char greens[] = { picture.argb[i - 1][j - 1].green, picture.argb[i - 1][j].green,
				picture.argb[i][j - 1].green, picture.argb[i][j + 1].green,
				picture.argb[i + 1][j - 1].green, picture.argb[i + 1][j].green,
				picture.argb[i + 1][j + 1].green, picture.argb[i - 1][j + 1].green, picture.argb[i][j].green };
			unsigned char alphas[] = { picture.argb[i - 1][j - 1].alpha, picture.argb[i - 1][j].alpha,
				picture.argb[i][j - 1].alpha, picture.argb[i][j + 1].alpha,
				picture.argb[i + 1][j - 1].alpha, picture.argb[i + 1][j].alpha,
				picture.argb[i + 1][j + 1].alpha, picture.argb[i - 1][j + 1].alpha, picture.argb[i][j].alpha };
			qsort(blues, 9, 1, cmp);
			qsort(reds, 9, 1, cmp);
			qsort(greens, 9, 1, cmp);
			qsort(alphas, 9, 1, cmp);
			cur[i][j].blue = blues[4];
			cur[i][j].red = reds[4];
			cur[i][j].green = greens[4];
			cur[i][j].alpha = alphas[4];
		}
	}
}

void applySobelXFilter(struct img picture, struct argb** cur)
{
	for (int i = 1; i < picture.height - 1; i++)
	{
		for (int j = 1; j < picture.width - 1; j++)
		{
			cur[i][j].blue = (unsigned char)min(255, abs((int)picture.argb[i - 1][j - 1].blue
				+ 2 * (int)picture.argb[i][j - 1].blue + (int)picture.argb[i + 1][j - 1].blue
				- (int)picture.argb[i - 1][j + 1].blue - 2 * (int)picture.argb[i][j + 1].blue
				- (int)picture.argb[i + 1][j + 1].blue));
			cur[i][j].green = (unsigned char)min(255, abs((int)picture.argb[i - 1][j - 1].green
				+ 2 * (int)picture.argb[i][j - 1].green + (int)picture.argb[i + 1][j - 1].green
				- (int)picture.argb[i - 1][j + 1].green - 2 * (int)picture.argb[i][j + 1].green
				- (int)picture.argb[i + 1][j + 1].green));
			cur[i][j].red = (unsigned char)min(255, abs((int)picture.argb[i - 1][j - 1].red
				+ 2 * (int)picture.argb[i][j - 1].red + (int)picture.argb[i + 1][j - 1].red
				- (int)picture.argb[i - 1][j + 1].red - 2 * (int)picture.argb[i][j + 1].red
				- (int)picture.argb[i + 1][j + 1].red));
			cur[i][j].alpha = (unsigned char)min(255, abs((int)picture.argb[i - 1][j - 1].alpha
				+ 2 * (int)picture.argb[i][j - 1].alpha + (int)picture.argb[i + 1][j - 1].alpha
				- (int)picture.argb[i - 1][j + 1].alpha - 2 * (int)picture.argb[i][j + 1].alpha
				- (int)picture.argb[i + 1][j + 1].alpha));
			cur[i][j].blue = cur[i][j].green = cur[i][j].red = cur[i][j].alpha
				= (unsigned char)(0.1 * cur[i][j].blue + 0.6 * cur[i][j].green + 0.3 * cur[i][j].red);
		}
	}
}

void applySobelYFilter(struct img picture, struct argb** cur)
{
	for (int i = 1; i < picture.height - 1; i++)
	{
		for (int j = 1; j < picture.width - 1; j++)
		{
			cur[i][j].blue = (unsigned char)min(255, abs((int)picture.argb[i - 1][j - 1].blue
				+ 2 * (int)picture.argb[i - 1][j].blue + (int)picture.argb[i - 1][j + 1].blue
				- (int)picture.argb[i + 1][j - 1].blue - 2 * (int)picture.argb[i + 1][j].blue
				- (int)picture.argb[i + 1][j + 1].blue));
			cur[i][j].green = (unsigned char)min(255, abs((int)picture.argb[i - 1][j - 1].green
				+ 2 * (int)picture.argb[i - 1][j].green + (int)picture.argb[i - 1][j + 1].green
				- (int)picture.argb[i + 1][j - 1].green - 2 * (int)picture.argb[i + 1][j].green
				- (int)picture.argb[i + 1][j + 1].green));
			cur[i][j].red = (unsigned char)min(255, abs((int)picture.argb[i - 1][j - 1].red
				+ 2 * (int)picture.argb[i - 1][j].red + (int)picture.argb[i - 1][j + 1].red
				- (int)picture.argb[i + 1][j - 1].red - 2 * (int)picture.argb[i + 1][j].red
				- (int)picture.argb[i + 1][j + 1].red));
			cur[i][j].alpha = (unsigned char)min(255, abs((int)picture.argb[i - 1][j - 1].alpha
				+ 2 * (int)picture.argb[i - 1][j].alpha + (int)picture.argb[i - 1][j + 1].alpha
				- (int)picture.argb[i + 1][j - 1].alpha - 2 * (int)picture.argb[i + 1][j].alpha
				- (int)picture.argb[i + 1][j + 1].alpha));
			cur[i][j].blue = cur[i][j].green = cur[i][j].red = cur[i][j].alpha
				= (unsigned char)(0.1 * cur[i][j].blue + 0.6 * cur[i][j].green + 0.3 * cur[i][j].red);
		}
	}
}

void applyGaussFilter(struct img picture, struct argb** cur)
{
	for (int i = 1; i < picture.height - 1; i++)
	{
		for (int j = 1; j < picture.width - 1; j++)
		{
			cur[i][j].blue = (unsigned char)(((int)picture.argb[i - 1][j - 1].blue
				+ 2 * (int)picture.argb[i - 1][j].blue + (int)picture.argb[i - 1][j + 1].blue
				+ 2 * (int)picture.argb[i][j - 1].blue + 4 * (int)picture.argb[i][j].blue
				+ 2 * (int)picture.argb[i][j + 1].blue + (int)picture.argb[i + 1][j - 1].blue
				+ 2 * (int)picture.argb[i + 1][j].blue + (int)picture.argb[i + 1][j + 1].blue) / 16);
			cur[i][j].green = (unsigned char)(((int)picture.argb[i - 1][j - 1].green
				+ 2 * (int)picture.argb[i - 1][j].green + (int)picture.argb[i - 1][j + 1].green
				+ 2 * (int)picture.argb[i][j - 1].green + 4 * (int)picture.argb[i][j].green
				+ 2 * (int)picture.argb[i][j + 1].green + (int)picture.argb[i + 1][j - 1].green
				+ 2 * (int)picture.argb[i + 1][j].green + (int)picture.argb[i + 1][j + 1].green) / 16);
			cur[i][j].red = (unsigned char)(((int)picture.argb[i - 1][j - 1].red
				+ 2 * (int)picture.argb[i - 1][j].red + (int)picture.argb[i - 1][j + 1].red
				+ 2 * (int)picture.argb[i][j - 1].red + 4 * (int)picture.argb[i][j].red
				+ 2 * (int)picture.argb[i][j + 1].red + (int)picture.argb[i + 1][j - 1].red
				+ 2 * (int)picture.argb[i + 1][j].red + (int)picture.argb[i + 1][j + 1].red) / 16);
			cur[i][j].alpha = (unsigned char)(((int)picture.argb[i - 1][j - 1].alpha
				+ 2 * (int)picture.argb[i - 1][j].alpha + (int)picture.argb[i - 1][j + 1].alpha
				+ 2 * (int)picture.argb[i][j - 1].alpha + 4 * (int)picture.argb[i][j].alpha
				+ 2 * (int)picture.argb[i][j + 1].alpha + (int)picture.argb[i + 1][j - 1].alpha
				+ 2 * (int)picture.argb[i + 1][j].alpha + (int)picture.argb[i + 1][j + 1].alpha) / 16);
		}
	}
}

void applyGrayFilter(struct img* picture)
{
	for (int i = 0; i < picture->height; i++)
	{
		for (int j = 0; j < picture->width; j++)
		{
			picture->argb[i][j].red = picture->argb[i][j].blue
				= picture->argb[i][j].green = picture->argb[i][j].alpha
				= (unsigned char)(0.1 * picture->argb[i][j].blue + 0.6 * picture->argb[i][j].green + 0.3 * picture->argb[i][j].red);
		}
	}
}

void applyFilter(struct img* picture, char* filterName)
{
	struct argb** cur = (struct argb**)malloc(sizeof(struct argb*) * picture->height);
	for (int i = 0; i < picture->height; i++) cur[i] = (struct argb*)malloc(sizeof(struct argb) * picture->width);

	if (!strcmp(filterName, "gray"))
	{
		applyGrayFilter(picture);
	}
	else
	{
		if (!strcmp(filterName, "median"))
		{
			applyMedianFilter(*picture, cur);
		}
		else if (!strcmp(filterName, "gauss"))
		{
			applyGaussFilter(*picture, cur);
		}
		else if (!strcmp(filterName, "sobelX"))
		{
			applySobelXFilter(*picture, cur);
		}
		else if (!strcmp(filterName, "sobelY"))
		{
			applySobelYFilter(*picture, cur);
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
		for (int j = 0; j < picture->width; j++)
		{
			cur[0][j] = cur[1][j];
			cur[picture->height - 1][j] = cur[picture->height - 2][j];
		}
		for (int i = 0; i < picture->height; i++)
		{
			cur[i][0] = cur[i][1];
			cur[i][picture->width - 1] = cur[i][picture->width - 2];
		}

		for (int i = 0; i < picture->height; i++)
		{
			free(picture->argb[i]);
		}
		free(picture->argb);

		picture->argb = cur;
	}
}

struct img readImage(FILE* fp, int height, int width, unsigned short int numberBitsPerPixel)
{
	struct img picture;
	picture.height = height;
	picture.width = width;
	picture.argb = (struct argb**)malloc(sizeof(struct argb*) * height);

	if (numberBitsPerPixel == 32)
	{
		for (int i = height - 1; i >= 0; i--)
		{
			picture.argb[i] = (struct argb*)malloc(sizeof(struct argb) * width);
			fileRead(picture.argb[i], sizeof(struct argb), (size_t)width, fp);
		}
	}
	else
	{
		for (int i = height - 1; i >= 0; i--)
		{
			picture.argb[i] = (struct argb*)malloc(sizeof(struct argb) * width);
			int garbage;
			for (int j = 0; j < width; j++)
			{
				fileRead(&picture.argb[i][j], 3, 1, fp);
				picture.argb[i][j].alpha = 0;
			}
			fileRead(&garbage, ((24 * width + 31) / 32) * 4 - width * 3, 1, fp);
		}
	}

	return picture;
}

void writeImage(struct bmpHeader header, struct bmpInfoHeader headerInfo, struct img picture, char** argv, unsigned short int numberBitsPerPixel)
{
	FILE* fpw = fileOpen(argv[3], "wb");

	fileWrite(header.identifier, 2, 1, fpw);
	fileWrite(&header.size, 3 * sizeof(int), 1, fpw);
	fileWrite(&headerInfo, sizeof(struct bmpInfoHeader), 1, fpw);

	if (numberBitsPerPixel == 32)
	{
		for (int i = picture.height - 1; i >= 0; i--)
		{
			fileWrite(picture.argb[i], 4, picture.width, fpw);
		}
	}
	else
	{
		for (int i = picture.height - 1; i >= 0; i--)
		{
			struct argb garbage;
			garbage.blue = garbage.red = garbage.green = 0;
			for (int j = 0; j < picture.width; j++)
			{
				fileWrite(&picture.argb[i][j], 3, 1, fpw);
			}
			fileWrite(&garbage, ((24 * picture.width + 31) / 32) * 4 - picture.width * 3, 1, fpw);
		}
	}

	for (int i = 0; i < picture.height; i++)
	{
		free(picture.argb[i]);
	}
	free(picture.argb);

	fclose(fpw);
}

void readHeaders(FILE* fp, struct bmpHeader* header, struct bmpInfoHeader* headerInfo)
{
	fileRead(header->identifier, 2, 1, fp);
	fileRead(&header->size, 12, 1, fp);
	fileRead(headerInfo, 40, 1, fp);
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
			"The file must be in 24bpp or 32bpp pixel format\n");
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