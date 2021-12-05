#include "filters.h"
#include <stdio.h>
#include <stdint.h>
#include <string.h>
#include <errno.h>
#include <fcntl.h>
#include <sys/mman.h>
#include <sys/stat.h>
#include <unistd.h>

#define INVALID_ARGS_ERROR 1
#define FILE_OPEN_ERROR 2
#define FILE_CLOSE_ERROR 3
#define FSTAT_ERROR 4
#define MAP_FAIL_ERROR 5
#define UNMAP_FAIL_ERROR 6
#define WRONG_BPP_ERROR 7
#define WRONG_COMPRESSION_METHOD 8

void print_usage()
{
	printf("USAGE:\n[compiled executable] [input image path] [output image path] [filter]\n");
	printf("[filter] can be one of these:\n\"median\" - for median 3x3 filter\n\"gauss\" - for gaussian 3x3 filter\n");
	printf("\"sobel_x\" and \"sobel_y\" - for Sobel's XX and Y filters\n\"gray\" - for grayscale image\n");
}

int main(int argc, char* argv[])
{
	printf("This program takes image from specified file, applies specified filter and writes result to another specified file.\nNotice that program supports only 24 and 32-bit .bmp images without compression.\n");
	if (argc != 4)
	{
		printf("%d", argc);
		print_usage();
		return INVALID_ARGS_ERROR;
	}

	int filter_mode;
	if (!(strcmp(argv[3], "median")))
	{
		filter_mode = 0;
	}
	else if (!(strcmp(argv[3], "gauss")))
	{
		filter_mode = 1;
	}
	else if (!(strcmp(argv[3], "sobel_x")))
	{
		filter_mode = 2;
	}
	else if (!(strcmp(argv[3], "sobel_y")))
	{
		filter_mode = 3;
	}
	else if (!(strcmp(argv[3], "gray")))
	{
		filter_mode = 4;
	}
	else
	{
		print_usage();
		return INVALID_ARGS_ERROR;
	}

	int input_file_descriptor = open(argv[1], O_RDONLY);
	if (input_file_descriptor < 0)
	{
		printf("Opening input file in readonly mode failed: %s!\n", strerror(errno));
		return FILE_OPEN_ERROR;
	}

	int output_file_descriptor = open(argv[2], O_RDWR | O_CREAT, S_IRWXU);
	if (output_file_descriptor < 0)
	{
		printf("Creating output file failed: %s!\n", strerror(errno));
		return FILE_OPEN_ERROR;
	}

	struct stat filestat;
	if (fstat(input_file_descriptor, &filestat))
	{
		printf("Fstat failed: %s!\n", strerror(errno));
		return FSTAT_ERROR;
	}

	size_t size = filestat.st_size;

	uint8_t* output_image = mmap(NULL, size, PROT_WRITE, MAP_SHARED, output_file_descriptor, 0);
	if (output_image == MAP_FAILED)
	{
		printf("Output image mapping failed: %s!\n", strerror(errno));
		return MAP_FAIL_ERROR;
	}

	uint8_t* image = mmap(NULL, size, PROT_READ, MAP_SHARED, input_file_descriptor, 0);
	if (image == MAP_FAILED)
	{
		printf("Input image mapping failed: %s!\n", strerror(errno));
		return MAP_FAIL_ERROR;
	}

	bitmap_file_header file_header;
	memcpy(&file_header, image, 14);
	bitmap_info_header info_header = {};
	uint32_t info_header_size = *(uint32_t*)(image + 14);

	if (info_header_size == 12) // old CORE version info header is used
	{
		info_header.size = info_header_size;
		info_header.width = (int32_t)*(uint16_t*)(image + 18);
		info_header.height = (int32_t)*(uint16_t*)(image + 20);
		info_header.planes = (uint32_t)*(uint16_t*)(image + 22);
		info_header.color_depth = (uint32_t)*(uint16_t*)(image + 20);
	}
	else
	{
		memcpy(&info_header, image + 14, info_header_size);
	}

	if ((info_header.color_depth != 24) && (info_header.color_depth != 32))
	{
		printf("This program supports only 24 and 32-bit BMPs!\n");
		return WRONG_BPP_ERROR;
	}

	if ((info_header.compression_method != 0) && (info_header.compression_method != 3) && (info_header.compression_method != 6))
	{
		printf("Used compression method (%d) is not supported!\n", info_header.compression_method);
		return WRONG_COMPRESSION_METHOD;
	}

	lseek(output_file_descriptor, size-1, SEEK_SET);
	write(output_file_descriptor, "", 1);
	memcpy(output_image , image, size);

	if (filter_mode == 4)
	{
		grayscale(image + file_header.bitmap_data_offset, &info_header, output_image + file_header.bitmap_data_offset);
	}
	else
	{
		filter(image + file_header.bitmap_data_offset, &info_header, output_image + file_header.bitmap_data_offset, filter_mode);
	}

	int unmap_error = munmap(image, size);
	if (unmap_error)
	{
		printf("Input file unmapping failed: %s!\n", strerror(errno));
		return UNMAP_FAIL_ERROR;
	}

	int close_error = close(input_file_descriptor);
	if (close_error)
	{
		printf("Closing input file failed: %s!\n", strerror(errno));
		return FILE_CLOSE_ERROR;
	}

	unmap_error = munmap(output_image, size);
	if (unmap_error)
	{
		printf("Output file unmapping failed: %s!\n", strerror(errno));
		return UNMAP_FAIL_ERROR;
	}

	close_error = close(output_file_descriptor);
	if (close_error)
	{
		printf("Closing output file failed: %s!\n", strerror(errno));
		return FILE_CLOSE_ERROR;
	}

	printf("Done!\n");

	return 0;
}