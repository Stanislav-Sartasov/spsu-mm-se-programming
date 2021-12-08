#include <stdio.h>
#include <string.h>
#include <sys/types.h>
#include <sys/stat.h>
#include <fcntl.h>
#include "mman.h"

int cmpStr(const void* str1, const void* str2)
{
	const char* rec1 = *(char**)str1;
	const char* rec2 = *(char**)str2;
	int val = strcmp(rec1, rec2);

	return val;
}

int currLen(char* arr)
{
	int i = 0;
	while (arr[i] != '\r' && arr[i] != '\n' && arr[i] != '\0')
	{
		i++;
	}
	return i;
}

int main(int argc, char* argv[])
{
	printf("This program gets an input file and writes in a new output file sorted strings from the input file.\n\n");

	if (argc != 3)
	{
		printf("Input error. Please, specify paths of an input and output files.\n\n");
		return -1;
	}

	int input = open(argv[1], O_RDWR);
	if (input < 0)
	{
		printf("\nUnable to open the input file for reading\n\n");
		return -1;
	}

	int output = open(argv[2], O_RDWR | O_CREAT | O_TRUNC);
	if (output < 0)
	{
		printf("\nUnable to open the output file for writing\n\n");
		close(input);
		return -1;
	}

	struct stat st;
	fstat(input, &st);
	char* map = mmap(NULL, st.st_size, PROT_READ | PROT_WRITE, MAP_PRIVATE, input, 0);
	if (map == MAP_FAILED)
	{
		printf("Mapping error.\n\n");
		close(input);
		close(output);
		return -1;
	}

	int kStr = 1;
	for (int i = 0; i < st.st_size; i++)
	{
		if (map[i] == '\n')
			kStr++;
	}

	int j = 0; int s = 0;
	char** arrStr = (char**)malloc(kStr * sizeof(char*)); 
	for (int i = 0; i < kStr; i++) 
	{
		arrStr[i] = &map[j];
		while (map[j] != '\n' && map[j] != '\r' && j < st.st_size)
		{
			j++;
		}
		if (map[j] == '\r')
			j += 2;
		else
			j++;
	}

	qsort(arrStr, kStr, sizeof(char*), cmpStr);

	for (int i = 0; i < kStr; i++)
	{
		write(output, arrStr[i], currLen(arrStr[i]));
		write(output, "\n", 1);
	}

	close(input);
	munmap(map, st.st_size);
	for (int i = 0; i < kStr; i++)
	{
		free(arrStr[i]);
	}
	free(arrStr);
	close(output);

	printf("\nStrings sorted. Check the output file.\n\n");
	
	return 0;
}