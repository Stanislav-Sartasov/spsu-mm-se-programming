#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int create_bw_image(struct bitmap_header header, struct dib_header dibheader, struct image pic, char* name);

struct bitmap_header
{
	char name[2];
	unsigned int size;
	int garbage;
	unsigned int image_offset;
};

struct dib_header
{
	unsigned int header_size;
	unsigned int width;
	unsigned int height;
	unsigned short int colorplanes;
	unsigned short int bits_per_pixel;
	unsigned int compression;
	unsigned int image_size;
	int palette[4];
};

struct argb
{
	unsigned char blue;
	unsigned char green;
	unsigned char red;
	unsigned char alpha;
};

struct image
{
	unsigned height;
	unsigned width;
	struct argb** argb;
};

int compare(const void* p1, const void* p2)
{
	return (*(int*)p1 - *(int*)p2);
}

void grayscale(struct argb* argb)
{
	argb->red = argb->blue = argb->green = argb->alpha = ((0.3 * argb->red + 0.6 * argb->green + 0.1 * argb->blue) / 3);
}

void median_filter(struct argb** pic, struct argb** pixels, int i, int k)
{
	unsigned char reds[9] = { 0 };
	unsigned char blues[9] = { 0 };
	unsigned char greens[9] = { 0 };
	unsigned char alphas[9] = { 0 };
	unsigned count = 0;
	for (int first_counter = i - 1; first_counter <= i + 1; first_counter++)
	{
		for (int second_counter = k - 1; second_counter <= k + 1; second_counter++)
		{
			reds[count] = pic[first_counter][second_counter].red;
			blues[count] = pic[first_counter][second_counter].blue;
			greens[count] = pic[first_counter][second_counter].green;
			alphas[count] = pic[first_counter][second_counter].alpha;
			count++;
		}
	}
	qsort(reds, 9, 1, compare);
	qsort(blues, 9, 1, compare);
	qsort(greens, 9, 1, compare);
	qsort(alphas, 9, 1, compare);
	pixels[i][k].red = reds[4];
	pixels[i][k].blue = blues[4];
	pixels[i][k].green = greens[4];
	pixels[i][k].alpha = alphas[4];
}

void gauss_filter_3x3(struct argb** pic, struct argb** pixels, int i, int k)
{
	int coefficients[] = { 4, 3, 2 };
	int first_counter, second_counter, div, dif, pix_red, pix_blue, pix_green, pix_alpha;
	pix_red = 0;
	pix_blue = 0;
	pix_green = 0;
	pix_alpha = 0;
	div = 24;
	for (first_counter = i - 1; first_counter < i + 2; ++first_counter)
	{
		for (second_counter = k - 1; second_counter < k + 2; ++second_counter)
		{
			dif = coefficients[abs(first_counter - i) + abs(second_counter - k)];
			pix_red += pic[first_counter][second_counter].red * dif;
			pix_blue += pic[first_counter][second_counter].blue * dif;
			pix_green += pic[first_counter][second_counter].green * dif;
			pix_alpha += pic[first_counter][second_counter].alpha * dif;
		}
	}
	pixels[i][k].red = pix_red / div;
	pixels[i][k].blue = pix_blue / div;
	pixels[i][k].green = pix_green / div;
	pixels[i][k].alpha = pix_alpha / div;
}

