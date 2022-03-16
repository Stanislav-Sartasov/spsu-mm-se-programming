
#include <stdio.h>
#include <stdlib.h>

#define MAX_N 999999
#define MAX(a, b) (a > b ? a : b)

int digital_root(int number)
{
	return number % 9 != 0 ? number % 9 : 9;
}

int MDRS(int number, int* array)
{
	int result = digital_root(number);

	for (int divider = 2; divider * divider <= number; divider++)
	{
		if (number % divider != 0) continue;

		result = MAX(result, array[number / divider - 2] + array[divider - 2]);
	}

	return result;
}

void fill_with_MDRS(int* array)
{
	for (int number = 2; number < MAX_N + 1; number++)
		array[number - 2] = MDRS(number, array);
}

long long sum(int* array)
{
	long long result = 0;

	for (int i = 0; i < MAX_N - 1; i++)
		result += array[i];
	
	return result;
}

int main()
{
	printf("This program counts the sum of MDRS(n) for n in range [2; 999999].\n\n");

	int* array = (int*)malloc(sizeof(int) * (MAX_N - 1));

	fill_with_MDRS(array);

	printf("The answer is %lld.\n", sum(array));

	free(array);

	return 0;
}
