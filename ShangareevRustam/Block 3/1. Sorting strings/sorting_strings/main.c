#define _CRT_SECURE_NO_WARNINGS

#include "mman.h"
#include <sys\stat.h>
#include <io.h>
#include <fcntl.h>
#include <stdio.h>
#include <stdlib.h>
#include <string.h>

int str_comparator(const void* first, const void* second);

void find_max_len_and_cnt_strs(int* max_len, int* str_cnt, char* file_strs, int file_size);

void input_check(int argc);

void open_input(char** argv, int* fd);

void filling_array(char** strs, char* file_strs, int max_len, int file_size, int str_cnt);

void write_answer(char** argv, char** strs, int str_cnt, int max_len);

int main(int argc, char** argv)
{
	int fd, max_len = 0, str_cnt = 0;
	struct stat file_data;
	printf("This program sorts the lines of the input file in lexicographic order"
		" and writes the result to the output file.\n");
	input_check(argc);
	open_input(argv, &fd);
	fstat(fd, &file_data);
	// Mapping a file to memory
	char* file_strs = mmap(NULL, file_data.st_size, PROT_READ,
		MAP_PRIVATE, fd, 0);
	if (file_strs == MAP_FAILED)
	{
		printf("Error! Failed to map file data to memory. Error message: %s",
			strerror(errno));
		exit(errno);
	}
	find_max_len_and_cnt_strs(&max_len, &str_cnt, file_strs, file_data.st_size);
	// Memory allocation
	char** strs = malloc(str_cnt * sizeof(char*));
	for (int i = 0; i < str_cnt; i++)
	{
		strs[i] = (char*)malloc(max_len * sizeof(char));
	}
	filling_array(strs, file_strs, max_len, file_data.st_size, str_cnt);
	_close(fd);
	// Sorting
	qsort(strs, str_cnt, sizeof(char*), str_comparator);
	write_answer(argv, strs, str_cnt, max_len);
	printf("Successfully! The lines of file %s are sorted and placed in file %s\n",
		argv[1], argv[2]);
	// Freeing up memory
	for (int i = 0; i < str_cnt; i++)
	{
		free(strs[i]);
	}
	free(strs);
	return 0;
}

int str_comparator(const void* first, const void* second)
{
	return strcmp(*(char**)first, *(char**)second);
}

void find_max_len_and_cnt_strs(int* max_len, int* str_cnt, char* file_strs, int file_size)
{
	int cur_cnt = 0, i = 0;
	while (i < file_size)
	{
		if (file_strs[i] == '\r' || file_strs[i] == '\n')
		{
			if (file_strs[i] == '\r') i++;
			*max_len = max(*max_len, cur_cnt);
			cur_cnt = 0;
			*str_cnt = *str_cnt += 1;
		}
		else
		{
			cur_cnt++;
		}
		i++;
	}
}

void input_check(int argc)
{
	if (argc != 3)
	{
		printf("Error! Wrong input format! Run the program " 
			"again with the correct input format: "
			"main.exe <input file> <output file>\n");
		exit(-1);
	}
}

void open_input(char** argv, int* fd)
{
	*fd = _open(argv[1], O_RDONLY);
	if (*fd == -1)
	{
		printf("An error occurred while opening the file"
			"with the entered name. The file may not exist\n");
		exit(3);
	}
}

void filling_array(char** strs, char* file_strs, int max_len, int file_size, int str_cnt)
{
	int i = 0, j = 0, k = 0;
	while (i < file_size)
	{
		if (file_strs[i] == '\r' || file_strs[i] == '\n')
		{
			if (file_strs[i] == '\r') i++;
			for (int l = k; l < max_len; l++) strs[j][l] = (char)0;
			j++;
			k = 0;
		}
		else
		{
			if (j >= str_cnt)
			{
				printf("Error reading lines from file! Possibly the (last)"
					" line is incorrectly formatted: all lines in the file should "
					"end according to the rules of the operating system(\\n or \\r\\n)");
				exit(-1);
			}
			strs[j][k] = file_strs[i];
			k++;
		}
		i++;
	}
}

void write_answer(char** argv, char** strs, int str_cnt, int max_len)
{
	FILE* output_file = fopen(argv[2], "w");
	for (int i = 0; i < str_cnt; i++)
	{
		for (int j = 0; j < max_len && strs[i][j] != 0; j++)
		{
			fputc(strs[i][j], output_file);
		}
		fputc('\n', output_file);
	}
	fclose(output_file);
}