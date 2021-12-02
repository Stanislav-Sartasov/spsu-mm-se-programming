#pragma once

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

void show_stat(struct BIT_header* bit_header, struct DIB_header* dib_header);

void read_image(struct FILE* bmp_in, struct bmp_image* picture);

void write_image(struct FILE* bmp_out, struct bmp_image* picture);

void free_image(struct bmp_image* picture);