#include <stdio.h>
#include <fcntl.h>
#include <unistd.h>

#include "sort/string_sort.h"
#include "mmap/strfile_mapping.h"
#include "algorithm/algorithm.h"

int validateDescriptor(int descriptor)
{
	return descriptor != -1;
}

int main(int argc, char** argv)
{
	if (argc != 3)
	{
		printf("Invalid arguments. Usage: %s <fromfile> <tofile>\n", argv[0]);
		return -1;
	}

	int inputFileDescriptor = open(argv[1], O_RDONLY);
	if (!validateDescriptor(inputFileDescriptor))
	{
		printf("Cannot open file %s\n", argv[1]);
		return -1;
	}

	char* source;
	size_t fileSize = mapFile(&source, inputFileDescriptor, PROT_READ, MAP_PRIVATE);
	size_t charCounter = fileSize / sizeof(char);
	if (source == MAP_FAILED)
	{
		printf("Mapping %s failed\n", argv[1]);
		return -1;
	}

	int stringCounter = count(source, source + charCounter, '\n');
	char** strings = (char**)malloc(stringCounter * sizeof(char*));
	parseFile(source, source + charCounter, strings, '\n');

	ssort(strings, strings + stringCounter, stringComparator);


	FILE* fout = fopen(argv[2], "w");
	for (int i = 0; i < stringCounter; i++)
	{
		int stringLength = find(strings[i], source + fileSize, '\n') - strings[i];
		fwrite(strings[i], sizeof(char), stringLength, fout);
		fputc('\n', fout);
	}

	fclose(fout);
	free(strings);
	unmapFile(source, fileSize);
	close(inputFileDescriptor);
	return 0;
}