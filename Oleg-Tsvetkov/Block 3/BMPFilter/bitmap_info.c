#include "bitmap_info.h"

struct bitmap_info get_bitmap_info(FILE *file)
{
	struct bitmap_info bmp;

	fseek(file, 2, SEEK_SET);
	fread(&(bmp.size), sizeof(int), 1, file);

	fseek(file, 10, SEEK_SET);
	fread(&(bmp.offset_bits_count), sizeof(int), 1, file);

	fseek(file, 18, SEEK_SET);
	fread(&(bmp.width), sizeof(int), 1, file);

	fseek(file, 22, SEEK_SET);
	fread(&(bmp.height), sizeof(int), 1, file);

	fseek(file, 26, SEEK_SET);
	fread(&(bmp.bit_planes_count), sizeof(short int), 1, file);

	fseek(file, 28, SEEK_SET);
	fread(&(bmp.bit_count), sizeof(short int), 1, file);

	fseek(file, 46, SEEK_SET);
	fread(&(bmp.colors_used_count), sizeof(int), 1, file);

	fseek(file, 50, SEEK_SET);
	fread(&(bmp.colors_important_count), sizeof(int), 1, file);

	return bmp;
}