void sobel_x(struct argb** pic, struct argb** pixels, int i, int k)
{
	int sobel_x_coeff[3][3] = { {-1, 0, 1}, {-2, 0, 2}, {-1, 0, 1} };
	int pix_red, pix_blue, pix_green, pix_alpha, first_counter, second_counter;
	pix_red = 0;
	pix_blue = 0;
	pix_green = 0;
	pix_alpha = 0;
	for (int first_counter = i - 1; first_counter <= i + 1; first_counter++)
	{
		for (int second_counter = k - 1; second_counter <= k + 1; second_counter++)
		{
			pix_red += pic[first_counter][second_counter].red * sobel_x_coeff[first_counter - i + 1][second_counter - k + 1];
			pix_green += pic[first_counter][second_counter].green * sobel_x_coeff[first_counter - i + 1][second_counter - k + 1];
			pix_blue += pic[first_counter][second_counter].blue * sobel_x_coeff[first_counter - i + 1][second_counter - k + 1];
			pix_alpha += pic[first_counter][second_counter].alpha * sobel_x_coeff[first_counter - i + 1][second_counter - k + 1];
		}
	}
	pixels[i][k].red = abs(pix_red) < 255 ? abs(pix_red) : 255;
	pixels[i][k].green = abs(pix_green) < 255 ? abs(pix_green) : 255;
	pixels[i][k].blue = abs(pix_blue) < 255 ? abs(pix_blue) : 255;
	pixels[i][k].alpha = abs(pix_alpha) < 255 ? abs(pix_alpha) : 255;
	grayscale(&pixels[i][k]);
}
void sobel_y(struct argb** pic, struct argb** pixels, int i, int k)
{
	int sobel_y_coeff[3][3] = { {-1, -2, -1}, {0, 0, 0}, {1, 2, 1} };
	int pix_red, pix_blue, pix_green, pix_alpha, first_counter, second_counter;
	pix_red = 0;
	pix_blue = 0;
	pix_green = 0;
	pix_alpha = 0;
	for (int first_counter = i - 1; first_counter <= i + 1; first_counter++)
	{
		for (int second_counter = k - 1; second_counter <= k + 1; second_counter++)
		{
			pix_red += pic[first_counter][second_counter].red * sobel_y_coeff[first_counter - i + 1][second_counter - k + 1];
			pix_green += pic[first_counter][second_counter].green * sobel_y_coeff[first_counter - i + 1][second_counter - k + 1];
			pix_blue += pic[first_counter][second_counter].blue * sobel_y_coeff[first_counter - i + 1][second_counter - k + 1];
			pix_alpha += pic[first_counter][second_counter].alpha * sobel_y_coeff[first_counter - i + 1][second_counter - k + 1];
		}
	}
	pixels[i][k].red = abs(pix_red) < 255 ? abs(pix_red) : 255;
	pixels[i][k].green = abs(pix_green) < 255 ? abs(pix_green) : 255;
	pixels[i][k].blue = abs(pix_blue) < 255 ? abs(pix_blue) : 255;
	pixels[i][k].alpha = abs(pix_alpha) < 255 ? abs(pix_alpha) : 255;
	grayscale(&pixels[i][k]);
}

