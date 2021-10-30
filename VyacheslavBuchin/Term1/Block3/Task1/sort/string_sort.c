//
// Created by Вячеслав Бучин on 26.10.2021.
//

#include "string_sort.h"

int cmp(const char* left, const char* right)
{
	while (*left == *right && *left != '\n')
	{
		left++;
		right++;
	}
	return *left - *right;
}

int stringComparator(const void* left, const void* right)
{
	return cmp(*(char**)left, *(char**)right);
}

void ssort(char** begin, char** end, int (* _Nonnull comparator) (const void*, const void*))
{
	size_t elementCounter = end - begin;
	qsort(begin, elementCounter, sizeof(char*), (int (*)(const void *, const void *)) comparator);
}
