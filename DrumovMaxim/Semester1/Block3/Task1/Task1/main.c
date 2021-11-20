#include <stdio.h>
#include <string.h>
#include <fcntl.h>
#include <io.h>
#include <share.h>
#include <sys/types.h>
#include <sys/stat.h>
#include "sys/mman.c"

int cmp(const char* firstStr, const char* secondStr)
{
	return strcmp(*(char**)firstStr, *(char**)secondStr);
}

int lengthStr(char* string)
{
	int i = 0;
	for (;(string[i] != '\r') && (string[i] != '\n') && (string[i] != '\0'); i++);
	return i;
}

int main(int argc, char* argv[])
{
	int fdin, fdout;
	char* src;
	struct stat statbuf;
	printf("This program sorts the strings from the input file using memory mapped files\n\n");
	if (argc != 3)
	{
		printf("Error, not 3 arguments entered");
		return 0;
	}
	if ((_sopen_s(&fdin, argv[1], _O_RDWR, _SH_DENYWR, _S_IREAD)) < 0)
	{
		printf("Input file %s could not open", argv[1]);
		return 0;
	}
	if ((_sopen_s(&fdout, argv[2], _O_RDWR | _O_CREAT | _O_TRUNC, _SH_DENYRD, _S_IWRITE)) < 0)
	{
		printf("Output file %s could not open", argv[2]);
		return 0;
	}
	if (fstat(fdin, &statbuf) < 0)
	{
		printf("Error, 'fstat'");
		return 0;
	}
	if ((src = mmap(0, statbuf.st_size, PROT_READ | PROT_WRITE, MAP_SHARED, fdin, 0)) == MAP_FAILED) // указатель на нулевой индекс
	{
		printf("Error in calling mmap function for input file");
		return 0;
	}

	long long numStr = 0;
	for (int i = 0; i < statbuf.st_size; i++)
	{
		if (src[i] == '\n')
		{
			numStr++;
		}
	}

	char** strs = (char**)malloc(sizeof(char*) * numStr);

	int j = 0;
	for (int i = 0; i < numStr; i++)
	{
		strs[i] = &src[j];
		while (src[j++] != '\n' && j < statbuf.st_size && i != numStr - 1);
	}

	qsort(strs, (size_t)numStr, sizeof(char*), cmp);

	for (int i = 0; i < numStr; i++)
	{
		_write(fdout, strs[i], lengthStr(strs[i]));
		_write(fdout, "\n", 1);
	}

	free(strs);
	munmap(src, statbuf.st_size);
	_close(fdin);
	_close(fdout);
	printf("Your file has been sorted successfully!\n");
	return 0;
}

