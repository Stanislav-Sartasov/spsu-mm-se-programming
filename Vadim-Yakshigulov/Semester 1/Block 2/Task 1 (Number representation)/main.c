#include <stdio.h>
#include <string.h>
#include <stdlib.h>

void print_as_bin(long long n, int size)
{
	char *array = (char *) malloc((size + 1) * (sizeof(char)));
	for (int i = 0; i < size; ++i)
		array[i] = '0';
	array[size] = '\0';

	for (int i = size - 1; i > -1; --i)
	{
		array[i] = (n & 2) ? '1' : '0';
		n >>= 1;
	}

	printf("%s\n", array);

	free(array);
}

int main()
{
	char name[] = "Vadim";
	char surname[] = "Yakshigulov";
	char patronymic[] = "Nailevich";

	int product = strlen(name) * strlen(surname) * strlen(patronymic);

	int int32 = -product;
	float float32 = product;
	double double64 = -product;

	print_as_bin(int32, 32);
	print_as_bin(*(int *) &float32, 32);
	print_as_bin(*(long long *) &double64, 64);

	return 0;
}