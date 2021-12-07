#include <stdint.h>
#pragma pack(1)

struct rgb
{
	uint8_t red;
	uint8_t green;
	uint8_t blue;
};

typedef struct rgb rgb;

struct bitmap_file_header
{
	char header_field[2];
	int32_t size;
	int8_t reserved_1[2];
	int8_t reserved_2[2];
	int32_t bitmap_data_offset;
};

typedef struct bitmap_file_header bitmap_file_header;

struct bitmap_info_header
{
	// universal values:
	uint32_t size;
	int32_t width;
	int32_t height;
	uint16_t planes;
	uint16_t color_depth;
	uint32_t compression_method;
	uint32_t bitmap_data_size;
	int32_t horizontal_resolution;
	int32_t vertical_resolution;
	uint32_t colors_number;
	uint32_t important_colors_number;
	// values added in v4, unused if v3 is used:
	uint32_t red_mask;
	uint32_t green_mask;
	uint32_t blue_mask;
	uint32_t alpha_mask;
	uint32_t color_space_type;
	int32_t rgb_xyz_endpoints[9];
	uint32_t gamma_red;
	uint32_t gamma_green;
	uint32_t gamma_blue;
	// values added in v5, unused if v3 and v4 are used
	uint32_t intent;
	uint32_t profile_data_offset;
	uint32_t profile_data_size;
	uint32_t reserved;
};

typedef struct bitmap_info_header bitmap_info_header;

int get_mask_offset(uint32_t mask);

rgb get_rgb(const uint8_t* pixel, bitmap_info_header* info_header);

void put_rgb(uint8_t* pixel, bitmap_info_header* info_header, rgb pixel_rgb);

int compare_pixels(const void* pixel_1_pointer, const void* pixel_2_pointer);
