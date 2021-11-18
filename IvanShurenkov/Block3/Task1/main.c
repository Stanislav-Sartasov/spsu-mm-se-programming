#ifdef IS_WINDOWS
#include "sys/mman.h"
#elif IS_LINUX
#include <sys/mman.h>
#endif

#include <stdio.h>
#include <fcntl.h>
#include <sys/stat.h>
#include <stdbool.h>
#include <string.h>
#include <stdlib.h>

#define MIN(a, b) ((a) < (b) ? (a) : (b))

int len_str(const char *string)
{
	int len = 0;
	while (string[len] != '\0' && string[len] != '\n' && string[len] != '\r')
		len++;
	return len;
}

bool cmp(const char *f_s, const char *s_s)
{
	char *first_str = *(char **) f_s;
	char *second_str = *(char **) s_s;
	int len_first = len_str(first_str);
	int len_second = len_str(second_str);
	for (int i = 0; i < MIN(len_first, len_second); i++)
	{
		if (first_str[i] == second_str[i])
			continue;
		if (first_str[i] < second_str[i])
			return false;
		else
			return true;
	}
	return len_first > len_second;
}

int main(int argc, char *argv[])
{
	printf("Sorting of strings in file\n");
	if (argc != 3)
	{
		if (argc > 3)
			printf("Too many arguments.\n");
		else
			printf("Too few arguments.\n");
		return 0;
	}

	int ifile = open(argv[1], O_RDWR | O_CREAT);
	if (ifile < 0)
	{
		printf("Input file not opened\n");
		return 0;
	}
	int ofile = open(argv[2], O_RDWR | O_CREAT | O_TRUNC);
	if (ofile < 0)
	{
		printf("Output file not opened\n");
		return 0;
	}
	struct stat input_stat;
	if (fstat(ifile, &input_stat) < 0)
	{
		printf("Couldn't get stat'");
	}
	char *map = mmap(0, input_stat.st_size, PROT_READ | PROT_WRITE, MAP_PRIVATE, ifile, 0);
	if (map == MAP_FAILED)
	{
		printf("mmap return error\n");
		return 0;
	}

	int cnt_str = 1;
	for (int i = 0; map[i] != '\0'; i++)
		if (map[i] == '\n' && map[i + 1] != '\0')
			cnt_str++;

	//printf("%d\n", cnt_str);
	char **strings = (char **) malloc(cnt_str * sizeof(char *));
	for (int i = 0, j = 0; i < cnt_str; i++)
	{
		strings[i] = &map[j];
		//printf("%d,", len_str(strings[i]));
		while (j < input_stat.st_size && map[j] != '\n')
			j++;
		j++;
	}

	qsort(strings, cnt_str, sizeof(char *), cmp);

	for (int i = 0; i < cnt_str; i++)
	{
		int len = len_str(strings[i]);
		write(ofile, strings[i], len_str(strings[i]));
		if (i + 1 != cnt_str)
			write(ofile, "\n", 1);
	}

	free(strings);
	strings = NULL;
	munmap(map, input_stat.st_size);
	close(ofile);
	ofile = NULL;
	close(ifile);
	ifile = NULL;
	return 0;
}
