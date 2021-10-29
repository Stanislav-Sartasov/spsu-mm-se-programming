#include <sys/types.h>
#include <sys/stat.h>
#include <stdbool.h>
#include <malloc.h>
#include <string.h>
#include <stdlib.h>
#include <stdio.h>
#include <fcntl.h>
#include "mman.h"


/* Ñombining a string and its size so as not to count the same number many times */
struct line_with_length
{
	char* line;
	int length;
};


/* <=> the second line is greater than or equal to the first ( line1 < line2 ) */
bool compare(struct line_with_length line1, struct line_with_length line2)
{
	int min_length = (line1.length <= line2.length) ? line1.length : line2.length;
	for (int s_index = 0; s_index < min_length; s_index++)
	{
		if (line1.line[s_index] < line2.line[s_index])
		{
			return true;
		}
		if (line1.line[s_index] > line2.line[s_index])
		{
			return false;
		}
	}
	return line1.length < line2.length;
}


int count_length(char* line)
{
	int length = 0;
	while (line[length] != '\n' && line[length] != '\r')
	{
		length++;
	}
	return length;
}


void quick_str_sort(struct line_with_length* array, int first_index, int last_index)
{
	if (first_index < last_index)
	{
		int left_index = first_index;
		int right_index = last_index;
		struct line_with_length middle = array[(left_index + right_index) / 2];
		struct line_with_length tmp;
		while (left_index < right_index)
		{
			while (compare(array[left_index], middle))
			{
				left_index++;
			}
			while (compare(middle, array[right_index]))
			{
				right_index--;
			}
			if (left_index <= right_index)
			{
				tmp = array[left_index];
				array[left_index] = array[right_index];
				array[right_index] = tmp;
				left_index++;
				right_index--;
			}
		}
		quick_str_sort(array, first_index, right_index);
		quick_str_sort(array, left_index, last_index);
	}
}


int main(int argc, char* argv[])
{
	printf("This program sorts the lines in the input file and writes the result to the output file\n");

	if (argc != 3)
	{
		printf("Incorrect parameters (expected path to the input file and path to the output file)\n");
		return 0;
	}

	int input_file = open(argv[1], O_RDWR);
	if (input_file == -1)
	{
		printf("Unable to open file for reading\n");
		return 0;
	}

	int output_file = open(argv[2], O_RDWR | O_TRUNC | O_CREAT, S_IWRITE);

	struct stat st;
	fstat(input_file, &st);
	int size_in_bytes = st.st_size;

	char* mmp_in = mmap(0, size_in_bytes, PROT_READ, MAP_SHARED, input_file, 0);

	if (mmp_in == MAP_FAILED)
	{
		printf("Calling the mmap function led to error\n");
		return 0;
	}

	int number_of_lines = 0;
	for (int s_index = 0; s_index < size_in_bytes; s_index++)
	{
		if (mmp_in[s_index] == '\n')
		{
			number_of_lines += 1;
		}
	}

	struct line_with_length* array = malloc(sizeof(struct line_with_length) * number_of_lines);

	char* start_of_line = mmp_in;
	int length_of_line;
	for (int n_line = 0; n_line < number_of_lines; n_line++)
	{
		if (*start_of_line == '\r')
		{
			start_of_line += 1;
		}
		if (*start_of_line == '\n')
		{
			start_of_line += 1;
		}
		length_of_line = count_length(start_of_line);
		array[n_line] = (struct line_with_length){ .line = start_of_line, .length = length_of_line };
		start_of_line += length_of_line;
	}

	quick_str_sort(array, 0, number_of_lines - 1);

	for (int n_line = 0; n_line < number_of_lines; n_line++)
	{
		write(output_file, array[n_line].line, array[n_line].length);
		write(output_file, "\n", 1);
	}

	munmap(mmp_in, size_in_bytes);
	close(input_file);
	close(output_file);
	free(array);
}