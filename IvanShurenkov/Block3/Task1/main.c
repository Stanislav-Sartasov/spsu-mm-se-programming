#ifdef IS_WINDOWS
#include "sys/mman.h"
#define MODE S_IWRITE | S_IREAD
#elif IS_LINUX
#include <sys/mman.h>
#define MODE S_IRWXO | S_IRWXG | S_IRWXU
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

int cmp(const void *f_s, const void *s_s)
{
	char *first_str = *(char **) f_s;
	char *second_str = *(char **) s_s;

	int ret = strcmp(first_str, second_str);

	return ret;
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
	int ofile = open(argv[2], O_RDWR | O_CREAT | O_TRUNC, MODE);
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
	for (int i = 0, j = 0; i < cnt_str && j < input_stat.st_size; i++)
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
		write(ofile, strings[i], len);
		if (i + 1 != cnt_str)
			write(ofile, "\n", 1);
	}

	close(ofile);
	ofile = NULL;
	free(strings);
	strings = NULL;
	munmap(map, input_stat.st_size);
	close(ifile);
	ifile = NULL;

	printf("Sorted");
	return 0;
}
