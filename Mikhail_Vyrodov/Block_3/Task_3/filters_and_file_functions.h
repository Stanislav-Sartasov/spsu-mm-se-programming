#pragma once
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

int apply_filters(struct image input, char* output_name, char* filter, struct dib_header dibheader, struct bitmap_header header);

struct image read_image(FILE* fp, int height, int width, short int bits_per_pixel);
