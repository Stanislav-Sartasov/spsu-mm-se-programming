#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <string.h>
#include "filters.h"

// header
#pragma pack(push)
#pragma pack(2)
typedef struct
{
	unsigned short bf_type;					// format
	int bf_size;							// size

	unsigned short bf_reserved1;			// system
	unsigned short bf_reserved2;

	int bf_off_bits;						// offset

	int bi_size;							// system

	int bi_width;							// widht in pixels
	int bi_height;							// heignt in pixels

	unsigned short bi_planes;				// system
	unsigned short bi_bit_count;			// system
	int bi_compression;						// system
	int bi_size_image;						// 0

	int bi_x_pels_per_metr;					// amerecan joke
	int bi_y_pels_per_metr;					// second
	int bi_clt_used;						// size table of colors
	int bi_clr_important;					// number of important colors

	// 54 bites (56)
} bmp_header;
#pragma pack(pop)

int main(int argc, char*argv[])
{
	printf("Applies filters to the .BMP image\n");
	printf("MIDDLE - averaging median filter\n");
	printf("SOBELX and SOBELY - Sobel filters on X and Y\n");
	printf("SOBEL - normal Sobel filter\n");
	printf("GAUSS - Gauss filter 5x5\n");
	printf("GREY - shades of grey\n");

	if (argc != 4)
	{
		printf("More/less parameters passed");
		return 0;
	}

	if ((strcmp(argv[2], "MIDDLE") != 0) && (strcmp(argv[2], "SOBELX") != 0) && (strcmp(argv[2], "SOBELY") != 0) && (strcmp(argv[2], "SOBEL") != 0) && (strcmp(argv[2], "GAUSS") != 0) && (strcmp(argv[2], "GREY") != 0))
	{
		printf("\nTry another filter from list of filters\n");
		return 0;
	}

	bmp_header bh;
	FILE* f;
	
	// reading
	if (!(f = fopen(argv[1], "rb")))
	{
		printf("\nUnable to open file for reading\n");
		return 0;
	}

	fread(&bh, sizeof(char), sizeof(bmp_header), f);
	int x = bh.bi_width;
	int y = bh.bi_height;

	int c = bh.bi_bit_count == 32;

	// * 4 for 32bit  and  * 3 & mod 4 == 0 for 24bit
	int row = ((3 + c) * x + !(c) * 3) & (-4);
	// 4 for 32bit and 3 for 24bit
	int t = 3 + c;

	unsigned char** image = (unsigned char**)malloc(y * sizeof(char*));
	for (int i = 0; i < y; i++)
	{
		image[i] = (char*)malloc((row + 2) * sizeof(char));
		fread(image[i], sizeof(char), row * sizeof(char), f);
	}


	if (strcmp(argv[2], "MIDDLE") == 0)
	{
		med_middle(image, row, y, t);
	}
	else if (strcmp(argv[2], "SOBELX") == 0)
	{
		multi_sobel(image, x, y, row, 'X', t);
	}
	else if (strcmp(argv[2], "SOBELY") == 0)
	{
		multi_sobel(image, x, y, row, 'Y', t);
	}
	else if (strcmp(argv[2], "SOBEL") == 0)
	{
		multi_sobel(image, x, y, row, '0', t);
	}
	else if (strcmp(argv[2], "GREY") == 0)
	{
		grey(image, row, y, t);
	}
	else
	{
		five_x_five_gauss(image, row, y, t);
	}


	// writing
	if (!(f = fopen(argv[3], "wb")))
	{
		printf("\nUnable to open file for writing\n");
		return 0;
	}
	fwrite(&bh, sizeof(char), sizeof(bmp_header), f);
	for (int i = 0; i < y; i++)
	{
		fwrite(image[i], sizeof(char), row * sizeof(char), f);
	}

	printf("\nFiltered\n");

	// free
	for (int i = 0; i < y; i++)
	{
		free(image[i]);
	}
	free(image);
	fclose(f);

	return 0;
}