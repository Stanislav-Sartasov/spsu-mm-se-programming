#include "filters.h"

void free_img(struct image* img);
void filter(struct image* img, char* str);
void gray_scale(struct image* img);
void median(struct image* img);
int compare(const void* x1, const void* x2);
void gauss_five(struct image* img);
void sobel(struct image* img, char* str);
void single_sobel(struct image* img, int matrix[]);

int read_bmp(FILE* file, struct bmp_file_header* file_head, struct bmp_info_header* file_info, struct image* img, char* palette, int* palette_size, int* padding)
{
	int buffer = 0;
	fread(file_head, sizeof(*file_head), 1, file);
	fread(file_info, sizeof(*file_info), 1, file);
	if (file_head->bf_type[0] != 'B' || file_head->bf_type[1] != 'M')
	{
		printf("This is not a BMP type\n");
		return 1;
	}
	if (file_info->bi_bit_count != 24 && file_info->bi_bit_count != 32)
	{
		printf("It's not 24 or 32 bits per pixel.\n");
		return 1;
	}
	if (file_info->bi_compression != 0)
	{
		printf("The file is compressed\n");
		return 1;
	}

	*palette_size = file_head->bf_offbits - sizeof(struct bmp_file_header) - sizeof(struct bmp_info_header);
	if (*palette_size)
	{
		palette = (char*)malloc(*palette_size);
		fread(palette, *palette_size, 1, file);
	}
	*padding = (4 - (file_info->bi_width * file_info->bi_bit_count / 8) % 4) % 4;
	img->height = file_info->bi_height;
	img->width = file_info->bi_width;
	img->bits = (struct pixel**)(malloc(sizeof(struct pixel*) * img->height));
	for (unsigned int i = 0; i < file_info->bi_height; i++)
	{
		img->bits[i] = (struct pixel*)malloc(sizeof(struct pixel) * file_info->bi_width);
		for (unsigned int j = 0; j < file_info->bi_width; j++)
		{
			fread(&img->bits[i][j], 1, sizeof(struct pixel), file);
			if (file_info->bi_bit_count == 32)
			{
				fread(&buffer, 1, sizeof(unsigned char), file);
			}
		}
		if (*padding)
		{
			fread(&buffer, *padding, sizeof(unsigned char), file);
		}
	}
	return 0;
}

int write_bmp(FILE* file, struct bmp_file_header file_head, struct bmp_info_header file_info, struct image* img, char* palette, int palette_size, int padding)
{
	fwrite(&file_head, sizeof(bmp_file_header), 1, file);
	fwrite(&file_info, sizeof(bmp_info_header), 1, file);
	if (palette_size != 0)
	{
		fwrite(palette, palette_size, 1, file);
		free(palette);
	}

	for (unsigned int i = 0; i < file_info.bi_height; ++i)
	{
		for (unsigned int j = 0; j < file_info.bi_width; j++)
		{
			fwrite(&img->bits[i][j], 1, sizeof(struct pixel), file);
			if (file_info.bi_bit_count == 32)
			{
				fwrite('0', 1, sizeof(unsigned char), file);
			}
		}
		if (padding)
		{
			fwrite('0', padding, sizeof(unsigned char), file);
		}
	}
	free_img(img);
	return 0;
}

void gray_scale(struct image* img)
{
	unsigned char gray;
	for (int i = 0; i < img->height; i++)
	{
		for (int j = 0; j < img->width; j++)
		{
			gray = (unsigned char)(img->bits[i][j].rgb[0] * 0.114 + img->bits[i][j].rgb[1] * 0.587 + img->bits[i][j].rgb[2] * 0.229);
			img->bits[i][j].rgb[0] = img->bits[i][j].rgb[1] = img->bits[i][j].rgb[2] = gray;
		}
	}
}

