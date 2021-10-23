#define _CRT_SECURE_NO_WARNINGS
#include <fcntl.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <io.h>
#include <stdio.h>
#include <share.h>
#include <string.h>
#include "sys/mman.c"

int own_cmpr(const char* str1, const char* str2);

int main(int argc, char* argv[])
{
	int fdin, fdout, err;
	struct stat statbuf;
	char* src, * dst;
	printf("This program sorts the lines in a text file using the memory mapped file mechanism.\n");
	if (argc != 3)
	{
		printf("There no 3 arguments");
		return 0;
	}
	if ((err = _sopen_s(&fdin, argv[1], _O_RDWR, _SH_DENYWR, _S_IREAD)) < 0)
	{
		printf("The file input could not be opened");
		return 0;
	}
	if ((err = _sopen_s(&fdout, argv[2], _O_RDWR | _O_CREAT | _O_TRUNC, _SH_DENYRD, _S_IWRITE)) < 0)
	{
		printf("The file output could not be opened");
		return 0;
	}
	if (fstat(fdin, &statbuf) < 0)
	{
		printf("Failed to fill the file structure");
		return 0;
	}
	if ((src = mmap(0, statbuf.st_size, PROT_READ | PROT_WRITE, MAP_SHARED, fdin, 0)) == MAP_FAILED)
	{
		printf("Could not get the address of the beginning of the mapped memory section");
		return 0;
	}
	unsigned long long numbers_str = 0;
	for (int i = 0; i < statbuf.st_size; i++)
	{
		if (src[i] == '\n' || i + 1 == statbuf.st_size)
		{
			numbers_str++;
		}
	}
	char** strs = (char**)malloc(sizeof(char*) * numbers_str);
	char* rch = src;
	char* lch = src;
	int len = 0;
	int str_i = 0;
	for (; str_i < numbers_str; lch = rch + 1)
	{
		rch = strchr(lch, '\n');
		if (rch != NULL)
		{
			len = rch - lch;
		}
		else
		{
			len = &src[statbuf.st_size] - lch;
		}
		strs[str_i] = malloc(sizeof(char) * (len + 1));
		memcpy(strs[str_i], lch, len);
		strs[str_i][len] = '\0';
		str_i++;
	}

	// eliminating unnecessary tabs
	for (int i = 0; i < numbers_str; i++)
	{
		int j = strlen(strs[i]);
		while (strs[i][j - 1] == '\n' || strs[i][j - 1] == '\r')
		{
			j--;
		}
		strs[i][j] = '\0';
	}

	qsort(strs, (size_t)numbers_str, sizeof(char*), own_cmpr);

	char end = '\n';

	for (int i = 0; i < numbers_str; i++)
	{
		_write(fdout, strs[i], strlen(strs[i]));
		_write(fdout, &end, 1);
	}
	for (int i = 0; i < numbers_str; i++)
	{
		free(strs[i]);
	}
	free(strs);
	munmap(src, statbuf.st_size);
	_close(fdin);
	_close(fdout);
	printf("Sorting completed successfully.");
	return 0;
}

int own_cmpr(const char* str_first, const char* str_second)
{
	char* str_new_first = *(char**)str_first;
	char* str_new_second = *(char**)str_second;
	return strcmp(str_new_first, str_new_second);
}