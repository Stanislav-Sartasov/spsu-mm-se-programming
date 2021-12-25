#define _CRT_SECURE_NO_WARNINGS

#include <stdio.h>
#include <stdlib.h>
#include "mman.h"
#include <fcntl.h>
#include <string.h>
#include <stdbool.h>
#include <sys/stat.h>

int comporator(const void* left, const void* right)
{
	return strcmp(*(char**)left, *(char**)right);
}

int main(int argc, char* argv[])
{
	printf("This program sorts strings from input file and place to new one.\n");
	if (argc != 3)
	{
		printf("Not enough/too many arguments.");
		return -1;
	}

	// information
	int f_in, f_out;
	char* arr;
	struct stat info;

	// open
	f_in = open(argv[1], O_RDWR);
	if (f_in < 0)
	{
		printf("Error occurred while opening file.\n");
		return -1;
	}

	if (fstat(f_in, &info) < 0)
	{
		printf("Could get input file information.");
		close(f_in);
		return -1;
	}

	f_out = open(argv[2], O_RDWR | O_CREAT | O_TRUNC, S_IWRITE);
	if (f_out < 0)
	{
		printf("Error occurred while opening file.\n");
		close(f_in);
		return -1;
	}

	if ((arr = mmap(0, info.st_size, PROT_READ | PROT_WRITE, MAP_PRIVATE, f_in, 0)) == MAP_FAILED)
	{
		printf("Failed to map input file to memory.");
		close(f_in);
		close(f_out);
		return -1;
	}

	int count_str = 0;

	for (int letter = 0; letter <= info.st_size; ++letter)
	{
		if (arr[letter] == '\n' || arr[letter] == '\0')
		{
			++count_str;
		}
	}

	char** arr_str = (char**)malloc(count_str * sizeof(char*));
	int* len_strs = (int*)malloc(count_str * sizeof(int));
	int value = 0;

	for (int num_str = 0; num_str < count_str; ++num_str)
	{
		int len_str = 0;
		while (arr[value + len_str] != '\0' && arr[value + len_str] != '\n' && value + len_str <= info.st_size)
		{
			++len_str;
		}
		arr_str[num_str] = &arr[value];
		arr_str[num_str][len_str] = '\0';
		value += len_str;
		len_strs[num_str] = len_str;
		if (arr[value] == '\n' || arr[value] == '\0')
		{
			++value;
		}
	}


	// Sort lines and output to file 
	qsort(arr_str, count_str, sizeof(char*), comporator);

	for (int i = 0; i < count_str; ++i)
	{
		if (arr_str[i][strlen(arr_str[i]) - 1] != '\r')
		{
			write(f_out, arr_str[i], strlen(arr_str[i]));
		}
		else
		{
			write(f_out, arr_str[i], strlen(arr_str[i]) - 1);
		}
		if (i < count_str - 1)
		{
			write(f_out, "\n", 1);
		}
	}

	for (int num_str = 0; num_str < count_str - 1; ++num_str)
	{
		arr_str[num_str][strlen(arr_str[num_str])] = '\n';
	}

	free(arr_str);
	munmap(arr, info.st_size);
	close(f_in);
	close(f_out);
	return 0;
}