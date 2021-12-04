#define _CRT_SECURE_NO_WARNINGS

#include "bmpEditor.h"

int cmp(int* x, int* y)
{
	return *x - *y;
}

unsigned char grayScale32(struct argb argb)
{
	return (0.1 * argb.blue + 0.6 * argb.green + 0.3 * argb.red);
}

void medianFilter32(struct img32* picture)
{
	struct argb** cur = (struct argb**)malloc(sizeof(struct argb*) * picture->height);
	for (int i = 0; i < picture->height; i++)
	{
		cur[i] = (struct argb*)malloc(sizeof(struct argb) * picture->width);
	}
	for (int i = 1; i < picture->height - 1; i++)
	{
		for (int j = 1; j < picture->width - 1; j++)
		{
			unsigned char reds[8] = { picture->argb[i - 1][j - 1].red, picture->argb[i - 1][j].red,
				picture->argb[i][j - 1].red, picture->argb[i][j + 1].red, picture->argb[i + 1][j - 1].red,
				picture->argb[i + 1][j].red, picture->argb[i + 1][j + 1].red, picture->argb[i - 1][j + 1].red };
			unsigned char blues[8] = { picture->argb[i - 1][j - 1].blue, picture->argb[i - 1][j].blue,
				picture->argb[i][j - 1].blue, picture->argb[i][j + 1].blue, picture->argb[i + 1][j - 1].blue,
				picture->argb[i + 1][j].blue, picture->argb[i + 1][j + 1].blue, picture->argb[i - 1][j + 1].blue };
			unsigned char greens[8] = { picture->argb[i - 1][j - 1].green, picture->argb[i - 1][j].green,
				picture->argb[i][j - 1].green, picture->argb[i][j + 1].green,
				picture->argb[i + 1][j - 1].green, picture->argb[i + 1][j].green,
				picture->argb[i + 1][j + 1].green, picture->argb[i - 1][j + 1].green };
			unsigned char alphas[8] = { picture->argb[i - 1][j - 1].alpha, picture->argb[i - 1][j].alpha,
				picture->argb[i][j - 1].alpha, picture->argb[i][j + 1].alpha,
				picture->argb[i + 1][j - 1].alpha, picture->argb[i + 1][j].alpha,
				picture->argb[i + 1][j + 1].alpha, picture->argb[i - 1][j + 1].alpha };
			qsort(reds, 8, 1, cmp);
			qsort(greens, 8, 1, cmp);
			qsort(alphas, 8, 1, cmp);
			qsort(blues, 8, 1, cmp);
			cur[i][j].blue = (blues[4] + blues[3]) / 2;
			cur[i][j].red = (reds[4] + reds[3]) / 2;
			cur[i][j].green = (greens[4] + greens[3]) / 2;
			cur[i][j].alpha = (alphas[4] + alphas[3]) / 2;
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
	free(picture->argb);
	picture->argb = cur;
}

void sobelXFilter32(struct img32* picture)
{
	struct argb** cur = (struct argb**)malloc(sizeof(struct argb*) * picture->height);
	for (int i = 0; i < picture->height; i++)
	{
		cur[i] = (struct argb*)malloc(sizeof(struct argb) * picture->width);
	}
	for (int i = 1; i < picture->height - 1; i++)
	{
		for (int j = 1; j < picture->width - 1; j++)
		{
			int blue = (int)picture->argb[i - 1][j - 1].blue + 2 * (int)picture->argb[i][j - 1].blue + (int)picture->argb[i + 1][j - 1].blue
				- (int)picture->argb[i - 1][j + 1].blue - 2 * (int)picture->argb[i][j + 1].blue - (int)picture->argb[i + 1][j + 1].blue;
			int red = (int)picture->argb[i - 1][j - 1].red + 2 * (int)picture->argb[i][j - 1].red + (int)picture->argb[i + 1][j - 1].red
				- (int)picture->argb[i - 1][j + 1].red - 2 * (int)picture->argb[i][j + 1].red - (int)picture->argb[i + 1][j + 1].red;
			int green = (int)picture->argb[i - 1][j - 1].green + 2 * (int)picture->argb[i][j - 1].green + (int)picture->argb[i + 1][j - 1].green
				- (int)picture->argb[i - 1][j + 1].green - 2 * (int)picture->argb[i][j + 1].green - (int)picture->argb[i + 1][j + 1].green;
			int alpha = (int)picture->argb[i - 1][j - 1].alpha + 2 * (int)picture->argb[i][j - 1].alpha + (int)picture->argb[i + 1][j - 1].alpha
				- (int)picture->argb[i - 1][j + 1].alpha - 2 * (int)picture->argb[i][j + 1].alpha - (int)picture->argb[i + 1][j + 1].alpha;
			cur[i][j].blue = (unsigned char)min(255, abs(blue));
			cur[i][j].green = (unsigned char)min(255, abs(green));
			cur[i][j].red = (unsigned char)min(255, abs(red));
			cur[i][j].alpha = (unsigned char)min(255, abs(alpha));
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
	free(picture->argb);
	picture->argb = cur;
}

void sobelYFilter32(struct img32* picture)
{
	struct argb** cur = (struct argb**)malloc(sizeof(struct argb*) * picture->height);
	for (int i = 0; i < picture->height; i++)
	{
		cur[i] = (struct argb*)malloc(sizeof(struct argb) * picture->width);
	}
	for (int i = 1; i < picture->height - 1; i++)
	{
		for (int j = 1; j < picture->width - 1; j++)
		{
			int blue = (int)picture->argb[i - 1][j - 1].blue + 2 * (int)picture->argb[i - 1][j].blue + (int)picture->argb[i - 1][j + 1].blue
				- (int)picture->argb[i + 1][j - 1].blue - 2 * (int)picture->argb[i + 1][j].blue - (int)picture->argb[i + 1][j + 1].blue;
			int red = (int)picture->argb[i - 1][j - 1].red + 2 * (int)picture->argb[i - 1][j].red + (int)picture->argb[i - 1][j + 1].red
				- (int)picture->argb[i + 1][j - 1].red - 2 * (int)picture->argb[i + 1][j].red - (int)picture->argb[i + 1][j + 1].red;
			int green = (int)picture->argb[i - 1][j - 1].green + 2 * (int)picture->argb[i - 1][j].green + (int)picture->argb[i - 1][j + 1].green
				- (int)picture->argb[i + 1][j - 1].green - 2 * (int)picture->argb[i + 1][j].green - (int)picture->argb[i + 1][j + 1].green;
			int alpha = (int)picture->argb[i - 1][j - 1].alpha + 2 * (int)picture->argb[i - 1][j].alpha + (int)picture->argb[i - 1][j + 1].alpha
				- (int)picture->argb[i + 1][j - 1].alpha - 2 * (int)picture->argb[i + 1][j].alpha - (int)picture->argb[i + 1][j + 1].alpha;
			cur[i][j].blue = (unsigned char)min(255, abs(blue));
			cur[i][j].green = (unsigned char)min(255, abs(green));
			cur[i][j].red = (unsigned char)min(255, abs(red));
			cur[i][j].alpha = (unsigned char)min(255, abs(alpha));
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
	free(picture->argb);
	picture->argb = cur;
}

void gaussFilter32(struct img32* picture)
{
	struct argb** cur = (struct argb**)malloc(sizeof(struct argb*) * picture->height);
	for (int i = 0; i < picture->height; i++)
	{
		cur[i] = (struct argb*)malloc(sizeof(struct argb) * picture->width);
	}
	for (int i = 1; i < picture->height - 1; i++)
	{
		for (int j = 1; j < picture->width - 1; j++)
		{
			int blue = ((int)picture->argb[i - 1][j - 1].blue + 2 * (int)picture->argb[i - 1][j].blue + (int)picture->argb[i - 1][j + 1].blue
				+ 2 * (int)picture->argb[i][j - 1].blue + 4 * (int)picture->argb[i][j].blue + 2 * (int)picture->argb[i][j + 1].blue
				+ (int)picture->argb[i + 1][j - 1].blue + 2 * (int)picture->argb[i + 1][j].blue + (int)picture->argb[i + 1][j + 1].blue) / 16;
			int green = ((int)picture->argb[i - 1][j - 1].green + 2 * (int)picture->argb[i - 1][j].green + (int)picture->argb[i - 1][j + 1].green
				+ 2 * (int)picture->argb[i][j - 1].green + 4 * (int)picture->argb[i][j].green + 2 * (int)picture->argb[i][j + 1].green
				+ (int)picture->argb[i + 1][j - 1].green + 2 * (int)picture->argb[i + 1][j].green + (int)picture->argb[i + 1][j + 1].green) / 16;
			int red = ((int)picture->argb[i - 1][j - 1].red + 2 * (int)picture->argb[i - 1][j].red + (int)picture->argb[i - 1][j + 1].red
				+ 2 * (int)picture->argb[i][j - 1].red + 4 * (int)picture->argb[i][j].red + 2 * (int)picture->argb[i][j + 1].red
				+ (int)picture->argb[i + 1][j - 1].red + 2 * (int)picture->argb[i + 1][j].red + (int)picture->argb[i + 1][j + 1].red) / 16;
			int alpha = ((int)picture->argb[i - 1][j - 1].alpha + 2 * (int)picture->argb[i - 1][j].alpha + (int)picture->argb[i - 1][j + 1].alpha
				+ 2 * (int)picture->argb[i][j - 1].alpha + 4 * (int)picture->argb[i][j].alpha + 2 * (int)picture->argb[i][j + 1].alpha
				+ (int)picture->argb[i + 1][j - 1].alpha + 2 * (int)picture->argb[i + 1][j].alpha + (int)picture->argb[i + 1][j + 1].alpha) / 16;
			cur[i][j].blue = (unsigned char)blue;
			cur[i][j].green = (unsigned char)green;
			cur[i][j].red = (unsigned char)red;
			cur[i][j].alpha = (unsigned char)alpha;
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
	free(picture->argb);
	picture->argb = cur;
}

void grayFilter32(struct img32* picture)
{
	for (int i = 0; i < picture->height; i++)
	{
		for (int j = 0; j < picture->width; j++)
		{
			unsigned char tmp = grayScale32(picture->argb[i][j]);
			picture->argb[i][j].red = tmp;
			picture->argb[i][j].blue = tmp;
			picture->argb[i][j].green = tmp;
			picture->argb[i][j].alpha = tmp;
		}
	}
}

struct img32 readImage32(FILE* fp, int height, int width)
{
	struct img32 picture;
	picture.height = height;
	picture.width = width;
	picture.argb = (struct argb**)malloc(sizeof(struct argb*) * height);

	for (int i = height - 1; i >= 0; i--)
	{
		picture.argb[i] = (struct argb*)malloc(sizeof(struct argb) * width);
		fread(picture.argb[i], sizeof(struct argb), width, fp);
	}
	return picture;
}

void writeImage32(struct bmpHeader header, struct bmpInfoHeader headerInfo, struct img32 picture, char** argv)
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
		if (fwrite(picture.argb[i], 4, picture.width, fpw) != picture.width)
		{
			printf("Error writing to file!\n");
			printf("Re-enter as <program name> <input file> <filter type> <output file>\n");
			exit(-1);
		}
	}
	fclose(fpw);
}