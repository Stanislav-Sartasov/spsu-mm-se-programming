#include <stdio.h>
#include "myMalloc.h"

void greetingsMessage()
{
	printf("This program was created to test basic functionality of\n");
	printf(" myMalloc library. For more thorough testing please contact\n");
	printf(" the author.\n\n");
}

void* mallocTest(size_t size)
{
	void* mem = myMalloc(size);
	if (mem == NULL)
		printf("! Failed to allocate %d bytes!\n\n", size);
	return mem;
}

void* reallocTest(void* ptr, size_t size)
{
	void* mem = myRealloc(ptr, size);
	if (mem == NULL)
		printf("! Failed to reallocate memory!\n\n");
	return mem;
}

int main()
{
	init();

	int* arr = mallocTest(100 * sizeof(int));
	if (arr == NULL) return -1;
	for (int i = 0; i < 100; i++)
		arr[i] = i;
	printf("Successfully created and filled an array of 100 ints!\n\n");

	int* arr2 = mallocTest(127 * sizeof(int));
	if (arr2 == NULL) return -1;
	for (int i = 0; i < 127; i++)
		arr2[i] = i;
	printf("Successfully created and filled an array of 127 ints!\n\n");

	myFree(arr);

	for (int i = 0; i < 50; i++)
	{
		void* tmp = mallocTest(sizeof(char));
		if (tmp == NULL) return -1;
	}
	printf("Successfully allocated memory for 50 char variables!\n\n");

	arr2 = reallocTest(arr2, 150 * sizeof(int));
	if (arr2 == NULL) return -1;
	if (arr2[125] == 125)
		printf("Successfully reallocated memory!\n\n");
	else
		printf("! Memory reallocation data loss!\n\n");

	initFree();

	return 0;
}