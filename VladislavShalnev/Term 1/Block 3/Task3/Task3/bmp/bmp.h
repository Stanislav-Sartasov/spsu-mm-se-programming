#ifndef BMP_H
#define BMP_H

#pragma pack(push)
#pragma pack(1)

typedef struct
{
	unsigned short type;		// signature
	unsigned int size;			// file size in bytes
	unsigned short reserved_1;	// not used
	unsigned short reserved_2;	// not used
	unsigned int offset;		// offset to image data in bytes from beginning
} bmp_header_t;

typedef struct
{
	unsigned int header_size;		// header size in bytes
	unsigned int width;				// width of the image
	unsigned int height;			// height of image
	unsigned short num_planes;		// number of color planes
	unsigned short bits_per_pixel;	// bits per pixel
	unsigned int compression;		// compression type
	unsigned int image_size;		// image size in bytes
	unsigned int x_resolution_ppm;	// y resolution pixels per meter
	unsigned int y_resolution_ppm;	// x resolution pixels per meter
	unsigned int num_colors;		// number of colors
	unsigned int important_colors;	// important colors
} bmp_info_t;

#pragma pack(pop)

typedef struct
{
	unsigned char blue;
	unsigned char green;
	unsigned char red;
} pixel;

typedef struct
{
	bmp_info_t info;
	bmp_header_t header;
	pixel* data;
	int size;
	int width;
	int height;
} bmp_image_t;

bmp_image_t* read_image(char* filename);

void close_image(bmp_image_t* image);

int write_image(char* filename, bmp_image_t* image);

#endif // BMP_H