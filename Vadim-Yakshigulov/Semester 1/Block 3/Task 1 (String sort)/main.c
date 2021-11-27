#include "mman.h"
#include <stdbool.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <fcntl.h>
#include <stddef.h>
#include <sys/stat.h>

struct memoryMappedFile
{
	void *self;
	char *name;
	size_t len;
	long long stringsCount;
	int boundFile;

} typedef mmfile;

int comparator(const void *left, const void *right)
{
	return strcmp(*(char **) left, *(char **) right);
}

int stringLen(const char *string)
{
	int i = 0;
	while (string[i] != '\n' && string[i] != '\r' && string[i] != '\0')
		i++;

	return i + 1;
}

size_t getFileSize(file)
{
	struct stat temp;
	fstat(file, &temp);
	return (size_t) temp.st_size;
}

long long countStrings(mmfile *file)
{
	long long result = 0;
	for (int i = 0; i < file->len; i++)
		if (((char *) file->self)[i] == '\n')
			result++;
	return result;
}

mmfile *openMemoryMappedFile(char *filename, int mode)
{
	mmfile *openedFile = malloc(sizeof(mmfile));
	openedFile->len = -1;
	openedFile->name = filename;
	openedFile->boundFile = -1;
	openedFile->self = NULL;
	openedFile->stringsCount = 0;

	int file = open(filename, mode);
	if (file < 0)
		return openedFile;
	openedFile->boundFile = file;

	openedFile->len = getFileSize(openedFile->boundFile);

	openedFile->self = mmap(0, openedFile->len, PROT_READ | PROT_WRITE, MAP_PRIVATE, openedFile->boundFile, 0);
	if (openedFile->self == MAP_FAILED)
	{
		openedFile->self = NULL;
		return openedFile;
	}

	openedFile->stringsCount = countStrings(openedFile);
	return openedFile;
}

bool isFileOpened(mmfile *file)
{
	return !((file->len < 0) || (file->self == NULL) || (file->boundFile < 0));
}

char **splitTextToStrings(mmfile *file)
{
	char **strings = malloc(sizeof(char *) * file->stringsCount);
	strings[0] = &((char *) file->self)[0];
	for (int i = 1, j = 0; i < file->len; i++)
		if (((char *) file->self)[i - 1] == '\n')
		{
			j++;
			strings[j] = &((char *) file->self)[i];
		}

	return strings;
}

char **sortText(mmfile *file)
{
	char **strings = splitTextToStrings(file);
	qsort(strings, file->stringsCount, sizeof(char *), comparator);
	return strings;
}

int main(int argc, char *argv[])
{
	if (argc != 3)
	{
		printf("Not enough/too many arguments.\n");
		printf("Call program with this format: main.exe input_file output_file\n");
		return -1;
	}

	mmfile *inputFile = openMemoryMappedFile(argv[1], O_RDWR);
	if (!isFileOpened(inputFile))
	{
		printf("Error occurred while opening file \"%s\"\n", inputFile->name);
		return -1;
	}

	int outputFile = open(argv[2], O_RDWR | O_CREAT | O_TRUNC, S_IWRITE);
	if (outputFile < 0)
	{
		printf("Error occurred while opening file \"%s\"\n", argv[2]);
		return -1;
	}

	char **strings = sortText(inputFile);
	for (int i = 0; i < inputFile->stringsCount; i++)
		write(outputFile, strings[i], stringLen(strings[i]));


	free(strings);
	close(outputFile);
	close(inputFile->boundFile);
	munmap(inputFile->self, inputFile->len);
	free(inputFile);
	printf("Sorted successfully\n");
	return 0;
}