void median(struct image* img)
{
	int matrix[9];
	int point = 0;
	int copy_pi, copy_pj;
	for (int i = 0; i + 2 < img->height; i += 3)
	{
		for (int j = 0; j + 2 < img->width; j += 3)
		{
			for (int p = 0; p < 3; p++)
			{
				point = 0;
				for (int pi = i - 1; pi < i + 2; pi++)
				{
					for (int pj = j - 1; pj < j + 2; pj++)
					{
						if (pj < 0 || pi < 0 || pj == img->width || pi == img->height)
						{
							copy_pi = pi;
							copy_pj = pj;
							if (copy_pi < 0)
							{
								copy_pi++;
							}
							else if (copy_pi == img->height)
							{
								copy_pi--;
							}
							if (copy_pj < 0)
							{
								copy_pj++;
							}
							else if (copy_pj == img->width)
							{
								copy_pj--;
							}
							matrix[point++] = img->bits[copy_pi][copy_pj].rgb[p];
						}
						else
						{
							matrix[point++] = img->bits[pi][pj].rgb[p];
						}
					}
				}
				qsort(matrix, 9, sizeof(int), compare);
				for (int pi = i - 1; pi < i + 2; pi++)
				{
					for (int pj = j - 1; pj < j + 2; pj++)
					{
						if (pi < 0 || pj < 0)
						{
							continue;
						}
						img->bits[pi][pj].rgb[p] = matrix[4];
					}
				}
			}
		}
	}
}

void gauss_five(struct image* img)
{
	double matrix[25] = \
	{0.000789, 0.006581, 0.013347, 0.006581, 0.000789, \
		0.006581, 0.054901, 0.111345, 0.054901, 0.006581, \
		0.013347, 0.111345, 0.225821, 0.111345, 0.013347, \
		0.006581, 0.054901, 0.111345, 0.054901, 0.006581, \
		0.000789, 0.006581, 0.013347, 0.006581, 0.000789};
	int point = 0;
	int copy_pi, copy_pj;
	long long gaussian = 0;
	for (int i = 0; i < img->height; i++)
	{
		for (int j = 0; j < img->width; j++)
		{
			for (int p = 0; p < 3; p++)
			{
				point = 0;
				gaussian = 0;
				for (int pi = i - 2; pi < i + 3; pi++)
				{
					for (int pj = j - 2; pj < j + 3; pj++)
					{
						if (pj < 0 || pi < 0 || pj >= img->width || pi >= img->height)
						{
							copy_pi = pi;
							copy_pj = pj;
							if (copy_pi < 0)
							{
								copy_pi = 0;
							}
							else if (copy_pi >= img->height)
							{
								copy_pi = img->height - 1;
							}
							if (copy_pj < 0)
							{
								copy_pj = 0;
							}
							else if (copy_pj >= img->width)
							{
								copy_pj = img->width - 1;
							}
							gaussian += matrix[point++] * img->bits[copy_pi][copy_pj].rgb[p];
						}
						else
						{
							gaussian += matrix[point++] * img->bits[pi][pj].rgb[p];
						}
					}
				}
				img->bits[i][j].rgb[p] = (unsigned char)gaussian;
			}
		}
	}
}
void single_sobel(struct image* img, int matrix[])
{
	gray_scale(img);
	int point = 0;
	long long mag = 0;
	struct pixel** result_bits = (struct pixel**)malloc(sizeof(struct pixel*) * img->height);
	for (unsigned int i = 0; i < img->height; i++)
	{
		result_bits[i] = (struct pixel*)malloc(sizeof(struct pixel) * img->width);
	}

	for (int i = 1; i < img->height - 1; i++)
	{
		for (int j = 1; j < img->width - 1; j++)
		{
			for (int p = 0; p < 3; p++)
			{
				point = 0;
				mag = 0;
				for (int pi = i - 1; pi < i + 2; pi++)
				{
					for (int pj = j - 1; pj < j + 2; pj++)
					{
						mag += matrix[point++] * img->bits[pi][pj].rgb[p];
					}
				}
				mag = abs(mag) > 255 ? 255 : abs(mag);
				result_bits[i][j].rgb[p] = (unsigned char)mag;
			}
		}
	}
	for (int i = 1; i < img->height - 1; i++)
	{
		for (int j = 1; j < img->width - 1; j++)
		{
			for (int p = 0; p < 3; p++)
			{
				img->bits[i][j].rgb[p] = result_bits[i][j].rgb[p];
			}
		}
	}
	for (unsigned int i = 0; i < img->height; i++)
	{
		free(result_bits[i]);
	}
	free(result_bits);
}

