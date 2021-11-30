#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h>

#pragma pack(push, 1)

struct bmp_header
{
	unsigned char signature[2];
	unsigned int file_size;
	unsigned int reserved;
	unsigned int data_offset;

	unsigned int size;
	unsigned int width;
	unsigned int height;
	unsigned short planes;
	unsigned short bits_per_pixel;
	unsigned int compression;
	unsigned int image_size;
	unsigned int x_pixels_per_m;
	unsigned int y_pixels_per_m;
	unsigned int colors_used;
	unsigned int colors_important;
};

#pragma pack(pop)

struct pixel_info
{
	unsigned char red;
	unsigned char green;
	unsigned char blue;
};

void shades_of_grey(struct bmp_header bh, struct pixel_info** pixels, FILE* file_in, FILE* file_out)
{
	fwrite(&bh, sizeof(bh), 1, file_out);

	for (int i = 0; i < bh.height; i++)
	{
		for (int j = 0; j < bh.width; j++)
		{
			int color = pixels[i][j].red * 0.2989 + pixels[i][j].green * 0.5870 + pixels[i][j].blue * 0.1140;
			putc(color, file_out);
			putc(color, file_out);
			putc(color, file_out);

			if (bh.bits_per_pixel == 32)
				putc(0, file_out);
		}
	}
}

void apply_mask(struct bmp_header bh, struct pixel_info** pixels, FILE* file_in, FILE* file_out, double* mask, _Bool is_sobel)
{
	fwrite(&bh, sizeof(bh), 1, file_out);

	for (int i = 0; i < bh.height; i++)
	{
		for (int j = 0; j < bh.width; j++)
		{
			if (i == 0 || j == 0 || i == bh.height - 1 || j == bh.width - 1)
			{
				putc(0, file_out);
				putc(0, file_out);
				putc(0, file_out);

				if (bh.bits_per_pixel == 32)
					putc(0, file_out);

				continue;
			}

			double sum_red = 0;
			double sum_green = 0;
			double sum_blue = 0;

			for (int mask_i = 0; mask_i < 3; mask_i++)
			{
				for (int mask_j = 0; mask_j < 3; mask_j++)
				{
					sum_red += pixels[i - 1 + mask_i][j - 1 + mask_j].red * mask[3 * mask_i + mask_j];
					sum_green += pixels[i - 1 + mask_i][j - 1 + mask_j].green * mask[3 * mask_i + mask_j];
					sum_blue += pixels[i - 1 + mask_i][j - 1 + mask_j].blue * mask[3 * mask_i + mask_j];
				}
			}

			if (is_sobel)
			{
				sum_blue = sum_blue > 255 ? 255 : ((sum_blue < 0) ? 0 : sum_blue);
				sum_green = sum_green > 255 ? 255 : ((sum_green < 0) ? 0 : sum_green);
				sum_red = sum_red > 255 ? 255 : ((sum_red < 0) ? 0 : sum_red);
			}

			putc((unsigned char)sum_blue, file_out);
			putc((unsigned char)sum_green, file_out);
			putc((unsigned char)sum_red, file_out);

			if (bh.bits_per_pixel == 32)
				putc(0, file_out);
		}
	}
}

void averaging(struct bmp_header bh, struct pixel_info** pixels, FILE* file_in, FILE* file_out)
{
	double mask[9] = { 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9, 1.0 / 9 };
	apply_mask(bh, pixels, file_in, file_out, mask, 0);
}

void gaussian(struct bmp_header bh, struct pixel_info** pixels, FILE* file_in, FILE* file_out)
{
	double mask[9] = { 1.0 / 16, 1.0 / 8, 1.0 / 16, 1.0 / 8, 1.0 / 4, 1.0 / 8, 1.0 / 16, 1.0 / 8, 1.0 / 16 };
	apply_mask(bh, pixels, file_in, file_out, mask, 0);
}

void sobel_x(struct bmp_header bh, struct pixel_info** pixels, FILE* file_in, FILE* file_out)
{
	double mask[9] = { -1.0, -2.0, -1.0, 0.0, 0.0, 0.0, 1.0, 2.0, 1.0 };
	apply_mask(bh, pixels, file_in, file_out, mask, 1);
}

void sobel_y(struct bmp_header bh, struct pixel_info** pixels, FILE* file_in, FILE* file_out)
{
	double mask[9] = { -1.0, 0.0, 1.0, -2.0, 0.0, 2.0, -1.0, 0.0, 1.0 };
	apply_mask(bh, pixels, file_in, file_out, mask, 1);
}

int main(int argc, char* argv[])
{
	FILE* file_out;
	struct bmp_header bh;

	printf("The program applies filters to the bmp image.\n\n");
	printf("Averaging filter 3x3 - \"averaging\"\n");
	printf("3x3 Gauss averaging filter - \"gaussian\"\n");
	printf("Sobel filter by X - \"sobel_x\"\n");
	printf("Sobel filter by Y - \"sobel_y\"\n");
	printf("From color to grayscale - \"grayscale\"\n\n");

	if (argc != 4)
	{
		printf("You must enter exactly 4 arguments!\n");
		return -1;
	}
	else
	{
		FILE* file_in = fopen(argv[1], "rb");

		if (file_in == NULL)
		{
			printf("Invalid file input!\n");
			return -1;
		}
		else if (strcmp(argv[2], "averaging") && strcmp(argv[2], "gaussian") && strcmp(argv[2], "sobel_x") &&
			strcmp(argv[2], "sobel_y") && strcmp(argv[2], "grayscale"))
		{
			printf("Incorrect filter name entered!\n");
			return -1;
		}

		FILE* file_out = fopen(argv[3], "wb");

		fread(&bh, sizeof(bh), 1, file_in); // reading the bmp file header

		if (!(bh.bits_per_pixel == 24 || bh.bits_per_pixel == 32))
		{
			printf("The bmp file must be 24 or 32 bit!\n");
			return -1;
		}

		struct pixel_info** pixels = malloc(sizeof(struct pixel_info*) * bh.height); // creating an array on image pixels
		for (int i = 0; i < bh.height; i++)
		{
			pixels[i] = malloc(sizeof(struct pixel_info) * bh.width);
		}
		for (int i = 0; i < bh.height; i++)
		{
			for (int j = 0; j < bh.width; j++)
			{
				pixels[i][j].blue = getc(file_in);
				pixels[i][j].green = getc(file_in);
				pixels[i][j].red = getc(file_in);

				if (bh.bits_per_pixel == 32)
					getc(file_in);
			}
		}

		fclose(file_in);

		if (!strcmp(argv[2], "averaging"))
			averaging(bh, pixels, file_in, file_out);
		else if (!strcmp(argv[2], "gaussian"))
			gaussian(bh, pixels, file_in, file_out);
		else if (!strcmp(argv[2], "sobel_x"))
			sobel_x(bh, pixels, file_in, file_out);
		else if (!strcmp(argv[2], "sobel_y"))
			sobel_y(bh, pixels, file_in, file_out);
		else
			shades_of_grey(bh, pixels, file_in, file_out);

		printf("Filtering was successful!\n");

		for (int i = 0; i < bh.height; i++)
			free(pixels[i]);
		free(pixels);
		fclose(file_out);
	}

	return 0;
}