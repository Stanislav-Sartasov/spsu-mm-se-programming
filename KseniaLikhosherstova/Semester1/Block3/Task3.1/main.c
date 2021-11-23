#include <fcntl.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <io.h>
#include <stdio.h>
#include <stdlib.h>
#include <share.h>
#include <string.h>
#include "sys/mman.c"


unsigned long long lengthStr(char* str)
{
	int i = 0;
	while ((str[i] != '\n') && (str[i] != '\r') && (str[i] != '\0'))
		i++;
	return i;
}


int cmpStr(const void* a, const void* b)
{
	return strcmp(*(char**)a, *(char**)b);
}


int main(int argc, char* argv[])
{
	int fdin, fdout;
	char* src;
	struct stat statbuf;

	printf("The program sorts the strings from the input file using memory mapped file.\n");

	if (argc != 3)
	{
		printf("There no 3 arguments\n");
		return 0;
	}

	if ((fdin = _open(argv[1], O_RDWR)) < 0)
	{
		printf("The file input could not be opened for reading\n");
	}

	if ((fdout = _open(argv[2], O_RDWR | O_CREAT | O_TRUNC, S_IWRITE)) < 0)
	{
		printf("The file output could not be opened for writing\n");
	}


	fstat(fdin, &statbuf);

	if ((src = mmap(0, statbuf.st_size, PROT_READ | PROT_WRITE, MAP_SHARED, fdin, 0)) == MAP_FAILED)
	{
		printf("Could not get the address of the beginning of the mapped memory section\n");
		return 0;
	}


	unsigned long long countStr = 0;
	for (int i = 0; i < statbuf.st_size; i++)
	{
		if (src[i] == '\n' || i + 1 == statbuf.st_size)
		{
			countStr++;
		}
	}


	char** strings = (char**)malloc(countStr * sizeof(char*));
	int j = 0;
	for (int i = 0; i < countStr; i++)
	{
		strings[i] = &src[j];
		while (src[j++] != '\n' && j < statbuf.st_size && i != countStr - 1);
	}

	qsort(strings, (size_t)countStr, sizeof(char*), cmpStr);


	char end = '\n';
	for (int i = 0; i < countStr; i++)
	{
		_write(fdout, strings[i], lengthStr(strings[i]));
		_write(fdout, &end, 1);
	}

	free(strings);
	munmap(src, statbuf.st_size);
	_close(fdin);
	_close(fdout);
	printf("Strings have been sorted.");
	return 0;
}