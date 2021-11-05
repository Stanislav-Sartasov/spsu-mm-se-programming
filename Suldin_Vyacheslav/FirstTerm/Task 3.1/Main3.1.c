#include <stdio.h>
#include <string.h>
#include <fcntl.h>
#include <sys/stat.h>
#include "mman.h"

int cmp(const void* first, const void* second)
{
	return strcmp(*(char**)first, *(char**)second);
}

int len(char* str)
{
	int i = 0;
	for (i = 0;str[i] != '\r' && str[i] != '\n' && str[i] != '\0';i++);
	return i;
}

int main()
{
	int fdin, fdout;
	char* src;
	struct stat statbuf;

	if ( (fdin = open("in.txt", O_RDWR)) < 0)
		printf("cant open %s for reading", "in.txt");
	if ( (fdout = open("out.txt", O_RDWR | O_TRUNC, S_IWRITE)) < 0)
		printf("cant open %s for writing", "out.txt");
	fstat(fdin, &statbuf);
	if ((src = mmap(0, statbuf.st_size, PROT_READ, MAP_SHARED, fdin, 0)) == MAP_FAILED)
		printf("reading with mmap error");
	
	long int stringNum = 1;
	for (int i = 0; i < statbuf.st_size; i++)
		if (src[i] == '\n')
			stringNum++;

	char** strings = (char**)malloc(stringNum * sizeof(char*));

	int j = 0;
	for (int i = 0; (strings[i] = &src[j]) && (i + 1 != stringNum); i++)
	{
		int len = 0;
		for (len = 0; src[j + len + 1] != '\n'; len++);
		j += len+2;
	}

	qsort(strings, stringNum, sizeof(char*), cmp);

	for (int i = 0; i < stringNum; i++)
	{
		write(fdout, strings[i], len(strings[i]));
		write(fdout, "\n", 1);
	}

	free(strings);
	munmap(src, statbuf.st_size);
	close(fdin);
	close(fdout);
	printf("Seems likes sorted");
	return 0;
}