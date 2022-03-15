#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <fcntl.h>
#include <sys/stat.h>
#include "mman.h"

int cmp(const void* first_arg, const void* second_arg)
{
	return strcmp(*(char**)first_arg, *(char**)second_arg);
}

int length_of_string(char* string)
{
	int index = 0;
	while (string[index] != '\n' && string[index] != '\0' && string[index] != '\r')
		index++;
	return index;
}

int main(int argc, char* argv[])
{
	printf("\nThis programm used for sorting files.\n"
		"Enter the name of original file as first command's argument "
		 "and the name of the resulting file as last argument\n");

	int r_input_file, r_out_file;
	struct stat statistic;
	char* result_mmap;

	if (argc != 3)
	{
		printf("Wrong count of input parameters. You must enter only 2 parameters, but you enter: %d \n", argc);
		return 0;
	}

	if ((r_input_file = open(argv[1], O_RDWR)) < 0)
	{
		printf("Invalid file path specified or the file is corrupted.\n");
		return 0;
	}

	if ((r_out_file = open(argv[2], O_RDWR | O_CREAT | O_TRUNC, S_IWRITE)) < 0)
	{
		printf("Unable to open the file to record the sorted file.\n");
		return 0;
	}

	fstat(r_input_file, &statistic);

	if ((result_mmap = mmap(0, statistic.st_size, PROT_READ | PROT_WRITE, MAP_PRIVATE, r_input_file, 0)) == MAP_FAILED)
	{
		printf("Error with mmap process.\n");
		return 0;
	}

	int strings_count = 1;
	for (int i = 0; i < statistic.st_size; i++)
	{
		if (result_mmap[i] == '\n')
			strings_count++;
	}

	char** list_of_stroke_pointers = (char**)malloc(strings_count * sizeof(char*));

	int stat_index = 0;
	list_of_stroke_pointers[0] = &result_mmap[0];
	for (int i = 1; i < strings_count; i++)
	{
		while (result_mmap[stat_index] != '\n' && stat_index < statistic.st_size)
			stat_index++;

		if (stat_index >= statistic.st_size)
			break;

		list_of_stroke_pointers[i] = &result_mmap[stat_index + 1];
		stat_index += 2;
	}
	close(r_input_file);

	qsort(list_of_stroke_pointers, strings_count, sizeof(char*), cmp);

	for (int i = 0; i < strings_count; i++)
	{
		write(r_out_file, list_of_stroke_pointers[i], length_of_string(list_of_stroke_pointers[i]));
		if (i != strings_count - 1)
			write(r_out_file, "\n", 1);
	}

	free(list_of_stroke_pointers);
	munmap(result_mmap, statistic.st_size);
	close(r_out_file);
	return 0;
}