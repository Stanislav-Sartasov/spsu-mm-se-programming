#include <stdio.h>
#include <string.h>
#include <fcntl.h>
#include <stdlib.h>
#include <search.h>
#include <sys/stat.h>
#include "sys/mman.h"

#define _CRT_SECURE_NO_WARNINGS



int compare(const void* arg1, const void* arg2)
{
	return _stricmp(*(char**)arg1, *(char**)arg2);
}

int strlength(char* line)
{
	int coefficient = 0;
	while (line[coefficient] != '\n' && line[coefficient] != '\0' && line[coefficient] != '\r')
	{
		coefficient++;
	}
	return coefficient + 1;
}

int main(int argc, char *argv[])
{
	printf("The programm sorts strings in the original file and writes the result in the new created file.\n");
	if (argc != 3)
	{
		printf("Usage: %s <initialfile> <sortedfile>\n", argv[0]);
		printf("Entered %d arguements, 2 arguments required to run the program.\n", argc - 1);
		printf("Completing the program.\n");
		return -1;
	}
	
	FILE
		*originalFile,
		*finalFile;
	
	if ((originalFile = open(argv[1], O_RDWR)) < 0)
	{
		printf("ERROR: failed to open %s for reading.\n", argv[1]);
		printf("Completing the program.\n");
		return -1;
	}
	if ((finalFile = open(argv[2], O_RDWR | O_TRUNC | O_CREAT, S_IWRITE)) < 0)
	{
		printf("ERROR: failed to create %s for writing.\n", argv[2]);
		printf("Completing the program.\n");
		return -1;
	}
	
	struct stat statbuf;
	if (fstat(originalFile, &statbuf) < 0)
	{
		printf("ERROR: failed to determine the size of the file.\n");
		printf("Completing the program.\n");
		return -1;
	}

	char* mmOrigin;
	if ((mmOrigin = mmap(0, statbuf.st_size, PROT_READ | PROT_WRITE, MAP_SHARED, originalFile, 0)) == MAP_FAILED)
	{
		printf("ERROR: failed function call \"mmap\" for the original file.\n");
		printf("Completing the program.\n");
		return -1;
	}

	int coefficient, amountOfLines = 1;
	for (coefficient = 0; coefficient < statbuf.st_size; coefficient++)
	{
		if (mmOrigin[coefficient - 1] == '\n')
		{
			amountOfLines++;
		}
	}

	char** lines = (char**)malloc((amountOfLines) * sizeof(char*));
	if (!lines)
	{
		printf("ERROR: failed to allocate memory.\n");
		printf("Completing the program.\n");
		return -1;
	}

	int number = 1;
	lines[0] = &mmOrigin[0];
	for (coefficient = 0; coefficient < statbuf.st_size; coefficient++)
	{
		if (mmOrigin[coefficient - 1] == '\n')
		{
			lines[number] = &mmOrigin[coefficient];
			number++;
		}
	}

	qsort((void*)lines, (size_t)amountOfLines, sizeof(char*), compare);

	for (coefficient = 0; coefficient < amountOfLines; coefficient++)
	{
		if (write(finalFile, lines[coefficient], strlength(lines[coefficient])) == -1)
		{
			printf("ERROR: failed to write the sorted strings in the file.\n");
			printf("Completing the program.\n");
			return -1;
		}
	}

	free(lines);

	if (munmap(mmOrigin, statbuf.st_size) == -1)
	{
		printf("ERROR: failed to unmap memory.\n");
		printf("Completing the program.\n");
		return -1;
	}

	if (close(originalFile) == -1)
	{
		printf("ERROR: failed to close original file.\n");
		printf("Completing the program.\n");
		return -1;
	}
	if (close(finalFile) == -1)
	{
		printf("ERROR: failed to close final file.\n");
		printf("Completing the program.\n");
		return -1;
	}

	return 0;
}