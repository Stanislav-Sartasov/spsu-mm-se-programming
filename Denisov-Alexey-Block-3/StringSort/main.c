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

int main()
{
	printf("This program gets an input file and writes in a new output file sorted strings from the input file.\n\n");
	
	printf("Enter an input file path: ");
	char inputName[260];
	gets(inputName);

	printf("\nEnter an output file path: ");
	char outputName[260];
	gets(outputName);

	int input = open(inputName, O_RDWR);
	if (input < 0)
	{
		printf("\nUnable to open the input file for reading\n\n");
		return 0;
	}

	struct stat st;
	fstat(input, &st);
	char* map = mmap(NULL, st.st_size, PROT_READ | PROT_WRITE, MAP_PRIVATE, input, 0);

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
		arrStr[i] = (char*)malloc(st.st_size * sizeof(char));
		while (map[j] != '\r' && map[j] != '\n' && j < st.st_size)
		{
			arrStr[i][s] = map[j];
			s++;
			j++;
		}
		if (map[j] == '\r')
			j += 2;
		else
			j++;
		arrStr[i][s] = '\0';
		s = 0;
	}

	qsort(arrStr, kStr, sizeof(char*), cmpStr);

	int output = open(outputName, O_RDWR | O_CREAT | O_TRUNC);
	if (output < 0)
	{
		printf("\nUnable to open the output file for writing\n\n");
		return 0;
	}

	for (int i = 0; i < kStr; i++)
	{
		write(output, arrStr[i], currLen(arrStr[i]));
		write(output, "\n", 1);
	}

	close(input);
	munmap(map, st.st_size);
	free(arrStr);
	close(output);

	printf("\nStrings sorted. Check the output file.\n\n");
	
	return 0;
}