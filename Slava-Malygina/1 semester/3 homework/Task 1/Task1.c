#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <fcntl.h>
#include <sys/stat.h>
#include <stdarg.h>
#include "mman.h"


int cmpr(const void* a, const void* b)
{
	char* str_a = *(char**)a;
	char* str_b = *(char**)b;
	return strcmp(str_a, str_b);
}

int main(int argc, char* argv[])
{
	printf("This program sorts the lines of a text file and writes the sorted data to the output file.\n");

	if (argc != 3)
	{
		printf("The input does not satisfy the 3-argument requirement. Enter the location and names of the input and output files to perform the sort.");
		return 0;
	}

	int input = open(argv[1], O_RDONLY);
	int output = open(argv[2], O_RDWR | O_CREAT | O_TRUNC, S_IWRITE);
	struct stat buffer;
	fstat(input, &buffer);
	char* map_input = mmap(0, buffer.st_size, PROT_READ, MAP_SHARED, input, 0);
	char* map_output = mmap(0, buffer.st_size, PROT_READ | PROT_WRITE, MAP_SHARED, output, 0);

	if (input < 0)
	{
		printf("Problems encountered during reading from input file.");
		return 0;
	}

	if (output < 0)
	{
		printf("Problems encountered during writing to output file.");
		return 0;
	}

	if (map_input == MAP_FAILED || map_output == MAP_FAILED)
	{
		printf("Problems encountered during the mapping");
		return 0;
	}

	int n = 0;
	for (int i = 0; i < buffer.st_size; i++)
		if (map_input[i] == '\n')
			n++;

	struct string
	{
		char* ptr;
		int len;
	};

	struct string* line = malloc(n * sizeof(struct string));
	if (line == NULL)
		printf("Failed to allocate memory\n");

	for (int i = 0, num = 0, buf_len = 0; i < buffer.st_size; i++)
	{
		while (map_input[i] != '\n' && map_input[i] != '\r\n')
			i++;
		line[num].ptr = &map_input[buf_len];
		line[num].len = i + 1 - buf_len;
		buf_len = i + 1;
		num++;
	}

	qsort(line, n, sizeof(struct string), cmpr);

	for (int i = 0, len = 0; i < n; i++)
	{
		memcpy(map_output + len, line[i].ptr, line[i].len * sizeof(char));
		len += line[i].len;
	}

	free(line);
	munmap(map_input, buffer.st_size);
	munmap(map_output, buffer.st_size);
	close(input);
	close(output);
	printf("Sorting completed.");

	return 0;
}