#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <malloc.h>
#include <sys/stat.h>
#include <sys/types.h>
#include "sys/mman.h"
#include <string.h>

unsigned long long lengthstr(char* str)
{
	unsigned long long i = 0;
	while ((str[i] != '\n') && (str[i] != '\r') && (str[i] != '\0'))
		++i;
	return i;
}

int compare(const void* arg1, const void* arg2)
{
	return _stricmp(*(char**)arg1, *(char**)arg2);
}

int arguementscheck(argcount)
{
	if (argcount != 3)
	{
		printf("You have to submit two arguments to the input.\n");
		return 0;
	}
}

int main(int argc, char* argv[])
{
	printf("This program sorts the strings from the input file using memory mapped files\n");

	int file_inp;
	int file_out;
	struct stat statbuf;

	if (!arguementscheck(argc))
		return -1;

	file_inp = open(argv[1], O_RDONLY);
	file_out = open(argv[2], O_RDWR | O_CREAT | O_TRUNC, S_IWRITE);

	if ((file_inp = open(argv[1], O_RDWR)) < 0)
	{
		printf("Something went wrong with the input file");
		return -1;
	}
	if ((file_out = open(argv[2], O_RDWR | O_CREAT | O_TRUNC, S_IWRITE)) < 0)
	{
		printf("Something went wrong with the output file");
		return -1;
	}

	fstat(file_inp, &statbuf);

	char* map = mmap(0, statbuf.st_size, PROT_READ | PROT_WRITE, MAP_PRIVATE, file_inp, 0);

	if (map == MAP_FAILED)
	{
		printf("Error when calling the mmap function");
		return 0;
	}

	long long numberstr = 0, maxlen = 0, len = 0;
	for (int i = 0; i < statbuf.st_size; ++i)
	{
		++len;
		if (map[i] == '\n')
		{
			++numberstr;
			if (maxlen < len)
				maxlen = len;
			len = 0;
		}
		else
			if (i + 1 == statbuf.st_size)
			{
				++numberstr;
				if (maxlen < len)
					maxlen = len;
				len = 0;
			}
	}

	char** strs = (char**)malloc(numberstr * sizeof(char*));

	strs[0] = strtok(map, "\n");
	char* prev = map + strlen(strs[0]);
	*prev = '\n';
	for (int i = 1; i < numberstr; ++i)
	{
		strs[i] = strtok(prev + 1, "\n");
		prev = prev + 1 + strlen(strs[i]);
		*prev = '\n';
	}

	qsort(strs, (size_t)numberstr, sizeof(char*), compare);

	char endl = '\n';
	for (int i = 0; i < numberstr; ++i)
	{
		write(file_out, strs[i], lengthstr(strs[i]));
		write(file_out, &endl, 1);
	}

	free(strs);
	munmap(map, statbuf.st_size);
	close(file_inp);
	close(file_out);

	printf("Your strings have been sorted!");
	return 0;
}