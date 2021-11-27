#include "mman.h"
#include <stdio.h>
#include <string.h>
#include <stdlib.h>
#include <fcntl.h>
#include <sys/stat.h>
#include <stdarg.h>

struct string {
	char* ptr;
	int len;
};

void greetingsMessage()
{
	printf("This program is designed to sort strings from one file\n");
	printf(" and write them into another.\n\n");
}

void startedSortingMessage()
{
	printf("The files are opened successfully, the sorting method has started.\n\n");
}

void farewellMessage()
{
	printf("The sorting is complete!\n");
}

int errorMessage(int errorCode, int strNum, ...)
{
	printf("Error: ");

	va_list args;
	va_start(args, strNum);
	for (int i = 0; i < strNum; i++)
	{
		printf(" %s ", va_arg(args, char*));
	}
	va_end(args);

	printf("\n");

	return errorCode;
}

int cmp(struct string* a, struct string* b)
{
	return strcmp(a->ptr, b->ptr);
}

int main(int argc, char* argv[])
{
	greetingsMessage();

	if (argc != 3)
		return errorMessage(-1, 3, "argument mismatch\n Please, use the following format:",
			argv[0], "<input file> <output file>");

	// opening files and mmap projections
	int fdin = open(argv[1], O_RDONLY);
	if (fdin < 0)
		return errorMessage(-2, 3, "file", argv[1], "could not be opened for reading");

	int fdout = open(argv[2], O_RDWR | O_TRUNC, S_IWRITE);
	if (fdout < 0)
		return errorMessage(-2, 3, "file", argv[2], "could not be opened for writing");

	struct stat statbuf;
	if (fstat(fdin, &statbuf) < 0)
		return errorMessage(1, 1, "fstat call error for the input file");

	char* src = mmap(0, statbuf.st_size, PROT_READ, MAP_SHARED, fdin, 0);
	if (src == MAP_FAILED)
		return errorMessage(1, 1, "mmap call error for the input file");

	char* dst = mmap(0, statbuf.st_size, PROT_READ | PROT_WRITE, MAP_SHARED, fdout, 0);
	if (dst == MAP_FAILED)
		return errorMessage(1, 1, "mmap call error for the output file");

	startedSortingMessage();

	// counting and sorting
	int stringsNum = 0;
	for (int i = 0; i < statbuf.st_size; i++)
		if (src[i] == '\n')
			stringsNum++;

	struct string* strings = malloc(stringsNum * sizeof(struct string));
	if (strings == NULL)
		return errorMessage(2, 1, "memory allocation error");

	for (int i = 0, curStrId = 0, curStrStart = 0; i < statbuf.st_size; i++)
	{
		while (src[i] != '\n')
			i++;
		strings[curStrId].ptr = &src[curStrStart];
		strings[curStrId].len = i - curStrStart + 1;
		curStrStart = i + 1;
		curStrId++;
	}

	qsort(strings, stringsNum, sizeof(struct string), cmp);

	for (int i = 0, curLen = 0; i < stringsNum; i++)
	{
		memcpy(dst + curLen, strings[i].ptr, strings[i].len * sizeof(char));
		curLen += strings[i].len;
	}

	free(strings);
	munmap(src, statbuf.st_size);
	munmap(dst, statbuf.st_size);
	close(fdin);
	close(fdout);

	farewellMessage();

	return 0;
}