void sobel(struct image* img, char* str)
{
	int matrix_x[] = \
	{-1, 0, 1, \
		- 2, 0, 2, \
		- 1, 0, 1};
	int matrix_y[] = \
	{ -1, -2, -1, \
		0, 0, 0, \
		1, 2, 1};
	if (strcmp(str, "sobelX") == 0)
	{
		single_sobel(img, matrix_x);
		return;
	}
	else if (strcmp(str, "sobelY") == 0)
	{
		single_sobel(img, matrix_y);
		return;
	}
	struct image new_img;
	new_img.height = img->height;
	new_img.width = img->width;
	new_img.bits = (struct pixel**)(malloc(sizeof(struct pixel*) * new_img.height));
	struct pixel** result_bits = (struct pixel**)malloc(sizeof(struct pixel*) * img->height);
	for (unsigned int i = 0; i < img->height; i++)
	{
		result_bits[i] = (struct pixel*)malloc(sizeof(struct pixel) * img->width);
	}
	for (unsigned int i = 0; i < new_img.height; i++)
	{
		new_img.bits[i] = (struct pixel*)malloc(sizeof(struct pixel) * new_img.width);
	}
	for (unsigned int i = 0; i < new_img.height; i++)
	{
		for (unsigned int j = 0; j < new_img.width; j++)
		{
			for (unsigned int p = 0; p < 3; p++)
			{
				new_img.bits[i][j].rgb[p] = img->bits[i][j].rgb[p];
			}
		}
	}
	single_sobel(img, matrix_x);
	single_sobel(&new_img, matrix_y);
	long long mag = 0;
	for (unsigned int i = 0; i < new_img.height; i++)
	{
		for (unsigned int j = 0; j < new_img.width; j++)
		{
			for (unsigned int p = 0; p < 3; p++)
			{
				mag = ceil(sqrt(img->bits[i][j].rgb[p] * img->bits[i][j].rgb[p] + new_img.bits[i][j].rgb[p] * new_img.bits[i][j].rgb[p]));
				mag = abs(mag) > 255 ? 255 : abs(mag);
				result_bits[i][j].rgb[p] = (unsigned char)mag;
			}
		}
	}
	for (int i = 1; i < img->height - 1; i++)
	{
		for (int j = 1; j < img->width - 1; j++)
		{
			for (int p = 0; p < 3; p++)
			{
				img->bits[i][j].rgb[p] = result_bits[i][j].rgb[p];
			}
		}
	}
	free_img(&new_img);
	for (unsigned int i = 0; i < img->height; i++)
	{
		free(result_bits[i]);
	}
	free(result_bits);
}

void filter(struct image* img, char* str)
{
	if (strcmp(str, "gray") == 0)
	{
		gray_scale(img);
	}
	else if (strcmp(str, "median") == 0)
	{
		median(img);
	}
	else if (strcmp(str, "gauss") == 0)
	{
		gauss_five(img);
	}
	else if (strcmp(str, "sobelX") == 0 || strcmp(str, "sobelY") == 0 || strcmp(str, "sobel") == 0)
	{
		sobel(img, str);
	}
	else
	{
		printf("There is no such filter as you have entered");
		return;
	}
}

void free_img(struct image* img)
{
	for (int i = img->height - 1; i >= 0; i--)
	{
		free(img->bits[i]);
	}
	free(img->bits);
}

int compare(const void* x1, const void* x2)
{
	return (*(int*)x1 - *(int*)x2);
}