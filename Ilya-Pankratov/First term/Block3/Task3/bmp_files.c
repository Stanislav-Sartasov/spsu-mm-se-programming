#include <stdio.h>
#include <stdlib.h>

// BitMapFileHeader

struct BIT_header
{
	unsigned char file_type[2];
	const unsigned int file_size;

	const unsigned short reserved1;
	const unsigned short reserved2;

	const unsigned int image_offset;
};

// BitMapInfoHeader

struct DIB_header
{
	const unsigned int header_size;

	const unsigned int width;
	const unsigned int height;

	const unsigned short planes;
	const unsigned short bit_per_pixel;
	const unsigned int compressison;
	const unsigned int size_image;

	const unsigned int x_pels_per_meter;
	const unsigned int y_pels_per_meter;
	const unsigned int colors_used;
	const unsigned int colors_important;
};

// structure for working with bmp files

struct bmp_image
{
	int width;
	int height;
	int bytes;
	int bytes_per_pixel;
	unsigned char** image;
};

void show_stat(struct BIT_header* bit_header, struct DIB_header* dib_header)
{
	printf("BMP file info:\n\n");
	printf("First two letters: %c%c\nFile size: %d\nImage offset: %d\nWidth:%d\nHeight %d\nplanes: %d\nbit_per_pixel: %d\ncompressison: %d\nImage size: %d\n"
		"x pels per meter: %d\ny pels per meter: %d\nColors used: %d\nColors important: %d\n\n", bit_header->file_type[0], bit_header->file_type[1],
		bit_header->file_size, bit_header->image_offset, dib_header->width, dib_header->height, 
		dib_header->planes, dib_header->bit_per_pixel, dib_header->compressison, dib_header->size_image, dib_header->x_pels_per_meter,
		dib_header->y_pels_per_meter, dib_header->colors_used, dib_header->colors_important);

	return;
}

void read_image(struct FILE* bmp_in, struct bmp_image* picture)
{
	picture->image = (unsigned char**)malloc(picture->height * sizeof(unsigned char*));

	for (int i = 0; i < picture->height; i++)
	{
		picture->image[i] = (unsigned char*)malloc(picture->bytes * sizeof(unsigned char));
		fread(picture->image[i], sizeof(unsigned char), picture->bytes, bmp_in);
	}

	return;
}

void write_image(struct FILE* bmp_out, struct bmp_image* picture)
{
	for (int i = 0; i < picture->height; i++)
		fwrite(picture->image[i], sizeof(unsigned char), picture->bytes, bmp_out);
}

void free_image(struct bmp_image* picture)
{
	for (int i = 0; i < picture->height; i++)
		free(picture->image[i]);

	free(picture->image);
	return;
}