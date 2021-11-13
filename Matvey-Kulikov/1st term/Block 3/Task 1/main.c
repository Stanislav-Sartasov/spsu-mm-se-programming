#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <errno.h>
#include <fcntl.h>
#include <sys/mman.h>
#include <sys/stat.h>
#define INVALID_ARGS_ERROR 1
#define FILE_OPEN_ERROR 2
#define FILE_CLOSE_ERROR 3
#define FSTAT_ERROR 4
#define MAP_FAIL_ERROR 5
#define UNMAP_FAIL_ERROR 6

char* get_string(char* string_start)
{
	int length = 0;
	while ((string_start[length] != '\n') && (string_start[length] != '\r') && (string_start[length] != '\0'))
	{
		length++;
	}
	char* string = (char*)malloc(sizeof(char)*(length+1));
	strncpy(string, string_start, length);
	string[length] = '\0';
	return string;
}

int comparator(const void* string_1_pointer, const void* string_2_pointer)
{
	const char* string_1 = *(char**)string_1_pointer;
	const char* string_2 = *(char**)string_2_pointer;
	return strcmp(string_1, string_2);
}

int main(int argc, char* argv[])
{

	printf("This program takes strings from specified file, sorts them and writes to another specified file.\n");
	if (argc != 3)
	{
		printf("You should provide two filename (input and output) as arguments!\n");
		return INVALID_ARGS_ERROR;
	}

	int input_file_descriptor = open(argv[1], O_RDONLY);
	if (input_file_descriptor < 0)
	{
		printf("Opening input file in readonly mode failed: %s!\n", strerror(errno));
		return FILE_OPEN_ERROR;
	}

	int output_file_descriptor = open(argv[2], O_RDWR | O_CREAT, S_IRWXU);
	if (output_file_descriptor < 0)
	{
		printf("Creating output file failed: %s!\n", strerror(errno));
		return FILE_OPEN_ERROR;
	}

	struct stat filestat;
	if (fstat(input_file_descriptor, &filestat))
	{
		printf("Fstat failed: %s!\n", strerror(errno));
		return FSTAT_ERROR;
	}

	size_t size = filestat.st_size;

	char* input_file = mmap(NULL, size, PROT_READ, MAP_SHARED, input_file_descriptor, 0);
	if (input_file == MAP_FAILED)
	{
		printf("Input file mapping failed: %s!\n", strerror(errno));
		return MAP_FAIL_ERROR;
	}

	char** strings = (char**)malloc(sizeof(char*)*size); // input_file can't contain more strings than symbols

	int strings_amount = 1;
	strings[strings_amount - 1] = get_string(&input_file[0]); // putting first string address to string array

	for (int i = 1; i < size; i++)
	{
		if ((input_file[i - 1] == '\n') || (input_file[i - 1] == '\r'))
		{
			strings_amount++;
			strings[strings_amount - 1] = get_string(&input_file[i]);
		}
	}

	int unmap_error = munmap(input_file, size);
	if (unmap_error)
	{
		printf("Input file unmapping failed: %s!\n", strerror(errno));
		return UNMAP_FAIL_ERROR;
	}

	int close_error = close(input_file_descriptor);
	if (close_error)
	{
		printf("Closing input file failed: %s!\n", strerror(errno));
		return FILE_CLOSE_ERROR;
	}

	strings = realloc(strings, sizeof(char*)*strings_amount); // make extra space we took free for allocation

	qsort(strings, strings_amount, sizeof(char*), comparator);

	char* output_file = mmap(NULL, size, PROT_WRITE, MAP_SHARED, output_file_descriptor, 0);
	if (output_file == MAP_FAILED)
	{
		printf("Output file mapping failed: %s!\n", strerror(errno));
		return MAP_FAIL_ERROR;
	}

	lseek(output_file_descriptor, size-1, SEEK_SET);
	write(output_file_descriptor, "", 1);

	int copying_start = 0;

	for (int i = 0; i < strings_amount; i++)
	{
		int length = (int)strlen(strings[i]);
		strncpy(&output_file[copying_start], strings[i], length);
		copying_start += length;
		output_file[copying_start] = '\n';
		copying_start++;
	}

	free(strings);

	unmap_error = munmap(output_file, size);
	if (unmap_error)
	{
		printf("Output file unmapping failed: %s!\n", strerror(errno));
		return UNMAP_FAIL_ERROR;
	}

	close_error = close(output_file_descriptor);
	if (close_error)
	{
		printf("Closing output file failed: %s!\n", strerror(errno));
		return FILE_CLOSE_ERROR;
	}

	printf("Done!\n");

	return 0;
}
