#define _CRT_SECURE_NO_WARNINGS

#include "bmpEditor.h"

int min(int a, int b)
{
	if (a > b) return b;
	else return a;
}

unsigned char grayScale(struct rgb rgb)
{
	return (0.1 * rgb.blue + 0.6 * rgb.green + 0.3 * rgb.red);
}

void medianFilter(struct img* picture)
{
	struct rgb** cur = (struct rgb**)malloc(sizeof(struct rgb*) * picture->height);
	for (int i = 0; i < picture->height; i++)
	{
		cur[i] = (struct rgb*)malloc(sizeof(struct rgb) * picture->width);
	}
	for (int i = 1; i < picture->height - 1; i++)
	{
		for (int j = 1; j < picture->width - 1; j++)
		{
			unsigned char reds[8] = { picture->rgb[i - 1][j - 1].red, picture->rgb[i - 1][j].red,
				picture->rgb[i][j - 1].red, picture->rgb[i][j + 1].red, picture->rgb[i + 1][j - 1].red,
				picture->rgb[i + 1][j].red, picture->rgb[i + 1][j + 1].red, picture->rgb[i - 1][j + 1].red };
			unsigned char blues[8] = { picture->rgb[i - 1][j - 1].blue, picture->rgb[i - 1][j].blue,
				picture->rgb[i][j - 1].blue, picture->rgb[i][j + 1].blue, picture->rgb[i + 1][j - 1].blue,
				picture->rgb[i + 1][j].blue, picture->rgb[i + 1][j + 1].blue, picture->rgb[i - 1][j + 1].blue };
			unsigned char greens[8] = { picture->rgb[i - 1][j - 1].green, picture->rgb[i - 1][j].green,
				picture->rgb[i][j - 1].green, picture->rgb[i][j + 1].green,
				picture->rgb[i + 1][j - 1].green, picture->rgb[i + 1][j].green,
				picture->rgb[i + 1][j + 1].green, picture->rgb[i - 1][j + 1].green };
			cur[i][j].blue = arrMedian(blues);
			cur[i][j].red = arrMedian(reds);
			cur[i][j].green = arrMedian(greens);
		}
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
	free(picture->rgb);
	picture->rgb = cur;
}

void sobelXFilter(struct img* picture)
{
	struct rgb** cur = (struct rgb**)malloc(sizeof(struct rgb*) * picture->height);
	for (int i = 0; i < picture->height; i++)
	{
		cur[i] = (struct rgb*)malloc(sizeof(struct rgb) * picture->width);
	}
	for (int i = 1; i < picture->height - 1; i++)
	{
		for (int j = 1; j < picture->width - 1; j++)
		{
			int blue = (int)picture->rgb[i - 1][j - 1].blue + 2 * (int)picture->rgb[i][j - 1].blue + (int)picture->rgb[i + 1][j - 1].blue
				- (int)picture->rgb[i - 1][j + 1].blue - 2 * (int)picture->rgb[i][j + 1].blue - (int)picture->rgb[i + 1][j + 1].blue;
			int red = (int)picture->rgb[i - 1][j - 1].red + 2 * (int)picture->rgb[i][j - 1].red + (int)picture->rgb[i + 1][j - 1].red
				- (int)picture->rgb[i - 1][j + 1].red - 2 * (int)picture->rgb[i][j + 1].red - (int)picture->rgb[i + 1][j + 1].red;
			int green = (int)picture->rgb[i - 1][j - 1].green + 2 * (int)picture->rgb[i][j - 1].green + (int)picture->rgb[i + 1][j - 1].green
				- (int)picture->rgb[i - 1][j + 1].green - 2 * (int)picture->rgb[i][j + 1].green - (int)picture->rgb[i + 1][j + 1].green;
			cur[i][j].blue = (unsigned char)min(255, abs(blue));
			cur[i][j].green = (unsigned char)min(255, abs(green));
			cur[i][j].red = (unsigned char)min(255, abs(red));
		}
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
	free(picture->rgb);
	picture->rgb = cur;
}

void sobelYFilter(struct img* picture)
{
	struct rgb** cur = (struct rgb**)malloc(sizeof(struct rgb*) * picture->height);
	for (int i = 0; i < picture->height; i++)
	{
		cur[i] = (struct rgb*)malloc(sizeof(struct rgb) * picture->width);
	}
	for (int i = 1; i < picture->height - 1; i++)
	{
		for (int j = 1; j < picture->width - 1; j++)
		{
			int blue = (int)picture->rgb[i - 1][j - 1].blue + 2 * (int)picture->rgb[i - 1][j].blue + (int)picture->rgb[i - 1][j + 1].blue
				- (int)picture->rgb[i + 1][j - 1].blue - 2 * (int)picture->rgb[i + 1][j].blue - (int)picture->rgb[i + 1][j + 1].blue;
			int red = (int)picture->rgb[i - 1][j - 1].red + 2 * (int)picture->rgb[i - 1][j].red + (int)picture->rgb[i - 1][j + 1].red
				- (int)picture->rgb[i + 1][j - 1].red - 2 * (int)picture->rgb[i + 1][j].red - (int)picture->rgb[i + 1][j + 1].red;
			int green = (int)picture->rgb[i - 1][j - 1].green + 2 * (int)picture->rgb[i - 1][j].green + (int)picture->rgb[i - 1][j + 1].green
				- (int)picture->rgb[i + 1][j - 1].green - 2 * (int)picture->rgb[i + 1][j].green - (int)picture->rgb[i + 1][j + 1].green;
			cur[i][j].blue = (unsigned char)min(255, abs(blue));
			cur[i][j].green = (unsigned char)min(255, abs(green));
			cur[i][j].red = (unsigned char)min(255, abs(red));
		}
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
	free(picture->rgb);
	picture->rgb = cur;
}

void gaussFilter(struct img* picture)
{
	struct rgb** cur = (struct rgb**)malloc(sizeof(struct rgb*) * picture->height);
	for (int i = 0; i < picture->height; i++)
	{
		cur[i] = (struct rgb*)malloc(sizeof(struct rgb) * picture->width);
	}
	for (int i = 1; i < picture->height - 1; i++)
	{
		for (int j = 1; j < picture->width - 1; j++)
		{
			int blue = ((int)picture->rgb[i - 1][j - 1].blue + 2 * (int)picture->rgb[i - 1][j].blue + (int)picture->rgb[i - 1][j + 1].blue
				+ 2 * (int)picture->rgb[i][j - 1].blue + 4 * (int)picture->rgb[i][j].blue + 2 * (int)picture->rgb[i][j + 1].blue
				+ (int)picture->rgb[i + 1][j - 1].blue + 2 * (int)picture->rgb[i + 1][j].blue + (int)picture->rgb[i + 1][j + 1].blue) / 16;
			int green = ((int)picture->rgb[i - 1][j - 1].green + 2 * (int)picture->rgb[i - 1][j].green + (int)picture->rgb[i - 1][j + 1].green
				+ 2 * (int)picture->rgb[i][j - 1].green + 4 * (int)picture->rgb[i][j].green + 2 * (int)picture->rgb[i][j + 1].green
				+ (int)picture->rgb[i + 1][j - 1].green + 2 * (int)picture->rgb[i + 1][j].green + (int)picture->rgb[i + 1][j + 1].green) / 16;
			int red = ((int)picture->rgb[i - 1][j - 1].red + 2 * (int)picture->rgb[i - 1][j].red + (int)picture->rgb[i - 1][j + 1].red
				+ 2 * (int)picture->rgb[i][j - 1].red + 4 * (int)picture->rgb[i][j].red + 2 * (int)picture->rgb[i][j + 1].red
				+ (int)picture->rgb[i + 1][j - 1].red + 2 * (int)picture->rgb[i + 1][j].red + (int)picture->rgb[i + 1][j + 1].red) / 16;
			cur[i][j].blue = (unsigned char)blue;
			cur[i][j].green = (unsigned char)green;
			cur[i][j].red = (unsigned char)red;
		}
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
	free(picture->rgb);
	picture->rgb = cur;
}

void grayFilter(struct img* picture)
{
	for (int i = 0; i < picture->height; i++)
	{
		for (int j = 0; j < picture->width; j++)
		{
			unsigned char tmp = grayScale(picture->rgb[i][j]);
			picture->rgb[i][j].red = tmp;
			picture->rgb[i][j].blue = tmp;
			picture->rgb[i][j].green = tmp;
		}
	}
}

struct img readImage(FILE* fp, int height, int width)
{
	struct img picture;
	picture.height = height;
	picture.width = width;
	picture.rgb = (struct rgb**)malloc(sizeof(struct rgb*) * height);
	int realSize = ((24 * width + 31) / 32) * 4;

	for (int i = height - 1; i >= 0; i--)
	{
		picture.rgb[i] = (struct rbg*)malloc(sizeof(struct rgb) * (realSize / sizeof(struct rgb) + 1));
		if (fread(picture.rgb[i], realSize, 1, fp) != 1)
		{
			printf("Error reading from file!\n");
			printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
			exit(-1);
		}
	}
	return picture;
}

void writeImage(struct bmpHeader header, struct bmpInfoHeader headerInfo, struct img picture, char** argv)
{
	FILE* fpw = fopen(argv[3], "wb");

	if (fpw == NULL)
	{
		printf("Error opening the output file!\n");
		printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
		exit(-1);
	}
	if (fwrite(header.identifier, 2, 1, fpw) != 1 ||
		fwrite(&header.size, 3 * sizeof(int), 1, fpw) != 1 ||
		fwrite(&headerInfo, sizeof(struct bmpInfoHeader), 1, fpw) != 1)
	{
		printf("Error writing to file!\n");
		printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
		exit(-1);
	}

	for (int i = picture.height - 1; i >= 0; i--)
	{
		if (fwrite(picture.rgb[i], ((24 * picture.width + 31) / 32) * 4, 1, fpw) != 1)
		{
			printf("Error writing to file!\n");
			printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
			exit(-1);
		}
	}
	fclose(fpw);
}