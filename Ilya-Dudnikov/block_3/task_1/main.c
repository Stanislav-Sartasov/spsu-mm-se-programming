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
	int word_count = 0, longest_word = 0;
	get_file_stat(from, size, &longest_word, &word_count);

	if (from[size - 1] != '\n')
	{
		fprintf(stderr, "Unexpected EOF: there should a newline at the end of input file");
	}
	char **string_arr = init(word_count, longest_word);
	parse(from, size, string_arr);
	munmap(from, size);

	sort_strings(string_arr, longest_word, word_count, 0, word_count, 0);

	char *to = get_input(argv[2], O_RDWR | O_CREAT | O_TRUNC, (mode_t)0600, &size);
	output_result(string_arr, to, word_count, longest_word);
	munmap(to, size);

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

	char *result;
	if (oflag == O_RDONLY)
	{
		*needed_size = size;
		result = mmap(0, *needed_size, PROT_READ, MAP_PRIVATE, file_desc, 0);
	}
	else
	{
		lseek(file_desc, *needed_size - 1, SEEK_SET);
		write(file_desc, "", 1);
		result = mmap(0, *needed_size, PROT_WRITE, MAP_SHARED, file_desc, 0);
	}
	close(file_desc);
	return result;
}

void parse(char *mem_file, int size, char **result)
{
	for (int i = 0, row = 0, col = 0; i < size; i++)
	{
		if (mem_file[i] == EOF)
			result[row][col++] = '\n';
		result[row][col++] = mem_file[i];
		if (mem_file[i] == '\n')
		{
			col = 0;
			row++;
		}
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
	*longest = max(*longest, current);
}

char **init(int dimension_1, int dimension_2)
{
	char **string_arr = (char **)malloc(dimension_1 * sizeof(char *));
	for (int i = 0; i < dimension_1; i++)
	{
		string_arr[i] = (char *)malloc((dimension_2 + 2) * sizeof(char));
		for (int j = 0; j < dimension_2 + 2; j++)
			string_arr[i][j] = '\0';
	}
	return string_arr;
}

void free_memory(char **array, int dimension_1)
{
	for (int i = 0; i < dimension_1; i++)
		free(array[i]);
	free(array);
}

void sort_strings(char **array, int longest_word, int word_count, int left, int right, int current_ind)
{
	if (current_ind > longest_word || left >= right)
		return;

	char *bucket[word_count];
	int cnt[128];
	memset(cnt, 0, sizeof(cnt));

	for (int i = left; i < right; i++)
	{
		int current = array[i][current_ind];
		cnt[array[i][current_ind]]++;
	}
	for (int i = 1; i < 128; i++)
		cnt[i] += cnt[i - 1];

	for (int i = left; i < right; i++)
	{
		bucket[left + cnt[array[i][current_ind]] - 1] = array[i];
		cnt[array[i][current_ind]]--;
	}
	for (int i = left; i < right; i++)
		array[i] = bucket[i];

	sort_strings(array, longest_word, word_count, left, left + cnt[0], current_ind + 1);
	for (int i = 1; i < 128; i++)
		sort_strings(array, longest_word, word_count, left + cnt[i - 1], left + cnt[i], current_ind + 1);
}

void output_result(char **array, char *to, int dimension_1, int dimension_2)
{
	int output_ind = 0;
	for (int i = 0; i < dimension_1; i++)
	{
		for (int j = 0; j <= dimension_2; j++)
		{
			to[output_ind++] = array[i][j];
			if (array[i][j] == '\n')
			{
				break;
			}
		}
	}
}
