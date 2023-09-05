#include <stdlib.h>
#include <stdio.h>
#include "manager.h"
#define _CRT_SECURE_NO_WARNINGS

int main()
{
	init();	
	printf("the program demonstrates the operability of the following functions (my_malloc, my_realloc, my_free)\n");
	int32_t* test_mass = (int32_t*)my_malloc(sizeof(int32_t) * 5);
	if (test_mass)
	{
		printf("memory allocated successfully with size : %d bytes in %x\n", sizeof(int32_t) * 5, test_mass);
	}
	else
	{
		printf("failed to allocate memory");
		return -1;
	}
	size_t j = 0;
	for (size_t i = 7; i < 36; i += 7)
	{
		test_mass[j++] = 1000 - i;
	}
	printf("successfully recorded data\n");
	for (size_t i = 0; i < 5; i++)
	{
		printf("adress: %x, value %d\n", test_mass + i, test_mass[i]);
	}
	test_mass = (int32_t*)my_realloc(test_mass, sizeof(int32_t) * 8);
	if (!test_mass)
	{
		printf("failed to reallocate memory");
	}
	else
	{
		printf("memory reallocated successfully with size : %d bytes in %x\n", sizeof(int32_t) * 8, test_mass);
	}
	for (size_t i = 42; i < 57; i += 7)
	{
		test_mass[j++] = 1000 - i;
	}
	printf("successfully recorded new data\n");
	for (size_t i = 0; i < 8; i++)
	{
		printf("adress: %x, value %d\n", test_mass + i, test_mass[i]);
	}
	my_free(test_mass);
	printf("successfully freed memory");
	init_free();
	return 0;
}