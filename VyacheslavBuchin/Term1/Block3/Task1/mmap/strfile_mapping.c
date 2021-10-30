//
// Created by Вячеслав Бучин on 30.10.2021.
//

#include <sys/stat.h>

#include "strfile_mapping.h"

size_t fileSize(int fileDescriptor)
{
	struct stat statBuff;
	fstat(fileDescriptor, &statBuff);
	return statBuff.st_size;
}

int unmapFile(void* ptr, size_t size)
{
	return munmap(ptr, size);
}

size_t mapFile(void** destination, int fileDescriptor, int prot, int flag)
{
	size_t size = fileSize(fileDescriptor);
	*destination = mmap(0, size, prot, flag, fileDescriptor, 0);
	return size;
}

void parseFile(char* begin, const char* end, char** destination, char symbol)
{
	while (begin < end) {
		*destination++ = begin;
		while (begin < end && *begin != symbol)
			begin++;
		begin++;
	}
}
