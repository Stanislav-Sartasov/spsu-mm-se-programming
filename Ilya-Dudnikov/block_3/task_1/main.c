#include <stdio.h>
#include <fcntl.h>
#include <sys/stat.h>
#include "sys/mman.h"
#include <minmax.h>
#include <stdlib.h>
#include "main.h"

int main(int argc, char *argv[]) {
	if (argc != 3)
	{
		fprintf(stderr, "Incorrect command line arguments, usage: <executable> <input file name> <output file name>\n");
		return 0;
	}

	int size = 0;
	char *from = get_input(argv[1], O_RDONLY, S_IREAD, &size);
	char *to = get_input(argv[2], O_WRONLY | O_TRUNC | O_CREAT, S_IWRITE, &size);

	int word_count = 0, longest_word = 0;
	get_file_stat(from, size, &longest_word, &word_count);

	char **string_arr = init(word_count, longest_word);
	parse(from, size, string_arr);

	free_memory(string_arr, word_count);
	return 0;
}

int get_file_size(char *path, int oflag, int mode, int *file_desc)
{
	if ((*file_desc = open(path, oflag, mode)) == -1)
	{
		fprintf(stderr, "Program was not able to find the file\n");
		exit(1);
	}

	struct stat stat_buf;
	fstat(*file_desc, &stat_buf);
	return stat_buf.st_size;
}

// pass 0 for needed_size if you are reading from a file
char *get_input(char *filename, int oflag, int mode, int *needed_size)
{
	int file_desc;
	int size = get_file_size(filename, oflag, mode, &file_desc);

	if (oflag == O_RDONLY)
	{
		*needed_size = size;
		return mmap(0, *needed_size, PROT_READ, MAP_PRIVATE, file_desc, 0);
	}
	else
	{
		return mmap(0, *needed_size, PROT_WRITE, MAP_PRIVATE, file_desc, 0);
	}
}

void parse(char *mem_file, int size, char **result)
{
	for (int i = 0, row = 0, col = 0; i < size; i++)
	{
		if (mem_file[i] == '\n')
		{
			col = 0;
			row++;
			continue;
		}

		result[row][col++] = mem_file[i];
	}
}

void get_file_stat(char *mem_file, int size, int *longest, int *word_count)
{
	int current = 0;
	for (int i = 0; i < size; i++)
	{
		if (mem_file[i] == '\n')
		{
			*longest = max(*longest, current);
			current = 0;
			(*word_count)++;
			continue;
		}
		current++;
	}
}

char **init(int dimension_1, int dimension_2)
{
	char **string_arr = (char **)malloc(dimension_1 * sizeof(char *));
	for (int i = 0; i < dimension_1; i++)
	{
		string_arr[i] = (char *)malloc((dimension_2 + 1) * sizeof(char));
		for (int j = 0; j <= dimension_2; j++)
			string_arr[i][j] = '\0';
	}
}

void free_memory(char **array, int dimension_1)
{
	for (int i = 0; i < dimension_1; i++)
		free(array[i]);
	free(array);
}

void sort_strings(char **array)
{

}
