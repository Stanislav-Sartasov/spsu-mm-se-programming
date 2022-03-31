#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <fcntl.h>
#include <sys/stat.h>
#include "mman.h"

int cmpr(const void* f_ptr, const void* s_ptr) // '>'
{
	char* s_s = *(char**)f_ptr;
	char* s_f = *(char**)s_ptr;

	int i = 0;
	while (1)
	{
		//printf("%c %c\n", s_f[i], s_s[i]);

		if (toupper(s_s[i]) == '\n')
		{
			return 1;
		}
		if (toupper(s_f[i]) == '\n')
		{
			return -1;
		}

		if (toupper(s_f[i]) > toupper(s_s[i]))
		{
			return 1;
		}
		else if (toupper(s_f[i]) < toupper(s_s[i]))
		{
			return -1;
		}
		else
		{
			i++;
		}
	}
}

int cool_cmpr(const void* f_ptr, const void* s_ptr) // '>'
{
	char* s_s = *(char**)f_ptr;
	char* s_f = *(char**)s_ptr;

	int l_f = length_of_string(s_f);
	int l_s = length_of_string(s_s);

	char c_f = s_f[l_f];
	char c_s = s_s[l_s];

	s_f[l_f] = '\0';
	s_s[l_s] = '\0';

	// magic cmpr
	int r = strcmp(s_f, s_s);

	s_f[l_f] = c_f;
	s_s[l_s] = c_s;

	return r;

}

int length_of_string(char* str)
{
	int i = 0;
	while (str[i] != '\r' && str[i] != '\n' && str[i] != '\0')
	{
		i++;
	}
	return i;
}

int main(int argc, char*argv[])
{

	printf("Sort file of strings\n");

	if (argc != 3)
	{
		printf("More\less parameters passed");
		return 0;
	}
	
	// open
	int f_in, f_out;
	if ((f_in = open(argv[1], O_RDWR)) < 0)
	{
		printf("Unable to open file for reading");
		return 0;
	}
	if ((f_out = open(argv[2], O_RDWR | O_CREAT | O_TRUNC, S_IWRITE)) < 0)
	{
		printf("Unable to open file for writing");
		return 0;
	}

	// info
	struct stat stat;
	fstat(f_in, &stat);

	// mmap
	char* mm = mmap(0, stat.st_size, PROT_READ | PROT_WRITE, MAP_PRIVATE, f_in, 0);
	if (mm == MAP_FAILED)
	{
		printf("Error when open mmap");
		return 0;
	}

	int count_of_strings = 1;
	for (int i = 0; i < stat.st_size; i++)
	{
		if (mm[i] == '\n')
		{
			count_of_strings++;
		}
	}

	// geting data in array of array
	char** data = (char**)malloc(count_of_strings * sizeof(char*));

	int k = 0;
	data[0] = &mm[0];
	for (int i = 1; i < count_of_strings; i++)
	{
		while (mm[k] != '\n' && k < stat.st_size)
		{
			k++;
		}
		if (k >= stat.st_size)
		{
			break;
		}
		data[i] = &mm[k + 1];
		k += 2;
	}

	qsort(data, count_of_strings, sizeof(char*), cool_cmpr);

	// output
	char* next = "\n";
	for (int i = 0; i < count_of_strings; i++)
	{
		write(f_out, data[i], length_of_string(data[i]));
		write(f_out, next, 1);
	}

	printf("Sorted");

	// free
	free(data);
	munmap(mm, stat.st_size);
	close(f_in);
	close(f_out);
	return 0;
}
