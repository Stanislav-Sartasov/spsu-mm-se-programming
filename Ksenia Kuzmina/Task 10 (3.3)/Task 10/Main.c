#include <string.h>
#include <stdio.h>
#include <stdlib.h>
#include <math.h>

#pragma pack(push)
#pragma pack(1)
struct file_header
{
	unsigned short filetype;
	unsigned int filesize;
	unsigned short reserved_byte_one;
	unsigned short reserved_byte_two;
	unsigned int offset;
};

struct bitmap_header
{
	unsigned int headersize;
	unsigned int width;
	unsigned int height;
	unsigned short colorplanes;
	unsigned short bits_per_pixel;
	unsigned int compression;
	unsigned int size_of_bitmap;
	unsigned int hresolution;
	unsigned int vresolution;
	unsigned int colors;
	unsigned int important_colors;
};
#pragma pack(pop)

struct pix24
{
	unsigned char r;
	unsigned char g;
	unsigned char b;
};

struct pix32
{
	unsigned char r;
	unsigned char g;
	unsigned char b;
	unsigned char a;
};

void gray_filter_24(struct pix24* pixels, int height, int width)
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

void gray_filter_32(struct pix32* pixels, int height, int width)
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

int bubble_sort(int* a)
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

int sobel_x(int* a)
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

int sobel_y(int* a)
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

void filter24(struct pix24* pixels, int height, int width, char* filter)
{
	struct pix24* copy_pixels;
	copy_pixels = (struct pix24*)malloc(width * height * sizeof(struct pix24));
	memcpy(copy_pixels, pixels, width * height * sizeof(struct pix24));
	int* block_red;
	int* block_green;
	int* block_blue;
	block_red = (int*)malloc(9 * sizeof(int));
	block_green = (int*)malloc(9 * sizeof(int));
	block_blue = (int*)malloc(9 * sizeof(int));
	for (int i = 0; i < height * width - 2 * width; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				if ((i % width != 0) && (i < (width * height - width)))
				{
					block_red[j + 3 * k] = copy_pixels[i + j + width * k].r;
					block_green[j + 3 * k] = copy_pixels[i + j + width * k].g;
					block_blue[j + 3 * k] = copy_pixels[i + j + width * k].b;
				}
			}
		}
		if (strcmp(filter, "median") == 0)
		{
			pixels[i + 1 + width].r = bubble_sort(block_red);
			pixels[i + 1 + width].g = bubble_sort(block_green);
			pixels[i + 1 + width].b = bubble_sort(block_blue);
		}
		if (strcmp(filter, "gauss") == 0)
		{
			pixels[i + 1 + width].r = (int)gauss(block_red);
			pixels[i + 1 + width].g = (int)gauss(block_green);
			pixels[i + 1 + width].b = (int)gauss(block_blue);
		}
		if (strcmp(filter, "sobel_x") == 0)
		{
			pixels[i + 1 + width].r = (int)sobel_x(block_red);
			pixels[i + 1 + width].g = (int)sobel_x(block_green);
			pixels[i + 1 + width].b = (int)sobel_x(block_blue);
		}
		if (strcmp(filter, "sobel_y") == 0)
		{
			pixels[i + 1 + width].r = (int)sobel_y(block_red);
			pixels[i + 1 + width].g = (int)sobel_y(block_green);
			pixels[i + 1 + width].b = (int)sobel_y(block_blue);
		}
	}
	free(block_red);
	free(block_green);
	free(block_blue);
}

void filter32(struct pix32* pixels, int height, int width, char* filter)
{
	struct pix32* copy_pixels;
	copy_pixels = (struct pix32*)malloc(width * height * sizeof(struct pix32));
	memcpy(copy_pixels, pixels, width * height * sizeof(struct pix32));
	int* block_red;
	int* block_green;
	int* block_blue;
	block_red = (int*)malloc(9 * sizeof(int));
	block_green = (int*)malloc(9 * sizeof(int));
	block_blue = (int*)malloc(9 * sizeof(int));
	for (int i = 0; i < height * width - 2 * width; i++)
	{
		for (int j = 0; j < 3; j++)
		{
			for (int k = 0; k < 3; k++)
			{
				if ((i % width != 0) && (i < (width * height - width)))
				{
					block_red[j + 3 * k] = copy_pixels[i + j + width * k].r;
					block_green[j + 3 * k] = copy_pixels[i + j + width * k].g;
					block_blue[j + 3 * k] = copy_pixels[i + j + width * k].b;
				}
			}
		}
		if (strcmp(filter, "median") == 0)
		{
			pixels[i + 1 + width].r = bubble_sort(block_red);
			pixels[i + 1 + width].g = bubble_sort(block_green);
			pixels[i + 1 + width].b = bubble_sort(block_blue);
		}
		if (strcmp(filter, "gauss") == 0)
		{
			pixels[i + 1 + width].r = (int)gauss(block_red);
			pixels[i + 1 + width].g = (int)gauss(block_green);
			pixels[i + 1 + width].b = (int)gauss(block_blue);
		}
		if (strcmp(filter, "sobel_x") == 0)
		{
			pixels[i + 1 + width].r = (int)sobel_x(block_red);
			pixels[i + 1 + width].g = (int)sobel_x(block_green);
			pixels[i + 1 + width].b = (int)sobel_x(block_blue);
		}
		if (strcmp(filter, "sobel_y") == 0)
		{
			pixels[i + 1 + width].r = (int)sobel_y(block_red);
			pixels[i + 1 + width].g = (int)sobel_y(block_green);
			pixels[i + 1 + width].b = (int)sobel_y(block_blue);
		}
	}
	free(block_red);
	free(block_green);
	free(block_blue);
}

