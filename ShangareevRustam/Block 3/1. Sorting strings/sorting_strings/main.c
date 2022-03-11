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

void filling_array(char** strs_pointers, char* file_strs, int file_size, int str_cnt);

void write_answer(char** argv, char** strs_pointers, int str_cnt, int max_len);

void mmap_check(char* file_strs, int file_size);

void cancel_changes(char* file_strs, int file_size);

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
	char* file_strs = mmap(NULL, file_data.st_size, PROT_READ | PROT_WRITE,
		MAP_PRIVATE, fd, 0);
	mmap_check(file_strs, file_data.st_size);
	find_max_len_and_cnt_strs(&max_len, &str_cnt, file_strs, file_data.st_size);
	// Memory allocation and filling array
	char** strs_pointers = malloc(str_cnt * sizeof(char*));
	filling_array(strs_pointers, file_strs, file_data.st_size, str_cnt);
	_close(fd);
	// Sorting
	qsort(strs_pointers, str_cnt, sizeof(char*), str_comparator);
	write_answer(argv, strs_pointers, str_cnt, max_len);
	printf("Successfully! The lines of file %s are sorted and placed in file %s\n",
		argv[1], argv[2]);
	// Resetting the input file to its original state
	cancel_changes(file_strs, file_data.st_size);
	// Freeing up memory
	free(strs_pointers);
	munmap(file_strs, file_data.st_size);
	return 0;
}

void cancel_changes(char* file_strs, int file_size)
{
	for (int i = 0; i < file_size; i++)
	{
		if (file_strs[i] == '\0')
		{
			if (i != file_size - 1)
			{
				if (file_strs[i + 1] == '\0')
				{
					file_strs[i] = '\r';
					file_strs[i + 1] = '\n';
				}
				else
				{
					file_strs[i] = '\n';
				}
			}
			else
			{
				file_strs[i] = '\n';
			}
		}
	}
}

void mmap_check(char* file_strs, int file_size)
{
	if (file_strs == MAP_FAILED)
	{
		printf("Error! Failed to map file data to memory. Error message: %s",
			strerror(errno));
		exit(errno);
	}
	else if (file_strs[file_size - 1] != '\n')
	{
		printf("Error reading lines from file! The last"
			" line is incorrectly formatted: all lines in the file should "
			"end according to the rules of the operating system(\\n or \\r\\n)");
		exit(-1);
	}
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
	*fd = _open(argv[1], O_RDWR);
	if (*fd == -1)
	{
		printf("An error occurred while opening the file"
			"with the entered name. The file may not exist\n");
		exit(3);
	}
}

void filling_array(char** strs_pointers, char* file_strs, int file_size, int str_cnt)
{
	strs_pointers[0] = (char*)file_strs;
	int j = 1;
	for (int i = 0; i < file_size; i++)
	{
		if (file_strs[i] == '\r')
		{
			file_strs[i] = '\0';
		}
		else if (file_strs[i] == '\n')
		{
			file_strs[i] = '\0';
			if (j < str_cnt)
			{
				strs_pointers[j] = (char*)file_strs + i + 1;
			}
			j++;
		}
	}
}

void write_answer(char** argv, char** strs_pointers, int str_cnt, int max_len)
{
	FILE* output_file = fopen(argv[2], "w");
	for (int i = 0; i < str_cnt; i++)
	{
		fputs(strs_pointers[i], output_file);
		fputc('\n', output_file);
	}
	fclose(output_file);
}