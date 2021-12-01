#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <fcntl.h>
#include <io.h>
#include <sys/stat.h>
#include "sys/mman.h"

int comparator(char* first, char* second)
{
	return strcmp(*(char**)first, *(char**)second);
}

int strLength(char* string)
{
	int i = 0;
	for (; (string[i] != '\r') && (string[i] != '\n') && (string[i] != '\0'); i++);
	return i;
}

int main(int argc, char* argv[])
{
	printf("This program sorts the strings of the input file" 
		"and outputs them to another file using memory-mapped files mechanism\n");

	int fdin, fdout;
	char* src;
	struct stat statbuf;
	
	if (argc != 3)
	{
		fprintf(stderr, "Use: <program.exe> <fromfile> <tofile>");
		return -1;
	}
	if ((fdin = _open(argv[1], O_RDONLY)) < 0)
	{
		fprintf(stderr, "Could not open the file for reading");
		return -1;
	}
	if ((fdout = _open(argv[2], O_RDWR | O_CREAT | O_TRUNC, S_IWRITE)) < 0)
	{
		fprintf(stderr, "Could not open the file for writing");
		return -1;
	}

	fstat(fdin, &statbuf); //запись размера входного файла в statbuf
	if ((src = mmap(0, statbuf.st_size, PROT_READ, MAP_SHARED, fdin, 0)) == MAP_FAILED) //отображение файла в память
	{
		fprintf(stderr, "mmap error");
		return -1;
	}

	int strsCount = 1;
	for (int i = 0; i < statbuf.st_size; i++) //подсчёт количества строк
	{
		if (src[i] == '\n')
			strsCount++;
	}

	char** strs = (char**)malloc(sizeof(char*) * strsCount);
	int j = 0;
	for (int i = 0; i < strsCount; i++) //запись строк исходного файла в массив
	{
		strs[i] = &src[j];
		while (src[j++] != '\n' && i != strsCount - 1 && j < statbuf.st_size);
	}

	printf("Sorting...");
	qsort(strs, strsCount, sizeof(char*), comparator);
	
	char lastc = '\n';
	for (int i = 0; i < strsCount; i++) //запись отсортированных строк в новый файл
	{
		_write(fdout, strs[i], strLength(strs[i]));
		_write(fdout, &lastc, 1);
	}

	free(strs);
	munmap(src, statbuf.st_size);
	_close(fdin);
	_close(fdout);
	printf("The file has been sorted!\n");

	return 0;
}