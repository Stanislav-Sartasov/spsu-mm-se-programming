#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <locale.h>
#include <string.h>
#include "bitmap_info.h"
#include "image_filters.h"

void print_program_description();

int main(int argc, char *argv[])
{
	setlocale(LC_ALL, "Russian");

	print_program_description();

	if (argc != 4)
	{
		printf("Неверное количество аргументов. Формат: <программа> <имя_входного_файла> <название_фильтра> <имя выходного файла>\n");
		return -1;
	}

	if (strcmp(argv[2], "median") != 0 && strcmp(argv[2], "gauss") != 0 && strcmp(argv[2], "sobelx") != 0 &&
		strcmp(argv[2], "sobely") != 0 && strcmp(argv[2], "gray") != 0)
	{
		printf("Введённый фильтр не относится к списку доступных. Попробуйте ещё раз.");
		return -1;
	}

	FILE *source_image = fopen(argv[1], "rb");
	if (source_image == NULL)
	{
		printf("Входной файл не был найден.");
		return -1;
	}

	FILE *destination_image = fopen(argv[3], "wb");
	if (destination_image == NULL)
	{
		printf("Не удалось создать/открыть выходной файл.");
		return -1;
	}

	struct bitmap_info bmp_info = get_bitmap_info(source_image);

	unsigned char **picture = (unsigned char **) malloc(bmp_info.height * sizeof(unsigned char *));
	int row_byte_size, channels;
	if (bmp_info.bit_count == 24)
	{
		row_byte_size = 3 * bmp_info.width + bmp_info.width % 4;
		channels = 3;
		//printf("Working with 24-bit bmp file\n");
	}
	else if (bmp_info.bit_count == 32)
	{
		row_byte_size = 4 * bmp_info.width;
		channels = 4;
		//printf("Working with 32-bit bmp file\n");
	}
	else
	{
		printf("Неподдерживаемая битность BMP-файла.");
		return -1;
	}

	fseek(source_image, 0, SEEK_SET);
	char head_information[HEADER_BITS_COUNT];
	fread(head_information, sizeof(unsigned char), HEADER_BITS_COUNT * sizeof(unsigned char), source_image);
	fseek(source_image, HEADER_BITS_COUNT, SEEK_SET);
	for (int i = 0; i < bmp_info.height; ++i)
	{
		picture[i] = (unsigned char *) malloc((row_byte_size) * sizeof(unsigned char));
		fread(picture[i], sizeof(unsigned char), row_byte_size * sizeof(unsigned char), source_image);
	}
	fwrite(head_information, sizeof(unsigned char), HEADER_BITS_COUNT, destination_image);

	if (strcmp("gray", argv[2]) == 0)
	{
		apply_gray(bmp_info, &picture, row_byte_size, channels);
		//printf("Applied gray");
	}
	else if (strcmp("median", argv[2]) == 0)
	{
		apply_median_3x3(bmp_info, &picture, row_byte_size, channels);
		//printf("Applied median 3x3");
	}
	else if (strcmp("gauss", argv[2]) == 0)
	{
		apply_gauss_3x3(bmp_info, &picture, row_byte_size, channels);
		//printf("Applied gauss 3x3");
	}
	else if (strcmp("sobelx", argv[2]) == 0)
	{
		apply_sobel_3x3(bmp_info, &picture, row_byte_size, channels, 0);
		//printf("Applied sobel x");
	}
	else if (strcmp("sobely", argv[2]) == 0)
	{
		apply_sobel_3x3(bmp_info, &picture, row_byte_size, channels, 1);
		//printf("Applied sobel y");
	}

	for (int i = 0; i < bmp_info.height; ++i)
	{
		fwrite(picture[i], sizeof(unsigned char), row_byte_size * sizeof(unsigned char), destination_image);
	}

	for (int i = 0; i < bmp_info.height; ++i)
	{
		free(picture[i]);
	}
	free(picture);
	fclose(source_image);
	fclose(destination_image);
	return 0;
}

void print_program_description()
{
	printf("Данная программа применяет фильтры к BMP-файлу (24-bit или 32-bit)\n");
	printf("Формат: <программа> <имя_входного_файла> <название_фильтра> <имя выходного файла>\n");
	printf("Далее будет выведен список доступных фильтров и их названий. Внимание: названия регистрозависимы.\n");
	printf("Усредняющий фильтр 3x3 - median\n");
	printf("Усредняющий фильтр Гаусса 3x3 - gauss\n");
	printf("Фильтр Собеля по X - sobelx\n");
	printf("Фильтр Собеля по Y - sobely\n");
	printf("Перевод в оттенки серого - gray\n");
}