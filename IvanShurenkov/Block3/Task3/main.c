#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "filter.h"

#ifdef IS_MSVC
typedef __int8 INT8;
typedef __int16 INT16;
typedef __int32 INT32;
typedef __int64 INT64;
typedef unsigned __int8 UINT8;
typedef unsigned __int16 UINT16;
typedef unsigned __int32 UINT32;
typedef unsigned __int64 UINT64;
#else
typedef __int8_t INT8;
typedef __int16_t INT16;
typedef __int32_t INT32;
typedef __int64_t INT64;
typedef __uint8_t UINT8;
typedef __uint16_t UINT16;
typedef __uint32_t UINT32;
typedef __uint64_t UINT64;
#endif

struct bmp_header
{
	UINT16 bf_type;
	UINT32 bf_size;

	UINT32 bf_reserved; //first must be 0 and second must be 0

	UINT32 bf_offset;

	UINT32 bi_size;
	UINT32 bi_width;
	UINT32 bi_height;

	UINT16 bi_planes;
	UINT16 bi_bit_cnt;

	UINT32 bi_compr;
	UINT32 bi_size_img;

	UINT32 bi_x_pels_per_meter;
	UINT32 bi_y_pels_per_meter;

	UINT32 bi_clr_used;
	UINT32 bi_clr_imp;
};

void read_write_bmp_header(struct bmp_header *header, FILE *file, size_t operation(void *, size_t, size_t, FILE *))
{
	operation(&(*header).bf_type, sizeof(UINT8), sizeof(UINT16), file);
	operation(&(*header).bf_size, sizeof(UINT8), sizeof(UINT32), file);

	operation(&(*header).bf_reserved, sizeof(UINT8), sizeof(UINT32), file);

	operation(&(*header).bf_offset, sizeof(UINT8), sizeof(UINT32), file);

	operation(&(*header).bi_size, sizeof(UINT8), sizeof(UINT32), file);
	operation(&(*header).bi_width, sizeof(UINT8), sizeof(UINT32), file);
	operation(&(*header).bi_height, sizeof(UINT8), sizeof(UINT32), file);

	operation(&(*header).bi_planes, sizeof(UINT8), sizeof(UINT16), file);
	operation(&(*header).bi_bit_cnt, sizeof(UINT8), sizeof(UINT16), file);

	operation(&(*header).bi_compr, sizeof(UINT8), sizeof(UINT32), file);
	operation(&(*header).bi_size_img, sizeof(UINT8), sizeof(UINT32), file);

	operation(&(*header).bi_x_pels_per_meter, sizeof(UINT8), sizeof(UINT32), file);
	operation(&(*header).bi_y_pels_per_meter, sizeof(UINT8), sizeof(UINT32), file);

	operation(&(*header).bi_clr_used, sizeof(UINT8), sizeof(UINT32), file);
	operation(&(*header).bi_clr_imp, sizeof(UINT8), sizeof(UINT32), file);
}

int main(int argc, char *argv[])
{
	printf("This program apply filter on image\n");
	printf("Filters:\n");
	printf("gr 			:translate color to gray\n");
	printf("m3 			:median filter 3x3\n");
	printf("g3 			:Gauss filter 3x3\n");
	printf("sx 			:Sobel filter to axis X\n");
	printf("sy			:Sobel filter to axis Y\n");


	FILE *bmp_input = fopen(argv[1], "rb");
	if (bmp_input == NULL)
	{
		printf("Input file not opened\n");
		return 0;
	}

	//read head image
	struct bmp_header bmp_head;
	read_write_bmp_header(&bmp_head, bmp_input, fread);
	UINT32 width = bmp_head.bi_width;
	UINT32 height = bmp_head.bi_height;
	UINT16 bit_cnt = bmp_head.bi_bit_cnt;
	UINT32 row = ((bit_cnt * width + 31) / 32) * 4;

	UINT8 **image = (UINT8 **) malloc(height * sizeof(UINT8 *));

	for (int i = 0; i < height; i++)
	{
		image[i] = (UINT8 *) malloc(row * sizeof(UINT8));
		fread(image[i], sizeof(UINT8), row * sizeof(UINT8), bmp_input);
	}

	int gx_sobel_matrix[3][3] = {{-1, 0, 1},
								 {-2, 0, 2},
								 {-1, 0, 1}};
	int gy_sobel_matrix[3][3] = {{-1, -2, -1},
								 {0,  0,  0},
								 {1,  2,  1}};

	//apply filter
	if (!strcmp(argv[2], "gr"))
		gray(image, height, row, bit_cnt);
	else if (!strcmp(argv[2], "m3"))
		median_filter(image, height, row, bit_cnt);
	else if (!strcmp(argv[2], "g3"))
		gauss_filter(image, height, row, bit_cnt);
	else if (!strcmp(argv[2], "sx"))
		sobel_filter(image, height, row, bit_cnt, gx_sobel_matrix);
	else if (!strcmp(argv[2], "sy"))
		sobel_filter(image, height, row, bit_cnt, gy_sobel_matrix);
	else
		printf("This filter don't exist or don't support");


	FILE *bmp_output = fopen(argv[3], "wb");
	if (bmp_output == NULL)
	{
		printf("Output file didn't open\n");
		return 0;
	}

	//write image

	read_write_bmp_header(&bmp_head, bmp_output, fwrite);
	for (int i = 0; i < height; i++)
	{
		fwrite(image[i], sizeof(UINT8), row * sizeof(UINT8), bmp_output);
	}

	for (int i = 0; i < height; i++)
	{
		free(image[i]);
	}
	free(image);
	fclose(bmp_input);
	printf("Image completed");
	return 0;
}