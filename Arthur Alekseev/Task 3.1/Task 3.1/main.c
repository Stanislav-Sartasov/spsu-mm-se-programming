#define _CRT_SECURE_NO_WARNINGS

#include "mman.h"
#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include <sys/stat.h>
#include <string.h>
#include <assert.h>
#include <locale.h>

// Structure that stores information about file opened with mmap
struct my_file
{
	void* pointer;
	int length;
	int bound_file;
};


int comporator(char* left, char* right)
{
	char* const* pp1 = left;
	char* const* pp2 = right;
	return strcmp(*pp1, *pp2);
}

int count_strings(char* text, int text_len)
{
	int ptr_index = 0;
	int string_count = 0;
	while (ptr_index < text_len)
	{
		// Sometimes \n can be at the end of the file
		if (text[ptr_index] == '\n' || ptr_index + 1 == text_len)
			string_count++;
		ptr_index++;
	}
	return string_count;
}

char** split_text(char* text, int text_len) {
	char** string_arr = (char**)malloc(sizeof(char*) * count_strings(text, text_len));
	// Putting the pointers of beginnings of the strings in the string_arr
	// These strings can be sorted and later they will be written in the output file only partly
	int str_index = 0;
	string_arr[0] = &text[0];
	for (int i = 1; i < text_len; i++)
		if (text[i - 1] == '\n')
		{
			str_index++;
			string_arr[str_index] = &text[i];
		}
	return string_arr;
}

long get_file_size(int file)
{
	struct stat statbuf;
	fstat(file, &statbuf);
	return statbuf.st_size;
}

struct my_file mmap_open(char* filename, int mode)
{
	struct my_file result;
	result.length = -1;

	int file = open(filename, mode);

	if (file < 0)
	{
		printf("Error occured while opening file \"%s\"\n", filename);
		return result;
	}
	result.pointer = mmap(0, get_file_size(file), PROT_READ | PROT_WRITE, MAP_PRIVATE, file, 0);

	result.bound_file = file;

	if (result.pointer == MAP_FAILED)
	{
		printf("Error occured while executing mmap_open\n");
		return result;
	}

	result.length = (size_t)get_file_size(file);
	return result;
}

char** sort_my_text(char* text, int text_len) 
{
	int str_count = count_strings(text, text_len);
	char** strings = split_text(text, text_len);
	qsort(strings, str_count, sizeof(char*), comporator);
	return strings;
}

int string_len(char* text)
{
	// Count string from beginning to some kind of separator
	int i = 0;
	while (text[i] != '\n' && text[i] != '\r' && text[i] != '\0')
		i++;
	// Seporator should be included too
	return i + 1;
}

int main(int argc, char* argv[])
{
	// Check if the right arguments were provided
	if (argc != 3)
	{
		printf("Not enough/too many arguments. Program should be called with this format:\nname.exe input_file output_file\n");
		return -1;
	}

	// Open input file
	struct my_file input_file = mmap_open(argv[1], O_RDWR);
	if (input_file.length == -1)
		return -1;
	// Open output file
	int out_file = open(argv[2], O_RDWR | O_CREAT | O_TRUNC, S_IWRITE);
	if (out_file < 0)
	{
		printf("Error opening output file");
		return -1;
	}
	// Get sorted strings
	char** strings = sort_my_text(input_file.pointer, input_file.length);

	// Write results to output file
	int str_cnt = count_strings(input_file.pointer, input_file.length);
	for (int i = 0; i < str_cnt; i++)
		write(out_file, strings[i], string_len(strings[i]));
	// Free and close everything
	if (strings != NULL)
		free(strings);
	close(out_file);
	close(input_file.bound_file);
	munmap(input_file.pointer, input_file.length);

	// Exit with success
	printf("Strings have been sorted\nPress any key to exit.\n");
	getchar();
	return 0;
}