int apply_filters(struct image input, char* output_name, char* filter, struct dib_header dibheader, struct bitmap_header header)
{
	unsigned i, j;
	struct image redacted_pic;
	redacted_pic.height = input.height;
	redacted_pic.width = input.width;
	struct argb** extended_pic;
	struct argb** extended_pic_copy;
	extended_pic = (struct argb**)malloc((input.height + 2) * sizeof(struct argb*));
	for (i = 0; i < input.height + 2; i++)
	{
		extended_pic[i] = malloc((input.width + 2) * sizeof(struct argb));
	}
	extended_pic_copy = (struct argb**)malloc((input.height + 2) * sizeof(struct argb*));
	for (i = 0; i < input.height + 2; i++)
	{
		extended_pic_copy[i] = malloc((input.width + 2) * sizeof(struct argb));
	}
	redacted_pic.argb = (struct argb**)malloc((input.height) * sizeof(struct argb*));
	for (i = 0; i < input.height; i++)
	{
		redacted_pic.argb[i] = malloc((input.width) * sizeof(struct argb));
	}
	for (i = 0; i < input.height; i++)
	{
		extended_pic[i + 1][0] = input.argb[i][0];
		extended_pic[i + 1][input.width + 1] = input.argb[i][input.width - 1];
		for (j = 0; j < input.width; j++)
		{
			extended_pic[i + 1][j + 1] = input.argb[i][j];
		}
	}
	for (j = 0; j < input.width; j++)
	{
		extended_pic[0][j] = extended_pic[1][j];
		extended_pic[input.height + 1][j] = extended_pic[input.height][j];
	}
	for (i = 0; i < input.height + 2; ++i)
	{
		for (j = 0; j < input.width + 2; ++j)
		{
			extended_pic_copy[i][j] = extended_pic[i][j];
		}
	}
	for (i = 1; i < input.height + 1; ++i)
	{
		for (j = 1; j < input.width + 1; ++j)
		{
			if (strcmp(filter, "median") == 0)
			{
				median_filter(extended_pic_copy, extended_pic, i, j);
			}
			else if (strcmp(filter, "gauss") == 0)
			{
				gauss_filter_3x3(extended_pic_copy, extended_pic, i, j);
			}
			else if (strcmp(filter, "sobelx") == 0)
			{
				sobel_x(extended_pic_copy, extended_pic, i, j);
			}
			else if (strcmp(filter, "sobely") == 0)
			{
				sobel_y(extended_pic_copy, extended_pic, i, j);
			}
			else if (strcmp(filter, "grayscale") == 0)
			{
				grayscale(&extended_pic[i][j]);
			}
			else
			{
				printf("Incorrect name of filter\n");
				return 0;
			}
		}
	}
	for (i = 1; i < input.height + 1; ++i)
	{
		for (j = 1; j < input.width + 1; ++j)
		{
			redacted_pic.argb[i - 1][j - 1] = extended_pic[i][j];
		}
	}
	create_bw_image(header, dibheader, redacted_pic, output_name);
	return 1;
}

void free_image(struct image* pic)
{
	for (unsigned i = 0; i < pic->height - 1; ++i)
	{
		free(pic->argb[i]);
	}
	free(pic->argb);
}

struct image read_image(FILE* fp, int height, int width, short int bits_per_pixel)
{
	struct image picture;
	picture.argb = (struct argb**)malloc(height * sizeof(struct argb*));
	picture.height = height;
	picture.width = width;
	int garbage;
	if (bits_per_pixel == 32)
	{
		for (int i = height - 1; i >= 0; i--)
		{
			picture.argb[i] = (struct argb*)malloc(width * sizeof(struct argb));
			fread(picture.argb[i], sizeof(struct argb), width, fp);
		}
	}
	else
	{
		for (int i = height - 1; i >= 0; i--)
		{
			picture.argb[i] = (struct argb*)malloc(width * sizeof(struct argb));
			for (int j = 0; j < width; j++)
			{
				fread(&picture.argb[i][j], 3, 1, fp);
				picture.argb[i][j].alpha = 0;
			}
			fread(&garbage, ((24 * width + 31) / 32) * 4 - width * 3, 1, fp);
		}
	}
	return picture;
}

int create_bw_image(struct bitmap_header header, struct dib_header dibheader, struct image pic, char* name)
{
	FILE* fp_write = fopen(name, "wb");
	int i;
	if (fp_write == NULL)
	{
		return 1;
	}
	fwrite(header.name, 2, 1, fp_write);
	fwrite(&header.size, 3 * sizeof(int), 1, fp_write);
	fwrite(&dibheader, sizeof(struct dib_header), 1, fp_write);
	if (dibheader.bits_per_pixel == 32)
	{
		for (i = (int)pic.height - 1; i >= 0; i--)
		{
			fwrite(pic.argb[i], sizeof(struct argb), pic.width, fp_write);
		}
	}
	else
	{
		for (i = (int)pic.height - 1; i >= 0; i--)
		{
			struct argb garbage;
			garbage.blue = garbage.green = garbage.red = 0;
			for (unsigned j = 0; j < pic.width; j++)
			{
				fwrite(&pic.argb[i][j], 3, 1, fp_write);
			}
			fwrite(&garbage, ((24 * pic.width + 31) / 32) * 4 - pic.width * 3, 1, fp_write);
		}
	}
	free_image(&pic);
	fclose(fp_write);
	return 0;
}