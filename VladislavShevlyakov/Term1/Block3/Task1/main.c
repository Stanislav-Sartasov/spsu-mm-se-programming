#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <sys\stat.h>
#include <string.h>
#include "mman.h"

int str_lenght(char* str)
{
	int lenght = 0;

	while (str[lenght] != '\n' && str[lenght] != '\r' && str[lenght] != '\0')
		lenght++;

	return lenght;
}

int comparison(const void* first, const void* second)
{
	return strcmp(*(char**)first, *(char**)second);
}

int main(int argc, char* argv[])
{
	FILE* file_in;
	FILE* file_out;
	struct stat file_info;
	char* str;

	printf("The program sorts the lines in the specified file using memory mapped files.\n");

	if (argc != 3)
	{
		printf("Exactly 3 arguments must be entered. Try again!\n");
		return -1;
	}

	file_in = open(argv[1], O_RDWR);

	if (file_in < 0)
	{
		printf("Error in opening the initial file!\n");
		return -1;
	}

	file_out = open(argv[2], O_RDWR | O_TRUNC | O_CREAT, S_IWRITE);

	if (file_in < 0)
	{
		printf("Error in opening the destination file!\n");
		return -1;
	}

	fstat(file_in, &file_info);

	str = mmap(0, file_info.st_size, PROT_READ | PROT_WRITE, MAP_SHARED, file_in, 0);

	if (str == MAP_FAILED)
	{
		printf("Error when calling the mmap function!\n");
		return -1;
	}

	close(file_in);

	int str_count = 0;
	for (int i = 0; i < file_info.st_size; i++)
		str_count += str[i] == '\n' ? 1 : 0;

	char** lines = (char**)malloc(sizeof(char*) * str_count);

	for (int i = 0, r = 0; i < str_count; i++)
	{
		lines[i] = &str[r];
		while (str[r++] != '\n' && r < file_info.st_size && i != str_count - 1);
	}

	qsort(lines, str_count, sizeof(char*), comparison);

	for (int i = 0; i < str_count; i++)
	{
		if (lines[i][0] != '\n')
		{
			write(file_out, lines[i], str_lenght(lines[i]));
			write(file_out, "\n", 1);
		}
	}

	free(lines);
	munmap(str, file_info.st_size);
	close(file_out);
	printf("Sorting was completed successfully!\n");

	return 0;
}