#include <stdio.h>
#include <stdlib.h>
#include "mman.h"
#include <fcntl.h>
#include <sys\stat.h>
#include <string.h>

int compare_strings(const void* first_string, const void* second_string)
{
	return strcmp(*(char**)first_string, *(char**)second_string);
}

int main(int argc, char* argv[])
{
	printf("Sorting strings\n\n");

	// check arguments

	if (argc != 3)
	{
		printf("You entered %d arguments, but 2 arguments are requared\n", argc - 1);
		return -1;
	}

	int f_in, f_out;
	struct stat info;

	// open file for reading

	if ((f_in = open(argv[1], O_RDWR)) < 0)
	{
		printf("Can't open file for reading\n");
		return -1;
	}

	// open file for writing

	if ((f_out = open(argv[2], O_WRONLY | O_CREAT | O_TRUNC, S_IWRITE)) < 0)
	{
		close(f_in);
		printf("Can't open file for writing\n");
		return -1;
	}

	//collect statystics about file 

	if (fstat(f_in, &info) < 0)
	{
		close(f_in);
		close(f_out);
		printf("Can't get statystics from the input file\n");
		return -1;
	}

	// mapping file

	char* map_file;

	if ((map_file = mmap(0, info.st_size, PROT_READ | PROT_WRITE, MAP_PRIVATE, f_in, 0)) == MAP_FAILED)
	{
		close(f_in);
		close(f_out);
		printf("Can't map file\n");
		return -1;
	}

	// count strings and finding endline

	int count_string = 0;
	char endline_check;
	char endline[2];

	for (long long int i = 0; i <= info.st_size; i++)
	{
		if (map_file[i] == '\n' || map_file[i] == '\r' || map_file[i] == '\0')
		{
			count_string++;

			if (map_file[i] == '\r' && map_file[i + 1] == '\n')
			{
				endline[0] = '\r';
				endline[1] = '\n';
				i++;
			}
			else if (map_file[i] == '\n')
			{
				endline[0] = '\n';
				endline[1] = '\0';
			}
			else if (map_file[i] == '\r')
			{
				endline[0] = '\r';
				endline[1] = '\0';
			}
		}
	}
	printf("There are %d strings in the input file\n", count_string);

	// creating and filling array of strings

	char** strings = (char**)malloc(count_string * sizeof(char*));
	long long current_symbol = 0;

	for (int i = 0; i < count_string; i++)
	{
		long long length = 0;

		while (map_file[current_symbol + length] != endline[0] && map_file[current_symbol + length] != '\0' && (current_symbol + length <= info.st_size))
		{
			length++;
		}

		strings[i] = &map_file[current_symbol];
		strings[i][length] = '\0'; // replacing /n(/r, /r/n) -> /0 for future sorting strings

		if (endline[1] == '\n')
			current_symbol += length + 2;
		else
			current_symbol += length + 1;
	}
	printf("The array of strings was filled\n");

	// sortings strings

	qsort(strings, count_string, sizeof(char*), compare_strings);
	printf("Strings are sorted\n");

	// writing strings in file

	for (int i = 0; i < count_string; i++)
	{
		write(f_out, strings[i], strlen(strings[i]));
		if ( i != count_string - 1)
		{
			if (endline[1] == '\n')
				write(f_out, "\n", 1);
			else if (endline[0] = '\n')
				write(f_out, "\n", 1);
			else
				write(f_out, "\r", 1);
		}
	}
	printf("Strings are written in output file\n");

	// replacing /0 -> /n(/r, /r/n)

	for (long long i = 0; i < count_string - 1; i++)
	{
		int len = strlen(strings[i]);
		strings[i][len] = endline[0];
	}

	// freeing memory, closing mapping and files

	printf("\nDONE! You can see the result in %s\n", argv[2]);
	free(strings);
	munmap(map_file, info.st_size);
	close(f_in);
	close(f_out);
	return 0;
}