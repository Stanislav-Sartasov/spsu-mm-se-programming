#define _CRT_SECURE_NO_WARNINGS
#include <stdio.h>
#include <stdlib.h>
#include <fcntl.h>
#include "sys/mman.h"
#include <sys/stat.h>
#include <string.h>

void new_qsort(char** mas, int size)
{
	int i = 0;
	int j = size - 1;

	char* mid = mas[size / 2];

	do
	{
		while (strcmp(mas[i], mid) == -1)
		{
			i++;
		}

		while (strcmp(mas[j], mid) == 1)
		{
			j--;
		}

		if (i <= j)
		{
			char* tmp = mas[i];
			mas[i] = mas[j];
			mas[j] = tmp;

			i++;
			j--;
		}
	} while (i <= j);

	if (j > 0)
	{
		new_qsort(mas, j + 1);
	}
	if (i < size)
	{
		new_qsort(&mas[i], size - i);
	}
}

char** split(char* str)
{
	int num_words = count_strings(str);
	char** my_words = malloc((1 + num_words) * sizeof(char*));
	const char delim[2] = "\n";
	char* token;
	token = strtok(str, delim);

	int i = 0;
	while (token != NULL)
	{
		my_words[i] = malloc(sizeof(char) * (1 + strlen(token)));
		strcpy(my_words[i], token);

		token = strtok(NULL, delim);
		i++;
	}
	my_words[i] = NULL;

	return my_words;
}

int count_strings(char* str)
{
	int cnt = 0;

	while (*str != '\0')
	{
		if (*str == '\n')
			cnt++;
		str++;
	}

	return ++cnt;
}

int arguementscheck(argcount)
{
	if (argcount != 3)
	{
		printf("You have to submit two arguments to the input.\n");
		return 0;
	}
}

int main(int argc, char* argv[])
{
	printf("This program sorts the strings from the input file using memory mapped files\n");
	int file_inp;
	int file_out;
	struct stat statbuf;

	if (!arguementscheck(argc))
		return -1;

	file_inp = open(argv[1], O_RDONLY);
	file_out = open(argv[2], O_RDWR | O_CREAT | O_TRUNC, S_IWRITE);

	if ((file_inp = open(argv[1], O_RDWR)) < 0)
	{
		printf("Something went wrong with the input file");
		return 0;
	}
	if ((file_out = open(argv[2], O_RDWR | O_CREAT | O_TRUNC, S_IWRITE)) < 0)
	{
		printf("Something went wrong with the output file");
		return 0;
	}

	fstat(file_inp, &statbuf);

	char* map = mmap(0, statbuf.st_size, PROT_READ, MAP_PRIVATE, file_inp, 0);

	if (map == MAP_FAILED)
	{
		printf("Error when calling the mmap function");
		return 0;
	}

	int count = count_strings(map);
	char* strings = malloc(1 + strlen(map));
	strcpy(strings, map);
	char** split_list = split(strings);

	new_qsort(split_list, count);

	char delim = '\n';
	for (int i = 0; i < count; i++)
	{
		write(file_out, split_list[i], strlen(split_list[i]));
		write(file_out, &delim, 1);
	}

	for (int i = 0; i < count; i++)
	{
		free(split_list[i]);
	}

	munmap(map, statbuf.st_size);
	close(file_inp);
	close(file_out);
	free(split_list);
	free(strings);

	printf("Your strings have been sorted!");

	return 0;
}