int arguementscheck(int argcount, char* filter)
{
	if (argcount != 4)
	{
		printf("You have to submit three arguments to the input.\n");
		return 0;
	}
	if ((strcmp(filter, "gauss") != 0) && (strcmp(filter, "median") != 0) && (strcmp(filter, "sobel_x") != 0)
		&& (strcmp(filter, "sobel_y") != 0) && (strcmp(filter, "gray") != 0))
	{
		printf("You have entered the wrong filter name. Available filters: gauss, median, sobel_x, sobel_y, gray.\n");
		return 0;
	}
	return 1;
}

int main(int argc, char* argv[])
{
	printf("This program applies the following filters to images:\n");
	printf("3x3 Gaussian filter, median filter, gray filter, Sobel x filter and Sobel y filter\n");
	if (!arguementscheck(argc, argv[2]))
		return -1;

	FILE* file_inp, * file_out;

	struct file_header header;
	struct bitmap_header bmpheader;

	fopen_s(&file_inp, argv[1], "rb");
	fopen_s(&file_out, argv[3], "wb");

	if (file_inp == NULL)
		return -1;

	fread(&header, sizeof(header), 1, file_inp);
	fread(&bmpheader, sizeof(bmpheader), 1, file_inp);

	if ((bmpheader.bits_per_pixel != 24) && (bmpheader.bits_per_pixel != 32))
		return -1;

	if (bmpheader.bits_per_pixel == 24)
	{
		struct pix24* array = (struct pix24*)malloc(header.filesize - sizeof(header) - sizeof(bmpheader));
		fseek(file_inp, header.offset, SEEK_SET);
		fread(array, header.filesize - sizeof(header) - sizeof(bmpheader), 1, file_inp);

		if (strcmp(argv[2], "gray") == 0)
			gray_filter_24(array, bmpheader.height, bmpheader.width, argv[2]);
		if (strcmp(argv[2], "median") == 0)
			filter24(array, bmpheader.height, bmpheader.width, argv[2]);
		if (strcmp(argv[2], "gauss") == 0)
			filter24(array, bmpheader.height, bmpheader.width, argv[2]);
		if (strcmp(argv[2], "sobel_x") == 0)
		{
			gray_filter_24(array, bmpheader.height, bmpheader.width);
			filter24(array, bmpheader.height, bmpheader.width, argv[2]);
		}
		if (strcmp(argv[2], "sobel_y") == 0)
		{
			gray_filter_24(array, bmpheader.height, bmpheader.width);
			filter24(array, bmpheader.height, bmpheader.width, argv[2]);
		}

		fwrite(&header, sizeof(header), 1, file_out);
		fwrite(&bmpheader, sizeof(bmpheader), 1, file_out);
		fwrite(array, header.filesize - sizeof(header) - sizeof(bmpheader), 1, file_out);
		free(array);
	}

	if (bmpheader.bits_per_pixel == 32)
	{
		struct pix32* array = (struct pix32*)malloc(header.filesize - sizeof(header) - sizeof(bmpheader));
		fseek(file_inp, header.offset, SEEK_SET);
		fread(array, header.filesize - sizeof(header) - sizeof(bmpheader), 1, file_inp);

		if (strcmp(argv[2], "gray") == 0)
			gray_filter_32(array, bmpheader.height, bmpheader.width);
		if (strcmp(argv[2], "median") == 0)
			filter32(array, bmpheader.height, bmpheader.width, argv[2]);
		if (strcmp(argv[2], "gauss") == 0)
			filter32(array, bmpheader.height, bmpheader.width, argv[2]);
		if (strcmp(argv[2], "sobel_x") == 0)
		{
			gray_filter_32(array, bmpheader.height, bmpheader.width);
			filter32(array, bmpheader.height, bmpheader.width, argv[2]);
		}
		if (strcmp(argv[2], "sobel_y") == 0)
		{
			gray_filter_32(array, bmpheader.height, bmpheader.width);
			filter32(array, bmpheader.height, bmpheader.width, argv[2]);
		}

		fwrite(&header, sizeof(header), 1, file_out);
		fwrite(&bmpheader, sizeof(bmpheader), 1, file_out);
		fwrite(array, header.filesize - sizeof(header) - sizeof(bmpheader), 1, file_out);
		free(array);
	}

	fclose(file_inp);
	fclose(file_out);

	printf("The filter has been applied");
	return 0;
}