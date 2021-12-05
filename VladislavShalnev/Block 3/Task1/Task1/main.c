
#include <stdio.h>
#include <sys/stat.h>
#include <fcntl.h>
#include <io.h>
#include <stdlib.h>
#include <string.h>

#include "sys/mman.h"
#include "lib/error.h"

int count_symbols(int input_descriptor)
{
	struct stat info;

	if (fstat(input_descriptor, &info) < 0)
		error("Runtime error in fstat function.\n");

	return info.st_size;
}

int count_strings(char* input, int symbols_count)
{
	int strings_count = 0;
	for (int i = 0; i < symbols_count; i++)
		if (input[i] == '\n' || i == symbols_count - 1) strings_count++;

	return strings_count;
}

void fill_strings(char** strings, char* input, int symbols_count)
{
	// setting first string
	strings[0] = &input[0];

	int current_string = 1;

	for (int i = 1; i < symbols_count; i++)
	{
		if (input[i - 1] == '\n')
		{
			strings[current_string] = &input[i];
			current_string++;
		}
	}
}

int compare(const void* a, const void* b) // strings compare function for qsort from docs.microsoft.com 
{
	return strcmp(*(char**)a, *(char**)b);
}

int count_length(char* string, char* breakpoint)
{
	int length = 0;
	while (string[length] != '\r' && string[length] != '\n')
	{
		length++;
		if (&string[length - 1] == breakpoint)
			break;
	}

	return length;
}

int main(int argc, char* argv[])
{
	printf("This program sorts the strings from the input file using strcmp() and writes them to the output file.\n\n");

	int input_descriptor, output_descriptor;

	char* input;

	// opening input and output files
	if (argc != 3)
		return error("Invalid command line parameters.\n");

	_sopen_s(&input_descriptor, argv[1], _O_RDONLY, _SH_DENYNO, _S_IREAD);
	if (input_descriptor < 0)
		return error("Unable to open input file.\n");

	_sopen_s(&output_descriptor, argv[2], _O_WRONLY | _O_CREAT | _O_TRUNC, _SH_DENYNO, _S_IWRITE);
	if (output_descriptor < 0)
		return error("Unable to create output file.\n");
	//

	// counting symbols in the file
	int symbols_count = count_symbols(input_descriptor);

	// using mmap
	input = mmap(0, symbols_count, PROT_READ, MAP_SHARED, input_descriptor, 0);

	if (input == MAP_FAILED)
		return error("Runtime error in mmap function.\n");
	//

	// counting strings
	int strings_count = count_strings(input, symbols_count);

	// defining strings list
	char** strings = (char**)malloc(strings_count * sizeof(char*));

	if (!strings)
		return error("Runtime error in malloc function.\n");
	//

	// filling array with pointers
	fill_strings(strings, input, symbols_count);

	// sorting strings
	qsort(strings, strings_count, sizeof(char*), compare);

	// breakpoint to detect end of the file
	char* breakpoint = &input[symbols_count - 1];

	// writing to the output file
	for (int i = 0; i < strings_count; i++)
	{
		_write(output_descriptor, strings[i], count_length(strings[i], breakpoint));

		// checking if there is '\n' at the end of the file
		if (i != strings_count - 1 || input[symbols_count - 1] == '\n')
			_write(output_descriptor, "\n", 1);
	}
	//

	// free
	free(strings);

	munmap(input, symbols_count);

	_close(input_descriptor);
	_close(output_descriptor);
	//

	printf("Done! Check the output file.");

	return 0;
}